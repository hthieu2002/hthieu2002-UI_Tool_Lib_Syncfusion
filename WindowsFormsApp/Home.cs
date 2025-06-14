﻿using Syncfusion.WinForms.Controls;
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
using System.Threading;

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
        private bool isOkClicked = false;    
        private bool isCancelClicked = false; 
        private LanguageManager lang ;
        private string _info = "Notice";
        private string _closeHome = "Are you want close program?";
        public static Home Instance { get; private set; }
        public Home()
        {
            InitializeComponent();
            Instance = this;
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

        private void btnAuto_Click(object sender, EventArgs e)
        {
            panelAutoSubMenu.Visible = !panelAutoSubMenu.Visible;
        }
        private void Home_Load(object sender, EventArgs e)
        {
            LoadLanguageHome();

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        public void LoadLanguageHome()
        {
            lang = new LanguageManager(FormVisibilityManager.IsLanguage);

            btnDieuKhien.Text = lang.Get("device");
            btnAuto.Text = lang.Get("auto");
            btnLichTrinh.Text = lang.Get("view");
            btnThanhToan.Text = lang.Get("payment");
            _info = lang.Get("info");
            _closeHome = lang.Get("closeHome");
            btnThanhToan.Visible = false;
            panelPlanInfo.Visible = false;
            //
            btnUpgrade.Text = "↑ " + lang.Get("upgrade");
           // lblUnlimited.Text = lang.Get("unlimited");
            lblExpire.Text = lang.Get("expired");
            lblPlanTitle.Text = lang.Get("defaultPlan");
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

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isOkClicked && !isCancelClicked)
            {
                var confirm = MessageBox.Show(_closeHome, _info, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (confirm == DialogResult.OK)
                {
                    isOkClicked = true;  
                    e.Cancel = false;     
                }
                else
                {
                    isCancelClicked = true;
                    e.Cancel = true;
                }
            }
            else if (isOkClicked)
            {
                e.Cancel = false;
                isOkClicked = false;
            }
            else if (isCancelClicked)
            {
                e.Cancel = true;
                isCancelClicked = false;
            }
        }
    }
}
