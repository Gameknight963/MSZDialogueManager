using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MSZDialougeManager
{
    public partial class Form1 : Form
    {
        public static DialogueForest forest;
        public static List<DialogueNodeDTO> nodes => forest.nodes;
        WindowsMediaPlayer wmp = new WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
            dialogueView.Columns[2].Width -= SystemInformation.VerticalScrollBarWidth;
            SetSidebarMode(SidebarMode.Idle);
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.FormClosing += Form1_FormClosing;
            this.Shown += Form1_Shown;

            dialogueView.ColumnWidthChanging += dialogueView_ColumnWidthChanging;
            dialogueView.ColumnWidthChanged += dialogueView_ColumnWidthChanged;
            dialogueView.SizeChanged += dialogueView_SizeChanged;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Directory.Exists(FilesystemManager.DataPath))
            {
                Directory.Delete(FilesystemManager.DataPath, true);
                Directory.CreateDirectory(FilesystemManager.DataPath);

                /* Confirmation with dialog
                DialogResult result = MessageBox.Show("Restore previous state?", "Temporary files found", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Stopwatch sw = new Stopwatch(); sw.Start();
                    forest = FilesystemManager.LoadJson(FilesystemManager.NodesJsonPath);
                    SetStatus("Loading project...");
                    dialogueView.UpdateDialogueView(nodes);
                    SetStatus($"Loaded project from temporary files, time: {sw.ElapsedMilliseconds}ms");
                }
                */
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) { }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dialogueView.SelectedItems.Clear();
                SetSidebarMode(SidebarMode.Idle);
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

            // Show scrollbar if we can't fit the minimum width
            bool needsScroll = remaining < MinTextColumnWidth;
            if (needsScroll) remaining = MinTextColumnWidth;

            dialogueView.Columns[2].Width = remaining;

            ScrollbarHelper.Set(dialogueView, ScrollbarHelper.Scrollbar.Horz, needsScroll);
        }

        private void dialogueView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            // if (e.ColumnIndex == 2 && e.NewWidth < MinTextColumnWidth) e.NewWidth = MinTextColumnWidth;
        }

        private void dialogueView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            // if (e.ColumnIndex != 2) ResizeTextColumn();
        }

        private void dialogueView_SizeChanged(object sender, EventArgs e)
        {
            //ResizeTextColumn();
        }


        private enum SidebarMode
        {
            ItemSelected,
            Idle,
        }

        private void SetSidebarMode(SidebarMode mode)
        {
            if(FilesystemManager.IsFileLoaded)
            {
                saveButton.Visible = true;
            }
            else
            {
                saveButton.Visible = false;
            }
            switch (mode)
            {
                // panels are for noobs
                case SidebarMode.Idle:
                    //loadButton.Visible = true;
                    //templeteButton.Visible = true;
                    textLabel.Visible = false;
                    textHeaderLabel.Visible = false;
                    nextNodesHeader.Visible = false;
                    nextNodesBox.Visible = false;
                    selectAudioButton.Visible = false;
                    audioFileLabel.Visible = false;
                    audioFileHeader.Visible = false;
                    audioPlayButton.Visible = false;
                    audioStopButton.Visible = false;
                    removeAudioButton.Visible = false;
                    break;
                case SidebarMode.ItemSelected:
                    //loadButton.Visible = false;
                    //templeteButton.Visible = false;
                    textLabel.Visible = true;
                    textHeaderLabel.Visible = true;
                    nextNodesHeader.Visible = true;
                    nextNodesBox.Visible = true;
                    audioFileLabel.Visible = true;
                    audioFileHeader.Visible = true;
                    selectAudioButton.Visible = true;
                    removeAudioButton.Visible = true;
                    nextNodesBox.UpdateNodesBox(GetSelectedNode().nextNodeIds);
                    int index = dialogueView.SelectedItems[0].Index;
                    if (nodes[index].HasAudioClip())
                    {
                        audioFileLabel.Text = Path.GetFileName(nodes[index].GetAudioClip());
                        audioPlayButton.Visible = true;
                        audioStopButton.Visible = true;
                    }
                    else
                    {
                        audioFileLabel.Text = "None";
                        audioPlayButton.Visible = false;
                        audioStopButton.Visible = false;
                        removeAudioButton.Visible = false;
                    }
                    break;

            }
        }

        private void loadButton_Click(object sender, EventArgs e)
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
                SetSidebarMode(SidebarMode.Idle);
            }
        }

        private void templeteButton_Click(object sender, EventArgs e)
        {
            forest =  FilesystemManager.LoadJson(FilesystemManager.Templete);
            dialogueView.UpdateDialogueView(nodes);
        }

        private DialogueNodeDTO GetSelectedNode()
        {
            int index = dialogueView.SelectedItems[0].Index;
            return nodes[index];
        }

        private void SetStatus(string text) => statusLabel.Text = text;

        private void dialogueView_SelectedIndexChanged(object sender, EventArgs e)
        {
            wmp.controls.stop();

            if (dialogueView.SelectedItems.Count == 0)
            {
                SetSidebarMode(SidebarMode.Idle);
                return;
            }
            DialogueNodeDTO node = GetSelectedNode();
            textLabel.Text = $"{node.speakerName}: {node.dialogueText}";
            SetStatus($"Selected: node {node.id}, spoken by {node.speakerName}");
            SetSidebarMode(SidebarMode.ItemSelected);
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

        private void selectAudioButton_Click(object sender, EventArgs e)
        {
            wmp.controls.stop();

            DialogueNodeDTO node = GetSelectedNode();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Audio Files (*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac)|*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac|All Files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                node.AddAudioClip(dialog.FileName);
                SetSidebarMode(SidebarMode.ItemSelected);
            }
        }

        private void audioPlayButton_Click(object sender, EventArgs e)
        {
            wmp.URL = GetSelectedNode().GetAudioClip();
            wmp.controls.play();
        }

        private void audioStopButton_Click(object sender, EventArgs e)
        {
            wmp.controls.stop();
        }

        private void removeAudioButton_Click(object sender, EventArgs e)
        {
            GetSelectedNode().RemoveAudioClip();
            SetSidebarMode(SidebarMode.ItemSelected);
            wmp.controls.stop();
        }

        private void saveButton_Click(object sender, EventArgs e)
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
    }
}
