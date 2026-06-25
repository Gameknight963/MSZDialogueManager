using MSZDialougeManager.Styling;
using MZDO.Shared;
using NAudio.Wave;
using static MSZDialougeManager.ThemeSwitchers;

namespace MSZDialougeManager
{
    public partial class DialogueEditor : ThemeableForm
    {
        public static DialoguePack? Pack { get; private set; }
        public static List<NodeRef> Nodes { get; private set; } = new();

        private IWavePlayer? waveOut;
        private WaveStream? audioStream;
        private int nextTemporaryNodeId = -1;
        private readonly string lastThemeFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lasttheme");

        private string? workingFilePath;

        public DialogueEditor(string? filePath = null)
        {
            InitializeComponent();
            SetUIMode(UIMode.Init);
            KeyPreview = true;
            KeyDown += Form1_KeyDown;
            dialogueViewContextMenu.Opening += ContextMenu_Opening;
            dialogueViewContextMenu.Opening += DialogueViewContextMenu_Opening;
            groupContextMenu.Opening += ContextMenu_Opening;

            dialogueView.ColumnWidthChanging += DialogueView_ColumnWidthChanging;
            dialogueView.ColumnWidthChanged += DialogueView_ColumnWidthChanged;

            FormClosing += DialogueEditor_FormClosing;

            if (Directory.Exists(FilesystemManager.DataPath))
            {
                Directory.Delete(FilesystemManager.DataPath, true);
                Directory.CreateDirectory(FilesystemManager.DataPath);
            }

            if (AssociationHelper.IsFileAssociationRegistered() && !AssociationHelper.IsFileAssociationCurrent())
                AssociationHelper.RegisterFileAssociation();

            shellToolStripMenuItem.Checked = AssociationHelper.IsFileAssociationRegistered();
            if (filePath != null) LoadPack(filePath);
            workingFilePath = filePath;
            if (File.Exists(lastThemeFile))
                ThemeManager.SetGlobalTheme(Enum.Parse<ThemeManager.Theme>(File.ReadAllText(lastThemeFile)), ThemeManager.TextRenderMode.ShadowText);
            else
                ThemeManager.SetGlobalTheme(ThemeManager.Theme.Acrylic, ThemeManager.TextRenderMode.ShadowText);

            searchBox.SetPlaceholder("Search by dialogue text...");
        }

        private void DialogueEditor_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _dialogueLvCts.Cancel();
        }

        const int WM_SIZE = 0x0005;

        const int SIZE_MINIMIZED = 1;
        const int SIZE_MAXIMIZED = 2;

