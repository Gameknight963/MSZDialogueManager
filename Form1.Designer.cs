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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dialogueView = new System.Windows.Forms.ListView();
            this.indexColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.speakerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.templeteButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.audioStopButton = new System.Windows.Forms.Button();
            this.audioPlayButton = new System.Windows.Forms.Button();
            this.removeAudioButton = new System.Windows.Forms.Button();
            this.selectAudioButton = new System.Windows.Forms.Button();
            this.nextNodesBox = new System.Windows.Forms.ListBox();
            this.audioFileHeader = new System.Windows.Forms.Label();
            this.nextNodesHeader = new System.Windows.Forms.Label();
            this.textHeaderLabel = new System.Windows.Forms.Label();
            this.audioFileLabel = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLoadPack = new System.Windows.Forms.ToolStripMenuItem();
            this.initializeTempleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsDialougePackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateWithTTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogueView
            // 
            this.dialogueView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogueView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.indexColumn,
            this.speakerColumn,
            this.textColumn});
            this.dialogueView.FullRowSelect = true;
            this.dialogueView.HideSelection = false;
            this.dialogueView.Location = new System.Drawing.Point(12, 27);
            this.dialogueView.Name = "dialogueView";
            this.dialogueView.Size = new System.Drawing.Size(562, 409);
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
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 439);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(313, 13);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Click \"Load from JSON\" or \"Initialize Templete\" to get started";
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadButton.Location = new System.Drawing.Point(6, 349);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(196, 23);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load from dialouge pack...";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // templeteButton
            // 
            this.templeteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.templeteButton.Location = new System.Drawing.Point(6, 378);
            this.templeteButton.Name = "templeteButton";
            this.templeteButton.Size = new System.Drawing.Size(196, 23);
            this.templeteButton.TabIndex = 0;
            this.templeteButton.Text = "Initialize Templete";
            this.templeteButton.UseVisualStyleBackColor = true;
            this.templeteButton.Click += new System.EventHandler(this.templeteButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.audioStopButton);
            this.panel1.Controls.Add(this.audioPlayButton);
            this.panel1.Controls.Add(this.removeAudioButton);
            this.panel1.Controls.Add(this.selectAudioButton);
            this.panel1.Controls.Add(this.nextNodesBox);
            this.panel1.Controls.Add(this.loadButton);
            this.panel1.Controls.Add(this.audioFileHeader);
            this.panel1.Controls.Add(this.nextNodesHeader);
            this.panel1.Controls.Add(this.textHeaderLabel);
            this.panel1.Controls.Add(this.audioFileLabel);
            this.panel1.Controls.Add(this.textLabel);
            this.panel1.Controls.Add(this.templeteButton);
            this.panel1.Location = new System.Drawing.Point(580, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 409);
            this.panel1.TabIndex = 2;
            // 
            // audioStopButton
            // 
            this.audioStopButton.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioStopButton.Location = new System.Drawing.Point(107, 267);
            this.audioStopButton.Name = "audioStopButton";
            this.audioStopButton.Size = new System.Drawing.Size(95, 23);
            this.audioStopButton.TabIndex = 5;
            this.audioStopButton.Text = "■ Stop";
            this.audioStopButton.UseVisualStyleBackColor = true;
            this.audioStopButton.Click += new System.EventHandler(this.audioStopButton_Click);
            // 
            // audioPlayButton
            // 
            this.audioPlayButton.Location = new System.Drawing.Point(6, 267);
            this.audioPlayButton.Name = "audioPlayButton";
            this.audioPlayButton.Size = new System.Drawing.Size(95, 23);
            this.audioPlayButton.TabIndex = 5;
            this.audioPlayButton.Text = "▶ Play";
            this.audioPlayButton.UseVisualStyleBackColor = true;
            this.audioPlayButton.Click += new System.EventHandler(this.audioPlayButton_Click);
            // 
            // removeAudioButton
            // 
            this.removeAudioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeAudioButton.Location = new System.Drawing.Point(6, 296);
            this.removeAudioButton.Name = "removeAudioButton";
            this.removeAudioButton.Size = new System.Drawing.Size(196, 23);
            this.removeAudioButton.TabIndex = 4;
            this.removeAudioButton.Text = "Remove file";
            this.removeAudioButton.UseVisualStyleBackColor = true;
            this.removeAudioButton.Click += new System.EventHandler(this.removeAudioButton_Click);
            // 
            // selectAudioButton
            // 
            this.selectAudioButton.Location = new System.Drawing.Point(6, 238);
            this.selectAudioButton.Name = "selectAudioButton";
            this.selectAudioButton.Size = new System.Drawing.Size(196, 23);
            this.selectAudioButton.TabIndex = 4;
            this.selectAudioButton.Text = "Select an audio file...";
            this.selectAudioButton.UseVisualStyleBackColor = true;
            this.selectAudioButton.Click += new System.EventHandler(this.selectAudioButton_Click);
            // 
            // nextNodesBox
            // 
            this.nextNodesBox.FormattingEnabled = true;
            this.nextNodesBox.Location = new System.Drawing.Point(6, 138);
            this.nextNodesBox.Name = "nextNodesBox";
            this.nextNodesBox.Size = new System.Drawing.Size(199, 56);
            this.nextNodesBox.TabIndex = 3;
            this.nextNodesBox.DoubleClick += new System.EventHandler(this.nextNodesBox_DoubleClick);
            // 
            // audioFileHeader
            // 
            this.audioFileHeader.AutoSize = true;
            this.audioFileHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioFileHeader.Location = new System.Drawing.Point(3, 200);
            this.audioFileHeader.Margin = new System.Windows.Forms.Padding(3);
            this.audioFileHeader.Name = "audioFileHeader";
            this.audioFileHeader.Size = new System.Drawing.Size(98, 13);
            this.audioFileHeader.TabIndex = 2;
            this.audioFileHeader.Text = "Internal filename:";
            // 
            // nextNodesHeader
            // 
            this.nextNodesHeader.AutoSize = true;
            this.nextNodesHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextNodesHeader.Location = new System.Drawing.Point(3, 119);
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
            // audioFileLabel
            // 
            this.audioFileLabel.Location = new System.Drawing.Point(3, 219);
            this.audioFileLabel.Margin = new System.Windows.Forms.Padding(3);
            this.audioFileLabel.Name = "audioFileLabel";
            this.audioFileLabel.Size = new System.Drawing.Size(115, 13);
            this.audioFileLabel.TabIndex = 1;
            this.audioFileLabel.Text = "None";
            // 
            // textLabel
            // 
            this.textLabel.Location = new System.Drawing.Point(3, 25);
            this.textLabel.Margin = new System.Windows.Forms.Padding(3);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(202, 88);
            this.textLabel.TabIndex = 1;
            this.textLabel.Text = "Text will appear here";
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.Location = new System.Drawing.Point(354, 436);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(220, 22);
            this.searchBox.TabIndex = 3;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.nodeToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLoadPack,
            this.initializeTempleteToolStripMenuItem,
            this.saveAsDialougePackToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripLoadPack
            // 
            this.toolStripLoadPack.Name = "toolStripLoadPack";
            this.toolStripLoadPack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripLoadPack.Size = new System.Drawing.Size(258, 22);
            this.toolStripLoadPack.Text = "Load from dialogue pack...";
            this.toolStripLoadPack.Click += new System.EventHandler(this.toolStripLoadPack_Click);
            // 
            // initializeTempleteToolStripMenuItem
            // 
            this.initializeTempleteToolStripMenuItem.Name = "initializeTempleteToolStripMenuItem";
            this.initializeTempleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.initializeTempleteToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.initializeTempleteToolStripMenuItem.Text = "Initialize templete";
            this.initializeTempleteToolStripMenuItem.Click += new System.EventHandler(this.initializeTempleteToolStripMenuItem_Click);
            // 
            // saveAsDialougePackToolStripMenuItem
            // 
            this.saveAsDialougePackToolStripMenuItem.Name = "saveAsDialougePackToolStripMenuItem";
            this.saveAsDialougePackToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAsDialougePackToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.saveAsDialougePackToolStripMenuItem.Text = "Save as dialouge pack...";
            this.saveAsDialougePackToolStripMenuItem.Click += new System.EventHandler(this.saveAsDialougePackToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateWithTTSToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // generateWithTTSToolStripMenuItem
            // 
            this.generateWithTTSToolStripMenuItem.Name = "generateWithTTSToolStripMenuItem";
            this.generateWithTTSToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.generateWithTTSToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.generateWithTTSToolStripMenuItem.Text = "Generate with TTS";
            this.generateWithTTSToolStripMenuItem.Click += new System.EventHandler(this.generateWithTTSToolStripMenuItem_Click);
            // 
            // nodeToolStripMenuItem
            // 
            this.nodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playAudioToolStripMenuItem,
            this.stopAudioToolStripMenuItem,
            this.assignAudioToolStripMenuItem});
            this.nodeToolStripMenuItem.Name = "nodeToolStripMenuItem";
            this.nodeToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.nodeToolStripMenuItem.Text = "Node";
            // 
            // playAudioToolStripMenuItem
            // 
            this.playAudioToolStripMenuItem.Name = "playAudioToolStripMenuItem";
            this.playAudioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.playAudioToolStripMenuItem.Text = "Play audio";
            this.playAudioToolStripMenuItem.Click += new System.EventHandler(this.playAudioToolStripMenuItem_Click);
            // 
            // stopAudioToolStripMenuItem
            // 
            this.stopAudioToolStripMenuItem.Name = "stopAudioToolStripMenuItem";
            this.stopAudioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopAudioToolStripMenuItem.Text = "Stop audio";
            this.stopAudioToolStripMenuItem.Click += new System.EventHandler(this.stopAudioToolStripMenuItem_Click);
            // 
            // assignAudioToolStripMenuItem
            // 
            this.assignAudioToolStripMenuItem.Name = "assignAudioToolStripMenuItem";
            this.assignAudioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.assignAudioToolStripMenuItem.Text = "Assign audio";
            this.assignAudioToolStripMenuItem.Click += new System.EventHandler(this.assignAudioToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.dialogueView);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Miside Zero Dialogue Manager";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView dialogueView;
        private System.Windows.Forms.ColumnHeader indexColumn;
        private System.Windows.Forms.ColumnHeader speakerColumn;
        private System.Windows.Forms.ColumnHeader textColumn;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button templeteButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Label textHeaderLabel;
        private System.Windows.Forms.ListBox nextNodesBox;
        private System.Windows.Forms.Label nextNodesHeader;
        private System.Windows.Forms.Button selectAudioButton;
        private System.Windows.Forms.Label audioFileHeader;
        private System.Windows.Forms.Label audioFileLabel;
        private System.Windows.Forms.Button audioPlayButton;
        private System.Windows.Forms.Button audioStopButton;
        private System.Windows.Forms.Button removeAudioButton;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripLoadPack;
        private System.Windows.Forms.ToolStripMenuItem initializeTempleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsDialougePackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateWithTTSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playAudioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAudioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignAudioToolStripMenuItem;
    }
}

