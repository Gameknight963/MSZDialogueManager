using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WMPLib;

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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // put a comfirmation prompt here or smth
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dialogueView.SelectedItems.Clear();
                SetSidebarMode(SidebarMode.Idle);
            }
        }

        private enum SidebarMode
        {
            ItemSelected,
            Idle,
        }

        private void SetSidebarMode(SidebarMode mode)
        {
            switch (mode)
            {
                // panels are for noobs
                case SidebarMode.Idle:
                    jsonButton.Visible = true;
                    templeteButton.Visible = true;
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
                    saveButton.Visible = false;
                    break;
                case SidebarMode.ItemSelected:
                    jsonButton.Visible = false;
                    templeteButton.Visible = false;
                    textLabel.Visible = true;
                    textHeaderLabel.Visible = true;
                    nextNodesHeader.Visible = true;
                    nextNodesBox.Visible = true;
                    audioFileLabel.Visible = true;
                    audioFileHeader.Visible = true;
                    selectAudioButton.Visible = true;
                    removeAudioButton.Visible = true;
                    saveButton.Visible = true;
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

        private void jsonButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                fd.Multiselect = false;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(fd.FileName);
                    forest = JsonConvert.DeserializeObject<DialogueForest>(json);
                    dialogueView.UpdateDialogueView(nodes);
                }
            }
        }

        private void templeteButton_Click(object sender, EventArgs e)
        {
            string json = File.ReadAllText(FilesystemManager.Templete);
            forest = JsonConvert.DeserializeObject<DialogueForest>(json);
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
            DialogueNodeDTO node = GetSelectedNode();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Audio Files (*.wav;*.mp3)|*.wav;*.mp3|All Files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                string ext = Path.GetExtension(path);
                string destination = Path.Combine(FilesystemManager.DataPath, $"{node.id}{ext}");

                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }
                File.Copy(path, destination);
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
            File.Delete(GetSelectedNode().GetAudioClip());
            SetSidebarMode(SidebarMode.ItemSelected);
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
