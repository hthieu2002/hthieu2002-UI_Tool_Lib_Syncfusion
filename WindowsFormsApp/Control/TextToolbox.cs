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
using WindowsFormsApp.Model;
using System.Runtime.InteropServices;

namespace WindowsFormsApp
{
    public partial class TextToolbox : UserControl
    {
        private ITextAppender textAppender;
        private ToolTip buttonToolTip = new ToolTip();

        private readonly Dictionary<string, string> buttonTooltips = new Dictionary<string, string>
{
    { "Send text", "Send text \n - Gửi text cố định \n SendText(\"Abcd\")" },
    { "Send text from file(delete)", "Gửi văn bản từ file và xóa sau khi gửi." },
    { "Send text from", "Gửi văn bản lấy từ nguồn xác định." },
    { "Random text & send", "Tạo văn bản ngẫu nhiên và gửi." },
    { "Xoá text(1 ký tự)", "Xóa một ký tự khỏi văn bản." },
    { "Xoá text(xoá all)", "Xóa toàn bộ văn bản." }
};

        public TextToolbox(ITextAppender textAppender)
        {
            InitializeComponent();
            this.textAppender = textAppender;
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
                btn.MouseEnter += (s, e) => btn.BackColor = Color.SlateBlue;
                // Hover ra: trả về màu gốc
                btn.MouseLeave += (s, e) => btn.BackColor = Color.MediumSlateBlue;

                if (buttonTooltips.TryGetValue(text, out var tooltip))
                {
                    buttonToolTip.SetToolTip(btn, tooltip);
                }
                else
                {
                    buttonToolTip.SetToolTip(btn, "Chức năng nút: " + text);
                }

                btn.Tag = new ButtonContext
                {
                    GroupName = title,
                    ButtonText = text
                };


                btn.Click += (s, e) =>
                {
                    var clickedButton = s as Button;
                    if (clickedButton?.Tag is ButtonContext ctx)
                    {
                        string sendText = GetMappedText(ctx.GroupName, ctx.ButtonText);
                        textAppender?.AppendText(sendText);
                    }
                };

                flow.Controls.Add(btn);
            }

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
            btnGetData.Click += BtnGetDataEmailFromDb_Click;
            // Panel 1: ComboBox 1 + Send1
            var panel1 = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            var combo1 = new ComboBox { Width = 120 };
            combo1.Items.AddRange(new string[] {
        "Email", "Password", "Email recovery",
        "Code backup", "Code authen",
        "Status3(0)", "DeleteStatus3(0)"
    });


            combo1.SelectedIndex = 0;

            var btnSend1 = CreateButton("Send", 70);
            btnSend1.Tag = combo1; // Gán combo1 vào Send1
            btnSend1.Click += BtnSendCombo1_Click; // Event riêng cho Send1

            panel1.Controls.Add(combo1);
            panel1.Controls.Add(btnSend1);

            // Panel 2: ComboBox 2 + Send2
            var panel2 = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            var combo2 = new ComboBox { Width = 120 };
            combo2.Items.AddRange(new string[] {
        "Backup code ok",
        "Backup code failed",
        "Code authen ok",
        "Code authen failed"
    });

            var btnSend2 = CreateButton("Send", 70);
            btnSend2.Tag = combo2; // Gán combo2 vào Send2
            btnSend2.Click += BtnSendCombo2_Click; // Event riêng cho Send2

            panel2.Controls.Add(combo2);
            panel2.Controls.Add(btnSend2);

            // Update mail status buttons
            var btnUpdate1 = CreateButton("Update mail status");
            var btnUpdate2 = CreateButton("Update mail status2");

            btnUpdate1.Click += BtnUpdateMailStatus_Click;
            btnUpdate2.Click += BtnUpdateMailStatus2_Click;

            // Add all controls into main flow
            flow.Controls.Add(btnGetData);
            flow.Controls.Add(panel1);
            flow.Controls.Add(panel2);
            flow.Controls.Add(btnUpdate1);
            flow.Controls.Add(btnUpdate2);

            // Căn giữa flow
            flow.PerformLayout();
            int centerX = (fixedWidth - flow.PreferredSize.Width) / 2;
            flow.Location = new Point(centerX, 30);

