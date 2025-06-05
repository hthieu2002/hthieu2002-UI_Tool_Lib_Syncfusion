using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Properties;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Principal;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using WindowsFormsApp.Model;
using System.IO;
using System.Net.Http;

namespace AccountCreatorForm.Controls
{
    public class HeaderViewCommon : UserControl
    {
        private Label lblTitle;
        private RichTextBox lblVersion;
        private Button btnRuns;
        private Button btnInspector;
        private PictureBox picAvatar;
        private SfButton button;
        private TableLayoutPanel layoutMain;
        private bool isDarkMode = false;
        private Panel accountPanel;
        private PictureBox picArrow;
        private ComboBox cbLanguage;
        public HeaderViewCommon()
        {
            init();
            this.Resize += HeaderViewCommon_Resize;
        }

        private void init()
        {
            layoutMain = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 6,
                RowCount = 1,
                Height = 80,
                BackColor = Color.Transparent
            };
            layoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));  // Tỷ lệ cho cột 1
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));  // Tỷ lệ cho cột 2
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            setTitle();
            //setVersion();
            setRun();
            setInspector();
          //  setDrakMode();
            setAccount();

            layoutMain.Controls.Add(lblTitle, 0, 0);
           // layoutMain.Controls.Add(lblVersion, 1, 0);
            layoutMain.Controls.Add(btnRuns, 2, 0);
            layoutMain.Controls.Add(cbLanguage, 3, 0);
          //  layoutMain.Controls.Add(button, 4, 0);
            layoutMain.Controls.Add(accountPanel, 5, 0);

            this.Controls.Add(layoutMain);
            UpdateControlPositions();
        }


        private void setTitle()
        {
            lblTitle = new Label
            {
                Text = "Tiêu đề",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = true,
                Margin = new Padding(10, 0, 0, 0)  // Căn lề trái
            };
        }
        private void setVersion()
        {
            lblVersion = new RichTextBox
            {
             //   Width = 400,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Text = "Version 1.1.0 is available - Update now ?",
                WordWrap = false, // Tự động xuống dòng khi văn bản dài
                AutoSize = true, // Tự động thay đổi kích thước
                BackColor = Color.Transparent
            };
            lblVersion.Width = TextRenderer.MeasureText(lblVersion.Text, lblVersion.Font).Width + 20;
            lblVersion.SelectionAlignment = HorizontalAlignment.Center;

            lblVersion.Select(8, 5);
            lblVersion.SelectionFont = new Font(lblVersion.Font, FontStyle.Bold);

            lblVersion.Select(33, 11);
            lblVersion.SelectionFont = new Font(lblVersion.Font, FontStyle.Bold);

            lblVersion.SelectAll();
            lblVersion.BackColor = Color.White;

            lblVersion.Click += (sender, e) =>
            {
                MessageBox.Show("Link clicked: Version 1.1.0");
            };
        }
        private void setRun()
        {
            btnRuns = new Button
            {
                Text = "🎚 Runs",
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(86, 119, 254),
                Width = 80,
                Height = 28
            };
            btnRuns.FlatAppearance.BorderColor = Color.FromArgb(86, 119, 254);
            btnRuns.FlatAppearance.BorderSize = 1;
            btnRuns.Margin = new Padding(5, 0, 5, 0); 
        }
        private void setInspector()
        {
             cbLanguage = new ComboBox
            {
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(86, 119, 254),
                Width = 120,
                Height = 28,
                DropDownStyle = ComboBoxStyle.DropDownList // Chỉ cho phép chọn, không nhập
            };

            cbLanguage.Items.Add("English");
            cbLanguage.Items.Add("Tiếng Việt");
            if (WindowsFormsApp.Properties.Settings.Default.Language == "en")
            {
                cbLanguage.SelectedIndex = 0;
            }
            else
            {
                cbLanguage.SelectedIndex = 1;

            }


                //btnInspector.FlatAppearance.BorderColor = Color.FromArgb(86, 119, 254);
                // btnInspector.FlatAppearance.BorderSize = 1;
                cbLanguage.Margin = new Padding(5, 0, 5, 0);

            cbLanguage.SelectedIndexChanged += CbLanguage_SelectedIndexChanged;

        }
        private void CbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem.ToString() == "English")
            {
                FormVisibilityManager.IsLanguage = "en";
                WindowsFormsApp.Properties.Settings.Default.Language = "en";  
            }
            else
            {
                FormVisibilityManager.IsLanguage = "vi";
                WindowsFormsApp.Properties.Settings.Default.Language = "vi"; 
            }

            WindowsFormsApp.Properties.Settings.Default.Save(); 
            CentralBridgeLanguage.Language();   

        }
        private void setDrakMode()
        {
            button = new SfButton()
            {
                Text = "",
                Image = Resources.light_mode,
                Size = new System.Drawing.Size(Resources.light_mode.Width, Resources.light_mode.Height),
            };
            button.Margin = new Padding(5, 0, 5, 0); 
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ThemeManager.ToggleDarkMode();
        }

        private void setAccount()
        {
            accountPanel = new Panel
            {
                Size = new Size(250, 40),  
                BackColor = Color.Transparent
            };

            // Tạo PictureBox (Avatar)
            picAvatar = new PictureBox
            {
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent,
                //Image = Resources.cuahang_0 
            };
            SetImageFromUrl("https://www.pngall.com/wp-content/uploads/5/User-Profile-PNG-Free-Download.png");
            Label lblEmail = new Label
            {
                Text = "admin@gmail.com",
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(10, 30, 0, 0),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = true
            };

            lblEmail.Location = new Point(picAvatar.Right + 10, picAvatar.Top + 10); 

            picArrow = new PictureBox
            {
                Size = new Size(10, 10),  
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Image = Resources.arrow_down,
                Location = new Point(lblEmail.Right + 5, picAvatar.Top)  
            };

            // Thêm PictureBox và Label vào Panel
            accountPanel.Controls.Add(picAvatar);
            accountPanel.Controls.Add(lblEmail);
            accountPanel.Controls.Add(picArrow);
        }
        private async void SetImageFromUrl(string imageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Tải hình ảnh từ URL
                    byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                    // Chuyển đổi byte array thành Image
                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {
                        Bitmap bitmap = new Bitmap(Image.FromStream(stream));

                        //Color white = Color.Red; 
                        //bitmap.MakeTransparent(white); // Biến màu trắng thành trong suốt

                        picAvatar.Image = bitmap;  // Gán hình ảnh đã xử lý vào PictureBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HeaderViewCommon_Resize(object sender, EventArgs e)
        {
            UpdateControlPositions();
        }

        private void UpdateControlPositions()
        {
           // lblVersion.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            btnRuns.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            cbLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Top;
          //  button.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            accountPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top;

           // lblVersion.Margin = new Padding(0, 10, 10, 0);  // Khoảng cách giữa avatar và các nút
            btnRuns.Margin = new Padding(0, 10, 10, 0); 
            cbLanguage.Margin = new Padding(0, 10, 10, 0); 
         //   button.Margin = new Padding(0, 10, 10, 0);  
            accountPanel.Margin = new Padding(0, 10, 10, 0); 
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        private void ToggleDarkLightMode()
        {
            isDarkMode = !isDarkMode;

            // Thay đổi màu nền của HeaderViewCommon
            this.BackColor = isDarkMode ? Color.FromArgb(33, 33, 33) : Color.White;

            // Thay đổi màu nền của TableLayoutPanel
            layoutMain.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;

            // Thay đổi màu sắc của các nút (button)
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    control.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
                    control.ForeColor = isDarkMode ? Color.White : Color.Black;
                }
                else if (control is Label)
                {
                    control.ForeColor = isDarkMode ? Color.White : Color.Black;
                }
                else if (control is TextBox || control is RichTextBox)
                {
                    control.BackColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.White;
                    control.ForeColor = isDarkMode ? Color.White : Color.Black;
                }
                else if (control is PictureBox)
                {
                    control.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
                }
            }

            // Cập nhật hình ảnh của các nút chế độ sáng/tối
            button.Image = isDarkMode ? Resources.dark_mode : Resources.light_mode;

            // Cập nhật avatar và các biểu tượng trong accountPanel
            picAvatar.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
            picArrow.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
            accountPanel.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
        }
    }
}
