using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class KeyButtonToolbox: UserControl
    {
        public KeyButtonToolbox()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.BackColor = Color.WhiteSmoke;

            var mainFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20)
            };

            // Panel chứa nút Send key + LinkLabel List key
            var panelTop = new Panel
            {
                AutoSize = true
            };

            var btnSendKey = CreateStyledButton("Send key");
            var linkListKey = new LinkLabel
            {
                Text = "List key",
                AutoSize = true,
                Location = new Point(btnSendKey.Width + 10, 10),
                LinkColor = Color.Brown
            };
            linkListKey.Click += (s, e) => MessageBox.Show("List of keys displayed here.");

            panelTop.Controls.Add(btnSendKey);
            panelTop.Controls.Add(linkListKey);

            // Các nút còn lại
            var btnCtrlA = CreateStyledButton("CTRL + A");

            var btnCheckKeyboard = CreateStyledButton("Check keyboard");

            // Thêm vào layout chính
            mainFlow.Controls.Add(panelTop);
            mainFlow.Controls.Add(btnCtrlA);
            mainFlow.Controls.Add(btnCheckKeyboard);

            this.Controls.Add(mainFlow);
        }

        private Button CreateStyledButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Width = 150,
                Height = 35,
                Margin = new Padding(3),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8f),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Paint += RoundedButtonPainter.PaintButton;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.SlateBlue;
            // Hover ra: trả về màu gốc
            btn.MouseLeave += (s, e) => btn.BackColor = Color.MediumSlateBlue;
            btn.Click += (s, e) => MessageBox.Show($"Clicked: {text}");
            return btn;
        }
    }
}

