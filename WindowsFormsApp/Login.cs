using AccountCreatorForm.Views;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Login: Form
    {
        private bool isHovered = false;
        private bool isPressed = false;
        public Login()
        {
            this.Height = 300;
            this.Width = 400;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            InitializeComponent();
            init();
            Event();

            btnLogin.Paint += BtnLogin_Paint;
            btnLogin.Style.HoverBackColor = Color.Transparent;
            btnLogin.Style.HoverForeColor = Color.Transparent;
        }

        public void init()
        {
            gradientPanelMainLogin.Dock = DockStyle.Fill;
            txtPassword.PasswordChar = '*';
            logLogin.ForeColor = Color.Red;
            logLogin.Text = "";
            lbLogin.Text = "Đăng Nhập";
            txtUsername.Text = "admin";
            txtPassword.Text = "1234";
        }
        public void Event()
        {
            txtUsername.KeyDown += Txt_KeyDown;
            txtPassword.KeyDown += Txt_KeyDown;
        }
        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  
            {
                btnLogin.PerformClick(); 
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "admin" && txtPassword.Text == "1234")  
            {
                Home home = new Home();
                home.Show();
                this.Hide();
                this.Enabled = false;
            }
            else
            {
                logLogin.Text = "Tài khoản hoặc mật khẩu sai !";
            }
        }
        private void BtnLogin_Paint(object sender, PaintEventArgs e)
        {
            //Rounded rectangle corder radius. The radius must be less than 10.
            int radius = 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(
            this.btnLogin.ClientRectangle.X + 1,
            this.btnLogin.ClientRectangle.Y + 1,
            this.btnLogin.ClientRectangle.Width - 2,
            this.btnLogin.ClientRectangle.Height - 2
            );
            btnLogin.Region = new Region(GetRoundedRect(rect, radius));
            rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            Pen borderPen;

            if (!btnLogin.Enabled) // Disabled state
            {
                borderPen = new Pen(btnLogin.Style.DisabledBorder.Color);
            }
            /*else if (isPressed) // Pressed state (based on MouseDown)
            {
                borderPen = new Pen(btnLogin.Style.PressedBorder.Color);
            }*/
            else if (isHovered) // Hover state (based on MouseHover)
            {
                borderPen = new Pen(btnLogin.Style.HoverBorder.Color);
            }
            else // Focused state
            {
                borderPen = new Pen(btnLogin.Style.FocusedBorder.Color);
            }

            // Draw the path with the determined border color
            e.Graphics.DrawPath(borderPen, GetRoundedRect(rect, radius));

            Color textColor = isHovered ? Color.Blue : btnLogin.ForeColor; // Set color to blue on hover, otherwise default color

            // Draw text with the appropriate color
            TextRenderer.DrawText(e.Graphics, btnLogin.Text, btnLogin.Font, rect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();

            // Top-left corner
            graphicsPath.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);

            // Top edge
            graphicsPath.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);

            // Top-right corner
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);

            // Right edge
            graphicsPath.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);

            // Bottom-right corner
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);

            // Bottom edge
            graphicsPath.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);

            // Bottom-left corner
            graphicsPath.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);

            // Left edge
            graphicsPath.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);

            // Close the figure to complete the path
            graphicsPath.CloseFigure();

            return graphicsPath;
        }
        private void Btn_Paint(object sender, PaintEventArgs e)
        {
            SfButton btn = sender as SfButton;

            // Tạo bút vẽ cho viền và màu nền
            using (Pen pen = new Pen(Color.LightBlue, 2)) // Màu viền
            using (SolidBrush brush = new SolidBrush(btn.BackColor)) // Màu nền của button
            {
                // Bán kính góc bo tròn
                int cornerRadius = 20;

                // Tạo GraphicsPath để vẽ hình chữ nhật bo tròn
                using (GraphicsPath path = new GraphicsPath())
                {
                    // Thêm các đoạn đường cong vào GraphicsPath
                    path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);  // Góc trên bên trái
                    path.AddArc(btn.Width - cornerRadius - 1, 0, cornerRadius, cornerRadius, 270, 90);  // Góc trên bên phải
                    path.AddArc(btn.Width - cornerRadius - 1, btn.Height - cornerRadius - 1, cornerRadius, cornerRadius, 0, 90);  // Góc dưới bên phải
                    path.AddArc(0, btn.Height - cornerRadius - 1, cornerRadius, cornerRadius, 90, 90);  // Góc dưới bên trái

                    path.CloseAllFigures();  // Đóng lại đường vẽ

                    // Vẽ nền bo tròn
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);

                    // Vẽ viền bo tròn
                    e.Graphics.DrawPath(pen, path);
                }
            }

            // Vẽ lại text của nút
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, new Point((btn.Width - TextRenderer.MeasureText(btn.Text, btn.Font).Width) / 2, (btn.Height - TextRenderer.MeasureText(btn.Text, btn.Font).Height) / 2), btn.ForeColor);
        }

        private void BtnLogin_MouseEnter(object sender, EventArgs e)
        {
            isHovered = true;
            Button btn = sender as Button;
            btn.Cursor = Cursors.Hand;
        }

        private void BtnLogin_MouseLeave(object sender, EventArgs e)
        {
            isHovered = false;
            Button btn = sender as Button;
            btn.Cursor = Cursors.Default;
        }

        private void BtnLogin_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            btnLogin.Invalidate();
        }

        private void BtnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            btnLogin.Invalidate();
        }
    }
}
