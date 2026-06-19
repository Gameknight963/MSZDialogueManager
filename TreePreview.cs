using MSZDialougeManager.Styling;
using MZDO;
using NAudio.Wave;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace MSZDialougeManager
{
    public partial class TreePreview : ThemeableForm
    {
        private IWavePlayer? waveOut;
        private WaveStream? audioStream;

        readonly PrivateFontCollection _pfc = new();
        readonly DialogueTreeDTO _tree;
        readonly int _treeIndex;
        readonly Dictionary<int, ListViewItem> itemsById = new();
        readonly Dictionary<int, DialogueNodeDTO> nodesById = new();
        CancellationTokenSource? playCts;

        bool updating;

        public TreePreview(DialogueTreeDTO tree, int treeIndex)
        {
            InitializeComponent();
            _tree = tree;
            _treeIndex = treeIndex;
            _pfc.AddFontFile("FontRu.ttf");
            dialogueLabel.Font = new Font(_pfc.Families[0], 14, FontStyle.Regular);
            NAudioHelpers.PreloadAll(tree.nodes.Select(n => FilesystemManager.GetNodeAudioPath(treeIndex, n.id)).OfType<string>());
            updating = true;
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
            updating = false;
            UpdateNodeColors();
            dialogueView.Items[0].Selected = true;
            stopButton.Enabled = false;

            switch (ThemeManager.ResolvedTheme)
            {
                case ThemeManager.Theme.Light:
                    Marshal.ThrowExceptionForHR(DwmApi.SetWindowTheme(dialogueView.Handle, "Explorer", null));
                    break;
                case ThemeManager.Theme.Dark:
                    Marshal.ThrowExceptionForHR(DwmApi.SetWindowTheme(dialogueView.Handle, "DarkMode_Explorer", null));
                    break;
                case ThemeManager.Theme.Blur:
                    Marshal.ThrowExceptionForHR(DwmApi.SetWindowTheme(dialogueView.Handle, "DarkMode_Explorer", null));
                    break;
                case ThemeManager.Theme.Acrylic:
                    Marshal.ThrowExceptionForHR(DwmApi.SetWindowTheme(dialogueView.Handle, "DarkMode_Explorer", null));
                    break;
                case ThemeManager.Theme.ExtendFrameDark:
                    Marshal.ThrowExceptionForHR(DwmApi.SetWindowTheme(dialogueView.Handle, "DarkMode_Explorer", null));
                    break;
            }
        }

        protected override void OnThemeWasApplied()
        {
            panel1.BackColor = SystemColors.ActiveCaption;
        }

        private const int typeSpeedMs = 25;        
        private async Task PlayNode(int id)
        {
            playCts?.Cancel();
            playCts?.Dispose();
            playCts = new CancellationTokenSource();

            dialogueView.SelectedItems.Clear();
            updating = true;
            itemsById[id].Selected = true;
            itemsById[id].EnsureVisible();
            updating = false;
            DialogueNodeDTO node = nodesById[id];
            dialogueLabel.Text = string.Empty;
            if (FilesystemManager.TryGetNodeAudioPath(_treeIndex, id, out string? path))
            {
                NAudioHelpers.PlayAudio(path, ref waveOut, ref audioStream);
            }
            foreach (char c in node.dialogueText)
            {
                await Task.Delay(typeSpeedMs, playCts.Token);
                dialogueLabel.Text += c;
            }
            await Task.Delay((int)(node.delay * 1000), playCts.Token);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            playCts?.Cancel();
            base.OnFormClosing(e);
        }

        private async void BeginDialogue()
        {
            stopButton.Enabled = true;
            int index = dialogueView.SelectedItems[0].Index;
            while (true)
            {
                DialogueNodeDTO selectedNode = nodesById[index];
                try
                {
                    await PlayNode(selectedNode.id);
                }
                catch (OperationCanceledException)
                {
                    dialogueLabel.Text = "";
                    NAudioHelpers.StopAudio(ref waveOut, ref audioStream);
                    break;
                }

                if (selectedNode.nextNodeIds.Length == 0) break;
                index = selectedNode.nextNodeIds[0];
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            BeginDialogue();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            playCts?.Cancel();
            stopButton.Enabled = false;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            playCts?.Cancel();
            Close();
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

        private void DialogueView_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selected = dialogueView.SelectedItems.Count > 0;
            playButton.Enabled = selected;
            if (!selected || updating) return;
            playCts?.Cancel();
            BeginDialogue();
        }
    }
}