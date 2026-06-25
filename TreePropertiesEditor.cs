using MSZDialougeManager.Styling;
using MZDO.Shared;

namespace MSZDialougeManager
{
    public partial class TreePropertiesEditor : ThemeableForm
    {
        public DialogueTreeDTO? ResultTree;

        public TreePropertiesEditor(DialogueTreeDTO? tree = null)
        {
            InitializeComponent();
            AcceptButton = okButton;
            CancelButton = cancelButton;
            StartPosition = FormStartPosition.CenterParent;
            if (tree == null) return;
            startNodesBox.Text = string.Join(", ", tree.startNodeIds);
            treeNameBox.Text = tree.name;
            chirpTimeBox.Text = tree.chirpTime.ToString();
            initialDelayBox.Text = tree.initialDelay.ToString();
            exitDelayBox.Text = tree.exitDelay.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ResultTree = new DialogueTreeDTO();
            List<int> values = new();
            if (startNodesBox.Text != "")
            {
                foreach (string part in startNodesBox.Text.Split(','))
                {
                    if (!int.TryParse(part.Trim(), out int value))
                    {
                        CoolMessageBox.Show($"Invalid integer: {part}", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    values.Add(value);
                }
            }
            ResultTree.startNodeIds = values;

            ResultTree.name = string.IsNullOrWhiteSpace(treeNameBox.Text) ? null : treeNameBox.Text;

            if (string.IsNullOrWhiteSpace(chirpTimeBox.Text))
                ResultTree.chirpTime = null;
            else
            {
                if (!float.TryParse(chirpTimeBox.Text, out float value))
                {
                    CoolMessageBox.Show($"Invalid float: {chirpTimeBox.Text}", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ResultTree.chirpTime = value;
            }

            if (string.IsNullOrWhiteSpace(initialDelayBox.Text))
                ResultTree.chirpTime = null;
            else
            {
                if (!float.TryParse(initialDelayBox.Text, out float value))
                {
                    CoolMessageBox.Show($"Invalid float: {initialDelayBox.Text}", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ResultTree.initialDelay = value;
            }

            if (string.IsNullOrWhiteSpace(exitDelayBox.Text))
                ResultTree.chirpTime = null;
            else
            {
                if (!float.TryParse(exitDelayBox.Text, out float value))
                {
                    CoolMessageBox.Show($"Invalid float: {exitDelayBox.Text}", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ResultTree.exitDelay = value;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
