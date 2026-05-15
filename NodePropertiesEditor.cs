using MZDO;
using Microsoft.VisualBasic;
using MSZDialougeManager.Styling;

namespace MSZDialougeManager
{
    public partial class NodePropertiesEditor : ThemeableForm
    {
        public DialogueNodeDTO modifiedNode;
        public NodePropertiesEditor(DialogueNodeDTO node)
        {
            InitializeComponent();
            modifiedNode = node;
            textOfNodeBox.Text = node.dialogueText;
            StartPosition = FormStartPosition.CenterParent;

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
                CoolMessageBox.Show("Dialogue text cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            modifiedNode.dialogueText = textOfNodeBox.Text;

            if (speakerDropDown.SelectedItem == null || string.IsNullOrWhiteSpace(speakerDropDown.SelectedItem.ToString()))
            {
                CoolMessageBox.Show("Please select a valid speaker.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            modifiedNode.speakerName = speakerDropDown.SelectedItem.ToString()!;

            if (!float.TryParse(delayBox.Text, out modifiedNode.delay))
            {
                CoolMessageBox.Show("Delay must be a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modifiedNode.dialogueText = textOfNodeBox.Text;
            modifiedNode.speakerName = speakerDropDown.SelectedItem.ToString()!;
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
            string? input = CoolInputBox.Prompt("Enter a custom speaker name:", "Custom Speaker");
            if (input == null) return;
            speakerDropDown.Items.Add(input);
            speakerDropDown.SelectedItem = input;
        }
    }
}
