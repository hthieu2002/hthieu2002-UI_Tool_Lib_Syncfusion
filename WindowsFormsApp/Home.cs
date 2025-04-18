using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Properties;
using System.Windows.Forms;
using WindowsFormsApp;
using System.Drawing.Drawing2D;

namespace AccountCreatorForm.Views
{
    public partial class Home : Form
    {
        private readonly Color ColorBackground = ColorTranslator.FromHtml("#F5F7FC");
        private readonly Color colorNormalBack = Color.White;
        private readonly Color colorNormalText = Color.Black;
        private readonly Color ColorActiveBack = ColorTranslator.FromHtml("#5677FE");
        private readonly Color ColorActiveText = Color.White;
        private readonly Color colorHoverBack = ColorTranslator.FromHtml("#EDF0FE");
        private readonly Color colorHoverText = Color.FromArgb(51, 51, 51);

        private SfButton currentActiveButton = null;
        public Form currentChildForm = null;
        private Dictionary<SfButton, Func<Form>> sidebarFormMap;
        private Dictionary<SfButton, Image> buttonIconMap;
        private Dictionary<SfButton, Image> buttonIconWhiteMap;
        public Home()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ContextMenuStrip = GlobalContextMenu.ContextMenu;
            GlobalContextMenu.SetHomeForm(this);
            clickEvent();
            init();
            Form_Load();
            Form_Load_Icon();
            foreach (var pair in buttonIconMap)
            {
                StyleSidebarButtonWithIcon(pair.Key, pair.Value);
                pair.Key.Click += SidebarButton_Click;
            }
            // this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
        public void Form_Load()
        {
            sidebarFormMap = new Dictionary<SfButton, Func<Form>>
{
    { btnDieuKhien, () => new ViewChange() },
    { btnStore, () => new ViewCuaHang() },
    { btnLichTrinh, () => new ScreenView() },
    //{ btnManagerApp, () => new ViewQuanLyUngDung() },
    //{ btnManagerAccount, () => new ViewQuanLyTaiKhoan() },
    { btnThanhToan, () => new ViewThanhToan() },
    { btnUngDung, () => new ViewUngDung() },
   /* { btnLuotChay, () => new ViewLuotChay() },
    { btnNhiemVu, () => new ViewNhiemVU() },
    { btnLichTrinh, () => new ViewLichTrinh() },*/

};

        }
        public void Form_Load_Icon()
        {
            buttonIconMap = new Dictionary<SfButton, Image>
{
    { btnDieuKhien, Resources.dieukhien_0 },
    { btnAuto, Resources.automation_0 },
    { btnUngDung, Resources.app_0 },
    { btnLuotChay, Resources.luotchay_0 },
    { btnNhiemVu, Resources.nhiemvu_0 },
    { btnStore, Resources.cuahang_0 },
    { btnManagerApp, Resources.quanlyungdung_0 },
    { btnManagerAccount, Resources.quanlynguoidung_0 },
    { btnLichTrinh, Resources.lichtrinh_0 },
    { btnThanhToan, Resources.thanhtoan_0 },
    { btnCaiDat, Resources.setting_0 },
    { btnHelp, Resources.help_0 }
};
        }
        public void init()
        {
            panelAutoSubMenu.AutoSize = true;
            panelAutoSubMenu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelAutoSubMenu.Visible = false;
            panelAutoSubMenu.Margin = new Padding(10, 0, 0, 0);
            panelAutoSubMenu.BackColor = Color.Transparent;
            panelAutoSubMenu.Dock = DockStyle.Top;

            panelSidebarContent.Dock = DockStyle.Top;
            panelSidebarContent.AutoSize = true;
            panelSidebarContent.BackColor = Color.Transparent;
            panelSidebarContent.Margin = new Padding(10, 0, 0, 0);
            panelSidebarContent.Padding = new Padding(10, 5, 10, 0);
            var sidebarButtons = new List<Control>
{

    btnThanhToan,
    btnManagerAccount,
    btnManagerApp,
    btnHelp,
    btnCaiDat,
    btnStore,
    btnLichTrinh,
    panelAutoSubMenu,
    btnAuto,
    btnDieuKhien,
    btnLogo
};

            foreach (var btn in sidebarButtons)
            {
                panelSidebarContent.Controls.Add(btn);
            }

            panelSidebarContent.Padding = new Padding(8, 8, 8, 8);

            gradientPanelSidebar.BackColor = ColorBackground;
            gradientPanelSidebar.Width = 240;
            //
            btnLogo.Text = "LOGO";
            btnLogo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogo.Dock = DockStyle.Top;
            btnLogo.Height = 50;

            btnLogo.Style.BackColor = ColorTranslator.FromHtml("#F5F7FC");
            btnLogo.Style.ForeColor = Color.FromArgb(51, 51, 51);

            btnLogo.FlatStyle = FlatStyle.Flat;
            btnLogo.FlatAppearance.BorderSize = 0;

            //
            StyleSidebarButton(btnDieuKhien);
            StyleSidebarButton(btnAuto);
            StyleSidebarButton(btnUngDung);
            StyleSidebarButton(btnLuotChay);
            StyleSidebarButton(btnNhiemVu);
            StyleSidebarButton(btnStore);
            StyleSidebarButton(btnManagerAccount);
            StyleSidebarButton(btnManagerApp);
            StyleSidebarButton(btnLichTrinh);
            StyleSidebarButton(btnThanhToan);
            StyleSidebarButton(btnCaiDat);
            StyleSidebarButton(btnHelp);
            //
            btnDieuKhien.Text = "Devices";
            btnAuto.Text = "Automation";
            btnLichTrinh.Text = "View Screen";

            panelAutoSubMenu.Visible = false;
            btnManagerApp.Visible = false;
            btnManagerAccount.Visible = false;
            btnUngDung.Visible = false;
            btnLuotChay.Visible = false;
            btnNhiemVu.Visible = false;

            StyleSidebarChildButton(btnUngDung);
            StyleSidebarChildButton(btnLuotChay);
            StyleSidebarChildButton(btnNhiemVu);


            AddPlanInfoToSidebar();

        }
        private void AddPlanInfoToSidebar()
        {
            var panelPlanInfo = new Panel
            {
                Height = 140,
                Dock = DockStyle.Bottom,
                BackColor = ColorTranslator.FromHtml("#5677FE"),
                Padding = new Padding(10),
                Margin = new Padding(10)
            };
            var picPlan = new PictureBox
            {
                Image = Resources.updated,
                Size = new Size(24, 24),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, 10)
            };

