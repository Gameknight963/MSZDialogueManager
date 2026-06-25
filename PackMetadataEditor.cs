using MSZDialougeManager.Properties;
using MSZDialougeManager.Styling;
using MZDO.Shared;

namespace MSZDialougeManager
{
    public partial class PackMetadataEditor : ThemeableForm
    {
        public readonly DialoguePack ResultPack;

        public PackMetadataEditor(DialoguePack pack)
        {
            InitializeComponent();
            ResultPack = pack;
            AcceptButton = acceptButton;
            CancelButton = cancelButton;
            packFormatVersionLabel.Text = pack.PackFormat.ToString();
            targetGameVersionBox.Text = pack.TargetGameVersion;
            StartPosition = FormStartPosition.CenterParent;
            CancelButton.DialogResult = DialogResult.Cancel;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            ResultPack.TargetGameVersion = targetGameVersionBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
