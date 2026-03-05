using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSZDialougeManager
{
    public partial class Form1 : Form
    {
        public List<DialogueNodeDTO> nodes;

        public Form1()
        {
            InitializeComponent();
            dialogueView.Columns[2].Width -= SystemInformation.VerticalScrollBarWidth;
        }

        private void jsonButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                fd.Multiselect = false;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(fd.FileName);
                    nodes = JsonConvert.DeserializeObject<List<DialogueNodeDTO>>(json);
                    dialogueView.UpdateDialogueView(nodes);
                }
            }
        }
    }
}
