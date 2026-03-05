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

namespace MSZDialougeManager
{
    public partial class Form1 : Form
    {
        public static List<DialogueNodeDTO> nodes;

        public Form1()
        {
            InitializeComponent();
            dialogueView.Columns[2].Width -= SystemInformation.VerticalScrollBarWidth;
            SetSidebarMode(SidebarMode.Idle);
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
                case SidebarMode.Idle:
                    jsonButton.Visible = true;
                    templeteButton.Visible = true;
                    textLabel.Visible = false;
                    textHeaderLabel.Visible = false;
                    nextNodesHeader.Visible = false;
                    nextNodesBox.Visible = false;
                    break;
                case SidebarMode.ItemSelected:
                    jsonButton.Visible = false;
                    templeteButton.Visible = false;
                    textLabel.Visible = true;
                    textHeaderLabel.Visible = true;
                    nextNodesHeader.Visible = true;
                    nextNodesBox.Visible = true;
                    nextNodesBox.UpdateNodesBox(GetSelectedNode().nextNodeIds);
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
                    nodes = JsonConvert.DeserializeObject<List<DialogueNodeDTO>>(json);
                    dialogueView.UpdateDialogueView(nodes);
                }
            }
        }

        private DialogueNodeDTO GetSelectedNode()
        {
            int index = dialogueView.SelectedItems[0].Index;
            return nodes[index];
        }

        private void dialogueView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dialogueView.SelectedItems.Count == 0)
            {
                SetSidebarMode(SidebarMode.Idle);
                return;
            }
            SetSidebarMode(SidebarMode.ItemSelected);

            textLabel.Text = GetSelectedNode().dialogueText;
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
    }
}
