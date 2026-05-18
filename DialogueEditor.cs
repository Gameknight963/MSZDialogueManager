using MSZDialougeManager.Styling;
using MZDO;
using NAudio.Wave;
using Newtonsoft.Json;

namespace MSZDialougeManager
{
    public partial class DialogueEditor : ThemeableForm
    {
        public record NodeRef(DialogueNodeDTO Node, int TreeIndex);

        public static DialoguePack? pack { get; private set; }
        public static List<NodeRef> nodes { get; private set; } = new();

        private IWavePlayer? waveOut;
        private WaveStream? audioStream;
        private int nextTemporaryNodeId = -1;
        private readonly string lastThemeFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lasttheme");

        private bool scrollHooked = false;

        private string? workingFilePath;

        public DialogueEditor(string? filePath = null)
        {
            InitializeComponent();
            SetUIMode(UIMode.Init);
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.Shown += Form1_Shown;
            dialogueViewContextMenu.Opening += ContextMenu_Opening;
            dialogueViewContextMenu.Opening += DialogueViewContextMenu_Opening;
            groupContextMenu.Opening += ContextMenu_Opening;

            dialogueView.ColumnWidthChanging += dialogueView_ColumnWidthChanging;
            dialogueView.ColumnWidthChanged += dialogueView_ColumnWidthChanged;

            Directory.CreateDirectory(FilesystemManager.DataPath);

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetScrollHooked(ThemeManager.ActiveTheme != ThemeManager.Theme.Light);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            File.WriteAllText(lastThemeFile, ThemeManager.ActiveTheme.ToString());
            base.OnFormClosing(e);
            ScrollHook.Uninstall();
        }

        private void SetScrollHooked(bool enabled)
        {
            if (scrollHooked != enabled)
            {
                if (enabled) ScrollHook.Install();
                else ScrollHook.Uninstall();
                scrollHooked = enabled;
            }
        }

        private void ContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ContextMenuStrip? cms = (ContextMenuStrip)sender!;
            DwmApi.SetAccentState(cms.Handle, DwmApi.AccentState.ACCENT_ENABLE_BLURBEHIND, 0x66000000);
            cms.BackColor = ThemeManager.AcrylicMainColor;
            cms.ForeColor = Color.White;
            cms.ShowImageMargin = false;
        }

