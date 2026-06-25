namespace MSZDialougeManager
{
    partial class DialogueEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogueEditor));
            dialogueView = new ListView();
            indexColumn = new ColumnHeader();
            speakerColumn = new ColumnHeader();
            textColumn = new ColumnHeader();
            dialogueViewContextMenu = new ContextMenuStrip(components);
            jumpToThisNodeToolStripMenuItem = new ToolStripMenuItem();
            addNodeContextMenuItem = new ToolStripMenuItem();
            deleteThisNodeToolStripMenuItem = new ToolStripMenuItem();
            propertiesContextMenuItem = new ToolStripMenuItem();
            statusLabel = new Label();
            loadButton = new Button();
            templateButton = new Button();
            panel1 = new Panel();
            audioStopButton = new Button();
            audioPlayButton = new Button();
            editPropertiesButton = new Button();
            removeAudioButton = new Button();
            selectAudioButton = new Button();
            nextNodesBox = new ListBox();
            audioFileHeader = new Label();
            nextNodesHeader = new Label();
            textHeaderLabel = new Label();
            audioFileLabel = new Label();
            textLabel = new Label();
            searchBox = new TextBox();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            toolStripLoadPack = new ToolStripMenuItem();
            initializetemplateToolStripMenuItem = new ToolStripMenuItem();
            saveAsDialougePackToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            changeChirpsToolStripMenuItem = new ToolStripMenuItem();
            editProjectMetadataToolStripMenuItem = new ToolStripMenuItem();
            generateWithTTSToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            shellToolStripMenuItem = new ToolStripMenuItem();
            themeToolStripMenuItem = new ToolStripMenuItem();
            lightToolStripMenuItem = new ToolStripMenuItem();
            darkToolStripMenuItem = new ToolStripMenuItem();
            blurToolStripMenuItem = new ToolStripMenuItem();
            acrylicToolStripMenuItem = new ToolStripMenuItem();
            blackToolStripMenuItem = new ToolStripMenuItem();
            groupContextMenu = new ContextMenuStrip(components);
            treePropertiesToolStripMenuItem = new ToolStripMenuItem();
            addNodeToThisTreeToolStripMenuItem = new ToolStripMenuItem();
            previewTreeToolStripMenuItem = new ToolStripMenuItem();
            dialogueViewContextMenu.SuspendLayout();
            panel1.SuspendLayout();
            menuStrip.SuspendLayout();
            groupContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // dialogueView
            // 
            dialogueView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dialogueView.Columns.AddRange(new ColumnHeader[] { indexColumn, speakerColumn, textColumn });
            dialogueView.ContextMenuStrip = dialogueViewContextMenu;
            dialogueView.FullRowSelect = true;
            dialogueView.Location = new Point(12, 27);
            dialogueView.Name = "dialogueView";
            dialogueView.Size = new Size(562, 409);
            dialogueView.TabIndex = 0;
            dialogueView.UseCompatibleStateImageBehavior = false;
            dialogueView.View = View.Details;
            dialogueView.SelectedIndexChanged += DialogueView_SelectedIndexChanged;
            dialogueView.MouseUp += DialogueView_MouseUp;
            // 
            // indexColumn
            // 
            indexColumn.Text = "#";
            indexColumn.Width = 43;
            // 
            // speakerColumn
            // 
            speakerColumn.Text = "Speaker";
            speakerColumn.Width = 100;
            // 
            // textColumn
            // 
            textColumn.Text = "Dialogue Text";
            textColumn.Width = 415;
            // 
            // dialogueViewContextMenu
            // 
            dialogueViewContextMenu.Items.AddRange(new ToolStripItem[] { jumpToThisNodeToolStripMenuItem, addNodeContextMenuItem, deleteThisNodeToolStripMenuItem, propertiesContextMenuItem });
            dialogueViewContextMenu.Name = "dialogueViewContextMenu";
            dialogueViewContextMenu.Size = new Size(208, 92);
            // 
            // jumpToThisNodeToolStripMenuItem
            // 
            jumpToThisNodeToolStripMenuItem.Name = "jumpToThisNodeToolStripMenuItem";
            jumpToThisNodeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.J;
            jumpToThisNodeToolStripMenuItem.Size = new Size(207, 22);
            jumpToThisNodeToolStripMenuItem.Text = "Jump to this node";
            jumpToThisNodeToolStripMenuItem.Click += JumpToThisNodeToolStripMenuItem_Click;
            // 
            // addNodeContextMenuItem
            // 
            addNodeContextMenuItem.Name = "addNodeContextMenuItem";
            addNodeContextMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            addNodeContextMenuItem.Size = new Size(207, 22);
            addNodeContextMenuItem.Text = "Add node here";
            addNodeContextMenuItem.Click += AddNodeContextMenuItem_Click;
            // 
            // deleteThisNodeToolStripMenuItem
            // 
            deleteThisNodeToolStripMenuItem.Name = "deleteThisNodeToolStripMenuItem";
            deleteThisNodeToolStripMenuItem.ShortcutKeys = Keys.Delete;
            deleteThisNodeToolStripMenuItem.Size = new Size(207, 22);
            deleteThisNodeToolStripMenuItem.Text = "Delete this node";
            deleteThisNodeToolStripMenuItem.Click += DeleteThisNodeToolStripMenuItem_Click;
            // 
            // propertiesContextMenuItem
            // 
            propertiesContextMenuItem.Name = "propertiesContextMenuItem";
            propertiesContextMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            propertiesContextMenuItem.Size = new Size(207, 22);
            propertiesContextMenuItem.Text = "Node properties";
            propertiesContextMenuItem.Click += PropertiesContextMenuItem_Click;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(12, 439);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(313, 13);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Click \"Load from JSON\" or \"Initialize template\" to get started";
            // 
            // loadButton
            // 
            loadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            loadButton.Location = new Point(6, 349);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(196, 23);
            loadButton.TabIndex = 0;
            loadButton.Text = "Load from dialouge pack...";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += LoadButton_Click;
            // 
            // templateButton
            // 
            templateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            templateButton.Location = new Point(6, 378);
            templateButton.Name = "templateButton";
            templateButton.Size = new Size(196, 23);
            templateButton.TabIndex = 0;
            templateButton.Text = "Initialize template";
            templateButton.UseVisualStyleBackColor = true;
            templateButton.Click += TemplateButton_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(audioStopButton);
            panel1.Controls.Add(audioPlayButton);
            panel1.Controls.Add(editPropertiesButton);
            panel1.Controls.Add(removeAudioButton);
            panel1.Controls.Add(selectAudioButton);
            panel1.Controls.Add(nextNodesBox);
            panel1.Controls.Add(loadButton);
            panel1.Controls.Add(audioFileHeader);
            panel1.Controls.Add(nextNodesHeader);
            panel1.Controls.Add(textHeaderLabel);
            panel1.Controls.Add(audioFileLabel);
            panel1.Controls.Add(textLabel);
            panel1.Controls.Add(templateButton);
            panel1.Location = new Point(580, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(208, 409);
            panel1.TabIndex = 2;
            // 
            // audioStopButton
            // 
            audioStopButton.Font = new Font("Segoe UI Emoji", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            audioStopButton.Location = new Point(107, 267);
            audioStopButton.Name = "audioStopButton";
            audioStopButton.Size = new Size(95, 23);
            audioStopButton.TabIndex = 5;
            audioStopButton.Text = "■ Stop";
            audioStopButton.UseVisualStyleBackColor = true;
            audioStopButton.Click += AudioStopButton_Click;
            // 
            // audioPlayButton
            // 
            audioPlayButton.Location = new Point(6, 267);
            audioPlayButton.Name = "audioPlayButton";
            audioPlayButton.Size = new Size(95, 23);
            audioPlayButton.TabIndex = 5;
            audioPlayButton.Text = "▶ Play";
            audioPlayButton.UseVisualStyleBackColor = true;
            audioPlayButton.Click += AudioPlayButton_Click;
            // 
            // editPropertiesButton
            // 
            editPropertiesButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editPropertiesButton.Location = new Point(6, 325);
            editPropertiesButton.Name = "editPropertiesButton";
            editPropertiesButton.Size = new Size(196, 23);
            editPropertiesButton.TabIndex = 4;
            editPropertiesButton.Text = "Edit node properties...";
            editPropertiesButton.UseVisualStyleBackColor = true;
            editPropertiesButton.Click += EditPropertiesButton_Click;
            // 
            // removeAudioButton
            // 
            removeAudioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            removeAudioButton.Location = new Point(6, 296);
            removeAudioButton.Name = "removeAudioButton";
            removeAudioButton.Size = new Size(196, 23);
            removeAudioButton.TabIndex = 4;
            removeAudioButton.Text = "Remove file";
            removeAudioButton.UseVisualStyleBackColor = true;
            removeAudioButton.Click += RemoveAudioButton_Click;
            // 
            // selectAudioButton
            // 
            selectAudioButton.Location = new Point(6, 238);
            selectAudioButton.Name = "selectAudioButton";
            selectAudioButton.Size = new Size(196, 23);
            selectAudioButton.TabIndex = 4;
            selectAudioButton.Text = "Select an audio file...";
            selectAudioButton.UseVisualStyleBackColor = true;
            selectAudioButton.Click += SelectAudioButton_Click;
            // 
            // nextNodesBox
            // 
            nextNodesBox.FormattingEnabled = true;
            nextNodesBox.ItemHeight = 13;
            nextNodesBox.Location = new Point(6, 138);
            nextNodesBox.Name = "nextNodesBox";
            nextNodesBox.Size = new Size(199, 56);
            nextNodesBox.TabIndex = 3;
            nextNodesBox.DoubleClick += NextNodesBox_DoubleClick;
            // 
            // audioFileHeader
            // 
            audioFileHeader.AutoSize = true;
            audioFileHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            audioFileHeader.Location = new Point(3, 200);
            audioFileHeader.Margin = new Padding(3);
            audioFileHeader.Name = "audioFileHeader";
            audioFileHeader.Size = new Size(98, 13);
            audioFileHeader.TabIndex = 2;
            audioFileHeader.Text = "Internal filename:";
            // 
            // nextNodesHeader
            // 
            nextNodesHeader.AutoSize = true;
            nextNodesHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nextNodesHeader.Location = new Point(3, 119);
            nextNodesHeader.Margin = new Padding(3);
            nextNodesHeader.Name = "nextNodesHeader";
            nextNodesHeader.Size = new Size(70, 13);
            nextNodesHeader.TabIndex = 2;
            nextNodesHeader.Text = "Next nodes:";
            // 
            // textHeaderLabel
            // 
            textHeaderLabel.AutoSize = true;
            textHeaderLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textHeaderLabel.Location = new Point(3, 6);
            textHeaderLabel.Margin = new Padding(3);
            textHeaderLabel.Name = "textHeaderLabel";
            textHeaderLabel.Size = new Size(102, 13);
            textHeaderLabel.TabIndex = 2;
            textHeaderLabel.Text = "Selected item text:";
            // 
            // audioFileLabel
            // 
            audioFileLabel.Location = new Point(3, 219);
            audioFileLabel.Margin = new Padding(3);
            audioFileLabel.Name = "audioFileLabel";
            audioFileLabel.Size = new Size(115, 13);
            audioFileLabel.TabIndex = 1;
            audioFileLabel.Text = "None";
            // 
            // textLabel
            // 
            textLabel.Location = new Point(3, 25);
            textLabel.Margin = new Padding(3);
            textLabel.Name = "textLabel";
            textLabel.Size = new Size(202, 88);
            textLabel.TabIndex = 1;
            textLabel.Text = "Text will appear here";
            // 
            // searchBox
            // 
            searchBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            searchBox.Location = new Point(354, 436);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(220, 22);
            searchBox.TabIndex = 3;
            searchBox.TextChanged += SearchBox_TextChanged;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, settingsToolStripMenuItem, themeToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 4;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripLoadPack, initializetemplateToolStripMenuItem, saveAsDialougePackToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // toolStripLoadPack
            // 
            toolStripLoadPack.Name = "toolStripLoadPack";
            toolStripLoadPack.ShortcutKeys = Keys.Control | Keys.O;
            toolStripLoadPack.Size = new Size(258, 22);
            toolStripLoadPack.Text = "Load from dialogue pack...";
            toolStripLoadPack.Click += ToolStripLoadPack_Click;
            // 
            // initializetemplateToolStripMenuItem
            // 
            initializetemplateToolStripMenuItem.Name = "initializetemplateToolStripMenuItem";
            initializetemplateToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.T;
            initializetemplateToolStripMenuItem.Size = new Size(258, 22);
            initializetemplateToolStripMenuItem.Text = "Initialize template";
            initializetemplateToolStripMenuItem.Click += InitializetemplateToolStripMenuItem_Click;
            // 
            // saveAsDialougePackToolStripMenuItem
            // 
            saveAsDialougePackToolStripMenuItem.Name = "saveAsDialougePackToolStripMenuItem";
            saveAsDialougePackToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveAsDialougePackToolStripMenuItem.Size = new Size(258, 22);
            saveAsDialougePackToolStripMenuItem.Text = "Save as dialouge pack...";
            saveAsDialougePackToolStripMenuItem.Click += SaveAsDialougePackToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeChirpsToolStripMenuItem, editProjectMetadataToolStripMenuItem, generateWithTTSToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // changeChirpsToolStripMenuItem
            // 
            changeChirpsToolStripMenuItem.Name = "changeChirpsToolStripMenuItem";
            changeChirpsToolStripMenuItem.Size = new Size(216, 22);
            changeChirpsToolStripMenuItem.Text = "Change speaker chirps";
            changeChirpsToolStripMenuItem.Click += ChangeChirpsToolStripMenuItem_Click;
            // 
            // editProjectMetadataToolStripMenuItem
            // 
            editProjectMetadataToolStripMenuItem.Name = "editProjectMetadataToolStripMenuItem";
            editProjectMetadataToolStripMenuItem.Size = new Size(216, 22);
            editProjectMetadataToolStripMenuItem.Text = "Edit project metadata";
            editProjectMetadataToolStripMenuItem.Click += EditProjectMetadataToolStripMenuItem_Click;
            // 
            // generateWithTTSToolStripMenuItem
            // 
            generateWithTTSToolStripMenuItem.Name = "generateWithTTSToolStripMenuItem";
            generateWithTTSToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            generateWithTTSToolStripMenuItem.Size = new Size(216, 22);
            generateWithTTSToolStripMenuItem.Text = "Generate with SAPI";
            generateWithTTSToolStripMenuItem.Click += GenerateWithTTSToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { shellToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // shellToolStripMenuItem
            // 
            shellToolStripMenuItem.CheckOnClick = true;
            shellToolStripMenuItem.Name = "shellToolStripMenuItem";
            shellToolStripMenuItem.Size = new Size(180, 22);
            shellToolStripMenuItem.Text = "File association";
            shellToolStripMenuItem.Click += ShellToolStripMenuItem_Click;
            // 
            // themeToolStripMenuItem
            // 
            themeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lightToolStripMenuItem, darkToolStripMenuItem, blurToolStripMenuItem, acrylicToolStripMenuItem, blackToolStripMenuItem });
            themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            themeToolStripMenuItem.Size = new Size(55, 20);
            themeToolStripMenuItem.Text = "Theme";
            // 
            // lightToolStripMenuItem
            // 
            lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            lightToolStripMenuItem.Size = new Size(180, 22);
            lightToolStripMenuItem.Text = "Light";
            lightToolStripMenuItem.Click += LightToolStripMenuItem_Click;
            // 
            // darkToolStripMenuItem
            // 
            darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            darkToolStripMenuItem.Size = new Size(180, 22);
            darkToolStripMenuItem.Text = "Dark";
            darkToolStripMenuItem.Click += DarkToolStripMenuItem_Click;
            // 
            // blurToolStripMenuItem
            // 
            blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            blurToolStripMenuItem.Size = new Size(180, 22);
            blurToolStripMenuItem.Text = "Blur";
            blurToolStripMenuItem.Click += BlurToolStripMenuItem_Click;
            // 
            // acrylicToolStripMenuItem
            // 
            acrylicToolStripMenuItem.Name = "acrylicToolStripMenuItem";
            acrylicToolStripMenuItem.Size = new Size(180, 22);
            acrylicToolStripMenuItem.Text = "Acrylic";
            acrylicToolStripMenuItem.Click += AcrylicToolStripMenuItem_Click;
            // 
            // blackToolStripMenuItem
            // 
            blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            blackToolStripMenuItem.Size = new Size(180, 22);
            blackToolStripMenuItem.Text = "Black";
            blackToolStripMenuItem.Click += BlackToolStripMenuItem_Click;
            // 
            // groupContextMenu
            // 
            groupContextMenu.Items.AddRange(new ToolStripItem[] { treePropertiesToolStripMenuItem, addNodeToThisTreeToolStripMenuItem, previewTreeToolStripMenuItem });
            groupContextMenu.Name = "groupContextMenu";
            groupContextMenu.Size = new Size(186, 70);
            // 
            // treePropertiesToolStripMenuItem
            // 
            treePropertiesToolStripMenuItem.Name = "treePropertiesToolStripMenuItem";
            treePropertiesToolStripMenuItem.Size = new Size(185, 22);
            treePropertiesToolStripMenuItem.Text = "Tree properties";
            treePropertiesToolStripMenuItem.Click += TreePropertiesToolStripMenuItem_Click;
            // 
            // addNodeToThisTreeToolStripMenuItem
            // 
            addNodeToThisTreeToolStripMenuItem.Name = "addNodeToThisTreeToolStripMenuItem";
            addNodeToThisTreeToolStripMenuItem.Size = new Size(185, 22);
            addNodeToThisTreeToolStripMenuItem.Text = "Add node to this tree";
            addNodeToThisTreeToolStripMenuItem.Click += AddNodeToThisTreeToolStripMenuItem_Click;
            // 
            // previewTreeToolStripMenuItem
            // 
            previewTreeToolStripMenuItem.Name = "previewTreeToolStripMenuItem";
            previewTreeToolStripMenuItem.Size = new Size(185, 22);
            previewTreeToolStripMenuItem.Text = "Preview tree";
            previewTreeToolStripMenuItem.Click += PreviewTreeToolStripMenuItem_Click;
            // 
            // DialogueEditor
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 461);
            Controls.Add(searchBox);
            Controls.Add(panel1);
            Controls.Add(statusLabel);
            Controls.Add(dialogueView);
            Controls.Add(menuStrip);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "DialogueEditor";
            Text = "Miside Zero Dialogue Manager";
            dialogueViewContextMenu.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            groupContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView dialogueView;
        private System.Windows.Forms.ColumnHeader indexColumn;
        private System.Windows.Forms.ColumnHeader speakerColumn;
        private System.Windows.Forms.ColumnHeader textColumn;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button templateButton;
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
        private System.Windows.Forms.ToolStripMenuItem initializetemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsDialougePackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateWithTTSToolStripMenuItem;
        private System.Windows.Forms.Button editPropertiesButton;
        private System.Windows.Forms.ContextMenuStrip dialogueViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem propertiesContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNodeContextMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem shellToolStripMenuItem;
        private ToolStripMenuItem themeToolStripMenuItem;
        private ToolStripMenuItem lightToolStripMenuItem;
        private ToolStripMenuItem darkToolStripMenuItem;
        private ToolStripMenuItem blurToolStripMenuItem;
        private ToolStripMenuItem acrylicToolStripMenuItem;
        private ToolStripMenuItem blackToolStripMenuItem;
        private ToolStripMenuItem deleteThisNodeToolStripMenuItem;
        private ContextMenuStrip groupContextMenu;
        private ToolStripMenuItem treePropertiesToolStripMenuItem;
        private ToolStripMenuItem addNodeToThisTreeToolStripMenuItem;
        private ToolStripMenuItem jumpToThisNodeToolStripMenuItem;
        private ToolStripMenuItem previewTreeToolStripMenuItem;
        private ToolStripMenuItem changeChirpsToolStripMenuItem;
        private ToolStripMenuItem editProjectMetadataToolStripMenuItem;
    }
}

