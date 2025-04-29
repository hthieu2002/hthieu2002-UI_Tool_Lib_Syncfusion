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
using WindowsFormsApp.Model;

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
            ToggleDarkLightMode();
            this.WindowState = FormWindowState.Maximized;
            //  this.Size = new Size(1600, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimumSize = new Size(1600, 750);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ContextMenuStrip = GlobalContextMenu.ContextMenu;
            GlobalContextMenu.SetHomeForm(this);
            clickEvent();
            init();
            Form_Load();
            Form_Load_Icon();
            this.Resize += MyForm_OnSizeChange;
            this.SizeChanged += MyForm_OnSizeChange;
            UpdateWindowMode();
            foreach (var pair in buttonIconMap)
            {
                StyleSidebarButtonWithIcon(pair.Key, pair.Value);
                pair.Key.Click += SidebarButton_Click;

            }
            loadingPanel.Visible = false;
            // this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
        private void MyForm_OnSizeChange(object sender, EventArgs e)
        {
            UpdateWindowMode();
        }

        // hoặc thay bằng override OnResize
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateWindowMode();
        }
        private void UpdateWindowMode()
        {
            var newMode = this.GetWindowMode();
            if (newMode != AppState.CurrentWindowMode)
            {
                AppState.CurrentWindowMode = newMode;
            }
        }
        private void ToggleDarkLightMode()
        {
            ThemeManager.ToggleDarkMode();
            ThemeManager.ApplyTheme();
        }
        //private void SetupSidebarButtons()
        //{
        //    foreach (var pair in sidebarFormMap)
        //    {
        //        pair.Key.Click += SidebarButtonLoading_Click;
        //    }
        //}
        //private async void SidebarButtonLoading_Click(object sender, EventArgs e)
        //{
        //    var btn = sender as SfButton;
        //    if (btn != null && sidebarFormMap.ContainsKey(btn))
        //    {
        //        var viewFactory = sidebarFormMap[btn];

        //        await LoadViewWithLoading(viewFactory);  // ⚡⚡ GỌI Ở ĐÂY!
        //    }
        //}
        //private async Task LoadViewWithLoading(Func<Form> viewFactory)
        //{
        //    ShowLoadingIndicator(true);                  // Bật loading
        //    await Task.Delay(100);                       // Cho loading hiển thị kịp

        //    Form view = null;

        //    // Khởi tạo View (không bị block UI)
        //    await Task.Run(() =>
        //    {
        //        view = viewFactory();
        //    });

        //    ShowLoadingIndicator(false);                 

        //    LoadView(view);                              
        //}
        private void LoadView(Form view)
        {
            panelMainView.Controls.Clear();
            view.TopLevel = false;
            view.FormBorderStyle = FormBorderStyle.None;
            view.Dock = DockStyle.Fill;
            panelMainView.Controls.Add(view);
            view.Show();
        }
        private void ShowLoadingIndicator(bool show)
        {
            loadingPanel.Visible = show;                
            loadingPanel.BringToFront();
            loadingPanel.Refresh();                    
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
    { btnAuto, () => new ViewAutomation() },
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
            btnStore.Visible = false;
            btnCaiDat.Visible = false;
            btnHelp.Visible = false;

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

            // Màu sắc cho chế độ Dark Mode và Light Mode
            Color backgroundColor = ThemeManager.IsDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;  // Màu nền
            Color textColor = ThemeManager.IsDarkMode ? Color.White : Color.Black;  // Màu chữ
            Color hoverBackColor = ThemeManager.IsDarkMode ? Color.FromArgb(67, 67, 70) : Color.FromArgb(240, 240, 240);  // Màu khi hover

            // Thiết lập màu sắc gốc cho button
            button.Style.BackColor = colorNormalBack;
            button.Style.ForeColor = colorNormalText;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(12, 0, 0, 0);
            button.Margin = new Padding(0, 4, 0, 0);

            // Thiết lập màu sắc cho hover và khi nhấn
            button.Style.HoverBackColor = hoverBackColor;
            button.Style.HoverForeColor = textColor;
            button.Style.PressedBackColor = hoverBackColor;
            button.Style.PressedForeColor = textColor;

            // Thêm hiệu ứng hover khi di chuột qua button
            button.MouseEnter += (s, e) =>
            {
                if (button != currentActiveButton)
                {
                    button.Style.BackColor = hoverBackColor;
                    button.Style.ForeColor = textColor;
                }
            };

            // Hiệu ứng khi di chuột ra khỏi button
            button.MouseLeave += (s, e) =>
            {
                if (button != currentActiveButton)
                {
                    button.Style.BackColor = colorNormalBack;  // Quay lại màu nền mặc định
                    button.Style.ForeColor = colorNormalText;  // Quay lại màu chữ mặc định
                }
            };
        }



        //private void StyleSidebarButton(Syncfusion.WinForms.Controls.SfButton button)
        //{
        //    button.Width = 150;
        //    button.Height = 44;
        //    button.Dock = DockStyle.Top;
        //    button.FlatStyle = FlatStyle.Flat;
        //    button.FlatAppearance.BorderSize = 0;

        //    Color backgroundColor = ThemeManager.IsDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
        //    Color textColor = ThemeManager.IsDarkMode ? Color.White : Color.Black;
        //    Color hoverBackColor = ThemeManager.IsDarkMode ? Color.FromArgb(67, 67, 70) : Color.FromArgb(240, 240, 240);

        //    button.Style.BackColor = colorNormalBack;
        //    button.Style.ForeColor = colorNormalText;
        //    button.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        //    button.TextAlign = ContentAlignment.MiddleLeft;
        //    button.Padding = new Padding(12, 0, 0, 0);
        //    button.Margin = new Padding(0, 4, 0, 0);
        //    button.Style.BackColor = colorNormalBack;
        //    button.Style.ForeColor = colorNormalText;
        //    button.Style.HoverBackColor = colorHoverBack;
        //    button.Style.HoverForeColor = colorHoverText;
        //    button.Style.PressedBackColor = colorHoverBack;
        //    button.Style.PressedForeColor = colorHoverText;


        //    button.MouseEnter += (s, e) =>
        //    {
        //        if (button != currentActiveButton)
        //            button.Style.BackColor = hoverBackColor;
        //        // button.Style.ForeColor = textColor;
        //    };

        //    button.MouseLeave += (s, e) =>
        //    {
        //        if (button != currentActiveButton)
        //            button.Style.BackColor = colorNormalBack;
        //    };
        //}

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
