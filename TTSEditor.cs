using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSZDialougeManager
{
    public partial class TTSEditor : Form
    {
        HashSet<string> speakers = new HashSet<string>();

        /// <summary>
        /// First item is speaker, second item is voice
        /// </summary>
        public Dictionary<string, string> speakerVoices = new Dictionary<string, string>();

        public TTSEditor(List<DialogueNodeDTO> nodes)
        {
            InitializeComponent();

            foreach (DialogueNodeDTO node in nodes)
            {
                speakers.Add(node.speakerName);
            }

            List<string> voices = TTSManager.GetVoices();
            voices.Add("None");
            voiceColumn.DataSource = voices;

            foreach (string speaker in speakers)
            {
                int rowIndex = dgv.Rows.Add();
                DataGridViewRow row = dgv.Rows[rowIndex];
                row.Cells[0].Value = speaker;
                row.Cells[1].Value = voices[0];
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // skip header

            if (dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                dgv.BeginEdit(true);

                if (dgv.EditingControl is ComboBox combo)
                    combo.DroppedDown = true;
            }
        }

        private async void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // ignore header clicks

            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];

                string speaker = row.Cells[0].Value.ToString();
                string voice = row.Cells[1].Value.ToString();

                if (voice == "None") return;

                string sampleText = $"This will be {speaker}'s voice.";
                _ = Task.Run(async () =>
                {
                    await TTSManager.PlayText(sampleText, voice);
                });
            }
        }

        private void generate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                string speaker = row.Cells[0].Value?.ToString();
                string voice = row.Cells[1].Value?.ToString() == "None" ? null : row.Cells[1].Value?.ToString();
                speakerVoices.Add(speaker, voice); 
            }

            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
