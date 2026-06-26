using MSZDialougeManager.Styling;
using MZDO.Shared;
using NAudio.Wave;
using System.Diagnostics;
using System.Drawing.Text;
using System.Threading.Tasks;
using static MSZDialougeManager.ThemeSwitchers;

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
            _pfc.AddFontFile(Path.Combine(FilesystemManager.BaseDir, "FontRu.ttf"));
            dialogueLabel.Font = new Font(_pfc.Families[0], 14, FontStyle.Regular);
            StartPosition = FormStartPosition.CenterParent;
            List<string> speakers = tree.nodes.Select(x => x.speakerName).Distinct().ToList();
            List<string> paths = tree.nodes.Select(n => FilesystemManager.GetNodeAudioPath(treeIndex, n.id)).OfType<string>().ToList();
            paths.AddRange(speakers);
            NAudioHelpers.PreloadAll(paths);
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
            SetFormThemeAndStuff(ThemeManager.ActiveTheme, dialogueView);
            _ = BeginDialogue();
        }

        const int WM_SIZE = 0x0005;

        const int SIZE_MINIMIZED = 1;
        const int SIZE_MAXIMIZED = 2;

        const int WM_EXITSIZEMOVE = 0x0232;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_EXITSIZEMOVE || (m.Msg == WM_SIZE && m.WParam.ToInt32() is SIZE_MINIMIZED or SIZE_MAXIMIZED)) ResizeTextColumn();
            base.WndProc(ref m);
        }

        private const int MinTextColumnWidth = 50;

        private void ResizeTextColumn()
        {
            int totalOtherColumns = 0;
            for (int i = 0; i < dialogueView.Columns.Count - 1; i++)
                totalOtherColumns += dialogueView.Columns[i].Width;

            int remaining = dialogueView.ClientSize.Width - totalOtherColumns;

            bool needsScroll = remaining < MinTextColumnWidth;
            if (needsScroll) remaining = MinTextColumnWidth;

            dialogueView.Columns[2].Width = remaining;

            ScrollbarHelper.Set(dialogueView, ScrollbarHelper.Scrollbar.Horz, needsScroll);
        }

        protected override void OnThemeWasApplied(ThemeManager.Theme resolvedTheme)
        {
            if (resolvedTheme == ThemeManager.Theme.Light)
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
            bool hasAudio = false;
            if (FilesystemManager.TryGetNodeAudioPath(_treeIndex, id, out string? path))
            {
                hasAudio = true;
                NAudioHelpers.PlayAudio(path, ref waveOut, ref audioStream);
            }
            
            bool hasChirp = FilesystemManager.TryGetSpeakerChirp(node.speakerName, out string? path2);

            CancellationToken token = playCts.Token;

            async Task TypingLoop()
            {
                Stopwatch sw = Stopwatch.StartNew();
                long nextCharMs = typeSpeedMs;
                foreach (char c in node.dialogueText)
                {
                    long remaining = nextCharMs - sw.ElapsedMilliseconds;
                    if (remaining > 0)
                        await Task.Delay((int)remaining, token);
                    token.ThrowIfCancellationRequested();
                    dialogueLabel.Text += c;
                    nextCharMs += typeSpeedMs;
                }
            }
            Task typingTask = TypingLoop();

            int chirpIntervalMs = (int)((_tree.chirpTime ?? 0.115f) * 1000);
            async Task ChirpLoop()
            {
                if (!hasChirp || hasAudio) return;
                Stopwatch sw = Stopwatch.StartNew();
                long nextChirpMs = chirpIntervalMs;
                int totalDuration = typeSpeedMs * node.dialogueText.Length;
                while (sw.ElapsedMilliseconds < totalDuration && !token.IsCancellationRequested)
                {
                    long remaining = nextChirpMs - sw.ElapsedMilliseconds;
                    if (remaining > 0)
                        await Task.Delay((int)remaining, token);
                    IWavePlayer? chirpWaveOut = null;
                    WaveStream? chirpAudioStream = null;
                    NAudioHelpers.PlayAudio(path2!, ref chirpWaveOut, ref chirpAudioStream);
                    chirpWaveOut!.PlaybackStopped += (_, _) =>
                    {
                        NAudioHelpers.StopAudio(ref chirpWaveOut, ref chirpAudioStream);
                    };
                    nextChirpMs += chirpIntervalMs;
                }
            }
            Task chirpTask = ChirpLoop();

            await Task.WhenAll(typingTask, chirpTask);
            await Task.Delay((int)(node.delay * 1000), token);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            playCts?.Cancel();
            base.OnFormClosing(e);
        }


        private async Task BeginDialogue()
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

                if (selectedNode.nextNodeIds.Length == 0)
                {
                    stopButton.Enabled = false;
                    break;
                }
                if (selectedNode.nextNodeIds.Length > 1)
                {
                    using TreeBranchSelection selection = new(selectedNode.nextNodeIds.Select(x => nodesById[x]).ToArray());
                    if (selection.ShowDialog() == DialogResult.Cancel) return;
                    index = selection.SelectedId;
                }
                else
                    index = selectedNode.nextNodeIds[0];
            }
        }

        private async void PlayButton_Click(object sender, EventArgs e)
        {
            await BeginDialogue();
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

                Color foreColor = ResolvedTheme == ThemeManager.Theme.Light ? SystemColors.ControlText : Color.White;
                item.ForeColor = hasBrokenRefs ? Color.Orange
                    : isStartNode ? Color.Green
                    : !isReachable ? Color.Red
                    : isTerminal ? Color.Blue
                    : foreColor;
            }
        }

        private Task dialogueTask = Task.CompletedTask;

        private void DialogueView_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selected = dialogueView.SelectedItems.Count > 0;
            playButton.Enabled = selected;
            if (!selected || updating || !stopButton.Enabled) return;
            playCts?.Cancel();
            dialogueTask = dialogueTask.ContinueWith(_ => BeginDialogue(),
                TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
        }
    }
}