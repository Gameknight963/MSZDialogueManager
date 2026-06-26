namespace MSZDialougeManager
{
    partial class TreePreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreePreview));
            dialogueView = new ListView();
            idColumn = new ColumnHeader();
            speakerColumn = new ColumnHeader();
            textColumn = new ColumnHeader();
            playButton = new Button();
            panel1 = new Panel();
            closeButton = new Button();
            stopButton = new Button();
            dialogueLabel = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dialogueView
            // 
            dialogueView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dialogueView.Columns.AddRange(new ColumnHeader[] { idColumn, speakerColumn, textColumn });
            dialogueView.FullRowSelect = true;
            dialogueView.Location = new Point(0, 0);
            dialogueView.Margin = new Padding(0);
            dialogueView.MultiSelect = false;
            dialogueView.Name = "dialogueView";
            dialogueView.Size = new Size(427, 264);
            dialogueView.TabIndex = 0;
            dialogueView.UseCompatibleStateImageBehavior = false;
            dialogueView.View = View.Details;
            dialogueView.SelectedIndexChanged += DialogueView_SelectedIndexChanged;
            // 
            // idColumn
            // 
            idColumn.Text = "#";
            idColumn.Width = 40;
            // 
            // speakerColumn
            // 
            speakerColumn.Text = "Speaker";
            speakerColumn.Width = 100;
            // 
            // textColumn
            // 
            textColumn.Text = "Text";
            textColumn.Width = 270;
            // 
            // playButton
            // 
            playButton.Location = new Point(12, 11);
            playButton.Name = "playButton";
            playButton.Size = new Size(75, 23);
            playButton.TabIndex = 1;
            playButton.Text = "▶ Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += PlayButton_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(closeButton);
            panel1.Controls.Add(stopButton);
            panel1.Controls.Add(playButton);
            panel1.Location = new Point(0, 346);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(427, 46);
            panel1.TabIndex = 2;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.Location = new Point(340, 11);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 7;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // stopButton
            // 
            stopButton.Font = new Font("Segoe UI Emoji", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stopButton.Location = new Point(93, 11);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(75, 23);
            stopButton.TabIndex = 6;
            stopButton.Text = "■ Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += StopButton_Click;
            // 
            // dialogueLabel
            // 
            dialogueLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dialogueLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dialogueLabel.Location = new Point(3, 267);
            dialogueLabel.Margin = new Padding(3);
            dialogueLabel.Name = "dialogueLabel";
            dialogueLabel.Size = new Size(421, 76);
            dialogueLabel.TabIndex = 3;
            dialogueLabel.Text = "Dialogue text will appear here";
            // 
            // TreePreview
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 392);
            Controls.Add(dialogueLabel);
            Controls.Add(panel1);
            Controls.Add(dialogueView);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TreePreview";
            Text = "Tree Player";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView dialogueView;
        private Button playButton;
        private Panel panel1;
        private Button stopButton;
        private Button closeButton;
        private Label dialogueLabel;
        private ColumnHeader idColumn;
        private ColumnHeader speakerColumn;
        private ColumnHeader textColumn;
    }
}