        private void DialogueViewContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            dialogueViewContextMenu.Tag = dialogueView.PointToClient(Cursor.Position);
        }

        private void Form1_Shown(object? sender, EventArgs e)
        {
            if (Directory.Exists(FilesystemManager.DataPath))
            {
                Directory.Delete(FilesystemManager.DataPath, true);
                Directory.CreateDirectory(FilesystemManager.DataPath);
            }
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
                PlayNodeAudio(GetSelectedNode());
            }
        }

        private const int MinTextColumnWidth = 415;

        private void ResizeTextColumn()
        {
            if (dialogueView.Columns.Count < 3) return;

            int totalOtherColumns = 0;
            for (int i = 0; i < dialogueView.Columns.Count - 1; i++)
                totalOtherColumns += dialogueView.Columns[i].Width;

            int remaining = dialogueView.ClientSize.Width - totalOtherColumns;

            bool needsScroll = remaining < MinTextColumnWidth;
            if (needsScroll) remaining = MinTextColumnWidth;

            dialogueView.Columns[2].Width = remaining;

            ScrollbarHelper.Set(dialogueView, ScrollbarHelper.Scrollbar.Horz, needsScroll);
        }

        private void dialogueView_ColumnWidthChanging(object? sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.NewWidth < MinTextColumnWidth)
                e.NewWidth = MinTextColumnWidth;
        }

        private void dialogueView_ColumnWidthChanged(object? sender, ColumnWidthChangedEventArgs e)
        {
            if (e.ColumnIndex != 2) ResizeTextColumn();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_EXITSIZEMOVE = 0x0232;
            if (m.Msg == WM_EXITSIZEMOVE)
                ResizeTextColumn();
            base.WndProc(ref m);
        }

        private enum UIMode
        {
            ItemSelected,
            Idle,
            Init,
        }

        private void SetUIMode(UIMode mode)
        {
            bool itemSelected = mode == UIMode.ItemSelected;
            bool init = mode == UIMode.Init;

            textLabel.Visible = itemSelected;
            textHeaderLabel.Visible = itemSelected;
            nextNodesHeader.Visible = itemSelected;
            nextNodesBox.Visible = itemSelected;
            selectAudioButton.Visible = itemSelected;
            audioFileLabel.Visible = itemSelected;
            audioFileHeader.Visible = itemSelected;
            audioPlayButton.Visible = itemSelected;
            audioStopButton.Visible = itemSelected;

            templateButton.Visible = init;
            loadButton.Visible = init;

            playAudioToolStripMenuItem.Enabled = itemSelected;
            stopAudioToolStripMenuItem.Enabled = itemSelected;
            assignAudioToolStripMenuItem.Enabled = itemSelected;
            removeAudioToolStripMenuItem.Enabled = itemSelected;
            editPropertiesButton.Visible = itemSelected;
            addNodeContextMenuItem.Visible = itemSelected;
            deleteThisNodeToolStripMenuItem.Visible = itemSelected;

            propertiesContextMenuItem.Visible = itemSelected;
            propertiesContextMenuItem.Enabled = itemSelected;
            propertiesToolStripMenuItem.Enabled = itemSelected;

            generateWithTTSToolStripMenuItem.Enabled = !init;

            removeAudioButton.Visible = false;
            if (!itemSelected) return;

            NodeRef selected = GetSelectedNode();
            UpdateNodesBox(nextNodesBox, GetSelectedNode());

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

        void UpdateNodesBox(ListBox nodesBox, NodeRef current)
        {
            nodesBox.BeginUpdate();
            nodesBox.Items.Clear();
            foreach (int id in current.Node.nextNodeIds)
            {
                NodeRef? nodeRef = nodes.FirstOrDefault(n => n.Node.id == id && n.TreeIndex == current.TreeIndex);
                NextNodesBoxItem item = nodeRef == null
                    ? new() { text = $"[{id}] ⚠ This node no longer exists", node = null }
                    : new() { text = $"[{id}] {nodeRef.Node.speakerName}: {nodeRef.Node.dialogueText}", node = nodeRef.Node };
                nodesBox.Items.Add(item);
            }
            nodesBox.EndUpdate();
        }

        public async Task UpdateDialogueView(List<NodeRef> nodes, CancellationToken ct = default)
        {
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
                    string groupName = pack!.trees[nodeRef.TreeIndex].name ?? $"Tree {nodeRef.TreeIndex}";
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
                        await Task.Delay(1);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                UseWaitCursor = false;
                dialogueView.EndUpdate();
            }
        }
        private HashSet<int> GetReachableNodes(int treeIndex)
        {
            HashSet<int> reachable = [];
            Queue<int> queue = new();

            foreach (int startId in pack!.trees[treeIndex].startNodeIds)
                queue.Enqueue(startId);

            while (queue.Count > 0)
            {
                int id = queue.Dequeue();
                if (!reachable.Add(id)) continue;

                NodeRef? node = nodes.FirstOrDefault(n => n.Node.id == id && n.TreeIndex == treeIndex);
                if (node == null) continue;

                foreach (int nextId in node.Node.nextNodeIds)
                    queue.Enqueue(nextId);
            }

            return reachable;
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
                bool isStartNode = pack!.trees[nodeRef.TreeIndex].startNodeIds?.Contains(nodeRef.Node.id) ?? false;
                bool isTerminal = nodeRef.Node.nextNodeIds == null || nodeRef.Node.nextNodeIds.Length == 0;

                item.ForeColor = hasBrokenRefs ? Color.Orange
                    : isStartNode ? Color.Green
                    : !reachable ? Color.Red
                    : isTerminal ? Color.Blue
                    : item.ListView!.ForeColor;
            }
        }

        public void AddToDialogueView(NodeRef nodeRef, ListViewGroup group)
        {
            ListViewItem item = new(nodeRef.Node.id.ToString()) { Group = group, Tag = nodeRef };
            item.SubItems.Add(nodeRef.Node.speakerName);
            item.SubItems.Add(nodeRef.Node.dialogueText);
            dialogueView.Items.Add(item);
            UpdateNodeColors();
        }

        static List<NodeRef> FlattenPack(DialoguePack pack) =>
            pack.trees
                .SelectMany((tree, treeIndex) => tree.nodes.Select(node => new NodeRef(node, treeIndex)))
                .ToList();

        async void Inittemplate()
        {
            SetUIMode(UIMode.Idle);
            pack = JsonConvert.DeserializeObject<DialoguePack>(File.ReadAllText(FilesystemManager.Template))!;
            pack.PackFormat = MZDO.Core.PackFormatVersion; // update pack format to latest right away
            nodes = FlattenPack(pack);
            await UpdateDialogueView(nodes);
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
                pack = FilesystemManager.LoadProj(fd.FileName)!;
                nodes = FlattenPack(pack);
                await UpdateDialogueView(nodes);
                dialogueView.Items[0].Selected = true;
                dialogueView.Focus();
                SetUIMode(UIMode.Idle);
            }
            SetScrollHooked(ThemeManager.ActiveTheme != ThemeManager.Theme.Light);
            Cursor = Cursors.Default;
        }

        async void LoadPack(string path)
        {
            Cursor = Cursors.WaitCursor;
            pack = FilesystemManager.LoadProj(path)!;
            nodes = FlattenPack(pack);
            await UpdateDialogueView(nodes);
            UpdateNodeColors();
            dialogueView.Items[0].Selected = true;
            dialogueView.Focus();
            SetUIMode(UIMode.Idle);
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
                FilesystemManager.SaveProj(dialog.FileName, pack!);
                workingFilePath = dialog.FileName;
            }
            SetScrollHooked(ThemeManager.ActiveTheme != ThemeManager.Theme.Light);
        }

        void LoadAudio(NodeRef nodeRef)
        {
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
            FilesystemManager.RemoveNodeAudio(nodeRef.TreeIndex, nodeRef.Node.id);
            SetUIMode(UIMode.ItemSelected);
            StopAudio();
        }

        void EditProperties()
        {
            NodeRef nodeRef = GetSelectedNode();
            NodePropertiesEditor editor = new(nodeRef.Node);
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

        private void loadButton_Click(object sender, EventArgs e) => LoadPack();
        private void toolStripLoadPack_Click(object sender, EventArgs e) => LoadPack();

        private void selectAudioButton_Click(object sender, EventArgs e) => LoadAudio(GetSelectedNode());
        private void assignAudioToolStripMenuItem_Click(object sender, EventArgs e) => LoadAudio(GetSelectedNode());

        private void saveAsDialougePackToolStripMenuItem_Click(object sender, EventArgs e) => SavePack();
        private void saveButton_Click(object sender, EventArgs e) => SavePack();

        private void initializetemplateToolStripMenuItem_Click(object sender, EventArgs e) => Inittemplate();
        private void templateButton_Click(object sender, EventArgs e) => Inittemplate();

        private void audioPlayButton_Click(object sender, EventArgs e) => PlayNodeAudio(GetSelectedNode());
        private void playAudioToolStripMenuItem_Click(object sender, EventArgs e) => PlayNodeAudio(GetSelectedNode());

        private void audioStopButton_Click(object sender, EventArgs e) => StopAudio();
        private void stopAudioToolStripMenuItem_Click(object sender, EventArgs e) => StopAudio();

        private void removeAudioButton_Click(object sender, EventArgs e) => RemoveAudio(GetSelectedNode());
        private void removeAudioToolStripMenuItem_Click(object sender, EventArgs e) => RemoveAudio(GetSelectedNode());

        private void editPropertiesButton_Click(object sender, EventArgs e) => EditProperties();

        private void addNodeContextMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = GetGroupAtPoint((Point)dialogueViewContextMenu.Tag!)!;
            AddNode(group);
        }

        private void addNodeToThisTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode((ListViewGroup)groupContextMenu.Tag!);
        }

        private void addNodeHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (groupContextMenu.Tag is ListViewGroup group)
            {
                AddNode(group);
            }
        }

        void AddNode(ListViewGroup group)
        {
            NodePropertiesEditor editor = new();
            editor.ShowDialog();
            if (editor.DialogResult == DialogResult.OK)
            {
                int treeIndex = (int)group.Tag!;
                editor.modifiedNode.id = nextTemporaryNodeId--;
                pack!.trees[treeIndex].nodes.Add(editor.modifiedNode);
                NodeRef newNode = new(editor.modifiedNode, treeIndex);
                nodes.Add(newNode);
                AddToDialogueView(newNode, group);
            }
        }

        private void deleteThisNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nodes.Count == 0)
                return;

            NodeRef nodeRef = GetSelectedNode();
            nodes.Remove(nodeRef);

            ListViewItem? item = dialogueView.Items.Cast<ListViewItem>()
                .FirstOrDefault(i => (NodeRef)i.Tag! == nodeRef);

            if (item != null)
                dialogueView.Items.Remove(item);

            UpdateNodeColors();
            SetUIMode(UIMode.Idle);
        }

        private void generateWithTTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pack == null) return;
            TTSEditor editor = new(pack);
            editor.ShowDialog();
            if (editor.DialogResult != DialogResult.OK) return;

            Cursor = Cursors.WaitCursor;
            foreach (NodeRef nodeRef in nodes)
                TTSManager.GenerateAudio(nodeRef, FilesystemManager.DataPath, editor.SpeakerVoices[nodeRef.Node.speakerName]);
            Cursor = Cursors.Default;
            UpdateUI();
        }

        private NodeRef GetSelectedNode() =>
            (NodeRef)dialogueView.SelectedItems[0].Tag!;

        private void dialogueView_SelectedIndexChanged(object sender, EventArgs e) => UpdateUI();

        private void SetStatus(string text) => statusLabel.Text = text;

        void UpdateUI()
        {
            StopAudio();

            if (dialogueView.SelectedItems.Count == 0)
            {
                SetUIMode(UIMode.Idle);
                return;
            }

            NodeRef nodeRef = GetSelectedNode();
            textLabel.Text = $"{nodeRef.Node.speakerName}: {nodeRef.Node.dialogueText}";
            SetStatus($"Selected: node {nodeRef.Node.id}, spoken by {nodeRef.Node.speakerName}");
            SetUIMode(UIMode.ItemSelected);
        }

        private void nextNodesBox_DoubleClick(object sender, EventArgs e)
        {
            int index = nextNodesBox.SelectedIndex;
            if (index == -1) return;
            NextNodesBoxItem item = (NextNodesBoxItem)nextNodesBox.Items[index];
            if (item.node == null) return;
            NodeRef current = GetSelectedNode();
            dialogueView.SelectedItems.Clear();
            NodeRef? target = nodes.FirstOrDefault(n => n.Node.id == item.node.id && n.TreeIndex == current.TreeIndex);
            ListViewItem? lvItem = dialogueView.Items.Cast<ListViewItem>()
                .FirstOrDefault(i => (NodeRef)i.Tag! == target);
            if (lvItem != null)
            {
                lvItem.Selected = true;
                UpdateNodesBox(nextNodesBox, GetSelectedNode());
            }
        }

        private CancellationTokenSource? _searchCts;

        private async void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (pack == null || nodes.Count == 0) return;

            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();

            string filter = searchBox.Text?.ToLower() ?? "";
            List<NodeRef> filtered = string.IsNullOrEmpty(filter)
                ? nodes
                : nodes.Where(n => n.Node.dialogueText.ToLower().Contains(filter)).ToList();

            try
            {
                await UpdateDialogueView(filtered, _searchCts.Token);
                SetUIMode(UIMode.Idle);
            }
            catch (OperationCanceledException) { }
        }

        private void PlayNodeAudio(NodeRef nodeRef)
        {
            string? audio = FilesystemManager.GetNodeAudioPath(nodeRef.TreeIndex, nodeRef.Node.id);
            if (audio == null) return;
            NAudioHelpers.PlayAudio(audio, ref waveOut, ref audioStream);
        }

        private void StopAudio() => NAudioHelpers.StopAudio(ref waveOut, ref audioStream);

        private void shellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shellToolStripMenuItem.Checked)
                AssociationHelper.RegisterFileAssociation();
            else
                AssociationHelper.UnregisterFileAssociation();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetScrollHooked(false);
            ThemeManager.SetGlobalTheme(ThemeManager.Theme.Light);
            UpdateNodeColors();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetScrollHooked(true);
            ThemeManager.SetGlobalTheme(ThemeManager.Theme.Dark);
            UpdateNodeColors();
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetScrollHooked(true);
            ThemeManager.SetGlobalTheme(ThemeManager.Theme.Blur);
            UpdateNodeColors();
        }

        private void acrylicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetScrollHooked(true);
            ThemeManager.SetGlobalTheme(ThemeManager.Theme.Acrylic);
            UpdateNodeColors();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetScrollHooked(true);
            ThemeManager.SetGlobalTheme(ThemeManager.Theme.ExtendFrameDark);
            UpdateNodeColors();
        }

        private void dialogueView_MouseUp(object sender, MouseEventArgs e)
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

        private void treePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = (ListViewGroup)groupContextMenu.Tag!;
            int treeIndex = (int)group.Tag!;
            DialogueTreeDTO tree = pack!.trees[treeIndex];
            TreePropertiesEditor editor = new(tree);
            editor.ShowDialog();
            if (editor.DialogResult == DialogResult.OK)
            {
                pack!.trees[treeIndex] = editor.ResultTree;
                UpdateUI();
                UpdateNodeColors();
            }
        }
    }
}