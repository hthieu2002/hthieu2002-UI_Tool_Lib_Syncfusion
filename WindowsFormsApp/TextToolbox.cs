using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp
{
    public partial class TextToolbox : UserControl
    {
        public TextToolbox()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.BackColor = Color.WhiteSmoke;

            var roolPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Group 1: Process Text
            roolPanel.Controls.Add(CreateGroup("Process Text", new string[] {
                "Send text",
                "Send text from file(delete)",
                "Send text from",
                "Random text & send",
                "Xoá text(1 ký tự)",
                "Xoá text(xoá all)"
            }, 250));

            // Group 2: Process Email
            roolPanel.Controls.Add(CreateEmailGroup());


            // Group 3: Process Text Data (sử dụng FlowDirection LeftToRight)
            roolPanel.Controls.Add(CreateGroupLayoutButton("Process text data", new string[] {
                "Get data from file",
                "Del data from file",
                "Send data (Value)",
                "Save data to file"
            }, 550));


            this.Controls.Add(roolPanel);
        }

        private GroupBox CreateGroup(string title, string[] buttons, int maxWidth, bool horizontal = false)
        {
            int fixedWidth = 270;
            int fixedHeight = 300;

            var group = new GroupBox
            {
                Text = title,
                AutoSize = false, // Tắt AutoSize để cố định khung
                Size = new Size(fixedWidth, fixedHeight),
                Padding = new Padding(5),
                Margin = new Padding(10, 5, 0, 5),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            var flow = new FlowLayoutPanel
            {
                AutoSize = true,                  // Để Flow tự động tính kích thước theo button
                WrapContents = true,
                FlowDirection = horizontal ? FlowDirection.LeftToRight : FlowDirection.TopDown
            };

            // Thêm các button vào FlowLayoutPanel
            foreach (var text in buttons)
            {
                var btn = new Button
                {
                    Text = text,
                    Width = 200,
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
                btn.Click += (s, e) => MessageBox.Show($"Clicked: {text}");
                flow.Controls.Add(btn);
            }

            // Tính toán vị trí căn giữa sau khi đã thêm xong button
            flow.PerformLayout(); // Bắt buộc gọi để cập nhật kích thước
            int centerX = (fixedWidth - flow.PreferredSize.Width) / 2;
            flow.Location = new Point(centerX, 30); // 30 là khoảng cách từ trên xuống, có thể chỉnh lại

            group.Controls.Add(flow);
            return group;
        }
        private GroupBox CreateEmailGroup()
        {
            int fixedWidth = 270;
            int fixedHeight = 300;

            var group = new GroupBox
            {
                Text = "Process Email",
                AutoSize = false,
                Size = new Size(fixedWidth, fixedHeight),
                Padding = new Padding(5),
                Margin = new Padding(10, 5, 0, 5),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            var flow = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.TopDown
            };

            // Button: Get data email from DB
            var btnGetData = CreateButton("Get data email from DB");

            // ComboBox + Send Button (Send Data Mail)
            var panel1 = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            var combo1 = new ComboBox { Width = 120 };
            combo1.Items.AddRange(new string[] { "Option 1", "Option 2", "Option 3" });
            var btnSend1 = CreateButton("Send", 70);
            panel1.Controls.Add(combo1);
            panel1.Controls.Add(btnSend1);

            // ComboBox + Send Button (Check Code)
            var panel2 = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            var combo2 = new ComboBox { Width = 120 };
            combo2.Items.AddRange(new string[] { "Code A", "Code B", "Code C" });
            var btnSend2 = CreateButton("Send", 70);
            panel2.Controls.Add(combo2);
            panel2.Controls.Add(btnSend2);

            // Update mail status buttons
            var btnUpdate1 = CreateButton("Update mail status");
            var btnUpdate2 = CreateButton("Update mail status2");

            // Add all controls into main flow
            flow.Controls.Add(btnGetData);
            flow.Controls.Add(panel1);
            flow.Controls.Add(panel2);
            flow.Controls.Add(btnUpdate1);
            flow.Controls.Add(btnUpdate2);

            // Căn giữa toàn bộ flow
            flow.PerformLayout();
            int centerX = (fixedWidth - flow.PreferredSize.Width) / 2;
            flow.Location = new Point(centerX, 30);

            group.Controls.Add(flow);
            return group;
        }

        private Button CreateButton(string text, int width = 200)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
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

        private GroupBox CreateGroupLayoutButton(string title, string[] buttons, int maxWidth)
        {
            int fixedWidth = 550;
            int fixedHeight = 150;

            var group = new GroupBox
            {
                Text = title,
                AutoSize = false,
                Size = new Size(fixedWidth, fixedHeight),
                Padding = new Padding(5),
                Margin = new Padding(10, 5, 0, 5),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            // Layout chính (trái sang phải)
            var mainFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,               // Quan trọng: để xếp từ trái sang phải
                FlowDirection = FlowDirection.LeftToRight
            };

            // Chia nút ra 2 nhóm
            string[] leftButtons = buttons.Take(2).ToArray();    // 2 nút đầu
            string[] rightButtons = buttons.Skip(2).ToArray();   // 2 nút sau

            // Panel trái (xếp từ trên xuống)
            var leftPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown
            };

            foreach (var text in leftButtons)
            {
                var btn = CreateStyledButton(text);
                leftPanel.Controls.Add(btn);
            }

            // Panel phải (xếp từ trên xuống)
            var rightPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown
            };

            foreach (var text in rightButtons)
            {
                var btn = CreateStyledButton(text);
                rightPanel.Controls.Add(btn);
            }

            // Thêm 2 panel vào layout chính
            mainFlow.Controls.Add(leftPanel);
            mainFlow.Controls.Add(rightPanel);

            // Căn giữa
            mainFlow.PerformLayout();
            int centerX = (fixedWidth - mainFlow.PreferredSize.Width) / 2;
            mainFlow.Location = new Point(centerX, 30);

            group.Controls.Add(mainFlow);
            return group;
        }

        private Button CreateStyledButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Width = 200,
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
