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
            this.panelPlanInfo = new System.Windows.Forms.Panel();
            this.picPlan = new System.Windows.Forms.PictureBox();
            this.lblPlanTitle = new System.Windows.Forms.Label();
            this.lblExpire = new System.Windows.Forms.Label();
            this.lblUnlimited = new System.Windows.Forms.Label();
            this.btnUpgrade = new Syncfusion.WinForms.Controls.SfButton();
            this.panelAutoSubMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanelSidebar)).BeginInit();
            this.gradientPanelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMainView
            // 
            this.panelMainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainView.Location = new System.Drawing.Point(240, 0);
            this.panelMainView.Name = "panelMainView";
            this.panelMainView.Size = new System.Drawing.Size(1022, 673);
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
            this.panelAutoSubMenu.AutoSize = true;
            this.panelAutoSubMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelAutoSubMenu.BackColor = System.Drawing.Color.Transparent;
            this.panelAutoSubMenu.Controls.Add(this.btnNhiemVu);
            this.panelAutoSubMenu.Controls.Add(this.btnLuotChay);
            this.panelAutoSubMenu.Controls.Add(this.btnUngDung);
            this.panelAutoSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAutoSubMenu.Location = new System.Drawing.Point(0, 16);
            this.panelAutoSubMenu.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelAutoSubMenu.Name = "panelAutoSubMenu";
            this.panelAutoSubMenu.Size = new System.Drawing.Size(236, 88);
            this.panelAutoSubMenu.TabIndex = 3;
            this.panelAutoSubMenu.Visible = false;
            // 
            // btnNhiemVu
            // 
            this.btnNhiemVu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnNhiemVu.Location = new System.Drawing.Point(2, 57);
            this.btnNhiemVu.Name = "btnNhiemVu";
            this.btnNhiemVu.Size = new System.Drawing.Size(210, 28);
            this.btnNhiemVu.TabIndex = 2;
            this.btnNhiemVu.Text = "Nhiệm vụ";
            this.btnNhiemVu.Visible = false;
            // 
            // btnLuotChay
            // 
            this.btnLuotChay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLuotChay.Location = new System.Drawing.Point(2, 29);
            this.btnLuotChay.Name = "btnLuotChay";
            this.btnLuotChay.Size = new System.Drawing.Size(210, 28);
            this.btnLuotChay.TabIndex = 1;
            this.btnLuotChay.Text = "Lượt chạy";
            this.btnLuotChay.Visible = false;
            // 
            // btnUngDung
            // 
            this.btnUngDung.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnUngDung.Location = new System.Drawing.Point(2, 1);
            this.btnUngDung.Name = "btnUngDung";
            this.btnUngDung.Size = new System.Drawing.Size(210, 28);
            this.btnUngDung.TabIndex = 0;
            this.btnUngDung.Text = "Ứng dụng";
            this.btnUngDung.Visible = false;
            // 
            // btnStore
            // 
            this.btnStore.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnStore.Location = new System.Drawing.Point(-2, 217);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(210, 28);
            this.btnStore.TabIndex = 4;
            this.btnStore.Text = "Cửa hàng";
            this.btnStore.Visible = false;
            // 
            // btnManagerApp
            // 
            this.btnManagerApp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnManagerApp.Location = new System.Drawing.Point(-2, 245);
            this.btnManagerApp.Name = "btnManagerApp";
            this.btnManagerApp.Size = new System.Drawing.Size(210, 28);
            this.btnManagerApp.TabIndex = 5;
            this.btnManagerApp.Text = "Quản lý ứng dụng";
            this.btnManagerApp.Visible = false;
            // 
            // btnManagerAccount
            // 
            this.btnManagerAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnManagerAccount.Location = new System.Drawing.Point(-2, 273);
            this.btnManagerAccount.Name = "btnManagerAccount";
            this.btnManagerAccount.Size = new System.Drawing.Size(210, 28);
            this.btnManagerAccount.TabIndex = 6;
            this.btnManagerAccount.Text = "Quản lý tài khoản";
            this.btnManagerAccount.Visible = false;
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
            this.btnCaiDat.Visible = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnHelp.Location = new System.Drawing.Point(-2, 418);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(210, 28);
            this.btnHelp.TabIndex = 11;
            this.btnHelp.Text = "Trợ giúp - tài liệu";
            this.btnHelp.Visible = false;
            // 
            // panelSidebarContent
            // 
            this.panelSidebarContent.AutoSize = true;
            this.panelSidebarContent.BackColor = System.Drawing.Color.Transparent;
            this.panelSidebarContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSidebarContent.Location = new System.Drawing.Point(0, 0);
            this.panelSidebarContent.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelSidebarContent.Name = "panelSidebarContent";
            this.panelSidebarContent.Padding = new System.Windows.Forms.Padding(8);
            this.panelSidebarContent.Size = new System.Drawing.Size(236, 16);
            this.panelSidebarContent.TabIndex = 0;
            // 
            // gradientPanelSidebar
            // 
            this.gradientPanelSidebar.AutoScroll = true;
            this.gradientPanelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
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
            this.gradientPanelSidebar.Size = new System.Drawing.Size(240, 673);
            this.gradientPanelSidebar.TabIndex = 0;
            // 
            // btnLogo
            // 
            this.btnLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogo.FlatAppearance.BorderSize = 0;
            this.btnLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogo.Location = new System.Drawing.Point(0, 104);
            this.btnLogo.Name = "btnLogo";
            this.btnLogo.Size = new System.Drawing.Size(236, 50);
            this.btnLogo.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.btnLogo.Style.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnLogo.TabIndex = 0;
            this.btnLogo.Text = "Logo";
            // 
            // panelPlanInfo
            // 
            this.panelPlanInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(119)))), ((int)(((byte)(254)))));
            this.panelPlanInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPlanInfo.Location = new System.Drawing.Point(0, 0);
            this.panelPlanInfo.Margin = new System.Windows.Forms.Padding(10);
            this.panelPlanInfo.Name = "panelPlanInfo";
            this.panelPlanInfo.Padding = new System.Windows.Forms.Padding(10);
            this.panelPlanInfo.Size = new System.Drawing.Size(200, 140);
            this.panelPlanInfo.TabIndex = 0;
            // 
            // picPlan
            // 
            this.picPlan.Image = global::WindowsFormsApp.Properties.Resources.updated;
            this.picPlan.Location = new System.Drawing.Point(10, 10);
            this.picPlan.Name = "picPlan";
            this.picPlan.Size = new System.Drawing.Size(24, 24);
            this.picPlan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPlan.TabIndex = 0;
            this.picPlan.TabStop = false;
            // 
            // lblPlanTitle
            // 
            this.lblPlanTitle.AutoSize = true;
            this.lblPlanTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPlanTitle.ForeColor = System.Drawing.Color.White;
            this.lblPlanTitle.Location = new System.Drawing.Point(34, 10);
            this.lblPlanTitle.Name = "lblPlanTitle";
            this.lblPlanTitle.Size = new System.Drawing.Size(100, 23);
            this.lblPlanTitle.TabIndex = 0;
            this.lblPlanTitle.Text = "Default Plan";
            // 
            // lblExpire
            // 
            this.lblExpire.AutoSize = true;
            this.lblExpire.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblExpire.ForeColor = System.Drawing.Color.White;
            this.lblExpire.Location = new System.Drawing.Point(10, 45);
            this.lblExpire.Name = "lblExpire";
            this.lblExpire.Size = new System.Drawing.Size(100, 23);
            this.lblExpire.TabIndex = 0;
            this.lblExpire.Text = "Expired at";
            // 
            // lblUnlimited
            // 
            this.lblUnlimited.AutoSize = true;
            this.lblUnlimited.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUnlimited.ForeColor = System.Drawing.Color.White;
            this.lblUnlimited.Location = new System.Drawing.Point(0, 0);
            this.lblUnlimited.Name = "lblUnlimited";
            this.lblUnlimited.Size = new System.Drawing.Size(100, 23);
            this.lblUnlimited.TabIndex = 0;
            this.lblUnlimited.Text = "Unlimited";
            // 
            // btnUpgrade
            // 
            this.btnUpgrade.FlatAppearance.BorderSize = 0;
            this.btnUpgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpgrade.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpgrade.Location = new System.Drawing.Point(0, 0);
            this.btnUpgrade.Name = "btnUpgrade";
            this.btnUpgrade.Size = new System.Drawing.Size(96, 32);
            this.btnUpgrade.Style.BackColor = System.Drawing.Color.White;
            this.btnUpgrade.Style.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(119)))), ((int)(((byte)(254)))));
            this.btnUpgrade.TabIndex = 0;
            this.btnUpgrade.Text = "↑  Upgrade";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelMainView);
            this.Controls.Add(this.gradientPanelSidebar);
            this.Name = "Home";
            this.Text = "Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            this.panelAutoSubMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanelSidebar)).EndInit();
            this.gradientPanelSidebar.ResumeLayout(false);
            this.gradientPanelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlan)).EndInit();
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