using MSZDialougeManager.Styling;
using MZDO;
using System.Drawing.Text;

namespace MSZDialougeManager
{
    public partial class TreePreview : ThemeableForm
    {
        readonly PrivateFontCollection _pfc = new();
        readonly DialogueTreeDTO _tree;
        readonly Dictionary<int, ListViewItem> itemsById = new();
        readonly Dictionary<int, DialogueNodeDTO> nodesById = new();
        public TreePreview(DialogueTreeDTO tree)
        {
            InitializeComponent();
            _tree = tree;
            _pfc.AddFontFile("FontRu.ttf");
            dialogueLabel.Font = new Font(_pfc.Families[0], 14, FontStyle.Regular);
            foreach (DialogueNodeDTO node in tree.nodes)
            {
                ListViewItem lvi = new(node.id.ToString());
                lvi.SubItems.Add(node.speakerName);
                lvi.SubItems.Add(node.dialogueText);
                lvi.Tag = node;
                dialogueView.Items.Add(lvi);

                itemsById.Add(node.id, lvi);
                nodesById.Add(node.id, node);
            }
            UpdateNodeColors();
        }

        protected override void OnThemeWasApplied()
        {
            panel1.BackColor = SystemColors.ActiveCaption;
        }

        private const int typeSpeedMs = 25;
        private async Task PlayNode(int id)
        {
            dialogueView.SelectedItems.Clear();
            itemsById[id].Selected = true;
            itemsById[id].EnsureVisible();
            DialogueNodeDTO node = nodesById[id];
            dialogueLabel.Text = string.Empty;
            foreach (char c in node.dialogueText)
            {
                await Task.Delay(typeSpeedMs);
                dialogueLabel.Text += c;
            }
            await Task.Delay((int)(node.delay * 1000));
        }

        private async void PlayButton_Click(object sender, EventArgs e)
        {
            int index = dialogueView.SelectedItems[0].Index;
            while (true)
            {
                DialogueNodeDTO selectedNode = nodesById[index];
                await PlayNode(selectedNode.id);
                if (selectedNode.nextNodeIds.Length == 0) break;
                index = selectedNode.nextNodeIds[0];
            }
        }

        void UpdateNodeColors()
        {
            HashSet<int> allIds = new(_tree.nodes.Select(n => n.id));
            HashSet<int> reachable = DialogueHelpers.GetReachableNodes(_tree);

            foreach (ListViewItem item in dialogueView.Items)
            {
                DialogueNodeDTO node = (DialogueNodeDTO)item.Tag!;

                bool isStartNode = _tree.startNodeIds.Contains(node.id);
                bool isTerminal = node.nextNodeIds == null || node.nextNodeIds.Length == 0;
                bool hasBrokenRefs = node.nextNodeIds != null && node.nextNodeIds.Any(id => !allIds.Contains(id));
                bool isReachable = reachable.Contains(node.id);

                item.ForeColor = hasBrokenRefs ? Color.Orange
                    : isStartNode ? Color.Green
                    : !isReachable ? Color.Red
                    : isTerminal ? Color.Blue
                    : dialogueView.ForeColor;
            }
        }
    }
}