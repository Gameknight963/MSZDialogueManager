using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Vorbis;

namespace MSZDialougeManager
{
    public partial class Form1 : Form
    {
        // Dialogue data
        public static DialogueForest forest;
        public static List<DialogueNodeDTO> nodes => forest.nodes;

        // NAudio playback
        private IWavePlayer waveOut;
        private WaveStream audioStream;

        public Form1()
        {
            InitializeComponent();
            SetUIMode(UIMode.Init);
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.Shown += Form1_Shown;

            dialogueView.ColumnWidthChanging += dialogueView_ColumnWidthChanging;
            dialogueView.ColumnWidthChanged += dialogueView_ColumnWidthChanged;

            searchBox.SetPlaceholder("Search by dialogue text...");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Directory.Exists(FilesystemManager.DataPath))
            {
                Directory.Delete(FilesystemManager.DataPath, true);
                Directory.CreateDirectory(FilesystemManager.DataPath);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
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

        // column resize logic

        private void dialogueView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.NewWidth < MinTextColumnWidth)
                e.NewWidth = MinTextColumnWidth;
        }

        private void dialogueView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (e.ColumnIndex != 2) ResizeTextColumn();
        }

        // i dont know what it fucking does
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
            // removing savebutton soon
            saveButton.Visible = false;

            bool itemSelected = (mode == UIMode.ItemSelected);
            textLabel.Visible = itemSelected;
            textHeaderLabel.Visible = itemSelected;
            nextNodesHeader.Visible = itemSelected;
            nextNodesBox.Visible = itemSelected;
            selectAudioButton.Visible = itemSelected;
            audioFileLabel.Visible = itemSelected;
            audioFileHeader.Visible = itemSelected;
            audioPlayButton.Visible = itemSelected;
            audioStopButton.Visible = itemSelected;
            templeteButton.Visible = (mode == UIMode.Init);
            loadButton.Visible = (mode == UIMode.Init);

            removeAudioButton.Visible = false;
            if (!itemSelected) return;

            DialogueNodeDTO selectedNode = GetSelectedNode();
            nextNodesBox.UpdateNodesBox(selectedNode.nextNodeIds);

            bool hasAudioClip = selectedNode.HasAudioClip();
            audioPlayButton.Visible = hasAudioClip;
            audioStopButton.Visible = hasAudioClip;
            removeAudioButton.Visible = hasAudioClip;
            audioFileLabel.Text = hasAudioClip ? Path.GetFileName(selectedNode.GetAudioClip()) : "None";
        }

        void InitTemplete()
        {
            SetUIMode(UIMode.Idle);
            forest = FilesystemManager.LoadJson(FilesystemManager.Templete);
            dialogueView.UpdateDialogueView(nodes);
            dialogueView.Items[0].Selected = true;
            dialogueView.Focus();
        }

        void LoadPack()
        {
            SetUIMode(UIMode.Idle);
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fd.Filter = $"Miside Zero Dialogue Project (*.{FilesystemManager.ext})|*.{FilesystemManager.ext}|All files (*.*)|*.*";
                fd.Multiselect = false;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    forest = FilesystemManager.LoadProj(fd.FileName);
                    dialogueView.UpdateDialogueView(nodes);
                    dialogueView.Items[0].Selected = true;
                    dialogueView.Focus();
                }
            }
        }

        void SavePack()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Save dialogue pack";
                dialog.Filter = $"Miside Zero Dialogue Project (*.{FilesystemManager.ext})|*.{FilesystemManager.ext}";
                dialog.FileName = $"CustomDialogue.{FilesystemManager.ext}";
                dialog.AddExtension = true;
                dialog.DefaultExt = FilesystemManager.ext;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FilesystemManager.SaveProj(dialog.FileName, forest);
                }
            }
        }

        void LoadAudio(DialogueNodeDTO node)
        {
            StopAudio();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Audio Files (*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg)|*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg|All Files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                node.AddAudioClip(dialog.FileName);
                SetUIMode(UIMode.ItemSelected);
            }
        }

        private void loadButton_Click(object sender, EventArgs e) => LoadPack();
        private void initializeTempleteToolStripMenuItem_Click(object sender, EventArgs e) => InitTemplete();
        private void selectAudioButton_Click(object sender, EventArgs e) => LoadAudio(GetSelectedNode());
        private void saveAsDialougePackToolStripMenuItem_Click(object sender, EventArgs e) => SavePack();
        private void saveButton_Click(object sender, EventArgs e) => SavePack();
        private void templeteButton_Click(object sender, EventArgs e) => InitTemplete();

        private DialogueNodeDTO GetSelectedNode()
        {
            int index = int.Parse(dialogueView.SelectedItems[0].Text);
            return nodes[index];
        }

        private void SetStatus(string text) => statusLabel.Text = text;

        private void dialogueView_SelectedIndexChanged(object sender, EventArgs e)
        {
            StopAudio();

            if (dialogueView.SelectedItems.Count == 0)
            {
                SetUIMode(UIMode.Idle);
                return;
            }

            DialogueNodeDTO node = GetSelectedNode();
            textLabel.Text = $"{node.speakerName}: {node.dialogueText}";
            SetStatus($"Selected: node {node.id}, spoken by {node.speakerName}");
            SetUIMode(UIMode.ItemSelected);
        }

        private void nextNodesBox_DoubleClick(object sender, EventArgs e)
        {
            int index = nextNodesBox.SelectedIndex;
            if (index == -1) return;
            NextNodesBoxItem item = (NextNodesBoxItem)nextNodesBox.SelectedItem;
            dialogueView.SelectedItems.Clear();
            dialogueView.Items[item.node.id].Selected = true;
            nextNodesBox.UpdateNodesBox(GetSelectedNode().nextNodeIds);
        }

        private void audioPlayButton_Click(object sender, EventArgs e)
        {
            PlayNodeAudio(GetSelectedNode());
        }

        private void audioStopButton_Click(object sender, EventArgs e)
        {
            StopAudio();
        }

        private void removeAudioButton_Click(object sender, EventArgs e)
        {
            GetSelectedNode().RemoveAudioClip();
            SetUIMode(UIMode.ItemSelected);
            StopAudio();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (forest == null || nodes.Count == 0) return;
            dialogueView.UpdateDialogueViewFiltered(nodes, searchBox.Text);
            SetUIMode(UIMode.Idle);
        }

        private void PlayNodeAudio(DialogueNodeDTO node)
        {
            if (node.HasAudioClip())
                NAudioHelpers.PlayAudio(node.GetAudioClip(), ref waveOut, ref audioStream);
        }

        private void StopAudio() => NAudioHelpers.StopAudio(ref waveOut, ref audioStream);

        private void toolStripLoadPack_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fd.Filter = $"Miside Zero Dialogue Project (*.{FilesystemManager.ext})|*.{FilesystemManager.ext}|All files (*.*)|*.*";
                fd.Multiselect = false;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    forest = FilesystemManager.LoadProj(fd.FileName);
                    dialogueView.UpdateDialogueView(nodes);
                }
                SetUIMode(UIMode.Idle);
            }
        }

        private void generateWithTTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (forest == null) return;
            Cursor = Cursors.WaitCursor;
            foreach (DialogueNodeDTO node in nodes)
            {
                TTSManager.GenerateAudio(node, FilesystemManager.DataPath);
            }
            Cursor = Cursors.Default;
            SetUIMode(UIMode.ItemSelected);
        }
    }
}
