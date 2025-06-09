namespace WindowsFormsApp
{
    partial class Login
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
            this.gradientPanelMainLogin = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.logLogin = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnLogin = new Syncfusion.WinForms.Controls.SfButton();
            this.txtPassword = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.txtUsername = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.lbPassword = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.lbUserName = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.lbLogin = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanelMainLogin)).BeginInit();
            this.gradientPanelMainLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientPanelMainLogin
            // 
            this.gradientPanelMainLogin.Controls.Add(this.logLogin);
            this.gradientPanelMainLogin.Controls.Add(this.btnLogin);
            this.gradientPanelMainLogin.Controls.Add(this.txtPassword);
            this.gradientPanelMainLogin.Controls.Add(this.txtUsername);
            this.gradientPanelMainLogin.Controls.Add(this.lbPassword);
            this.gradientPanelMainLogin.Controls.Add(this.lbUserName);
            this.gradientPanelMainLogin.Controls.Add(this.lbLogin);
            this.gradientPanelMainLogin.Location = new System.Drawing.Point(2, 4);
            this.gradientPanelMainLogin.Name = "gradientPanelMainLogin";
            this.gradientPanelMainLogin.Size = new System.Drawing.Size(625, 311);
            this.gradientPanelMainLogin.TabIndex = 0;
            // 
            // logLogin
            // 
            this.logLogin.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logLogin.Location = new System.Drawing.Point(256, 185);
            this.logLogin.Name = "logLogin";
            this.logLogin.Size = new System.Drawing.Size(0, 17);
            this.logLogin.TabIndex = 6;
            // 
            // btnLogin
            // 
            this.btnLogin.AllowDrop = true;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLogin.Location = new System.Drawing.Point(256, 217);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(115, 41);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnLogin_MouseDown);
            this.btnLogin.MouseEnter += new System.EventHandler(this.BtnLogin_MouseEnter);
            this.btnLogin.MouseLeave += new System.EventHandler(this.BtnLogin_MouseLeave);
            this.btnLogin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnLogin_MouseUp);
            // 
            // txtPassword
            // 
            this.txtPassword.BeforeTouchSize = new System.Drawing.Size(240, 22);
            this.txtPassword.Location = new System.Drawing.Point(256, 148);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(240, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // txtUsername
            // 
            this.txtUsername.BeforeTouchSize = new System.Drawing.Size(240, 22);
            this.txtUsername.Location = new System.Drawing.Point(256, 104);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(240, 22);
            this.txtUsername.TabIndex = 3;
            // 
            // lbPassword
            // 
            this.lbPassword.Location = new System.Drawing.Point(158, 154);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(67, 16);
            this.lbPassword.TabIndex = 2;
            this.lbPassword.Text = "Password";
            // 
            // lbUserName
            // 
            this.lbUserName.Location = new System.Drawing.Point(158, 110);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(70, 16);
            this.lbUserName.TabIndex = 1;
            this.lbUserName.Text = "Username";
            // 
            // lbLogin
            // 
            this.lbLogin.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.Location = new System.Drawing.Point(256, 25);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(90, 38);
            this.lbLogin.TabIndex = 0;
            this.lbLogin.Text = "Login";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 312);
            this.Controls.Add(this.gradientPanelMainLogin);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanelMainLogin)).EndInit();
            this.gradientPanelMainLogin.ResumeLayout(false);
            this.gradientPanelMainLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanelMainLogin;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbLogin;
        private Syncfusion.Windows.Forms.Tools.AutoLabel logLogin;
        private Syncfusion.WinForms.Controls.SfButton btnLogin;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtPassword;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtUsername;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbPassword;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbUserName;
    }
}