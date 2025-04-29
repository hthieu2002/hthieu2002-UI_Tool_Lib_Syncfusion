using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using WindowsFormsApp.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace AccountCreatorForm.Views
{
    partial class Home
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
            this.panelMainView = new System.Windows.Forms.Panel();
            this.btnDieuKhien = new Syncfusion.WinForms.Controls.SfButton();
            this.btnAuto = new Syncfusion.WinForms.Controls.SfButton();
            this.panelAutoSubMenu = new System.Windows.Forms.Panel();
            this.btnNhiemVu = new Syncfusion.WinForms.Controls.SfButton();
            this.btnLuotChay = new Syncfusion.WinForms.Controls.SfButton();
            this.btnUngDung = new Syncfusion.WinForms.Controls.SfButton();
            this.btnStore = new Syncfusion.WinForms.Controls.SfButton();
            this.btnManagerApp = new Syncfusion.WinForms.Controls.SfButton();
            this.btnManagerAccount = new Syncfusion.WinForms.Controls.SfButton();
            this.btnLichTrinh = new Syncfusion.WinForms.Controls.SfButton();
            this.btnThanhToan = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCaiDat = new Syncfusion.WinForms.Controls.SfButton();
            this.btnHelp = new Syncfusion.WinForms.Controls.SfButton();
            this.panelSidebarContent = new System.Windows.Forms.Panel();
            this.gradientPanelSidebar = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.btnLogo = new Syncfusion.WinForms.Controls.SfButton();
            this.panelAutoSubMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanelSidebar)).BeginInit();
            this.gradientPanelSidebar.SuspendLayout();
            this.SuspendLayout();
            this.panelAutoSubMenu.Visible = false;
            this.btnManagerApp.Visible = false;
            this.btnManagerAccount.Visible = false;
            this.btnUngDung.Visible = false;
            this.btnLuotChay.Visible = false;
            this.btnNhiemVu.Visible = false;
            this.btnStore.Visible = false;
            this.btnCaiDat.Visible = false;
            this.btnHelp.Visible = false;
            this.panelPlanInfo = new Panel();
            this.picPlan = new PictureBox();
            this.lblPlanTitle = new Label();
            this.lblExpire = new Label();
            this.lblUnlimited = new Label();
            this.btnUpgrade = new Syncfusion.WinForms.Controls.SfButton();
            // 
            // panelMainView
            // 
            this.panelMainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainView.Location = new System.Drawing.Point(214, 0);
            this.panelMainView.Name = "panelMainView";
            this.panelMainView.Size = new System.Drawing.Size(1048, 673);
            this.panelMainView.TabIndex = 1;
            // 
            // btnDieuKhien
            // 
            this.btnDieuKhien.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnDieuKhien.Location = new System.Drawing.Point(-2, 75);
            this.btnDieuKhien.Name = "btnDieuKhien";
            this.btnDieuKhien.Size = new System.Drawing.Size(210, 28);
            this.btnDieuKhien.TabIndex = 1;
            this.btnDieuKhien.Text = "Tool";
            // 
            // btnAuto
            // 
            this.btnAuto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAuto.Location = new System.Drawing.Point(-2, 103);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(210, 28);
            this.btnAuto.TabIndex = 2;
            this.btnAuto.Text = "Tự động hóa";
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // panelAutoSubMenu
            // 
            this.panelAutoSubMenu.Controls.Add(this.btnNhiemVu);
            this.panelAutoSubMenu.Controls.Add(this.btnLuotChay);
            this.panelAutoSubMenu.Controls.Add(this.btnUngDung);
            this.panelAutoSubMenu.Location = new System.Drawing.Point(-2, 131);
            this.panelAutoSubMenu.Name = "panelAutoSubMenu";
            this.panelAutoSubMenu.Size = new System.Drawing.Size(210, 86);
            this.panelAutoSubMenu.TabIndex = 3;
            this.panelAutoSubMenu.AutoSize = true;
            this.panelAutoSubMenu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.panelAutoSubMenu.Visible = false;
            this.panelAutoSubMenu.Margin = new Padding(10, 0, 0, 0);
            this.panelAutoSubMenu.BackColor = Color.Transparent;
            this.panelAutoSubMenu.Dock = DockStyle.Top;
            // 
            // btnNhiemVu
            // 
            this.btnNhiemVu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnNhiemVu.Location = new System.Drawing.Point(2, 57);
            this.btnNhiemVu.Name = "btnNhiemVu";
            this.btnNhiemVu.Size = new System.Drawing.Size(210, 28);
            this.btnNhiemVu.TabIndex = 2;
            this.btnNhiemVu.Text = "Nhiệm vụ";
            // 
            // btnLuotChay
            // 
            this.btnLuotChay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLuotChay.Location = new System.Drawing.Point(2, 29);
            this.btnLuotChay.Name = "btnLuotChay";
            this.btnLuotChay.Size = new System.Drawing.Size(210, 28);
            this.btnLuotChay.TabIndex = 1;
            this.btnLuotChay.Text = "Lượt chạy";
            // 
            // btnUngDung
            // 
            this.btnUngDung.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnUngDung.Location = new System.Drawing.Point(2, 1);
            this.btnUngDung.Name = "btnUngDung";
            this.btnUngDung.Size = new System.Drawing.Size(210, 28);
            this.btnUngDung.TabIndex = 0;
            this.btnUngDung.Text = "Ứng dụng";
            // 
            // btnStore
            // 
            this.btnStore.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnStore.Location = new System.Drawing.Point(-2, 217);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(210, 28);
            this.btnStore.TabIndex = 4;
            this.btnStore.Text = "Cửa hàng";
            // 
            // btnManagerApp
            // 
            this.btnManagerApp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnManagerApp.Location = new System.Drawing.Point(-2, 245);
            this.btnManagerApp.Name = "btnManagerApp";
            this.btnManagerApp.Size = new System.Drawing.Size(210, 28);
            this.btnManagerApp.TabIndex = 5;
            this.btnManagerApp.Text = "Quản lý ứng dụng";
            // 
            // btnManagerAccount
            // 
            this.btnManagerAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnManagerAccount.Location = new System.Drawing.Point(-2, 273);
            this.btnManagerAccount.Name = "btnManagerAccount";
            this.btnManagerAccount.Size = new System.Drawing.Size(210, 28);
            this.btnManagerAccount.TabIndex = 6;
            this.btnManagerAccount.Text = "Quản lý tài khoản";
            // 
            // btnLichTrinh
            // 
            this.btnLichTrinh.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLichTrinh.Location = new System.Drawing.Point(-2, 301);
            this.btnLichTrinh.Name = "btnLichTrinh";
            this.btnLichTrinh.Size = new System.Drawing.Size(210, 33);
            this.btnLichTrinh.TabIndex = 7;
            this.btnLichTrinh.Text = "Lịch trình";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnThanhToan.Location = new System.Drawing.Point(-2, 362);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(210, 28);
            this.btnThanhToan.TabIndex = 9;
            this.btnThanhToan.Text = "Thanh toán";
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCaiDat.Location = new System.Drawing.Point(-2, 390);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Size = new System.Drawing.Size(210, 28);
            this.btnCaiDat.TabIndex = 10;
            this.btnCaiDat.Text = "Cài đặt";
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnHelp.Location = new System.Drawing.Point(-2, 418);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(210, 28);
            this.btnHelp.TabIndex = 11;
            this.btnHelp.Text = "Trợ giúp - tài liệu";
            // 
            // panelSidebarContent
            // 
            this.panelSidebarContent.AutoSize = true;
            this.panelSidebarContent.BackColor = System.Drawing.Color.Transparent;
            this.panelSidebarContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSidebarContent.Location = new System.Drawing.Point(0, 0);
            this.panelSidebarContent.Name = "panelSidebarContent";
            this.panelSidebarContent.Size = new System.Drawing.Size(210, 0);
            this.panelSidebarContent.TabIndex = 0;
            this.panelSidebarContent.Dock = DockStyle.Top;
            this.panelSidebarContent.AutoSize = true;
            this.panelSidebarContent.BackColor = Color.Transparent;
            this.panelSidebarContent.Margin = new Padding(10, 0, 0, 0);
            this.panelSidebarContent.Padding = new Padding(8, 8, 8, 8);
            // 
            // gradientPanelSidebar
            // 
            this.gradientPanelSidebar.AutoScroll = true;
            this.gradientPanelSidebar.BackColor = System.Drawing.Color.LightGray;
            this.gradientPanelSidebar.Controls.Add(this.btnLogo);
            this.gradientPanelSidebar.Controls.Add(this.btnHelp);
            this.gradientPanelSidebar.Controls.Add(this.btnCaiDat);
            this.gradientPanelSidebar.Controls.Add(this.btnThanhToan);
            this.gradientPanelSidebar.Controls.Add(this.btnLichTrinh);
            this.gradientPanelSidebar.Controls.Add(this.btnManagerAccount);
            this.gradientPanelSidebar.Controls.Add(this.btnManagerApp);
            this.gradientPanelSidebar.Controls.Add(this.btnStore);
            this.gradientPanelSidebar.Controls.Add(this.panelAutoSubMenu);
            this.gradientPanelSidebar.Controls.Add(this.btnAuto);
            this.gradientPanelSidebar.Controls.Add(this.btnDieuKhien);
            this.gradientPanelSidebar.Controls.Add(this.panelSidebarContent);
            this.gradientPanelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.gradientPanelSidebar.Location = new System.Drawing.Point(0, 0);
            this.gradientPanelSidebar.Name = "gradientPanelSidebar";
            this.gradientPanelSidebar.Size = new System.Drawing.Size(214, 673);
            this.gradientPanelSidebar.TabIndex = 0;
            this.gradientPanelSidebar.BackColor = ColorTranslator.FromHtml("#F5F7FC");
            this.gradientPanelSidebar.Width = 240;
            // 
            // btnLogo
            // 
            this.btnLogo.Dock = System.Windows.Forms.DockStyle.Top;
           
            this.btnLogo.Location = new System.Drawing.Point(0, 0);
            this.btnLogo.Name = "btnLogo";
            this.btnLogo.Size = new System.Drawing.Size(210, 71);
            this.btnLogo.TabIndex = 0;
            this.btnLogo.Text = "Logo";
            this.btnLogo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnLogo.Height = 50;
            this.btnLogo.Style.BackColor = ColorTranslator.FromHtml("#F5F7FC");
            this.btnLogo.Style.ForeColor = Color.FromArgb(51, 51, 51);
            this.btnLogo.FlatStyle = FlatStyle.Flat;
            this.btnLogo.FlatAppearance.BorderSize = 0;
            // 
            // Home
            // 

            //
            //panelPlanInfo
            //
            this.panelPlanInfo.Height = 140;
            this.panelPlanInfo.Dock = DockStyle.Bottom;
            this.panelPlanInfo.BackColor = ColorTranslator.FromHtml("#5677FE");
            this.panelPlanInfo.Padding = new Padding(10);
            this.panelPlanInfo.Margin = new Padding(10);
            //
            //picPlan
            //
            this.picPlan.Image = Resources.updated;
            this.picPlan.Size = new Size(24, 24);
            this.picPlan.SizeMode = PictureBoxSizeMode.Zoom;
            this.picPlan.Location = new Point(10, 10);
            //
            //lblPlanTitle
            //
            lblPlanTitle.Text = "Default Plan";
            lblPlanTitle.ForeColor = Color.White;
            lblPlanTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPlanTitle.AutoSize = true;
            lblPlanTitle.Location = new Point(picPlan.Right + 8, picPlan.Top + 4);
            //
            //lblExpire
            //
            lblExpire.Text = "Expired at";
            lblExpire.ForeColor = Color.White;
            lblExpire.Font = new Font("Segoe UI", 8F);
            lblExpire.AutoSize = true;
            lblExpire.Location = new Point(10, 45);
            //
            //lblUnlimited
            //
            lblUnlimited.Text = "Unlimited";
            lblUnlimited.ForeColor = Color.White;
            lblUnlimited.Font = new Font("Segoe UI", 8F);
            lblUnlimited.AutoSize = true;
            //
            //btnUpgrade
            //
            btnUpgrade.Text = "↑  Upgrade";
            btnUpgrade.Height = 32;
            btnUpgrade.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnUpgrade.FlatStyle = FlatStyle.Flat;
            btnUpgrade.Style.BackColor = Color.White;
            btnUpgrade.Style.ForeColor = ColorTranslator.FromHtml("#5677FE");
            btnUpgrade.FlatAppearance.BorderSize = 0;

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelMainView);
            this.Controls.Add(this.gradientPanelSidebar);
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panelAutoSubMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanelSidebar)).EndInit();
            this.gradientPanelSidebar.ResumeLayout(false);
            this.gradientPanelSidebar.PerformLayout();
            this.ResumeLayout(false);


        }

        #endregion
        private System.Windows.Forms.Panel panelMainView;
        private Syncfusion.WinForms.Controls.SfButton btnAuto;
        private Syncfusion.WinForms.Controls.SfButton btnDieuKhien;
        private Syncfusion.WinForms.Controls.SfButton btnLichTrinh;
        private Syncfusion.WinForms.Controls.SfButton btnManagerAccount;
        private Syncfusion.WinForms.Controls.SfButton btnManagerApp;
        private Syncfusion.WinForms.Controls.SfButton btnStore;
        private System.Windows.Forms.Panel panelAutoSubMenu;
        private Syncfusion.WinForms.Controls.SfButton btnNhiemVu;
        private Syncfusion.WinForms.Controls.SfButton btnLuotChay;
        private Syncfusion.WinForms.Controls.SfButton btnUngDung;
        private Syncfusion.WinForms.Controls.SfButton btnHelp;
        private Syncfusion.WinForms.Controls.SfButton btnCaiDat;
        private Syncfusion.WinForms.Controls.SfButton btnThanhToan;
        private System.Windows.Forms.Panel panelSidebarContent;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanelSidebar;
        private Syncfusion.WinForms.Controls.SfButton btnLogo;
        private Panel panelPlanInfo;
        private PictureBox picPlan;
        private Label lblPlanTitle;
        private Label lblExpire;
        private Label lblUnlimited;
        private Syncfusion.WinForms.Controls.SfButton btnUpgrade;
    }
}