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
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.statusLabel.Size = new System.Drawing.Size(295, 13);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 383);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Initialize Templete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.jsonButton);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(580, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(208, 413);
            this.panel1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.dialogueView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
    }
}

