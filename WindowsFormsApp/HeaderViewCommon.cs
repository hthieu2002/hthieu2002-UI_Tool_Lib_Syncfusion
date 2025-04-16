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
            setDrakMode();
            setAccount();

            layoutMain.Controls.Add(lblTitle, 0, 0);
           // layoutMain.Controls.Add(lblVersion, 1, 0);
            layoutMain.Controls.Add(btnRuns, 2, 0);
            layoutMain.Controls.Add(btnInspector, 3, 0);
            layoutMain.Controls.Add(button, 4, 0);
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
            btnRuns.Margin = new Padding(5, 0, 5, 0);  // Thêm khoảng cách giữa các nút
        }
        private void setInspector()
        {
            btnInspector = new Button
            {
                Text = "✨ Inspector",
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(86, 119, 254),
                Width = 90,
                Height = 28
            };
            btnInspector.FlatAppearance.BorderColor = Color.FromArgb(86, 119, 254);
            btnInspector.FlatAppearance.BorderSize = 1;
            btnInspector.Margin = new Padding(5, 0, 5, 0);  // Thêm khoảng cách giữa các nút
        }
        private void setDrakMode()
        {
            button = new SfButton()
            {
                Text = "",
                Image = Resources.light_mode,
                Size = new System.Drawing.Size(Resources.light_mode.Width, Resources.light_mode.Height),
            };
            button.Margin = new Padding(5, 0, 5, 0);  // Thêm khoảng cách giữa các nút
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ToggleDarkLightMode();
        }

        private void setAccount()
        {
            accountPanel = new Panel
            {
                Size = new Size(250, 40),  // Đặt kích thước cho panel
                BackColor = Color.Transparent
            };

            // Tạo PictureBox (Avatar)
            picAvatar = new PictureBox
            {
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent,
                Image = Resources.cuahang_0 // Đảm bảo có ảnh trong tài nguyên
            };

            // Tạo Label để hiển thị email
            Label lblEmail = new Label
            {
                Text = "blua@gmail.com",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = true
            };

            // Đặt vị trí của Label cạnh PictureBox (hình trước chữ)
            lblEmail.Location = new Point(picAvatar.Right + 10, picAvatar.Top); // Đặt Label cách PictureBox 10px

            // Tạo PictureBox cho mũi tên xuống
            picArrow = new PictureBox
            {
                Size = new Size(10, 10),  // Kích thước của mũi tên
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Image = Resources.arrow_down,  // Đảm bảo có ảnh mũi tên xuống trong tài nguyên
                Location = new Point(lblEmail.Right + 5, picAvatar.Top)  // Đặt mũi tên sau email
            };

            // Thêm PictureBox và Label vào Panel
            accountPanel.Controls.Add(picAvatar);
            accountPanel.Controls.Add(lblEmail);
            accountPanel.Controls.Add(picArrow);
        }
        private void HeaderViewCommon_Resize(object sender, EventArgs e)
        {
            UpdateControlPositions();
        }

        private void UpdateControlPositions()
        {
            // Đảm bảo các control được căn chỉnh theo chiều dọc trong TableLayoutPanel
           // lblVersion.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            btnRuns.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            btnInspector.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            button.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            accountPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            // Cập nhật Margin và Padding để căn giữa theo chiều dọc và ngang
           // lblVersion.Margin = new Padding(0, 10, 10, 0);  // Khoảng cách giữa avatar và các nút
            btnRuns.Margin = new Padding(0, 10, 10, 0);  // Khoảng cách giữa các nút
            btnInspector.Margin = new Padding(0, 10, 10, 0);  // Khoảng cách giữa các nút
            button.Margin = new Padding(0, 10, 10, 0);  // Khoảng cách giữa các nút
            accountPanel.Margin = new Padding(0, 10, 10, 0);  // Khoảng cách giữa các nút
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        private void ToggleDarkLightMode()
        {
            isDarkMode = !isDarkMode;

            this.BackColor = isDarkMode ? Color.Black : Color.White;

            layoutMain.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;

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
                else if (control is TextBox)
                {
                    control.BackColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.White;
                    control.ForeColor = isDarkMode ? Color.White : Color.Black;
                }
                else if (control is PictureBox)
                {
                    control.BackColor = isDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
                }
                else if (control is RichTextBox)
                {
                    control.BackColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.White;
                    control.ForeColor = isDarkMode ? Color.White : Color.Black;
                }
            }
            button.Image = isDarkMode ? Resources.dark_mode : Resources.light_mode;
          //  lblVersion.BackColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.White;
            //lblVersion.SelectionColor = isDarkMode ? Color.White : Color.Black;
        }
    }
}
