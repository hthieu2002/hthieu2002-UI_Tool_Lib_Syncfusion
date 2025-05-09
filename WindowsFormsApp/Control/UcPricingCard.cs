using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AccountCreatorForm.Views
{
    public partial class UcPricingCard : UserControl
    {
        private Label lblTitle;
        private Label lblPrice;
        private Label lblSubtitle;
        private Label lbTotal;
        private Button btnAction;
        private FlowLayoutPanel pnlFeatures;
        private Panel contentPanel;

        public string Title { get => lblTitle.Text; set => lblTitle.Text = value; }
        public string Price { get => lblPrice.Text; set => lblPrice.Text = value; }
        public string Subtitle { get => lblSubtitle.Text; set => lblSubtitle.Text = value; }
        public string ButtonText { get => btnAction.Text; set => btnAction.Text = value; }

        public string[] Features
        {
            set
            {
                pnlFeatures.Controls.Clear();
                foreach (var feature in value)
                {
                    var lbl = new Label
                    {
                        Text = "\u2714 " + feature, // ✔ check mark
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(33, 33, 33),
                        Margin = new Padding(3, 3, 3, 3)
                    };
                    pnlFeatures.Controls.Add(lbl);
                }
            }
        }

        public UcPricingCard()
        {
            InitializeCard();
        }

        private void InitializeCard()
        {
            this.Width = 300;
            this.Height = 650;
            this.Margin = new Padding(10, 0, 10, 0);
            this.Padding = new Padding(10);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.None;

            // Initialize labels, button, etc.
            lblTitle = new Label { Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(33, 33, 33), AutoSize = true, Margin = new Padding(10, 0, 0, 10) };
            lblPrice = new Label { Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.Black, AutoSize = true, Margin = new Padding(10, 0, 0, 10)};
            lblSubtitle = new Label { Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, AutoSize = true, Margin = new Padding(10, 0, 0, 10) };

            btnAction = new Button
            {
                Height = 36,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(86, 119, 254),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Top,
                Margin = new Padding(10, 0, 0, 10)
            };

            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Click += Button_Click;
            btnAction.MouseEnter += Button_MouseEnter;
            btnAction.MouseLeave += Button_MouseLeave;

            lbTotal = new Label
            {
                Height = 36,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = "Bao gồm",
                Dock = DockStyle.Top,
                Margin = new Padding(10, 0, 0, 10)
            };

            pnlFeatures = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false, Margin = new Padding(0, 0, 0, 0) };

            contentPanel = new FlowLayoutPanel
            {
                Margin = new Padding(10, 0, 0, 10),
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10),
                AutoSize = true,
                WrapContents = false,
                Dock = DockStyle.Fill
            };

            contentPanel.Paint += FlowLayoutPanel_Paint;
            contentPanel.Controls.Add(lblTitle);
            contentPanel.Controls.Add(lblPrice);
            contentPanel.Controls.Add(lblSubtitle);
            contentPanel.Controls.Add(btnAction);
            contentPanel.Controls.Add(lbTotal);
            contentPanel.Controls.Add(pnlFeatures);

            // Gán sự kiện MouseEnter và MouseLeave
            contentPanel.MouseEnter += Panel_MouseEnter;
            contentPanel.MouseLeave += Panel_MouseLeave;

            this.Controls.Add(contentPanel);
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                panel.Cursor = Cursors.Hand;
            }
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                panel.Cursor = Cursors.Default;
            }
        }
        private void FlowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            using (Pen borderPen = new Pen(Color.LightBlue, 2)) 
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                GraphicsPath path = GetRoundedRect(panel.ClientRectangle, 20);

                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(borderPen, path);
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            graphicsPath.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90); 
            graphicsPath.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90); 
            graphicsPath.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
            graphicsPath.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            graphicsPath.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);
            graphicsPath.CloseFigure();

            return graphicsPath;
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

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (Price == "Miễn phí")
                {
                    MessageBox.Show($"Đã free còn click làm gì ?");
                }
                else
                {
                    MessageBox.Show($"Nạp tiền vào em, gói này giá {Price}");
                }
            }
        }
    }
}