            var lblPlanTitle = new Label
            {
                Text = "Default Plan",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(picPlan.Right + 8, picPlan.Top + 4)
            };

            var lblExpire = new Label
            {
                Text = "Expired at",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F),
                AutoSize = true,
                Location = new Point(10, 45)
            };

            var lblUnlimited = new Label
            {
                Text = "Unlimited",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8F),
                AutoSize = true
            };

            var btnUpgrade = new SfButton
            {
                Text = "↑  Upgrade",
                Height = 32,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat
            };
            btnUpgrade.Style.BackColor = Color.White;
            btnUpgrade.Style.ForeColor = ColorTranslator.FromHtml("#5677FE");
            btnUpgrade.FlatAppearance.BorderSize = 0;

            btnUpgrade.Click += (s, e) =>
            {
                SetActiveSidebarButton(btnThanhToan);
                if (sidebarFormMap.TryGetValue(btnThanhToan, out var formFactory))
                {
                    OpenChildForm(formFactory());
                }
            };

            panelPlanInfo.Controls.Add(picPlan);
            panelPlanInfo.Controls.Add(lblPlanTitle);
            panelPlanInfo.Controls.Add(lblExpire);
            panelPlanInfo.Controls.Add(lblUnlimited);
            panelPlanInfo.Controls.Add(btnUpgrade);
            panelPlanInfo.Resize += (s, e) =>
            {
                lblUnlimited.Location = new Point(
                    panelPlanInfo.ClientSize.Width - lblUnlimited.PreferredWidth - 10,
                    lblExpire.Top
                );

                btnUpgrade.Width = panelPlanInfo.ClientSize.Width - 20;
                btnUpgrade.Location = new Point(10, 75);
            };

            panelPlanInfo.PerformLayout();
            panelPlanInfo.Width += 1; panelPlanInfo.Width -= 1;

            panelSidebarContent.Controls.Add(panelPlanInfo);
            panelSidebarContent.Controls.SetChildIndex(panelPlanInfo, panelSidebarContent.Controls.Count - 1);
        }


        public void clickEvent()
        {
            btnDieuKhien.Click += SidebarButton_Click;
            btnAuto.Click += SidebarButton_Click;
            btnUngDung.Click += SidebarButton_Click;
            btnLuotChay.Click += SidebarButton_Click;
            btnNhiemVu.Click += SidebarButton_Click;
            btnStore.Click += SidebarButton_Click;
            btnManagerAccount.Click += SidebarButton_Click;
            btnManagerApp.Click += SidebarButton_Click;
            btnLichTrinh.Click += SidebarButton_Click;
            btnThanhToan.Click += SidebarButton_Click;
            btnCaiDat.Click += SidebarButton_Click;
            btnHelp.Click += SidebarButton_Click;

            SetButtonHover();
        }

        private void SetButtonHover()
        {
            Button[] buttons = new Button[]
            {
        btnDieuKhien, btnAuto, btnUngDung, btnLuotChay, btnNhiemVu,
        btnStore, btnManagerAccount, btnManagerApp, btnLichTrinh,
        btnThanhToan, btnCaiDat, btnHelp
            };

            foreach (var button in buttons)
            {
                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;
                button.Paint += button_Paint;
            }
        }

        private void StyleSidebarButtonWithIcon(SfButton btn, Image icon)
        {
            btn.Image = icon;
            btn.ImageSize = new Size(20, 20);
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.ImageMargin = new Padding(8, 0, 5, 0);
            btn.AutoSize = false;
        }

        private void StyleSidebarButton(Syncfusion.WinForms.Controls.SfButton button)
        {
            button.Width = 150;
            button.Height = 44;
            button.Dock = DockStyle.Top;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Style.BackColor = colorNormalBack;
            button.Style.ForeColor = colorNormalText;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(12, 0, 0, 0);
            button.Margin = new Padding(0, 4, 0, 0);
            button.Style.BackColor = colorNormalBack;
            button.Style.ForeColor = colorNormalText;
            button.Style.HoverBackColor = colorHoverBack;
            button.Style.HoverForeColor = colorHoverText;
            button.Style.PressedBackColor = colorHoverBack;
            button.Style.PressedForeColor = colorHoverText;
            button.MouseEnter += (s, e) =>
            {
                if (button != currentActiveButton)
                    button.Style.BackColor = colorHoverBack;
            };

            button.MouseLeave += (s, e) =>
            {
                if (button != currentActiveButton)
                    button.Style.BackColor = colorNormalBack;
            };
        }

        private void StyleSidebarChildButton(SfButton button)
        {
            StyleSidebarButton(button);
            button.Padding = new Padding(30, 0, 0, 0);
        }
        private void SetActiveSidebarButton(SfButton btn)
        {
            if (currentActiveButton != null && currentActiveButton != btn)
            {
                ResetSidebarButton(currentActiveButton);
            }

            btn.Style.BackColor = ColorActiveBack;
            btn.Style.ForeColor = ColorActiveText;

            btn.Style.HoverBackColor = ColorActiveBack;
            btn.Style.HoverForeColor = ColorActiveText;

            btn.Style.PressedBackColor = ColorActiveBack;
            btn.Style.PressedForeColor = ColorActiveText;

            currentActiveButton = btn;
        }
        private void ResetSidebarButton(SfButton btn)
        {
            btn.Style.BackColor = colorNormalBack;
            btn.Style.ForeColor = colorNormalText;
            btn.Style.HoverBackColor = colorHoverBack;
            btn.Style.HoverForeColor = colorNormalText;
            btn.Style.PressedBackColor = colorHoverBack;
            btn.Style.PressedForeColor = colorNormalText;
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Hide();  
            }

            currentChildForm = childForm;
            currentChildForm.TopLevel = false;
            currentChildForm.FormBorderStyle = FormBorderStyle.None;
            currentChildForm.Dock = DockStyle.Fill;

            panelMainView.Controls.Clear();
            panelMainView.Controls.Add(childForm);
            childForm.Show();
            GlobalContextMenu.SetHomeForm(this);
        }

        private void SidebarButton_Click(object sender, EventArgs e)
        {
            if (sender is SfButton btn)
            {
                SetActiveSidebarButton(btn);

                if (sidebarFormMap.TryGetValue(btn, out var formFactory))
                {
                    var childForm = formFactory();
                    if (currentChildForm != null && currentChildForm.GetType() == childForm.GetType())
                    {
                        currentChildForm.BringToFront(); 
                        return;
                    }

                    OpenChildForm(childForm);
                }
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            panelAutoSubMenu.Visible = !panelAutoSubMenu.Visible;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {

                btn.Cursor = Cursors.Hand;
            }
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {

                btn.Cursor = Cursors.Default;
            }
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.ShowDialog();
        }

        private void button_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
