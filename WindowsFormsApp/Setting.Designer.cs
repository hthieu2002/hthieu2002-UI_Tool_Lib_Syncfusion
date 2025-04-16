namespace WindowsFormsApp
{
    partial class Setting
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sfButton1 = new Syncfusion.WinForms.Controls.SfButton();
            this.sfButton2 = new Syncfusion.WinForms.Controls.SfButton();
            this.sfButton3 = new Syncfusion.WinForms.Controls.SfButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = global::WindowsFormsApp.Properties.Resources.setting_0;
            this.pictureBox1.Image = global::WindowsFormsApp.Properties.Resources.setting_0;
            this.pictureBox1.InitialImage = global::WindowsFormsApp.Properties.Resources.setting_0;
            this.pictureBox1.Location = new System.Drawing.Point(36, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.Location = new System.Drawing.Point(94, 34);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(80, 25);
            this.autoLabel1.TabIndex = 1;
            this.autoLabel1.Text = "Setting";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sfButton3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.sfButton2);
            this.panel1.Controls.Add(this.autoLabel1);
            this.panel1.Controls.Add(this.sfButton1);
            this.panel1.Location = new System.Drawing.Point(-9, -10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 102);
            this.panel1.TabIndex = 2;
            // 
            // sfButton1
            // 
            this.sfButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfButton1.Location = new System.Drawing.Point(55, 65);
            this.sfButton1.Name = "sfButton1";
            this.sfButton1.Size = new System.Drawing.Size(96, 28);
            this.sfButton1.TabIndex = 3;
            this.sfButton1.Text = "General";
            this.sfButton1.Click += new System.EventHandler(this.sfButton1_Click);
            // 
            // sfButton2
            // 
            this.sfButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfButton2.Location = new System.Drawing.Point(157, 65);
            this.sfButton2.Name = "sfButton2";
            this.sfButton2.Size = new System.Drawing.Size(96, 28);
            this.sfButton2.TabIndex = 4;
            this.sfButton2.Text = "Automation";
            this.sfButton2.Click += new System.EventHandler(this.sfButton2_Click);
            // 
            // sfButton3
            // 
            this.sfButton3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfButton3.Location = new System.Drawing.Point(268, 65);
            this.sfButton3.Name = "sfButton3";
            this.sfButton3.Size = new System.Drawing.Size(96, 28);
            this.sfButton3.TabIndex = 5;
            this.sfButton3.Text = "Performace";
            this.sfButton3.Click += new System.EventHandler(this.sfButton3_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.autoLabel3);
            this.panel2.Controls.Add(this.autoLabel2);
            this.panel2.Location = new System.Drawing.Point(0, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(776, 204);
            this.panel2.TabIndex = 3;
            // 
            // autoLabel2
            // 
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.Location = new System.Drawing.Point(98, 24);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(99, 22);
            this.autoLabel2.TabIndex = 0;
            this.autoLabel2.Text = "Ngôn Ngữ";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.Location = new System.Drawing.Point(98, 102);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(74, 22);
            this.autoLabel3.TabIndex = 1;
            this.autoLabel3.Text = "Chủ đề";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(98, 60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(585, 30);
            this.comboBox1.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(98, 145);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(585, 30);
            this.comboBox2.TabIndex = 4;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(769, 303);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton sfButton1;
        private Syncfusion.WinForms.Controls.SfButton sfButton2;
        private Syncfusion.WinForms.Controls.SfButton sfButton3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
    }
}