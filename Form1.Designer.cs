namespace MSZDialougeManager
{
    partial class Form1
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
            this.dialogueView = new System.Windows.Forms.ListView();
            this.indexColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.speakerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusLabel = new System.Windows.Forms.Label();
            this.jsonButton = new System.Windows.Forms.Button();
            this.templeteButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nextNodesBox = new System.Windows.Forms.ListBox();
            this.nextNodesHeader = new System.Windows.Forms.Label();
            this.textHeaderLabel = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.Label();
            this.audioFileHeader = new System.Windows.Forms.Label();
            this.audioFileLabel = new System.Windows.Forms.Label();
            this.selectAudioButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogueView
            // 
            this.dialogueView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.indexColumn,
            this.speakerColumn,
            this.textColumn});
            this.dialogueView.FullRowSelect = true;
            this.dialogueView.HideSelection = false;
            this.dialogueView.Location = new System.Drawing.Point(12, 12);
            this.dialogueView.Name = "dialogueView";
            this.dialogueView.Size = new System.Drawing.Size(562, 413);
            this.dialogueView.TabIndex = 0;
            this.dialogueView.UseCompatibleStateImageBehavior = false;
            this.dialogueView.View = System.Windows.Forms.View.Details;
            this.dialogueView.SelectedIndexChanged += new System.EventHandler(this.dialogueView_SelectedIndexChanged);
            // 
            // indexColumn
            // 
            this.indexColumn.Text = "#";
            this.indexColumn.Width = 43;
            // 
            // speakerColumn
            // 
            this.speakerColumn.Text = "Speaker";
            this.speakerColumn.Width = 100;
            // 
            // textColumn
            // 
            this.textColumn.Text = "Dialogue Text";
            this.textColumn.Width = 415;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 428);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(313, 13);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Click \"Load from JSON\" or \"Initialize Templete\" to get started";
            // 
            // jsonButton
            // 
            this.jsonButton.Location = new System.Drawing.Point(3, 354);
            this.jsonButton.Name = "jsonButton";
            this.jsonButton.Size = new System.Drawing.Size(202, 23);
            this.jsonButton.TabIndex = 0;
            this.jsonButton.Text = "Load from JSON...";
            this.jsonButton.UseVisualStyleBackColor = true;
            this.jsonButton.Click += new System.EventHandler(this.jsonButton_Click);
            // 
            // templeteButton
            // 
            this.templeteButton.Location = new System.Drawing.Point(3, 383);
            this.templeteButton.Name = "templeteButton";
            this.templeteButton.Size = new System.Drawing.Size(202, 23);
            this.templeteButton.TabIndex = 0;
            this.templeteButton.Text = "Initialize Templete";
            this.templeteButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectAudioButton);
            this.panel1.Controls.Add(this.nextNodesBox);
            this.panel1.Controls.Add(this.audioFileHeader);
            this.panel1.Controls.Add(this.nextNodesHeader);
            this.panel1.Controls.Add(this.textHeaderLabel);
            this.panel1.Controls.Add(this.audioFileLabel);
            this.panel1.Controls.Add(this.textLabel);
            this.panel1.Controls.Add(this.jsonButton);
            this.panel1.Controls.Add(this.templeteButton);
            this.panel1.Location = new System.Drawing.Point(580, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 413);
            this.panel1.TabIndex = 2;
            // 
            // nextNodesBox
            // 
            this.nextNodesBox.FormattingEnabled = true;
            this.nextNodesBox.Location = new System.Drawing.Point(6, 149);
            this.nextNodesBox.Name = "nextNodesBox";
            this.nextNodesBox.Size = new System.Drawing.Size(199, 56);
            this.nextNodesBox.TabIndex = 3;
            this.nextNodesBox.DoubleClick += new System.EventHandler(this.nextNodesBox_DoubleClick);
            // 
            // nextNodesHeader
            // 
            this.nextNodesHeader.AutoSize = true;
            this.nextNodesHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextNodesHeader.Location = new System.Drawing.Point(3, 130);
            this.nextNodesHeader.Margin = new System.Windows.Forms.Padding(3);
            this.nextNodesHeader.Name = "nextNodesHeader";
            this.nextNodesHeader.Size = new System.Drawing.Size(70, 13);
            this.nextNodesHeader.TabIndex = 2;
            this.nextNodesHeader.Text = "Next nodes:";
            // 
            // textHeaderLabel
            // 
            this.textHeaderLabel.AutoSize = true;
            this.textHeaderLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHeaderLabel.Location = new System.Drawing.Point(3, 6);
            this.textHeaderLabel.Margin = new System.Windows.Forms.Padding(3);
            this.textHeaderLabel.Name = "textHeaderLabel";
            this.textHeaderLabel.Size = new System.Drawing.Size(102, 13);
            this.textHeaderLabel.TabIndex = 2;
            this.textHeaderLabel.Text = "Selected item text:";
            // 
            // textLabel
            // 
            this.textLabel.Location = new System.Drawing.Point(3, 25);
            this.textLabel.Margin = new System.Windows.Forms.Padding(3);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(202, 99);
            this.textLabel.TabIndex = 1;
            this.textLabel.Text = "Text will appear here";
            // 
            // audioFileHeader
            // 
            this.audioFileHeader.AutoSize = true;
            this.audioFileHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioFileHeader.Location = new System.Drawing.Point(3, 211);
            this.audioFileHeader.Margin = new System.Windows.Forms.Padding(3);
            this.audioFileHeader.Name = "audioFileHeader";
            this.audioFileHeader.Size = new System.Drawing.Size(105, 13);
            this.audioFileHeader.TabIndex = 2;
            this.audioFileHeader.Text = "Selected audio file:";
            // 
            // audioFileLabel
            // 
            this.audioFileLabel.Location = new System.Drawing.Point(3, 227);
            this.audioFileLabel.Margin = new System.Windows.Forms.Padding(3);
            this.audioFileLabel.Name = "audioFileLabel";
            this.audioFileLabel.Size = new System.Drawing.Size(115, 13);
            this.audioFileLabel.TabIndex = 1;
            this.audioFileLabel.Text = "None";
            // 
            // selectAudioButton
            // 
            this.selectAudioButton.Location = new System.Drawing.Point(6, 246);
            this.selectAudioButton.Name = "selectAudioButton";
            this.selectAudioButton.Size = new System.Drawing.Size(199, 23);
            this.selectAudioButton.TabIndex = 4;
            this.selectAudioButton.Text = "Select an audio file...";
            this.selectAudioButton.UseVisualStyleBackColor = true;
            this.selectAudioButton.Click += new System.EventHandler(this.selectAudioButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.dialogueView);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView dialogueView;
        private System.Windows.Forms.ColumnHeader indexColumn;
        private System.Windows.Forms.ColumnHeader speakerColumn;
        private System.Windows.Forms.ColumnHeader textColumn;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button jsonButton;
        private System.Windows.Forms.Button templeteButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Label textHeaderLabel;
        private System.Windows.Forms.ListBox nextNodesBox;
        private System.Windows.Forms.Label nextNodesHeader;
        private System.Windows.Forms.Button selectAudioButton;
        private System.Windows.Forms.Label audioFileHeader;
        private System.Windows.Forms.Label audioFileLabel;
    }
}

