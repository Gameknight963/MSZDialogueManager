namespace MSZDialougeManager
{
    partial class TTSEditor
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.generate = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.speakerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voiceColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.previewColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.speakerColumn,
            this.voiceColumn,
            this.previewColumn});
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(474, 215);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // generate
            // 
            this.generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.generate.Location = new System.Drawing.Point(12, 233);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(94, 23);
            this.generate.TabIndex = 1;
            this.generate.Text = "Generate all";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancel.Location = new System.Drawing.Point(112, 233);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(89, 23);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // speakerColumn
            // 
            this.speakerColumn.FillWeight = 68.02031F;
            this.speakerColumn.HeaderText = "Speaker";
            this.speakerColumn.Name = "speakerColumn";
            this.speakerColumn.ReadOnly = true;
            // 
            // voiceColumn
            // 
            this.voiceColumn.FillWeight = 131.9797F;
            this.voiceColumn.HeaderText = "Voice";
            this.voiceColumn.Name = "voiceColumn";
            this.voiceColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.voiceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // previewColumn
            // 
            this.previewColumn.FillWeight = 50F;
            this.previewColumn.HeaderText = "Preview";
            this.previewColumn.Name = "previewColumn";
            this.previewColumn.ReadOnly = true;
            this.previewColumn.Text = "Preview";
            this.previewColumn.UseColumnTextForButtonValue = true;
            // 
            // TTSEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 268);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.generate);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TTSEditor";
            this.Text = "Text-to-Speech Generator";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn speakerColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn voiceColumn;
        private System.Windows.Forms.DataGridViewButtonColumn previewColumn;
    }
}