        const int WM_EXITSIZEMOVE = 0x0232;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_EXITSIZEMOVE || (m.Msg == WM_SIZE && m.WParam.ToInt32() is SIZE_MINIMIZED or SIZE_MAXIMIZED)) ResizeTextColumn();
            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            File.WriteAllText(lastThemeFile, ThemeManager.ActiveTheme.ToString());
            base.OnFormClosing(e);
            ScrollHook.Uninstall();
        }

        private void ContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ContextMenuStrip? cms = (ContextMenuStrip)sender!;
            DwmApi.SetAccentState(cms.Handle, DwmApi.AccentState.ACCENT_ENABLE_BLURBEHIND,
                ResolvedTheme == ThemeManager.Theme.Light ? 0x64B38867 : 0x66000000);
            cms.BackColor = ThemeManager.AcrylicMainColor;
            cms.ForeColor = Color.White;
            cms.ShowImageMargin = false;
        }

        private void DialogueViewContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            dialogueViewContextMenu.Tag = dialogueView.PointToClient(Cursor.Position);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetTheme(ThemeManager.ActiveTheme);
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                dialogueView.Focus();
                searchBox.Clear();
                if (dialogueView.Items.Count == 0) return;
                if (dialogueView.SelectedItems.Count == 0)
                    dialogueView.Items[0].Selected = true;
                SetUIMode(UIMode.ItemSelected);
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (dialogueView.SelectedItems.Count == 0) return;
                PlayNodeAudio(GetSelectedNode()!);
            }
        }

        private const int MinTextColumnWidth = 415;

        private void ResizeTextColumn()
        {
            int totalOtherColumns = 0;
            for (int i = 0; i < dialogueView.Columns.Count - 1; i++)
                totalOtherColumns += dialogueView.Columns[i].Width;

            int remaining = dialogueView.ClientSize.Width - totalOtherColumns;

            bool needsScroll = remaining < MinTextColumnWidth;
            if (needsScroll) remaining = MinTextColumnWidth;

            dialogueView.Columns[2].Width = remaining;

            ScrollbarHelper.Set(dialogueView, ScrollbarHelper.Scrollbar.Horz, needsScroll);
        }

        private void DialogueView_ColumnWidthChanging(object? sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.NewWidth < MinTextColumnWidth)
                e.NewWidth = MinTextColumnWidth;
        }

        private void DialogueView_ColumnWidthChanged(object? sender, ColumnWidthChangedEventArgs e)
        {
            if (e.ColumnIndex != 2) ResizeTextColumn();
        }

        private enum UIMode
        {
            ItemSelected,
            ItemSelectedSearching,
            Idle,
            Init,
        }

        private void SetUIMode(UIMode mode)
        {
            bool itemSelected = mode == UIMode.ItemSelected;
            bool init = mode == UIMode.Init;

            textLabel.Visible =
            textHeaderLabel.Visible =
            nextNodesHeader.Visible =
            nextNodesBox.Visible =
            selectAudioButton.Visible =
            audioFileLabel.Visible =
            audioFileHeader.Visible =
            audioPlayButton.Visible =
            audioStopButton.Visible =
            editPropertiesButton.Visible =
            addNodeContextMenuItem.Visible =
            deleteThisNodeToolStripMenuItem.Visible =
            propertiesContextMenuItem.Visible =
            propertiesContextMenuItem.Enabled = itemSelected;

            changeChirpsToolStripMenuItem.Enabled = 
            editProjectMetadataToolStripMenuItem.Enabled = FilesystemManager.IsFileLoaded;

            templateButton.Visible =
            loadButton.Visible = init;

            jumpToThisNodeToolStripMenuItem.Visible = mode == UIMode.ItemSelectedSearching;

            generateWithTTSToolStripMenuItem.Enabled = !init;

            removeAudioButton.Visible = false;
            if (!itemSelected) return;

            NodeRef selected = GetSelectedNode()!;
            UpdateNextNodesBox(GetSelectedNode()!);

            bool hasAudioClip = FilesystemManager.DoesNodeAudioExist(selected.TreeIndex, selected.Node.id);
            audioPlayButton.Visible = hasAudioClip;
            audioStopButton.Visible = hasAudioClip;
            removeAudioButton.Visible = hasAudioClip;
            audioFileLabel.Text = hasAudioClip
                ? Path.GetFileName(FilesystemManager.GetNodeAudioPath(selected.TreeIndex, selected.Node.id))
                : "None";
        }

        ListViewGroup? GetGroupAtPoint(Point p)
        {
            ListViewGroup? lastGroup = null;
            foreach (ListViewGroup group in dialogueView.Groups)
            {
                if (group.Items.Count == 0) continue;
                Rectangle first = group.Items[0].Bounds;
                Rectangle last = group.Items[^1].Bounds;
                if (p.Y >= first.Top && p.Y <= last.Bottom)
                    return group;
                if (p.Y > last.Bottom)
                    lastGroup = group;
            }
            return lastGroup;
        }

        void UpdateNextNodesBox(NodeRef current)
        {
            nextNodesBox.BeginUpdate();
            nextNodesBox.Items.Clear();
            foreach (int id in current.Node.nextNodeIds)
            {
                NodeRef? nodeRef = Nodes.FirstOrDefault(n => n.Node.id == id && n.TreeIndex == current.TreeIndex);
                NextNodesBoxItem item = nodeRef == null
                    ? new() { text = $"[{id}] ⚠ This node no longer exists", node = null }
                    : new() { text = $"[{id}] {nodeRef.Node.speakerName}: {nodeRef.Node.dialogueText}", node = nodeRef.Node };
                nextNodesBox.Items.Add(item);
            }
            nextNodesBox.EndUpdate();
        }

        public async Task<bool> UpdateDialogueView(List<NodeRef> nodes, bool catchOperationCancelled)
        {
            _dialogueLvCts ??= new CancellationTokenSource();
            CancellationToken ct = _dialogueLvCts.Token;
            dialogueView.BeginUpdate();
            UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            try
            {
                dialogueView.Items.Clear();
                dialogueView.Groups.Clear();
                int i = 0;
                foreach (NodeRef nodeRef in nodes)
                {
                    ct.ThrowIfCancellationRequested();
                    string groupKey = $"tree_{nodeRef.TreeIndex}";
                    string groupName = Pack!.trees[nodeRef.TreeIndex].name ?? $"Tree {nodeRef.TreeIndex}";
                    ListViewGroup? group = dialogueView.Groups.Cast<ListViewGroup>()
                        .FirstOrDefault(g => g.Name == groupKey);
                    if (group == null)
                    {
                        group = new ListViewGroup(groupKey, groupName);
                        group.Tag = nodeRef.TreeIndex;
                        dialogueView.Groups.Add(group);
                    }
                    AddToDialogueView(nodeRef, group);
                    if (++i % 50 == 0)
                        await Task.Delay(1, ct);
                }
                return true;
            }
            catch (OperationCanceledException)
            {
                if (!catchOperationCancelled) throw;
                return false;
            }
            finally
            {
                Cursor = Cursors.Default;
                UseWaitCursor = false;
                if (!dialogueView.IsDisposed) dialogueView.EndUpdate();
            }
        }

        void UpdateNodeColors()
        {
            Dictionary<int, HashSet<int>> reachableByTree = [];
            Dictionary<int, HashSet<int>> allIdsByTree = [];

            foreach (ListViewItem item in dialogueView.Items)
            {
                NodeRef nodeRef = (NodeRef)item.Tag!;
                if (!allIdsByTree.ContainsKey(nodeRef.TreeIndex))
                    allIdsByTree[nodeRef.TreeIndex] = new HashSet<int>();
                allIdsByTree[nodeRef.TreeIndex].Add(nodeRef.Node.id);
            }

            foreach (ListViewItem item in dialogueView.Items)
            {
                NodeRef nodeRef = (NodeRef)item.Tag!;

                if (!reachableByTree.ContainsKey(nodeRef.TreeIndex))
                    reachableByTree[nodeRef.TreeIndex] = GetReachableNodes(nodeRef.TreeIndex);
                bool hasBrokenRefs = nodeRef.Node.nextNodeIds.Any(id =>
                    !allIdsByTree[nodeRef.TreeIndex].Contains(id));
                bool reachable = reachableByTree[nodeRef.TreeIndex].Contains(nodeRef.Node.id);
                bool isStartNode = Pack!.trees[nodeRef.TreeIndex].startNodeIds?.Contains(nodeRef.Node.id) ?? false;
                bool isTerminal = nodeRef.Node.nextNodeIds == null || nodeRef.Node.nextNodeIds.Length == 0;

                item.ForeColor = hasBrokenRefs ? Color.Orange
                    : isStartNode ? Color.Green
                    : !reachable ? Color.Red
                    : isTerminal ? Color.Blue
                    : item.ListView!.ForeColor;
            }
        }

        static List<NodeRef> FlattenPack(DialoguePack pack) =>
            pack.trees
            .SelectMany((tree, treeIndex) => tree.nodes.Select(node => new NodeRef(node, treeIndex)))
            .ToList();

        private static HashSet<int> GetReachableNodes(int treeIndex)
        {
            HashSet<int> reachable = [];
            Queue<int> queue = new();

            foreach (int startId in Pack!.trees[treeIndex].startNodeIds)
                queue.Enqueue(startId);

            while (queue.Count > 0)
            {
                int id = queue.Dequeue();
                if (!reachable.Add(id)) continue;

                NodeRef? node = Nodes.FirstOrDefault(n => n.Node.id == id && n.TreeIndex == treeIndex);
                if (node == null) continue;

                foreach (int nextId in node.Node.nextNodeIds)
                    queue.Enqueue(nextId);
            }

            return reachable;
        }

        public void AddToDialogueView(NodeRef nodeRef, ListViewGroup group)
        {
            ListViewItem item = new(nodeRef.Node.id.ToString()) { Group = group, Tag = nodeRef };
            item.SubItems.Add(nodeRef.Node.speakerName);
            item.SubItems.Add(nodeRef.Node.dialogueText);
            dialogueView.Items.Add(item);
            UpdateNodeColors();
        }

        async void Inittemplate()
        {
            SetUIMode(UIMode.Idle);
            Pack = FilesystemManager.CreateTemplete();
            Nodes = FlattenPack(Pack);
            if (!await UpdateDialogueView(Nodes, true)) return;
            dialogueView.Items[0].Selected = true;
            dialogueView.Focus();
        }

        async void LoadPack()
        {
            Cursor = Cursors.WaitCursor;
            SetScrollHooked(false);
            using OpenFileDialog fd = new()
            {
                InitialDirectory = workingFilePath,
                Filter = $"Miside Zero Dialogue Project (*.{FilesystemManager.ext})|*.{FilesystemManager.ext}|All files (*.*)|*.*",
                Multiselect = false
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Pack = FilesystemManager.LoadProj(fd.FileName);
                if (Pack != null)
                {
                    Nodes = FlattenPack(Pack);
                    Text = $"{fd.SafeFileName} - Miside Zero Dialogue Manager";
                    if (!await UpdateDialogueView(Nodes, true)) return;
                    dialogueView.Items[0].Selected = true;
                    dialogueView.Focus();
                    SetUIMode(UIMode.Idle);
                }
                else
                    SetUIMode(UIMode.Init);
            }
            SetScrollHooked(ThemeManager.ActiveTheme != ThemeManager.Theme.Light);
            Cursor = Cursors.Default;
        }

        async void LoadPack(string path)
        {
            Cursor = Cursors.WaitCursor;
            Pack = FilesystemManager.LoadProj(path);
            if (Pack != null)
            {
                Text = $"{Path.GetFileName(path)} - Miside Zero Dialogue Manager";
                Nodes = FlattenPack(Pack);
                if (!await UpdateDialogueView(Nodes, true)) return; // form closing probably
                UpdateNodeColors();
                dialogueView.Items[0].Selected = true;
                dialogueView.Focus();
                SetUIMode(UIMode.Idle);
            }
            else
                SetUIMode(UIMode.Init);

            Cursor = Cursors.Default;
        }

        void SavePack()
        {
            SetScrollHooked(false);
            using SaveFileDialog dialog = new()
            {
                Title = "Save dialogue pack",
                Filter = $"Miside Zero Dialogue Project (*.{FilesystemManager.ext})|*.{FilesystemManager.ext}",
                FileName = Path.GetFileName(workingFilePath) ?? $"CoolDialogue.{FilesystemManager.ext}",
                AddExtension = true,
                DefaultExt = FilesystemManager.ext,
                InitialDirectory = Path.GetDirectoryName(workingFilePath),
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilesystemManager.SaveProj(dialog.FileName, Pack!);
                Text = $"{Path.GetFileName(dialog.FileName)} - Miside Zero Dialogue Manager";
                workingFilePath = dialog.FileName;
            }
            SetScrollHooked(ThemeManager.ActiveTheme != ThemeManager.Theme.Light);
        }

        void LoadAudio(NodeRef nodeRef)
        {
            ArgumentNullException.ThrowIfNull(nodeRef);
            StopAudio();
            SetScrollHooked(false);
            using OpenFileDialog dialog = new()
            {
                Filter = "Audio Files (*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg)|*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg|All Files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilesystemManager.AddNodeAudio(nodeRef.TreeIndex, nodeRef.Node.id, dialog.FileName);
                SetUIMode(UIMode.ItemSelected);
            }
            SetScrollHooked(ThemeManager.ActiveTheme != ThemeManager.Theme.Light);
        }

        void RemoveAudio(NodeRef nodeRef)
        {
            ArgumentNullException.ThrowIfNull(nodeRef);
            FilesystemManager.RemoveNodeAudio(nodeRef.TreeIndex, nodeRef.Node.id);
            SetUIMode(UIMode.ItemSelected);
            StopAudio();
        }

        void EditProperties()
        {
            NodeRef nodeRef = GetSelectedNode() ?? throw new InvalidOperationException("Cannot edit properties when no node is selected.");
            using NodePropertiesEditor editor = new(nodeRef.Node);
            editor.ShowDialog();
            if (editor.DialogResult == DialogResult.OK)
            {
                nodeRef.Node.dialogueText = editor.modifiedNode.dialogueText;
                nodeRef.Node.speakerName = editor.modifiedNode.speakerName;
                nodeRef.Node.delay = editor.modifiedNode.delay;
                ListViewItem? item = dialogueView.Items.Cast<ListViewItem>()
                    .FirstOrDefault(i => (NodeRef)i.Tag! == nodeRef);
                if (item != null)
                {
                    item.SubItems[0].Text = nodeRef.Node.id.ToString();
                    item.SubItems[1].Text = nodeRef.Node.speakerName;
                    item.SubItems[2].Text = nodeRef.Node.dialogueText;
                }
                UpdateNodeColors();
                UpdateUI();
            }
        }

        private void LoadButton_Click(object sender, EventArgs e) => LoadPack();
        private void ToolStripLoadPack_Click(object sender, EventArgs e) => LoadPack();

        private void SelectAudioButton_Click(object sender, EventArgs e) => LoadAudio(GetSelectedNode()!);
        private void AssignAudioToolStripMenuItem_Click(object sender, EventArgs e) => LoadAudio(GetSelectedNode()!);

        private void SaveAsDialougePackToolStripMenuItem_Click(object sender, EventArgs e) => SavePack();
        private void SaveButton_Click(object sender, EventArgs e) => SavePack();

        private void InitializetemplateToolStripMenuItem_Click(object sender, EventArgs e) => Inittemplate();
        private void TemplateButton_Click(object sender, EventArgs e) => Inittemplate();

        private void AudioPlayButton_Click(object sender, EventArgs e) => PlayNodeAudio(GetSelectedNode()!);
        private void AudioStopButton_Click(object sender, EventArgs e) => StopAudio();
        private void RemoveAudioButton_Click(object sender, EventArgs e) => RemoveAudio(GetSelectedNode()!);

        private void PropertiesContextMenuItem_Click(object sender, EventArgs e) => EditProperties();
        private void EditPropertiesButton_Click(object sender, EventArgs e) => EditProperties();

        private void AddNodeContextMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = GetGroupAtPoint((Point)dialogueViewContextMenu.Tag!)!;
            AddNode(group);
        }

        private void AddNodeToThisTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode((ListViewGroup)groupContextMenu.Tag!);
        }

        private void AddNodeHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (groupContextMenu.Tag is ListViewGroup group)
            {
                AddNode(group);
            }
        }

        void AddNode(ListViewGroup group)
        {
            using NodePropertiesEditor editor = new();
            editor.ShowDialog();
            if (editor.DialogResult == DialogResult.OK)
            {
                int treeIndex = (int)group.Tag!;
                editor.modifiedNode.id = nextTemporaryNodeId--;
                Pack!.trees[treeIndex].nodes.Add(editor.modifiedNode);
                NodeRef newNode = new(editor.modifiedNode, treeIndex);
                Nodes.Add(newNode);
                AddToDialogueView(newNode, group);
            }
        }

        private void DeleteThisNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Nodes.Count == 0) return;
            if (GetSelectedNode() is not NodeRef nodeRef) return;
            Nodes.Remove(nodeRef);
            Pack!.trees[nodeRef.TreeIndex].nodes.RemoveAll(node => node.id == nodeRef.Node.id);

            ListViewItem? item = dialogueView.Items.Cast<ListViewItem>().FirstOrDefault(i => (NodeRef)i.Tag! == nodeRef);
            if (item != null) dialogueView.Items.Remove(item);

            UpdateNodeColors();
            SetUIMode(UIMode.Idle);
        }

        private void GenerateWithTTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Pack == null) return;
            using TTSEditor editor = new(Pack);
            editor.ShowDialog();
            if (editor.DialogResult != DialogResult.OK) return;

            Cursor = Cursors.WaitCursor;
            foreach (NodeRef nodeRef in Nodes)
                TTSManager.GenerateAudio(nodeRef, FilesystemManager.DataPath, editor.SpeakerVoices[nodeRef.Node.speakerName]);
            Cursor = Cursors.Default;
            UpdateUI();
        }

        private NodeRef? GetSelectedNode()
        {
            if (dialogueView.SelectedItems.Count == 0) return null;
            return (NodeRef?)dialogueView.SelectedItems[0].Tag;
        }

        private void DialogueView_SelectedIndexChanged(object sender, EventArgs e) => UpdateUI();

        private void SetStatus(string text) => statusLabel.Text = text;

        void UpdateUI()
        {
            StopAudio();

            if (dialogueView.SelectedItems.Count == 0)
            {
                SetUIMode(UIMode.Idle);
                return;
            }

            NodeRef nodeRef = GetSelectedNode()!;
            textLabel.Text = $"{nodeRef.Node.speakerName}: {nodeRef.Node.dialogueText}";
            SetStatus($"Selected: node {nodeRef.Node.id}, spoken by {nodeRef.Node.speakerName}");
            SetUIMode(string.IsNullOrEmpty(searchBox.Text) ? UIMode.ItemSelected : UIMode.ItemSelectedSearching);
        }

        private void NextNodesBox_DoubleClick(object sender, EventArgs e)
        {
            int index = nextNodesBox.SelectedIndex;
            if (index == -1) return;
            NextNodesBoxItem item = (NextNodesBoxItem)nextNodesBox.Items[index];
            if (item.node == null) return;
            NodeRef current = GetSelectedNode()!;
            dialogueView.SelectedItems.Clear();
            NodeRef? target = Nodes.FirstOrDefault(n => n.Node.id == item.node.id && n.TreeIndex == current.TreeIndex);
            ListViewItem? lvItem = dialogueView.Items.Cast<ListViewItem>()
                .FirstOrDefault(i => (NodeRef)i.Tag! == target);
            if (lvItem != null)
            {
                lvItem.Selected = true;
                UpdateNextNodesBox(GetSelectedNode()!);
            }
        }

        private bool _updatingText = false;
        private CancellationTokenSource _dialogueLvCts = new();

        private async Task SearchBoxTextChanged()
        {
            if (Pack == null || Nodes.Count == 0 || _updatingText) return;

            _dialogueLvCts.Cancel();
            _dialogueLvCts = new CancellationTokenSource();

            string filter = searchBox.Text?.ToLower() ?? "";
            List<NodeRef> filtered = string.IsNullOrEmpty(filter)
                ? Nodes
                : Nodes.Where(n => n.Node.dialogueText.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

            try
            {
                if (!await UpdateDialogueView(filtered, true)) return;
                SetUIMode(UIMode.Idle);
            }
            catch (OperationCanceledException) { }
        }
        private async void SearchBox_TextChanged(object sender, EventArgs e) => await SearchBoxTextChanged();

        private async Task SetSearchBoxText(string text)
        {
            _updatingText = true;
            searchBox.Text = text;
            _updatingText = false;
            await SearchBoxTextChanged();
        }

        private void PlayNodeAudio(NodeRef nodeRef)
        {
            ArgumentNullException.ThrowIfNull(nodeRef);
            string? audio = FilesystemManager.GetNodeAudioPath(nodeRef.TreeIndex, nodeRef.Node.id);
            if (audio == null) return;
            NAudioHelpers.PlayAudio(audio, ref waveOut, ref audioStream);
        }

        private void StopAudio() => NAudioHelpers.StopAudio(ref waveOut, ref audioStream);

        private void ShellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shellToolStripMenuItem.Checked)
                AssociationHelper.RegisterFileAssociation();
            else
                AssociationHelper.UnregisterFileAssociation();
        }

        private void LightToolStripMenuItem_Click(object sender, EventArgs e) => SetTheme(ThemeManager.Theme.Light);

        private void DarkToolStripMenuItem_Click(object sender, EventArgs e) => SetTheme(ThemeManager.Theme.Dark);

        private void BlurToolStripMenuItem_Click(object sender, EventArgs e) => SetTheme(ThemeManager.Theme.Blur);

        private void AcrylicToolStripMenuItem_Click(object sender, EventArgs e) => SetTheme(ThemeManager.Theme.Acrylic);

        private void BlackToolStripMenuItem_Click(object sender, EventArgs e) => SetTheme(ThemeManager.Theme.ExtendFrameDark);

        private void SetTheme(ThemeManager.Theme theme)
        {
            SetFormThemeAndStuff(theme, dialogueView);
            ThemeManager.SetGlobalTheme(theme);
            UpdateNodeColors();
        }

        private void DialogueView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewGroup? clickedGroup = ListViewGroupHelpers.GetGroupAt(dialogueView, e.Location);
                if (clickedGroup != null)
                {
                    groupContextMenu.Tag = clickedGroup;
                    groupContextMenu.Show(dialogueView, e.Location);
                }
            }
        }

        private void TreePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = (ListViewGroup)groupContextMenu.Tag!;
            int treeIndex = (int)group.Tag!;
            DialogueTreeDTO tree = Pack!.trees[treeIndex];
            using TreePropertiesEditor editor = new(tree);
            editor.ShowDialog();
            if (editor.DialogResult == DialogResult.OK)
            {
                Pack!.trees[treeIndex] = editor.ResultTree;
                UpdateUI();
                UpdateNodeColors();
            }
        }

        private async void JumpToThisNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NodeRef nodeRef = GetSelectedNode()!;
            await SetSearchBoxText("");
            ListViewItem item = dialogueView.Items.Cast<ListViewItem>().FirstOrDefault(i => (NodeRef)i.Tag! == nodeRef)!;
            item.EnsureVisible();
            item.Selected = true;
        }

        private void PreviewTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = (ListViewGroup)groupContextMenu.Tag!;
            int treeIndex = (int)group.Tag!;
            DialogueTreeDTO tree = Pack!.trees[treeIndex];
            using TreePreview preview = new(tree, treeIndex);
            preview.ShowDialog();
        }

        private void ChangeChirpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using ChangeChirpsForm a = new(Pack!);
            a.ShowDialog();
        }

        private void EditProjectMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using PackMetadataEditor editor = new(Pack!);
            editor.ShowDialog();
            Pack = editor.ResultPack;
        }
    }
}