using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.ListView;
using Syncfusion.WinForms.ListView.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class ViewAutomation: Form
    {
        private SfComboBox cbLoadFile;
        private SfButton btnLoadFile;
        private NumericUpDown nudNumber;
        private CheckBox cbAuto;
        private SfButton btnRun;
        private SfButton btnScript;
        //
        private SfComboBox cbNomal;
        private SfButton btnAutoRun;
        private SfButton btnBackup;
        private SfButton btnScreen;
        private TextBoxExt txtRestore;
        private SfButton btnRestore;
        public ViewAutomation()
        {
            InitializeComponent();
            setControl();
        }

        private void setControl()
        {
            // 
            cbLoadFile = new SfComboBox
            {
                Width = 250, // Set chiều rộng lớn hơn
             //   Style = Syncfusion.WinForms.ListView.Enums.Style.Office2016Colorful, // Style đẹp
                DropDownStyle = DropDownStyle.DropDownList
            };
            btnLoadFile = new SfButton
            {
                Text = "Load file",
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(150, 140, 250), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            btnLoadFile.Paint += BtnCommon_Paint;
            flowLayoutPanel2.Controls.Add(cbLoadFile);
            flowLayoutPanel2.Controls.Add(btnLoadFile);

            nudNumber = new NumericUpDown
            {
                Value = 1,
                Width = 60,
                Height = 30,
                Font = new Font("Segoe UI", 10)
            };
            cbAuto = new CheckBox
            {
                Text = "Vô hạn",
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Height = 30
            };
            btnRun = new SfButton
            {
                Text = "Run Script",
                Width = 120,
                Height = 35,
                BackColor = Color.Teal, 
                ForeColor = Color.White,
               // Image = ,
                //ImageAlignment = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText
            };
            btnRun.Paint += BtnCommon_Paint;
            btnScript = new SfButton
            {
                Text = "Code script",
                Width = 120,
                Height = 35,
                BackColor = Color.MediumPurple, // tím đẹp
                ForeColor = Color.White,
             //   Image = codeIcon,
              //  ImageAlignment = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
            };
            btnScript.Paint += BtnCommon_Paint;
            flowLayoutPanel3.Controls.Add(nudNumber);
            flowLayoutPanel3.Controls.Add(cbAuto);
            flowLayoutPanel3.Controls.Add(btnRun);
            flowLayoutPanel3.Controls.Add(btnScript);

        }

        private void BtnCommon_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            int radius = 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(
                btn.ClientRectangle.X + 1,
                btn.ClientRectangle.Y + 1,
                btn.ClientRectangle.Width - 2,
                btn.ClientRectangle.Height - 2
            );

            btn.Region = new Region(GetRoundedRect(rect, radius));
            rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            e.Graphics.FillRectangle(new SolidBrush(btn.BackColor), rect);

            Pen borderPen = GetButtonBorderPen(btn);

            e.Graphics.DrawPath(borderPen, GetRoundedRect(rect, radius));

            Color textColor = GetButtonTextColor(btn);

            Rectangle textRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4); // Điều chỉnh phạm vi để tránh chữ bị đè lên
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private Pen GetButtonBorderPen(Button btn)
        {
            if (!btn.Enabled)
            {
                return new Pen(Color.Gray);
            }
            else if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                return new Pen(Color.Blue);
            }
            else if (btn.Focused)
            {
                return new Pen(Color.Green);
            }
            else
            {
                return new Pen(Color.Gray);
            }
        }

        private Color GetButtonTextColor(Button btn)
        {
            if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                return Color.Blue;
            }
            return btn.ForeColor;
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); // Top-left corner
            graphicsPath.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y); // Top edge
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90); // Top-right corner
            graphicsPath.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius); // Right edge
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90); // Bottom-right corner
            graphicsPath.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom); // Bottom edge
            graphicsPath.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90); // Bottom-left corner
            graphicsPath.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius); // Left edge
            graphicsPath.CloseFigure();
            return graphicsPath;
        }
    }
}
