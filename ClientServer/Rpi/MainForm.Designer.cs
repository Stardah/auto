namespace Rpi
{
    /// Класс формы.
    partial class MainForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.replyClientButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.autoUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.logTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.replyClientButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.connectButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ipTextBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 462);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(3, 3);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(478, 372);
            this.logTextBox.TabIndex = 2;
            // 
            // replyClientButton
            // 
            this.replyClientButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.replyClientButton.Location = new System.Drawing.Point(3, 436);
            this.replyClientButton.Name = "replyClientButton";
            this.replyClientButton.Size = new System.Drawing.Size(478, 23);
            this.replyClientButton.TabIndex = 0;
            this.replyClientButton.Text = "Ответить клиенту";
            this.replyClientButton.UseVisualStyleBackColor = true;
            this.replyClientButton.Click += new System.EventHandler(this.replyClientButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectButton.Location = new System.Drawing.Point(3, 407);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(478, 23);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Присоединить устройство";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // ipTextBox
            // 
            this.ipTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipTextBox.Location = new System.Drawing.Point(3, 381);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(478, 20);
            this.ipTextBox.TabIndex = 5;
            this.ipTextBox.Text = "127.0.0.1";
            this.ipTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // autoUpdateTimer
            // 
            this.autoUpdateTimer.Enabled = true;
            this.autoUpdateTimer.Interval = 1000;
            this.autoUpdateTimer.Tick += new System.EventHandler(this.autoUpdateTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Сервер";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button replyClientButton;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Timer autoUpdateTimer;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox ipTextBox;
    }
}

