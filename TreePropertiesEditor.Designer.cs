using MSZDialougeManager.Properties;

namespace MSZDialougeManager
{
    partial class TreePropertiesEditor
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
            startNodesBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            treeNameBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            chirpTimeBox = new TextBox();
            initialDelayBox = new TextBox();
            exitDelayBox = new TextBox();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // startNodesBox
            // 
            startNodesBox.Location = new Point(12, 23);
            startNodesBox.Name = "startNodesBox";
            startNodesBox.PlaceholderText = "e.g. 15, 0, -3...";
            startNodesBox.Size = new Size(218, 23);
            startNodesBox.TabIndex = 0;
            toolTip1.SetToolTip(startNodesBox, "A comma separated list indicating which nodes this tree should begin with.");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 5);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 1;
            label1.Text = "Starting nodes indicies:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 91);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 1;
            label2.Text = "Chrip time:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(138, 91);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 1;
            label3.Text = "Initial delay:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 137);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 1;
            label4.Text = "Exit delay:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 47);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 1;
            label5.Text = "Name";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(49, 47);
            label6.Name = "label6";
            label6.Size = new Size(92, 15);
            label6.TabIndex = 3;
            label6.Text = "(cosmetic only):";
            // 
            // treeNameBox
            // 
            treeNameBox.Location = new Point(12, 65);
            treeNameBox.Name = "treeNameBox";
            treeNameBox.PlaceholderText = "barafelabaldealac";
            treeNameBox.Size = new Size(218, 23);
            treeNameBox.TabIndex = 0;
            toolTip1.SetToolTip(treeNameBox, "The name of this tree that will be displayed in the editor.");
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(239, 194);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 4;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(155, 194);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // chirpTimeBox
            // 
            chirpTimeBox.Location = new Point(12, 111);
            chirpTimeBox.Name = "chirpTimeBox";
            chirpTimeBox.PlaceholderText = "0.1";
            chirpTimeBox.Size = new Size(120, 23);
            chirpTimeBox.TabIndex = 0;
            toolTip1.SetToolTip(chirpTimeBox, "Chirp time. Optional, leave blank if unwanted.");
            // 
            // initialDelayBox
            // 
            initialDelayBox.Location = new Point(138, 111);
            initialDelayBox.Name = "initialDelayBox";
            initialDelayBox.PlaceholderText = "1";
            initialDelayBox.Size = new Size(114, 23);
            initialDelayBox.TabIndex = 0;
            toolTip1.SetToolTip(initialDelayBox, "Delay when entering this tree. Optional, leave blank if unwanted.");
            // 
            // exitDelayBox
            // 
            exitDelayBox.Location = new Point(12, 155);
            exitDelayBox.Name = "exitDelayBox";
            exitDelayBox.PlaceholderText = "1";
            exitDelayBox.Size = new Size(120, 23);
            exitDelayBox.TabIndex = 0;
            toolTip1.SetToolTip(exitDelayBox, "Delay when exiting this tree. Optional, leave blank if unwanted.");
            // 
            // TreePropertiesEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 229);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(initialDelayBox);
            Controls.Add(exitDelayBox);
            Controls.Add(chirpTimeBox);
            Controls.Add(treeNameBox);
            Controls.Add(startNodesBox);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TreePropertiesEditor";
            Text = "Tree Properties Editor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox startNodesBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox treeNameBox;
        private Button okButton;
        private Button cancelButton;
        private TextBox chirpTimeBox;
        private TextBox initialDelayBox;
        private TextBox exitDelayBox;
        private ToolTip toolTip1;
    }
}