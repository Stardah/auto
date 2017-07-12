namespace Automation2010
{
    partial class Form1
    {

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
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.connection_status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.listSecond = new System.Windows.Forms.ListView();
            this.listFirst = new System.Windows.Forms.ListView();
            this.listDone = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.listAll = new System.Windows.Forms.ListView();
            this.btnFurther = new System.Windows.Forms.Button();
            this.btnNear = new System.Windows.Forms.Button();
            this.textIP = new System.Windows.Forms.TextBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(89, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Новые УП";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(169, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Статус соединения с Raspberry Pi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(533, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "УП дальнего станка";
            // 
            // connection_status
            // 
            this.connection_status.AutoSize = true;
            this.connection_status.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connection_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.connection_status.Location = new System.Drawing.Point(412, 9);
            this.connection_status.Name = "connection_status";
            this.connection_status.Size = new System.Drawing.Size(100, 16);
            this.connection_status.TabIndex = 11;
            this.connection_status.Text = "Нет сигнала";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "УП ближнего станка";
            // 
            // btn_connect
            // 
            this.btn_connect.BackgroundImage = global::Rpi.Properties.Resources.refresh;
            this.btn_connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_connect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_connect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_connect.Location = new System.Drawing.Point(574, 5);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(24, 24);
            this.btn_connect.TabIndex = 13;
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // listSecond
            // 
            this.listSecond.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listSecond.LabelWrap = false;
            this.listSecond.Location = new System.Drawing.Point(536, 340);
            this.listSecond.Name = "listSecond";
            this.listSecond.Size = new System.Drawing.Size(236, 222);
            this.listSecond.TabIndex = 5;
            this.listSecond.UseCompatibleStateImageBehavior = false;
            this.listSecond.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listSecond_ColumnClick);
            this.listSecond.SelectedIndexChanged += new System.EventHandler(this.listSecond_SelectedIndexChanged);
            // 
            // listFirst
            // 
            this.listFirst.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listFirst.LabelWrap = false;
            this.listFirst.Location = new System.Drawing.Point(12, 340);
            this.listFirst.Name = "listFirst";
            this.listFirst.Size = new System.Drawing.Size(236, 222);
            this.listFirst.TabIndex = 4;
            this.listFirst.UseCompatibleStateImageBehavior = false;
            this.listFirst.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listFirst_ColumnClick);
            this.listFirst.SelectedIndexChanged += new System.EventHandler(this.listFirst_SelectedIndexChanged);
            // 
            // listDone
            // 
            this.listDone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listDone.LabelWrap = false;
            this.listDone.Location = new System.Drawing.Point(265, 340);
            this.listDone.Name = "listDone";
            this.listDone.Size = new System.Drawing.Size(256, 222);
            this.listDone.TabIndex = 18;
            this.listDone.UseCompatibleStateImageBehavior = false;
            this.listDone.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listDone_ColumnClick);
            this.listDone.Click += new System.EventHandler(this.listDone_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(262, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Выполненные УП";
            // 
            // listAll
            // 
            this.listAll.AllowDrop = true;
            this.listAll.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listAll.LabelWrap = false;
            this.listAll.Location = new System.Drawing.Point(92, 49);
            this.listAll.MultiSelect = false;
            this.listAll.Name = "listAll";
            this.listAll.Size = new System.Drawing.Size(600, 222);
            this.listAll.TabIndex = 2;
            this.listAll.UseCompatibleStateImageBehavior = false;
            this.listAll.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listAll_ColumnClick);
            this.listAll.SelectedIndexChanged += new System.EventHandler(this.listAll_SelectedIndexChanged);
            this.listAll.Click += new System.EventHandler(this.listAll_Click);
            this.listAll.DragDrop += new System.Windows.Forms.DragEventHandler(this.listAll_DragDrop);
            this.listAll.DragEnter += new System.Windows.Forms.DragEventHandler(this.listAll_DragEnter);
            // 
            // btnFurther
            // 
            this.btnFurther.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnFurther.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFurther.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFurther.Location = new System.Drawing.Point(536, 277);
            this.btnFurther.Name = "btnFurther";
            this.btnFurther.Size = new System.Drawing.Size(111, 30);
            this.btnFurther.TabIndex = 1;
            this.btnFurther.Text = "На дальний";
            this.btnFurther.UseVisualStyleBackColor = false;
            this.btnFurther.Click += new System.EventHandler(this.btn_second_Click);
            // 
            // btnNear
            // 
            this.btnNear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNear.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNear.Location = new System.Drawing.Point(137, 277);
            this.btnNear.Name = "btnNear";
            this.btnNear.Size = new System.Drawing.Size(111, 30);
            this.btnNear.TabIndex = 0;
            this.btnNear.Text = "На ближний";
            this.btnNear.UseVisualStyleBackColor = false;
            this.btnNear.Click += new System.EventHandler(this.btn_first_Click);
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(518, 8);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(50, 20);
            this.textIP.TabIndex = 20;
            this.textIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textIP_KeyPress);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDel.Location = new System.Drawing.Point(338, 277);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(111, 30);
            this.btnDel.TabIndex = 21;
            this.btnDel.Text = "Удалить";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.Location = new System.Drawing.Point(604, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 24);
            this.button1.TabIndex = 22;
            this.button1.Text = "ПРЕРВАТЬ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(784, 570);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.btnFurther);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listSecond);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listAll);
            this.Controls.Add(this.listDone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNear);
            this.Controls.Add(this.connection_status);
            this.Controls.Add(this.listFirst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label connection_status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.ListView listSecond;
        private System.Windows.Forms.ListView listFirst;
        private System.Windows.Forms.ListView listDone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listAll;
        private System.Windows.Forms.Button btnFurther;
        private System.Windows.Forms.Button btnNear;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.IContainer components;
    }
}

