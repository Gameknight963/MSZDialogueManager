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
            components = new System.ComponentModel.Container();
            textOfNodeBox = new TextBox();
            NodeTextLabel = new Label();
            label1 = new Label();
            speakerDropDown = new ComboBox();
            customSpeakerLink = new LinkLabel();
            delayLabel = new Label();
            toolTip1 = new ToolTip(components);
            delayBox = new TextBox();
            nextNodesIntArrayBox = new TextBox();
            Ok = new Button();
            Cancel = new Button();
            nextNodesLabel = new Label();
            SuspendLayout();
            // 
            // textOfNodeBox
            // 
            textOfNodeBox.Location = new Point(15, 25);
            textOfNodeBox.Multiline = true;
            textOfNodeBox.Name = "textOfNodeBox";
            textOfNodeBox.Size = new Size(419, 71);
            textOfNodeBox.TabIndex = 0;
            toolTip1.SetToolTip(textOfNodeBox, "The text that will appear when this node is spoken.");
            // 
            // NodeTextLabel
            // 
            NodeTextLabel.AutoSize = true;
            NodeTextLabel.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NodeTextLabel.Location = new Point(12, 9);
            NodeTextLabel.Name = "NodeTextLabel";
            NodeTextLabel.Size = new Size(61, 13);
            NodeTextLabel.TabIndex = 1;
            NodeTextLabel.Text = "Node text:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 99);
            label1.Name = "label1";
            label1.Size = new Size(82, 13);
            label1.TabIndex = 1;
            label1.Text = "Speaker name:";
            // 
            // speakerDropDown
            // 
            speakerDropDown.FormattingEnabled = true;
            speakerDropDown.ItemHeight = 13;
            speakerDropDown.Items.AddRange(new object[] { "Kiri", "Kind" });
            speakerDropDown.Location = new Point(15, 115);
            speakerDropDown.Name = "speakerDropDown";
            speakerDropDown.Size = new Size(176, 21);
            speakerDropDown.TabIndex = 3;
            toolTip1.SetToolTip(speakerDropDown, "Who the node should be spoken by.");
            // 
            // customSpeakerLink
            // 
            customSpeakerLink.AutoSize = true;
            customSpeakerLink.Location = new Point(197, 118);
            customSpeakerLink.Name = "customSpeakerLink";
            customSpeakerLink.Size = new Size(138, 13);
            customSpeakerLink.TabIndex = 4;
            customSpeakerLink.TabStop = true;
            customSpeakerLink.Text = "Choose a custom speaker";
            customSpeakerLink.LinkClicked += customSpeakerLink_LinkClicked;
            // 
            // delayLabel
            // 
            delayLabel.AutoSize = true;
            delayLabel.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            delayLabel.Location = new Point(12, 139);
            delayLabel.Name = "delayLabel";
            delayLabel.Size = new Size(39, 13);
            delayLabel.TabIndex = 1;
            delayLabel.Text = "Delay:";
            // 
            // delayBox
            // 
            delayBox.Location = new Point(15, 156);
            delayBox.Name = "delayBox";
            delayBox.Size = new Size(176, 22);
            delayBox.TabIndex = 5;
            toolTip1.SetToolTip(delayBox, "The delay (in seconds) after the node is spoken.");
            // 
            // nextNodesIntArrayBox
            // 
            nextNodesIntArrayBox.Location = new Point(15, 197);
            nextNodesIntArrayBox.Name = "nextNodesIntArrayBox";
            nextNodesIntArrayBox.Size = new Size(176, 22);
            nextNodesIntArrayBox.TabIndex = 5;
            toolTip1.SetToolTip(nextNodesIntArrayBox, "A comma separated list that can include indicies of any node in this tree.\r\n");
            // 
            // Ok
            // 
            Ok.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Ok.Location = new Point(278, 221);
            Ok.Name = "Ok";
            Ok.Size = new Size(75, 23);
            Ok.TabIndex = 6;
            Ok.Text = "Ok";
            Ok.UseVisualStyleBackColor = true;
            Ok.Click += Ok_Click;
            // 
            // Cancel
            // 
            Cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Cancel.Location = new Point(359, 221);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(75, 23);
            Cancel.TabIndex = 6;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // nextNodesLabel
            // 
            nextNodesLabel.AutoSize = true;
            nextNodesLabel.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nextNodesLabel.Location = new Point(15, 181);
            nextNodesLabel.Name = "nextNodesLabel";
            nextNodesLabel.Size = new Size(109, 13);
            nextNodesLabel.TabIndex = 1;
            nextNodesLabel.Text = "Next nodes indicies:";
            // 
            // NodePropertiesEditor
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(446, 256);
            Controls.Add(Cancel);
            Controls.Add(Ok);
            Controls.Add(nextNodesIntArrayBox);
            Controls.Add(delayBox);
            Controls.Add(customSpeakerLink);
            Controls.Add(speakerDropDown);
            Controls.Add(nextNodesLabel);
            Controls.Add(delayLabel);
            Controls.Add(label1);
            Controls.Add(NodeTextLabel);
            Controls.Add(textOfNodeBox);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.Black;
            Name = "NodePropertiesEditor";
            Text = "Node Properties Editor";
            ResumeLayout(false);
            PerformLayout();

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
        private Label nextNodesLabel;
        private TextBox nextNodesIntArrayBox;
    }
}