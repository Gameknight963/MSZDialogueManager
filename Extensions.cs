using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MSZDialougeManager
{
    public static class Extensions
    {
        public static void UpdateDialogueView(this ListView dialogueView, List<DialogueNodeDTO> nodes)
        {
            dialogueView.Items.Clear();
            foreach (DialogueNodeDTO n in nodes)
            {
                ListViewItem item = new ListViewItem(n.id.ToString());
                item.SubItems.Add(n.speakerName);
                item.SubItems.Add(n.dialogueText);
                dialogueView.Items.Add(item);
            }
        }
    }
}
