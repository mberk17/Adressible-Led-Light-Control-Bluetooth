namespace LedProject1._0
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.scrollR = new System.Windows.Forms.HScrollBar();
            this.scrollG = new System.Windows.Forms.HScrollBar();
            this.scrollB = new System.Windows.Forms.HScrollBar();
            this.topButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.allLed = new System.Windows.Forms.Button();
            this.senderButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.continiousButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.SeaShell;
            this.pictureBox.Location = new System.Drawing.Point(16, 15);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(700, 400);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            // 
            // scrollR
            // 
            this.scrollR.Location = new System.Drawing.Point(26, 436);
            this.scrollR.Maximum = 255;
            this.scrollR.Name = "scrollR";
            this.scrollR.Size = new System.Drawing.Size(144, 29);
            this.scrollR.TabIndex = 1;
            this.scrollR.ValueChanged += new System.EventHandler(this.scroll_ValueChanged);
            // 
            // scrollG
            // 
            this.scrollG.Location = new System.Drawing.Point(185, 436);
            this.scrollG.Maximum = 255;
            this.scrollG.Name = "scrollG";
            this.scrollG.Size = new System.Drawing.Size(144, 29);
            this.scrollG.TabIndex = 1;
            this.scrollG.ValueChanged += new System.EventHandler(this.scroll_ValueChanged);
            // 
            // scrollB
            // 
            this.scrollB.Location = new System.Drawing.Point(339, 436);
            this.scrollB.Maximum = 255;
            this.scrollB.Name = "scrollB";
            this.scrollB.Size = new System.Drawing.Size(144, 29);
            this.scrollB.TabIndex = 1;
            this.scrollB.ValueChanged += new System.EventHandler(this.scroll_ValueChanged);
            // 
            // topButton
            // 
            this.topButton.Location = new System.Drawing.Point(14, 23);
            this.topButton.Name = "topButton";
            this.topButton.Size = new System.Drawing.Size(75, 23);
            this.topButton.TabIndex = 2;
            this.topButton.Text = "Top";
            this.topButton.UseVisualStyleBackColor = true;
            this.topButton.Click += new System.EventHandler(this.topButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(111, 519);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(75, 23);
            this.leftButton.TabIndex = 2;
            this.leftButton.Text = "Left";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(189, 519);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(75, 23);
            this.rightButton.TabIndex = 2;
            this.rightButton.Text = "Right";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // allLed
            // 
            this.allLed.Location = new System.Drawing.Point(270, 519);
            this.allLed.Name = "allLed";
            this.allLed.Size = new System.Drawing.Size(75, 23);
            this.allLed.TabIndex = 2;
            this.allLed.Text = "All";
            this.allLed.UseVisualStyleBackColor = true;
            this.allLed.Click += new System.EventHandler(this.allLed_Click);
            // 
            // senderButton
            // 
            this.senderButton.Location = new System.Drawing.Point(30, 558);
            this.senderButton.Name = "senderButton";
            this.senderButton.Size = new System.Drawing.Size(75, 45);
            this.senderButton.TabIndex = 2;
            this.senderButton.Text = "Write Colors";
            this.senderButton.UseVisualStyleBackColor = true;
            this.senderButton.Click += new System.EventHandler(this.senderButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.topButton);
            this.groupBox1.Controls.Add(this.continiousButton);
            this.groupBox1.Location = new System.Drawing.Point(16, 496);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 124);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Leds";
            // 
            // continiousButton
            // 
            this.continiousButton.Location = new System.Drawing.Point(95, 62);
            this.continiousButton.Name = "continiousButton";
            this.continiousButton.Size = new System.Drawing.Size(75, 45);
            this.continiousButton.TabIndex = 2;
            this.continiousButton.Text = "Continious Led Control";
            this.continiousButton.UseVisualStyleBackColor = true;
            this.continiousButton.Click += new System.EventHandler(this.continiousButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(15, 421);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 53);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RGB Values";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(757, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(91, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(746, 84);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(113, 41);
            this.ConnectButton.TabIndex = 6;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(640, 518);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 673);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.senderButton);
            this.Controls.Add(this.allLed);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.scrollB);
            this.Controls.Add(this.scrollG);
            this.Controls.Add(this.scrollR);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Led Control V1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.HScrollBar scrollR;
        private System.Windows.Forms.HScrollBar scrollG;
        private System.Windows.Forms.HScrollBar scrollB;
        private System.Windows.Forms.Button topButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button allLed;
        private System.Windows.Forms.Button senderButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button continiousButton;
        private System.Windows.Forms.Button button1;
    }
}

