using MSZDialougeManager.Styling;
using MZDO;

namespace MSZDialougeManager
{
    public partial class NodePropertiesEditor : ThemeableForm
    {
        public DialogueNodeDTO modifiedNode;
        public NodePropertiesEditor(DialogueNodeDTO? node = null)
        {
            InitializeComponent();
            modifiedNode = node ?? new DialogueNodeDTO();
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = Ok;
            CancelButton = Cancel;
            TextBoxHelpers.SetPlaceholder(nextNodesIntArrayBox, "1,2,3");

            if (node == null) return;
            textOfNodeBox.Text = node.dialogueText;

            speakerDropDown.Items.AddRange(DialogueEditor.nodes.Select(x => x.Node.speakerName).Distinct().ToArray());

            speakerDropDown.SelectedItem = node.speakerName;

            delayBox.Text = node.delay.ToString();

            nextNodesIntArrayBox.Text = string.Join(", ", node.nextNodeIds);

            expressionBox.Text = node.expression;
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
                CoolMessageBox.Show("Delay must be a valid float.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> values = new();
            if (nextNodesIntArrayBox.Text != "")
            {
                foreach (string part in nextNodesIntArrayBox.Text.Split(','))
                {
                    if (!int.TryParse(part.Trim(), out int value))
                    {
                        CoolMessageBox.Show($"Invalid integer: {part}", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    values.Add(value);
                }
            }

            modifiedNode.nextNodeIds = values.ToArray();
            modifiedNode.dialogueText = textOfNodeBox.Text;
            modifiedNode.speakerName = speakerDropDown.SelectedItem.ToString()!;
            modifiedNode.expression = expressionBox.Text;
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
