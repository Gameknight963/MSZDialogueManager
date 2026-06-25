namespace MSZDialougeManager
{
    partial class ChangeChirpsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeChirpsForm));
            speakerLv = new ListView();
            assignedColumn = new ColumnHeader();
            speakerColumn = new ColumnHeader();
            closeBtn = new Button();
            playBtn = new Button();
            assignBtn = new Button();
            removeBtn = new Button();
            SuspendLayout();
            // 
            // speakerLv
            // 
            speakerLv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            speakerLv.Columns.AddRange(new ColumnHeader[] { assignedColumn, speakerColumn });
            speakerLv.FullRowSelect = true;
            speakerLv.Location = new Point(12, 12);
            speakerLv.MultiSelect = false;
            speakerLv.Name = "speakerLv";
            speakerLv.Size = new Size(334, 168);
            speakerLv.TabIndex = 0;
            speakerLv.UseCompatibleStateImageBehavior = false;
            speakerLv.View = View.Details;
            speakerLv.SelectedIndexChanged += SpeakerLv_SelectedIndexChanged;
            // 
            // assignedColumn
            // 
            assignedColumn.Text = "Assigned?";
            assignedColumn.Width = 100;
            // 
            // speakerColumn
            // 
            speakerColumn.Text = "Speaker";
            speakerColumn.Width = 200;
            // 
            // closeBtn
            // 
            closeBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeBtn.Location = new Point(271, 186);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(75, 23);
            closeBtn.TabIndex = 1;
            closeBtn.Text = "Close";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += CloseBtn_Click;
            // 
            // playBtn
            // 
            playBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            playBtn.Location = new Point(12, 186);
            playBtn.Name = "playBtn";
            playBtn.Size = new Size(78, 23);
            playBtn.TabIndex = 2;
            playBtn.Text = "▶ Play";
            playBtn.UseVisualStyleBackColor = true;
            playBtn.Click += PlayBtn_Click;
            // 
            // assignBtn
            // 
            assignBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            assignBtn.Location = new Point(96, 186);
            assignBtn.Name = "assignBtn";
            assignBtn.Size = new Size(75, 23);
            assignBtn.TabIndex = 3;
            assignBtn.Text = "Assign";
            assignBtn.UseVisualStyleBackColor = true;
            assignBtn.Click += AssignBtn_Click;
            // 
            // removeBtn
            // 
            removeBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            removeBtn.Location = new Point(177, 186);
            removeBtn.Name = "removeBtn";
            removeBtn.Size = new Size(75, 23);
            removeBtn.TabIndex = 3;
            removeBtn.Text = "Remove";
            removeBtn.UseVisualStyleBackColor = true;
            removeBtn.Click += RemoveBtn_Click;
            // 
            // ChangeChirpsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(358, 221);
            Controls.Add(removeBtn);
            Controls.Add(assignBtn);
            Controls.Add(playBtn);
            Controls.Add(closeBtn);
            Controls.Add(speakerLv);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ChangeChirpsForm";
            Text = "Change Speaker Chirps";
            ResumeLayout(false);
        }

        #endregion

        private ListView speakerLv;
        private Button closeBtn;
        private ColumnHeader assignedColumn;
        private ColumnHeader speakerColumn;
        private Button playBtn;
        private Button assignBtn;
        private Button removeBtn;
    }
}