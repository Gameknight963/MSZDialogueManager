using MSZDialougeManager.Styling;
using MZDO.Shared;

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

            speakerDropDown.Items.AddRange(DialogueEditor.Nodes.Select(x => x.Node.speakerName).Distinct().ToArray());
            speakerDropDown.SelectedItem = node.speakerName;
            delayBox.Text = node.delay.ToString();
            nextNodesIntArrayBox.Text = string.Join(", ", node.nextNodeIds);
            expressionDropDown.Items.AddRange(DialogueEditor.Nodes.Select(x => x.Node.expression).Distinct().ToArray());
            expressionDropDown.SelectedItem = node.expression;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            modifiedNode.dialogueText = textOfNodeBox.Text;
            modifiedNode.speakerName = speakerDropDown.SelectedItem!.ToString()!;
            modifiedNode.expression = expressionDropDown.Text;

            modifiedNode.delay = float.Parse(delayBox.Text);

            List<int> values = new();

            if (!string.IsNullOrWhiteSpace(nextNodesIntArrayBox.Text))
            {
                foreach (string part in nextNodesIntArrayBox.Text.Split(','))
                {
                    if (int.TryParse(part.Trim(), out int value))
                        values.Add(value);
                }
            }

            modifiedNode.nextNodeIds = values.ToArray();

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateInput()
        {
            errorProvider1.Clear();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(textOfNodeBox.Text))
            {
                errorProvider1.SetError(textOfNodeBox, "Dialogue text cannot be empty.");
                ok = false;
            }

            if (speakerDropDown.SelectedItem == null ||
                string.IsNullOrWhiteSpace(speakerDropDown.SelectedItem.ToString()))
            {
                errorProvider1.SetError(speakerDropDown, "Please select a valid speaker.");
                ok = false;
            }

            if (!float.TryParse(delayBox.Text, out _))
            {
                errorProvider1.SetError(delayBox, "Delay must be a valid float.");
                ok = false;
            }

            if (!string.IsNullOrWhiteSpace(nextNodesIntArrayBox.Text))
            {
                foreach (string part in nextNodesIntArrayBox.Text.Split(','))
                {
                    if (!int.TryParse(part.Trim(), out _))
                    {
                        errorProvider1.SetError(nextNodesIntArrayBox, $"Invalid integer: {part}");
                        ok = false;
                        break;
                    }
                }
            }

            return ok;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CustomSpeakerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string? input = CoolInputBox.Prompt("Enter a custom speaker name.", "Custom Speaker");
            if (input == null) return;
            speakerDropDown.Items.Add(input);
            speakerDropDown.SelectedItem = input;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string? input = CoolInputBox.Prompt("Enter a custom expression. Not recommended unless you know what you're doing.", "Custom Expression");
            if (input == null) return;
            expressionDropDown.Items.Add(input);
            expressionDropDown.SelectedItem = input;
        }
    }
}
