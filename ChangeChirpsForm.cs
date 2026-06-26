using MSZDialougeManager.Styling;
using MZDO.Shared;
using NAudio.Wave;
using static MSZDialougeManager.ThemeSwitchers;

namespace MSZDialougeManager
{
    public partial class ChangeChirpsForm : ThemeableForm
    {
        public ChangeChirpsForm(DialoguePack pack)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            SetFormThemeAndStuff(ThemeManager.ActiveTheme, speakerLv);
            playBtn.Enabled = false;
            CancelButton = closeBtn;
            List<string> speakers = pack.trees.SelectMany(tree => tree.nodes).Select(x => x.speakerName).Distinct().ToList();
            speakers.Remove("HELLO"); // not supported currently
            speakerLv.KeyDown += SpeakerLv_KeyPress;
            foreach (string speaker in speakers)
            {
                bool hasAudio = FilesystemManager.DoesSpeakerChirpExist(speaker);
                ListViewItem item = new(hasAudio ? "Yes" : "No");
                item.SubItems.Add(speaker);
                item.Tag = speaker;
                speakerLv.Items.Add(item);
            }
        }

        private void SpeakerLv_KeyPress(object? sender, KeyEventArgs e)
        {
            if (speakerLv.SelectedIndices.Count == 0) return;
            if (e.KeyCode is Keys.Space or Keys.Enter)
            {
                string? chirp = FilesystemManager.GetSpeakerChirp(speakerLv.SelectedItems[0].SubItems[1].Text);
                if (chirp != null) NAudioHelpers.PlayAudio(chirp, ref waveOut, ref audioStream);
            }
        }

        private void SpeakerLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selected = speakerLv.SelectedIndices.Count > 0;
            playBtn.Enabled = assignBtn.Enabled = removeBtn.Enabled = selected;
            if (!selected) return;
            playBtn.Enabled = removeBtn.Enabled = FilesystemManager.DoesSpeakerChirpExist(speakerLv.SelectedItems[0].SubItems[1].Text); ;
        }

        private void AssignBtn_Click(object sender, EventArgs e)
        {
            if (speakerLv.SelectedIndices.Count == 0) return;
            SetScrollHooked(false);
            using OpenFileDialog dialog = new()
            {
                Filter = "Audio Files (*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg)|*.wav;*.mp3;*.wma;*.aac;*.m4a;*.flac;*.ogg|All Files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilesystemManager.SetSpeakerChirp(speakerLv.SelectedItems[0].SubItems[1].Text, dialog.FileName);
                playBtn.Enabled = removeBtn.Enabled = true;
                speakerLv.SelectedItems[0].Text = "Yes";
            }
            SetScrollHooked(ThemeManager.ActiveTheme != ThemeManager.Theme.Light);
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (speakerLv.SelectedIndices.Count == 0) return;
            if (!FilesystemManager.DeleteSpeakerChirp(speakerLv.SelectedItems[0].SubItems[1].Text))
            {
                MessageBox.Show("Failed to delete: file not found", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            playBtn.Enabled = removeBtn.Enabled = false;
            speakerLv.SelectedItems[0].Text = "No";
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private IWavePlayer? waveOut;
        private WaveStream? audioStream;

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            if (speakerLv.SelectedIndices.Count == 0) return;
            string? chirp = FilesystemManager.GetSpeakerChirp(speakerLv.SelectedItems[0].SubItems[1].Text);
            if (chirp != null) NAudioHelpers.PlayAudio(chirp, ref waveOut, ref audioStream);
        }
    }
}
