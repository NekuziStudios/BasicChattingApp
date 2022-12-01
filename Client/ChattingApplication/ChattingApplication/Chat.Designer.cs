namespace ChattingApplication
{
    partial class Chat
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
            this.contentbox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MutedBg = new System.Windows.Forms.PictureBox();
            this.MutedText = new System.Windows.Forms.Label();
            this.MsgBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MutedBg)).BeginInit();
            this.SuspendLayout();
            // 
            // contentbox
            // 
            this.contentbox.Location = new System.Drawing.Point(12, 417);
            this.contentbox.Name = "contentbox";
            this.contentbox.Size = new System.Drawing.Size(640, 23);
            this.contentbox.TabIndex = 1;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(658, 417);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(130, 25);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(658, 31);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(136, 380);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(658, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "User List:";
            // 
            // MutedBg
            // 
            this.MutedBg.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MutedBg.Location = new System.Drawing.Point(12, 417);
            this.MutedBg.Name = "MutedBg";
            this.MutedBg.Size = new System.Drawing.Size(640, 30);
            this.MutedBg.TabIndex = 5;
            this.MutedBg.TabStop = false;
            // 
            // MutedText
            // 
            this.MutedText.AutoSize = true;
            this.MutedText.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MutedText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MutedText.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MutedText.Location = new System.Drawing.Point(36, 422);
            this.MutedText.Name = "MutedText";
            this.MutedText.Size = new System.Drawing.Size(434, 20);
            this.MutedText.TabIndex = 6;
            this.MutedText.Text = "You have been muted in Chatting App. Contact .nekuzi#0001";
            // 
            // MsgBox
            // 
            this.MsgBox.Location = new System.Drawing.Point(9, 15);
            this.MsgBox.Multiline = true;
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.ReadOnly = true;
            this.MsgBox.Size = new System.Drawing.Size(643, 396);
            this.MsgBox.TabIndex = 7;
            this.MsgBox.TextChanged += new System.EventHandler(this.MsgBox_TextChanged);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.MutedText);
            this.Controls.Add(this.MutedBg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.contentbox);
            this.Name = "Chat";
            this.Text = "ChattingApp";
            this.Load += new System.EventHandler(this.Chat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MutedBg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox contentbox;
        private Button SendButton;
        private RichTextBox richTextBox2;
        private Label label1;
        private PictureBox MutedBg;
        private Label MutedText;
        private TextBox MsgBox;
    }
}