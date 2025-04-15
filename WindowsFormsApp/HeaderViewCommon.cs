using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Properties;
using System.Windows.Forms;

namespace AccountCreatorForm.Controls
{
    public class HeaderViewCommon : UserControl
    {
        public Label lblTitle;
        public Button btnRuns;
        public Button btnInspector;
        public PictureBox picAvatar;

        public HeaderViewCommon()
        {
            InitializeLayout();
            this.Resize += HeaderViewCommon_Resize;
        }

        private void InitializeLayout()
        {
            this.Dock = DockStyle.Top;
            this.Height = 80;
            this.BackColor = Color.White;

            lblTitle = new Label
            {
                Text = "Tiêu đề",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = true,
                Location = new Point(20, 18)
            };

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

            picAvatar = new PictureBox
            {
                Size = new Size(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent,
                Image = Resources.cuahang_0 // đảm bảo bạn có ảnh mặc định
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnRuns);
            this.Controls.Add(btnInspector);
            this.Controls.Add(picAvatar);

            UpdateControlPositions();
        }

        private void HeaderViewCommon_Resize(object sender, EventArgs e)
        {
            UpdateControlPositions();
        }

        private void UpdateControlPositions()
        {
            int paddingRight = 20;
            int spacing = 10;

            picAvatar.Location = new Point(this.Width - picAvatar.Width - paddingRight, 15);
            btnInspector.Location = new Point(picAvatar.Left - btnInspector.Width - spacing, 16);
            btnRuns.Location = new Point(btnInspector.Left - btnRuns.Width - spacing, 16);
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }
    }
}
