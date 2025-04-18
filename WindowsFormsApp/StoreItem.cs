using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AccountCreatorForm.Views
{
    public partial class StoreItem : UserControl
    {
        private Label lblTitle;
        private Label lblUserName;
        private Label lblDescription;
        private SfButton btnAction;
        private PictureBox picItemImage;
        private FlowLayoutPanel panelContent;

        private FlowLayoutPanel panelMain;
        private Panel panelLeft;
        private FlowLayoutPanel panelFill;
        public event EventHandler UserControlClicked;
        public string Title { get => lblTitle.Text; set => lblTitle.Text = value; }
        public string UserName { get => lblUserName.Text; set => lblUserName.Text = value; }
        public string Description { get => lblDescription.Text; set => lblDescription.Text = value; }
        public string ButtonText { get => btnAction.Text; set => btnAction.Text = value; }
        public Image ItemImage { get => picItemImage.Image; set => picItemImage.Image = value; }

        public StoreItem()
        {
            this.Margin = new Padding(10,20,0,10);
            this.BackColor = Color.White;
            this.Padding = new Padding(20);
            //this.BorderStyle = BorderStyle.FixedSingle;
            this.BorderStyle = BorderStyle.None;
            this.MaximumSize = new Size(470, 1000);  
            this.MinimumSize = new Size(470, 250);
            init();
        }
        private void init()
        {
            SetItemMain();
            this.Controls.Add(panelMain);
        }
        private void SetItemMain()
        {
            panelMain = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(100),
                Padding = new Padding(0,10,0,0),
                AutoSize = true,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown
            };
            panelMain.Paint += FlowLayoutPanel_Paint;
            SetItemLeft();
            SetItemFill();
            panelMain.Controls.Add(panelLeft);
            panelMain.Controls.Add(panelFill);

            panelMain.Click += PanelMain_Click;
            panelMain.MouseEnter += PanelMain_MouseEnter;
            panelMain.MouseLeave += PanelMain_MouseLeave;
            panelLeft.Click += PanelMain_Click;
            panelFill.Click += PanelMain_Click;


            panelLeft.MouseEnter += PanelMain_MouseEnter;
            panelLeft.MouseLeave += PanelMain_MouseLeave;

            panelFill.MouseEnter += PanelMain_MouseEnter;
            panelFill.MouseLeave += PanelMain_MouseLeave;
        }

        private void SetItemLeft()
        {
            panelLeft = new Panel
            {
                Dock = DockStyle.Left,
                Margin = new Padding(10),
                Width = 24
            };
            SetImage();
            panelLeft.Controls.Add(picItemImage);
        }
        private void SetItemFill()
        {
            panelFill = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Margin = new Padding(10, 0, 0, 0),
                AutoSize = true,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown
            };
            SetLabelTitle();
            SetLabelUserName();
            SetLabelDescription();
            SetBtnAction();
            panelFill.Controls.Add(lblTitle);
            panelFill.Controls.Add(lblUserName);
            panelFill.Controls.Add(lblDescription);
            panelFill.Controls.Add(btnAction);
        }
        private void SetImage()
        {
            picItemImage = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 24, 
                Height = 24,
                Margin = new Padding(20) 
            };
        }

        private void SetLabelTitle()
        {
            lblTitle = new Label
            {
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,  
                ForeColor = Color.FromArgb(33, 33, 33),
                Margin = new Padding(3, 3, 3, 6),
                MaximumSize = new Size(340, 0), 
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblTitle.MouseEnter += Label_MouseEnter;
            lblTitle.MouseLeave += Label_MouseLeave;

            lblTitle.Text = lblTitle.Text.Length > 25 ? lblTitle.Text.Substring(0, 25) + "..." : lblTitle.Text;
        }

        private void SetLabelUserName()
        {
            lblUserName = new Label
            {
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,  
                ForeColor = Color.Gray,
                Margin = new Padding(3, 0, 3, 6),
                MaximumSize = new Size(340, 0)
            };
        }
        private void SetLabelDescription()
        {
            lblDescription = new Label
            {
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                ForeColor = Color.Gray,
                Margin = new Padding(3, 0, 3, 6),
                MaximumSize = new Size(340, 0),
                TextAlign = ContentAlignment.TopLeft
            };
        }

        private void SetBtnAction()
        {
            btnAction = new SfButton
            {
                Height = 36,
                Width = 100,  
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = "Facebook",
                BackColor = Color.FromArgb(86, 119, 254),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,  
                MaximumSize = new Size(100, 36)  
            };

            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Margin = new Padding(0, 5, 0, 10);
        }
      
        private void FlowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            FlowLayoutPanel panel = sender as FlowLayoutPanel;

            using (BufferedGraphicsContext context = BufferedGraphicsManager.Current)
            {
                BufferedGraphics bufferedGraphics = context.Allocate(e.Graphics, panel.ClientRectangle);

                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                {
                    bufferedGraphics.Graphics.FillRectangle(shadowBrush, 5, 5, panel.Width, panel.Height);  
                }

                using (Pen pen = new Pen(Color.LightBlue, 2))
                using (SolidBrush brush = new SolidBrush(Color.White)) 
                {
                    bufferedGraphics.Graphics.FillRectangle(brush, 0, 0, panel.Width, panel.Height);

                    int cornerRadius = 20; 
                    bufferedGraphics.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    bufferedGraphics.Graphics.DrawArc(pen, 0, 0, cornerRadius, cornerRadius, 180, 90); 
                    bufferedGraphics.Graphics.DrawArc(pen, panel.Width - cornerRadius - 1, 0, cornerRadius, cornerRadius, 270, 90);
                    bufferedGraphics.Graphics.DrawArc(pen, 0, panel.Height - cornerRadius - 1, cornerRadius, cornerRadius, 90, 90);
                    bufferedGraphics.Graphics.DrawArc(pen, panel.Width - cornerRadius - 1, panel.Height - cornerRadius - 1, cornerRadius, cornerRadius, 0, 90);

                    bufferedGraphics.Graphics.DrawLine(pen, cornerRadius / 2, 0, panel.Width - cornerRadius / 2, 0); 
                    bufferedGraphics.Graphics.DrawLine(pen, cornerRadius / 2, panel.Height - 1, panel.Width - cornerRadius / 2, panel.Height - 1); 
                    bufferedGraphics.Graphics.DrawLine(pen, 0, cornerRadius / 2, 0, panel.Height - cornerRadius / 2); 
                    bufferedGraphics.Graphics.DrawLine(pen, panel.Width - 1, cornerRadius / 2, panel.Width - 1, panel.Height - cornerRadius / 2); 
                }

                bufferedGraphics.Render(e.Graphics);
            }
        }
        private void PanelMain_MouseEnter(object sender, EventArgs e)
        {
            panelMain.Cursor = Cursors.Hand;
        }

        private void PanelMain_MouseLeave(object sender, EventArgs e)
        {
            panelMain.Cursor = Cursors.Default;
        }
        private void PanelMain_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PanelMain đã được click!");
        }
        private void Label_MouseEnter(object sender, EventArgs e)
        {
            lblTitle.Cursor = Cursors.Hand;
            lblTitle.ForeColor = Color.Blue;  
            lblTitle.Font = new Font(lblTitle.Font, FontStyle.Underline); 
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            lblTitle.Cursor = Cursors.Default;
            lblTitle.ForeColor = Color.FromArgb(33, 33, 33);  
            lblTitle.Font = new Font(lblTitle.Font, FontStyle.Bold);  
        }
    }
}
