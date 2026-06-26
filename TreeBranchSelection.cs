using MSZDialougeManager.Styling;
using MZDO.Shared;

namespace MSZDialougeManager
{
    public partial class TreeBranchSelection : ThemeableForm
    {
        public int SelectedId { get; private set; }
        public DialogueNodeDTO? SelectedNode { get; private set; }

        public TreeBranchSelection(DialogueNodeDTO[] nextNodes)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            foreach (DialogueNodeDTO node in nextNodes)
            {
                Button button = new Button
                {
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Text = node.dialogueText,
                    Tag = node,
                };
                button.Width = flowLayoutPanel.ClientSize.Width - button.Margin.Horizontal;
                Size textSize = TextRenderer.MeasureText(
                    button.Text,
                    button.Font,
                    new Size(button.Width - 12, int.MaxValue),
                    TextFormatFlags.WordBreak);

                button.Height = textSize.Height + 12; button.Click += Button_Click;
                flowLayoutPanel.Controls.Add(button);
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button button = (Button)sender!;
            SelectedNode = (DialogueNodeDTO)button.Tag!;
            SelectedId = SelectedNode.id;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
