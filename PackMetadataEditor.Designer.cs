namespace MSZDialougeManager
{
    partial class PackMetadataEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackMetadataEditor));
            toolTip = new ToolTip(components);
            packFormatVersionLabel = new Label();
            targetGameVersionBox = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            acceptButton = new Button();
            cancelButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 500;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            // 
            // packFormatVersionLabel
            // 
            packFormatVersionLabel.AutoSize = true;
            packFormatVersionLabel.Location = new Point(173, 3);
            packFormatVersionLabel.Margin = new Padding(3);
            packFormatVersionLabel.Name = "packFormatVersionLabel";
            packFormatVersionLabel.Size = new Size(136, 15);
            packFormatVersionLabel.TabIndex = 1;
            packFormatVersionLabel.Text = "packFormatVersionLabel";
            toolTip.SetToolTip(packFormatVersionLabel, "The pack format is converted to the pack format version of the manager upon opening packs. Due to this it can't be changed manually.");
            // 
            // targetGameVersionBox
            // 
            targetGameVersionBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            targetGameVersionBox.Location = new Point(173, 30);
            targetGameVersionBox.Name = "targetGameVersionBox";
            targetGameVersionBox.Size = new Size(219, 23);
            targetGameVersionBox.TabIndex = 2;
            toolTip.SetToolTip(targetGameVersionBox, resources.GetString("targetGameVersionBox.ToolTip"));
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(packFormatVersionLabel, 1, 0);
            tableLayoutPanel1.Controls.Add(targetGameVersionBox, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.Size = new Size(395, 59);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 3);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(112, 15);
            label1.TabIndex = 0;
            label1.Text = "Pack format version";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 33);
            label2.Margin = new Padding(3, 6, 3, 3);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 0;
            label2.Text = "Target game version";
            // 
            // acceptButton
            // 
            acceptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            acceptButton.Location = new Point(332, 85);
            acceptButton.Name = "acceptButton";
            acceptButton.Size = new Size(75, 23);
            acceptButton.TabIndex = 1;
            acceptButton.Text = "Accept";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += AcceptButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(251, 85);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // PackMetadataEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 120);
            Controls.Add(cancelButton);
            Controls.Add(acceptButton);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PackMetadataEditor";
            Text = "PackMetadataEditor";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Button acceptButton;
        private Button cancelButton;
        private Label packFormatVersionLabel;
        private Label label2;
        private TextBox targetGameVersionBox;
    }
}