            group.Controls.Add(flow);
            return group;
        }
        private void BtnSendCombo1_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is ComboBox combo)
            {
                string selectedText = combo.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedText))
                {
                   // string command = ConvertCombo1TextToCommand(selectedText);
                   // textAppender?.AppendText(command);
                }
                else
                {
                    MessageBox.Show("Please select an option for ComboBox 1!");
                }
            }
        }
        private void BtnSendCombo2_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is ComboBox combo)
            {
                string selectedText = combo.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedText))
                {
                  //  string command = ConvertCombo2TextToCommand(selectedText);
                  //  textAppender?.AppendText(command);
                }
                else
                {
                    MessageBox.Show("Please select an option for ComboBox 2!");
                }
            }
        }
        private void BtnGetDataEmailFromDb_Click(object sender, EventArgs e)
        {
          //  textAppender?.AppendText("GetDataEmailFromDb()");
        }

        private void BtnUpdateMailStatus_Click(object sender, EventArgs e)
        {
          //  textAppender?.AppendText("UpdateMailStatus()");
        }

        private void BtnUpdateMailStatus2_Click(object sender, EventArgs e)
        {
           // textAppender?.AppendText("UpdateMailStatus2()");
        }

        //private string ConvertCombo1TextToCommand(string comboText)
        //{
        //    switch (comboText)
        //    {
        //        case "Email":
        //            return "SendUserName()";
        //        case "Password":
        //            return "SendPassword()";
        //        case "Email recovery":
        //            return "SendEmailRecovery()";
        //        case "Code backup":
        //            return "SendCodeBackup()";
        //        case "Code authen":
        //            return "SendCodeAuthen()";
        //        case "Status3(0)":
        //            return "SendStatus3(0)";
        //        case "DeleteStatus3(0)":
        //            return "DeleteStatus3(0)";
        //        default:
        //            return comboText;
        //    }
        //}
        //private string ConvertCombo2TextToCommand(string comboText)
        //{
        //    switch (comboText)
        //    {
        //        case "Backup code ok":
        //            return "SendBackupCodeOK()";
        //        case "Backup code failed":
        //            return "SendBackupCodeFailed()";
        //        case "Code authen ok":
        //            return "SendAuthenCodeOK()";
        //        case "Code authen failed":
        //            return "SendAuthenCodeFailed()";
        //        default:
        //            return comboText;
        //    }
        //}

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

            //btn.Tag = new ButtonContext
            //{
            //    GroupName = title,
            //    ButtonText = text
            //};


            //btn.Click += (s, e) =>
            //{
            //    var clickedButton = s as Button;
            //    if (clickedButton?.Tag is ButtonContext ctx)
            //    {
            //        string sendText = GetMappedText(ctx.GroupName, ctx.ButtonText);
            //        textAppender?.AppendText(sendText);
            //    }
            //};

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
            string[] leftButtons = buttons.Take(2).ToArray();
            string[] rightButtons = buttons.Skip(2).ToArray();

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
            btn.Tag = text; // Gán text vào Tag để click xử lý
            btn.Click += BtnProcessTextData_Click;
            return btn;
        }
        private void BtnProcessTextData_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string text)
            {
               // string command = ConvertTextDataButtonText(text);
             //   textAppender?.AppendText(command);
            }
        }
        //private string ConvertTextDataButtonText(string buttonText)
        //{
        //    switch (buttonText)
        //    {
        //        case "Get data from file":
        //            return "GetDataFromFile(\"*.txt\")";
        //        case "Del data from file":
        //            return "DelDataFromFile(\"*.txt\")";
        //        case "Send data (Value)":
        //            return "SendDataValue(\"value\")";
        //        case "Save data to file":
        //            return "SaveDataToFile(\"*.txt\")";
        //        default:
        //            return buttonText;
        //    }
        //}

        private string GetMappedText(string groupName, string buttonText)
        {
            if (groupName == "Process Text")
            {
                switch (buttonText)
                {
                    case "Send text":
                        return "SendText(\"abc\")";
                    case "Send text from file(delete)":
                        return "SendTextFromFileDel(\"*.txt\")";
                    case "Send text from":
                        return "SendTextRandomFromFile(\"*.txt\")";
                    case "Random text & send":
                        return "RandomTextAndSend(15)";
                    case "Xoá text(1 ký tự)":
                        return "DelTextChar(1)";
                    case "Xoá text(xoá all)":
                        return "DelAllText()";
                }
            }
            return buttonText;
        }
    }
}
