using MZDO;
using NAudio.Wave;
using Newtonsoft.Json;

namespace MSZDialougeManager
{
    public partial class DialogueEditor : Form
    {
        public record NodeRef(DialogueNodeDTO Node, int TreeIndex);

        // Dialogue data
        public static DialoguePack? pack { get; private set; }
        public static List<NodeRef> nodes { get; private set; } = new();

        // NAudio playback
        private IWavePlayer? waveOut;
        private WaveStream? audioStream;

        public DialogueEditor(string? filePath = null)
        {
            InitializeComponent();
            SetUIMode(UIMode.Init);
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.Shown += Form1_Shown;

            dialogueView.ColumnWidthChanging += dialogueView_ColumnWidthChanging;
            dialogueView.ColumnWidthChanged += dialogueView_ColumnWidthChanged;

            if (AssociationHelper.IsFileAssociationRegistered() && !AssociationHelper.IsFileAssociationCurrent())
                AssociationHelper.RegisterFileAssociation();

            shellToolStripMenuItem.Checked = AssociationHelper.IsFileAssociationRegistered();

            if (filePath != null) LoadPack(filePath);

            searchBox.SetPlaceholder("Search by dialogue text...");
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
            Init
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

            propertiesContextMenuItem.Visible = itemSelected;
            propertiesContextMenuItem.Enabled = itemSelected;
            propertiesToolStripMenuItem.Enabled = itemSelected;

            generateWithTTSToolStripMenuItem.Enabled = !init;

            removeAudioButton.Visible = false;
            if (!itemSelected) return;

            NodeRef selected = GetSelectedNode();
            UpdateNodesBox(nextNodesBox, selected.Node.nextNodeIds);

            bool hasAudioClip = FilesystemManager.DoesNodeAudioExist(selected.TreeIndex, selected.Node.id);
            audioPlayButton.Visible = hasAudioClip;
            audioStopButton.Visible = hasAudioClip;
            removeAudioButton.Visible = hasAudioClip;
            audioFileLabel.Text = hasAudioClip
                ? Path.GetFileName(FilesystemManager.GetNodeAudioPath(selected.TreeIndex, selected.Node.id))
                : "None";
        }

        void UpdateNodesBox(ListBox nodesBox, int[] nodeIds)
        {
            nodesBox.BeginUpdate();
            nodesBox.Items.Clear();
            foreach (int id in nodeIds)
            {
                NodeRef? nodeRef = nodes.FirstOrDefault(n => n.Node.id == id);
                if (nodeRef == null) continue;
                NextNodesBoxItem item = new()
                {
                    text = $"[{id}] {nodeRef.Node.speakerName}: {nodeRef.Node.dialogueText}",
                    node = nodeRef.Node
                };
                nodesBox.Items.Add(item);
            }
            nodesBox.EndUpdate();
        }

        public static void UpdateDialogueView(ListView dialogueView, List<NodeRef> nodes)
        {
            dialogueView.Items.Clear();
            dialogueView.Groups.Clear();
            foreach (NodeRef nodeRef in nodes)
            {
                string groupKey = $"tree_{nodeRef.TreeIndex}";
                string groupName = pack!.trees[nodeRef.TreeIndex].name ?? $"Tree {nodeRef.TreeIndex}";
                ListViewGroup? group = dialogueView.Groups.Cast<ListViewGroup>()
                    .FirstOrDefault(g => g.Name == groupKey);
                if (group == null)
                {
                    group = new ListViewGroup(groupKey, groupName);
                    dialogueView.Groups.Add(group);
                }
                ListViewItem item = new(nodeRef.Node.id.ToString()) { Group = group, Tag = nodeRef };
                item.SubItems.Add(nodeRef.Node.speakerName);
                item.SubItems.Add(nodeRef.Node.dialogueText);
                dialogueView.Items.Add(item);
            }
        }

        static List<NodeRef> FlattenPack(DialoguePack pack) =>
            pack.trees
                .SelectMany((tree, treeIndex) => tree.nodes.Select(node => new NodeRef(node, treeIndex)))
                .ToList();

