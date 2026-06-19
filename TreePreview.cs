using MSZDialougeManager.Styling;
using MZDO;
using System.Drawing.Text;

namespace MSZDialougeManager
{
    public partial class TreePreview : ThemeableForm
    {
        readonly PrivateFontCollection pfc = new();
        public TreePreview(DialogueTreeDTO tree)
        {
            InitializeComponent();
            pfc.AddFontFile("FontRu.ttf");
            dialogueLabel.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            foreach (DialogueNodeDTO node in tree.nodes)
            {
                ListViewItem lvi = new(node.id.ToString());
                lvi.SubItems.Add(node.speakerName);
                lvi.SubItems.Add(node.dialogueText);
                lvi.Tag = node;

                dialogueView.Items.Add(lvi);
            }
        }


        protected override void OnThemeWasApplied()
        {
            panel1.BackColor = SystemColors.ActiveCaption;
        }
    }
}
 