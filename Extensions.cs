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

        public static void UpdateDialogueViewFiltered(this ListView dialogueView, List<DialogueNodeDTO> nodes, string filter)
        {
            dialogueView.BeginUpdate();
            dialogueView.Items.Clear();

            string lowerFilter = filter?.ToLower() ?? "";

            foreach (DialogueNodeDTO n in nodes)
            {
                // kip nodes that don't match the filter
                if (!string.IsNullOrEmpty(lowerFilter) && !n.dialogueText.ToLower().Contains(lowerFilter))
                    continue;

                ListViewItem item = new ListViewItem(n.id.ToString());
                item.SubItems.Add(n.speakerName);
                item.SubItems.Add(n.dialogueText);
                dialogueView.Items.Add(item);
            }

            dialogueView.EndUpdate();
        }


        public static void UpdateNodesBox(this ListBox nodesBox, int[] nodeIndicies)
        {
            nodesBox.BeginUpdate();
            nodesBox.Items.Clear();
            foreach (int index in nodeIndicies)
            {
                DialogueNodeDTO node = Form1.nodes[index];
                NextNodesBoxItem item = new NextNodesBoxItem();
                item.text = $"[{index}] {node.speakerName}: {node.dialogueText}";
                item.node = node;
                nodesBox.Items.Add(item);
            }
            nodesBox.EndUpdate();
        }


        public static bool HasAudioClip(this DialogueNodeDTO node)
        {
            return FilesystemManager.DoesNodeAudioExist(node);
        }

        public static string GetAudioClip(this DialogueNodeDTO node)
        {
            return FilesystemManager.GetNodeAudioClip(node);
        }

        public static void AddAudioClip(this DialogueNodeDTO node, string audioPath)
        {
            FilesystemManager.AddNodeAudio(node, audioPath);
        }

        public static void RemoveAudioClip(this DialogueNodeDTO node)
        {
            FilesystemManager.RemoveNodeAudio(node);
        }
    }
}
