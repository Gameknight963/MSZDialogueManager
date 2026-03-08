namespace MSZDialougeManager
{
    partial class NodePropertiesEditor
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
            this.components = new System.ComponentModel.Container();
            this.textOfNodeBox = new System.Windows.Forms.TextBox();
            this.NodeTextLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.speakerDropDown = new System.Windows.Forms.ComboBox();
            this.customSpeakerLink = new System.Windows.Forms.LinkLabel();
            this.delayLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.delayBox = new System.Windows.Forms.TextBox();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textOfNodeBox
            // 
            this.textOfNodeBox.Location = new System.Drawing.Point(15, 25);
            this.textOfNodeBox.Multiline = true;
            this.textOfNodeBox.Name = "textOfNodeBox";
            this.textOfNodeBox.Size = new System.Drawing.Size(419, 71);
            this.textOfNodeBox.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textOfNodeBox, "The text that will appear when this node is spoken.");
            // 
            // NodeTextLabel
            // 
            this.NodeTextLabel.AutoSize = true;
            this.NodeTextLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NodeTextLabel.Location = new System.Drawing.Point(12, 9);
            this.NodeTextLabel.Name = "NodeTextLabel";
            this.NodeTextLabel.Size = new System.Drawing.Size(61, 13);
            this.NodeTextLabel.TabIndex = 1;
            this.NodeTextLabel.Text = "Node text:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Speaker name:";
            // 
            // speakerDropDown
            // 
            this.speakerDropDown.FormattingEnabled = true;
            this.speakerDropDown.ItemHeight = 13;
            this.speakerDropDown.Items.AddRange(new object[] {
            "Kiri",
            "Kind"});
            this.speakerDropDown.Location = new System.Drawing.Point(15, 115);
            this.speakerDropDown.Name = "speakerDropDown";
            this.speakerDropDown.Size = new System.Drawing.Size(176, 21);
            this.speakerDropDown.TabIndex = 3;
            this.toolTip1.SetToolTip(this.speakerDropDown, "Who the node should be spoken by.");
            // 
            // customSpeakerLink
            // 
            this.customSpeakerLink.AutoSize = true;
            this.customSpeakerLink.Location = new System.Drawing.Point(197, 118);
            this.customSpeakerLink.Name = "customSpeakerLink";
            this.customSpeakerLink.Size = new System.Drawing.Size(138, 13);
            this.customSpeakerLink.TabIndex = 4;
            this.customSpeakerLink.TabStop = true;
            this.customSpeakerLink.Text = "Choose a custom speaker";
            this.customSpeakerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.customSpeakerLink_LinkClicked);
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delayLabel.Location = new System.Drawing.Point(12, 139);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(39, 13);
            this.delayLabel.TabIndex = 1;
            this.delayLabel.Text = "Delay:";
            // 
            // delayBox
            // 
            this.delayBox.Location = new System.Drawing.Point(15, 156);
            this.delayBox.Name = "delayBox";
            this.delayBox.Size = new System.Drawing.Size(176, 22);
            this.delayBox.TabIndex = 5;
            this.toolTip1.SetToolTip(this.delayBox, "The delay (in seconds) after the node is spoken.");
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(12, 198);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(75, 23);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(93, 198);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // NodePropertiesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 233);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.delayBox);
            this.Controls.Add(this.customSpeakerLink);
            this.Controls.Add(this.speakerDropDown);
            this.Controls.Add(this.delayLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NodeTextLabel);
            this.Controls.Add(this.textOfNodeBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NodePropertiesEditor";
            this.Text = "Node Properties Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textOfNodeBox;
        private System.Windows.Forms.Label NodeTextLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox speakerDropDown;
        private System.Windows.Forms.LinkLabel customSpeakerLink;
        private System.Windows.Forms.Label delayLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox delayBox;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
    }
}