namespace UDP_Chat_Client_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSendBroadcast = new System.Windows.Forms.Button();
            this.tbBroadcastText = new System.Windows.Forms.TextBox();
            this.tbRemotePort = new System.Windows.Forms.TextBox();
            this.tbLocalPort = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFileSelectedPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSendImage = new System.Windows.Forms.Button();
            this.pbImageReceived = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImageReceived)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Remote Port: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Broadcast Text:";
            // 
            // btnSendBroadcast
            // 
            this.btnSendBroadcast.Location = new System.Drawing.Point(434, 9);
            this.btnSendBroadcast.Name = "btnSendBroadcast";
            this.btnSendBroadcast.Size = new System.Drawing.Size(75, 36);
            this.btnSendBroadcast.TabIndex = 3;
            this.btnSendBroadcast.Text = "&Broadcast";
            this.btnSendBroadcast.UseVisualStyleBackColor = true;
            this.btnSendBroadcast.Click += new System.EventHandler(this.btnSendBroadcast_Click);
            // 
            // tbBroadcastText
            // 
            this.tbBroadcastText.Location = new System.Drawing.Point(88, 67);
            this.tbBroadcastText.Name = "tbBroadcastText";
            this.tbBroadcastText.Size = new System.Drawing.Size(100, 20);
            this.tbBroadcastText.TabIndex = 4;
            this.tbBroadcastText.Text = "<DISCOVER>";
            // 
            // tbRemotePort
            // 
            this.tbRemotePort.Location = new System.Drawing.Point(88, 41);
            this.tbRemotePort.Name = "tbRemotePort";
            this.tbRemotePort.Size = new System.Drawing.Size(100, 20);
            this.tbRemotePort.TabIndex = 5;
            this.tbRemotePort.Text = "23000";
            // 
            // tbLocalPort
            // 
            this.tbLocalPort.Location = new System.Drawing.Point(88, 15);
            this.tbLocalPort.Name = "tbLocalPort";
            this.tbLocalPort.Size = new System.Drawing.Size(100, 20);
            this.tbLocalPort.TabIndex = 6;
            this.tbLocalPort.Text = "23001";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(434, 51);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(75, 36);
            this.btnSendMessage.TabIndex = 7;
            this.btnSendMessage.Text = "&Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(194, 28);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(234, 59);
            this.tbMessage.TabIndex = 9;
            this.tbMessage.Text = "Hello World!!!";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Message Text:";
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(3, 112);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.Size = new System.Drawing.Size(506, 286);
            this.tbConsole.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Console:";
            // 
            // tbFileSelectedPath
            // 
            this.tbFileSelectedPath.Location = new System.Drawing.Point(579, 12);
            this.tbFileSelectedPath.Name = "tbFileSelectedPath";
            this.tbFileSelectedPath.Size = new System.Drawing.Size(411, 20);
            this.tbFileSelectedPath.TabIndex = 13;
            this.tbFileSelectedPath.Text = "C:\\Users\\Public\\Pictures\\Sample Pictures\\Koala-Tiny.jpg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(515, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Local Port:";
            // 
            // btnSendImage
            // 
            this.btnSendImage.Location = new System.Drawing.Point(579, 38);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(411, 36);
            this.btnSendImage.TabIndex = 14;
            this.btnSendImage.Text = "Send &Image";
            this.btnSendImage.UseVisualStyleBackColor = true;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // pbImageReceived
            // 
            this.pbImageReceived.Location = new System.Drawing.Point(579, 112);
            this.pbImageReceived.Name = "pbImageReceived";
            this.pbImageReceived.Size = new System.Drawing.Size(411, 286);
            this.pbImageReceived.TabIndex = 15;
            this.pbImageReceived.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 410);
            this.Controls.Add(this.pbImageReceived);
            this.Controls.Add(this.btnSendImage);
            this.Controls.Add(this.tbFileSelectedPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.tbLocalPort);
            this.Controls.Add(this.tbRemotePort);
            this.Controls.Add(this.tbBroadcastText);
            this.Controls.Add(this.btnSendBroadcast);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Client Form";
            ((System.ComponentModel.ISupportInitialize)(this.pbImageReceived)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSendBroadcast;
        private System.Windows.Forms.TextBox tbBroadcastText;
        private System.Windows.Forms.TextBox tbRemotePort;
        private System.Windows.Forms.TextBox tbLocalPort;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFileSelectedPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.PictureBox pbImageReceived;
    }
}

