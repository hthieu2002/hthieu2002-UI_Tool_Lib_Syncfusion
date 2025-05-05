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

namespace AccountCreatorForm
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

        private Dictionary<Type, Form> formInstances = new Dictionary<Type, Form>();
        private ViewChange viewChangeInstance;

        public Home()
        {
            InitializeComponent();
            ToggleDarkLightMode();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1600, 750);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ContextMenuStrip = GlobalContextMenu.ContextMenu;
            this.Resize += MyForm_OnSizeChange;
            this.SizeChanged += MyForm_OnSizeChange;
            this.LocationChanged += MyForm_OnSizeChange;
            this.ClientSizeChanged += MyForm_OnSizeChange;
            GlobalContextMenu.SetHomeForm(this);
            ///
            ///
            ///
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
            ///
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

            init();
            Form_Load();
            Form_Load_Icon();
            UpdateWindowMode();
            foreach (var pair in buttonIconMap)
            {
                StyleSidebarButtonWithIcon(pair.Key, pair.Value);
                pair.Key.Click += SidebarButton_Click;
            }
        }
        private void MyForm_OnSizeChange(object sender, EventArgs e)
        {
            UpdateWindowMode();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateWindowMode();
        }
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            UpdateWindowMode();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            UpdateWindowMode();
        }

        public void Form_Load()
        {
            sidebarFormMap = new Dictionary<SfButton, Func<Form>>
            {
                { btnDieuKhien, () => new ViewChange() },
                { btnLichTrinh, () => new ScreenView() },
                { btnThanhToan, () => new ViewThanhToan() },
                { btnAuto, () => new ViewAutomation() },
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
           
            //
            btnDieuKhien.Text = "Devices";
            btnAuto.Text = "Automation";
            btnLichTrinh.Text = "View Screen";

            

            StyleSidebarChildButton(btnUngDung);
            StyleSidebarChildButton(btnLuotChay);
            StyleSidebarChildButton(btnNhiemVu);

            AddPlanInfoToSidebar();
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
       
    }
}
