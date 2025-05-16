using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp;
using WindowsFormsApp.Model;
using WindowsFormsApp.Properties;

namespace AccountCreatorForm.Views
{
    public partial class Home : Form
    {
        private List<Form> currentChildForms = new List<Form>();
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
        private void AddPlanInfoToSidebar()
        {
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
            }
        }
        private void StyleSidebarButton(Syncfusion.WinForms.Controls.SfButton button)
        {
            button.Width = 150;
            button.Height = 44;
            button.Dock = DockStyle.Top;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            Color backgroundColor = ThemeManager.IsDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;  // Màu nền
            Color textColor = ThemeManager.IsDarkMode ? Color.White : Color.Black;  // Màu chữ
            Color hoverBackColor = ThemeManager.IsDarkMode ? Color.FromArgb(67, 67, 70) : Color.FromArgb(240, 240, 240);
            button.Style.BackColor = colorNormalBack;
            button.Style.ForeColor = colorNormalText;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(12, 0, 0, 0);
            button.Margin = new Padding(0, 4, 0, 0);

            button.Style.HoverBackColor = hoverBackColor;
            button.Style.HoverForeColor = textColor;
            button.Style.PressedBackColor = hoverBackColor;
            button.Style.PressedForeColor = textColor;

            button.MouseEnter += (s, e) =>
            {
                if (button != currentActiveButton)
                {
                    button.Style.BackColor = hoverBackColor;
                    button.Style.ForeColor = textColor;
                }
            };

            button.MouseLeave += (s, e) =>
            {
                if (button != currentActiveButton)
                {
                    button.Style.BackColor = colorNormalBack;
                    button.Style.ForeColor = colorNormalText;
                }
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
        private async void OpenChildForm(Form childForm)
        {
            var existingForm = currentChildForms.FirstOrDefault(f => f.GetType() == childForm.GetType());

            if (existingForm != null)
            {
                if (currentChildForm != null)
                {
                    currentChildForm.Hide();
                }

                currentChildForm = existingForm;

                currentChildForm.TopLevel = false;
                currentChildForm.FormBorderStyle = FormBorderStyle.None;
                currentChildForm.Dock = DockStyle.Fill;

                panelMainView.Controls.Clear();
                panelMainView.Controls.Add(currentChildForm);

                currentChildForm.Show();
            }
            else
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
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        var loading = new Loading();
                        loading.Dock = DockStyle.Fill;
                        panelMainView.Controls.Add(loading);
                        loading.BringToFront();


                        childForm.Shown += (sender, e) =>
                        {
                            panelMainView.Controls.Remove(loading);
                        };
                        if (!(childForm is ScreenView))
                        {
                            currentChildForms.Add(childForm);
                        }

                        childForm.Show();
                        GlobalContextMenu.SetHomeForm(this);
                    });
                });
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
    }
}