        void Inittemplate()
        {
            SetUIMode(UIMode.Idle);
            pack = JsonConvert.DeserializeObject<DialoguePack>(File.ReadAllText(FilesystemManager.Template))!;
            nodes = FlattenPack(pack);
            UpdateDialogueView(dialogueView, nodes);
            dialogueView.Items[0].Selected = true;
            dialogueView.Focus();
        }

        void LoadPack()
        {
            Cursor = Cursors.WaitCursor;
            using OpenFileDialog fd = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = $"Dialogue Project (*.{FilesystemManager.ext})|*.{FilesystemManager.ext}|All files (*.*)|*.*",
                Multiselect = false
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                pack = FilesystemManager.LoadProj(fd.FileName)!;
                nodes = FlattenPack(pack);
                UpdateDialogueView(dialogueView, nodes);
                dialogueView.Items[0].Selected = true;
                dialogueView.Focus();
                SetUIMode(UIMode.Idle);
            }
            Cursor = Cursors.Default;
        }

        void LoadPack(string path)
        {
            Cursor = Cursors.WaitCursor;
            pack = FilesystemManager.LoadProj(path)!;
            nodes = FlattenPack(pack);
            UpdateDialogueView(dialogueView, nodes);
            dialogueView.Items[0].Selected = true;
            dialogueView.Focus();
            SetUIMode(UIMode.Idle);
            Cursor = Cursors.Default;
        }

        void SavePack()
        {
            using SaveFileDialog dialog = new()
            {
                Title = "Save dialogue pack",
                Filter = $"Dialogue Project (*.{FilesystemManager.ext})|*.{FilesystemManager.ext}",
                FileName = $"CustomDialogue.{FilesystemManager.ext}",
                AddExtension = true,
                DefaultExt = FilesystemManager.ext
            };
            if (dialog.ShowDialog() == DialogResult.OK)
                FilesystemManager.SaveProj(dialog.FileName, pack!);
        }

        void LoadAudio(NodeRef nodeRef)
        {
            StopAudio();
            using OpenFileDialog dialog = new()
            {
                Filter = "Audio Files (*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg)|*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg|All Files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilesystemManager.AddNodeAudio(nodeRef.TreeIndex, nodeRef.Node.id, dialog.FileName);
                SetUIMode(UIMode.ItemSelected);
            }
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

        private void generateWithTTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pack == null) return;
            TTSEditor editor = new(pack);
            editor.ShowDialog();
            if (editor.DialogResult != DialogResult.OK) return;

            Cursor = Cursors.WaitCursor;
            foreach (NodeRef nodeRef in nodes)
                TTSManager.GenerateAudio(nodeRef.Node, FilesystemManager.DataPath, editor.speakerVoices[nodeRef.Node.speakerName]);
            Cursor = Cursors.Default;
            UpdateUI();
        }

        private NodeRef GetSelectedNode() =>
            (NodeRef)dialogueView.SelectedItems[0].Tag!;

        private void SetStatus(string text) => statusLabel.Text = text;

        private void dialogueView_SelectedIndexChanged(object sender, EventArgs e) => UpdateUI();

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
            dialogueView.SelectedItems.Clear();
            ListViewItem? lvItem = dialogueView.Items.Cast<ListViewItem>()
                .FirstOrDefault(i => i.Text == item.node.id.ToString());
            if (lvItem != null)
            {
                lvItem.Selected = true;
                UpdateNodesBox(nextNodesBox, GetSelectedNode().Node.nextNodeIds);
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (pack == null || nodes.Count == 0) return;

            string filter = searchBox.Text?.ToLower() ?? "";

            List<NodeRef> filtered = string.IsNullOrEmpty(filter)
                ? nodes
                : nodes.Where(n => n.Node.dialogueText.ToLower().Contains(filter)).ToList();

            UpdateDialogueView(dialogueView, filtered);
            SetUIMode(UIMode.Idle);
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
    }
}