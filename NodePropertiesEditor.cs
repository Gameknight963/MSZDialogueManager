using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MSZDialougeManager
{
    public partial class NodePropertiesEditor : Form
    {
        public DialogueNodeDTO modifiedNode;
        public NodePropertiesEditor(DialogueNodeDTO node)
        {
            modifiedNode = node;
            InitializeComponent();
            textOfNodeBox.Text = node.dialogueText;

            if (!speakerDropDown.Items.Contains(node.speakerName))
            {
                speakerDropDown.Items.Add(node.speakerName);
            }
            speakerDropDown.SelectedItem = node.speakerName;

            delayBox.Text = node.delay.ToString();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textOfNodeBox.Text))
            {
                MessageBox.Show("Dialogue text cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            modifiedNode.dialogueText = textOfNodeBox.Text;

            if (speakerDropDown.SelectedItem == null || string.IsNullOrWhiteSpace(speakerDropDown.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select a valid speaker.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            modifiedNode.speakerName = speakerDropDown.SelectedItem.ToString();

            if (!float.TryParse(delayBox.Text, out modifiedNode.delay))
            {
                MessageBox.Show("Delay must be a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modifiedNode.dialogueText = textOfNodeBox.Text;
            modifiedNode.speakerName = speakerDropDown.SelectedItem.ToString();
            float.TryParse(delayBox.Text, out modifiedNode.delay);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void customSpeakerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string input = Interaction.InputBox("Enter a custom speaker name:", "Custom Speaker");
            if (input == "") return;
            speakerDropDown.Items.Add(input);
            speakerDropDown.SelectedItem = input;
        }
    }
}
