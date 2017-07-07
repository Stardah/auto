namespace Rpi
{
    partial class Dialog
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
            this.comboGas = new System.Windows.Forms.ComboBox();
            this.textMaterial = new System.Windows.Forms.TextBox();
            this.textAbout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите дополнительные параметры УП";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(40, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Материал:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(40, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Газ:";
            // 
            // comboGas
            // 
            this.comboGas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGas.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboGas.FormattingEnabled = true;
            this.comboGas.Items.AddRange(new object[] {
            "Кислород",
            "Азот"});
            this.comboGas.Location = new System.Drawing.Point(135, 99);
            this.comboGas.Name = "comboGas";
            this.comboGas.Size = new System.Drawing.Size(156, 24);
            this.comboGas.TabIndex = 2;
            // 
            // textMaterial
            // 
            this.textMaterial.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textMaterial.Location = new System.Drawing.Point(135, 61);
            this.textMaterial.Name = "textMaterial";
            this.textMaterial.Size = new System.Drawing.Size(156, 23);
            this.textMaterial.TabIndex = 1;
            this.textMaterial.TextChanged += new System.EventHandler(this.textMaterial_TextChanged);
            // 
            // textAbout
            // 
            this.textAbout.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textAbout.Location = new System.Drawing.Point(135, 139);
            this.textAbout.Name = "textAbout";
            this.textAbout.Size = new System.Drawing.Size(156, 23);
            this.textAbout.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(40, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Примечание:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Verdana", 10F);
            this.button1.Location = new System.Drawing.Point(0, 176);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(365, 26);
            this.button1.TabIndex = 4;
            this.button1.Text = "Готово";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 202);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textAbout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textMaterial);
            this.Controls.Add(this.comboGas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.Text = "Dialog";
            this.Load += new System.EventHandler(this.Dialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboGas;
        private System.Windows.Forms.TextBox textMaterial;
        private System.Windows.Forms.TextBox textAbout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}