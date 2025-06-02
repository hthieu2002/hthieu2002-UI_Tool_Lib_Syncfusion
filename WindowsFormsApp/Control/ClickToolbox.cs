using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Model;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp
{
    public partial class ClickToolbox : UserControl
    {
        private ToolTip buttonToolTip = new ToolTip();
        private readonly Dictionary<(string groupName, string buttonText), string> buttonTooltips = new Dictionary<(string, string), string>
{
    { ("Click tọa độ", "ClickXY"), "Click theo tọa độ x,y \n Ví dụ \n - Click cố định \n ClickXY(300 400) \n Sự dụng capture để chụp ảnh lấy tọa độ" },
    { ("Click tọa độ", "Swipe"), "Thao tác cuộn \n Cuộn sẽ có 4 giá trị x1 x2 y1 y2 \n x1 x2 là x,y điểm ban đầu \n y1 y2 là điểm kết thúc \n cuộn sẽ bắt đầu từ điểm x đến y theo bất cứ chiều lên xuống nào \n Ví dụ \n Swipe(200 100 600 800) \n 200 100 tương ứng x1 x2 \n 600 800 tương ứng y1 y2 \n Thông số thứ 5 là độ trễ ms(mặc định là 500ms) \n Sự dụng capture để lấy tọa độ" },
    { ("Click tọa độ", "Random Click"), "Click random \n - Được ngăn cách 2 giá trị bởi dấu , \n Ví dụ ClickRandom(100 200 , 300 800) \n X được random trong khoảng 100-200\n Y được random trong khoảng 300-800" },
    { ("Click tọa độ", "Wait"), "Wait \n Là lệnh chờ đợi \n Wait(1000) chờ 1 giây " },
    { ("Search text click", "Tìm đúng && click"), "Tìm đúng text và click " +
                "\n Lệnh tìm chữ Next nếu nó tồn tại thì click " +
                "\n SearchAndClick(\"Next\")" },
    { ("Search text click", "Tìm đúng && wait"), "Tìm đúng và đợi" +
                "\n Lệnh này giống lệnh tìm đúng và click nhưng nó sẽ đợi thêm số giây sau khi đã click" +
                "\n SearchWaitClick(\"Next\", 1000)" },
    { ("Search text click", "Tìm đúng && tiếp tục"), "Tìm đúng và tiếp tục" +
                "\n Lệnh này giống lệnh tìm đúng" +
                "\n Nhưng lệnh sẽ bỏ qua khi tìm thấy" +
                "\n Phù hợp trong các lệnh điều kiện if" +
                "SearchAndContinue(\"Next\")" },
    { ("Xử lý logic", "IF"), "If dùng để đặt lệnh trong nó khi thỏa mãn điều kiện " +
                "\n Ví dụ if = điều kiện { run }" +
                "\n điều kiện là lệnh thỏa mãn if để run được chạy" },
    { ("Xử lý logic", "FOR LOOP"), "For " +
                "\n Vòng lặp for dùng để đặt vòng lặp xử lý cho các lệnh ở trong đó" +
                "\n Mẫu " +
                "\n for=main,end=100" +
                "\n {" +
                "\n Wait(1000)" +
                "\n }" +
                "\n main là tên bắt buộc phải có giữa các for" +
                "\n không được đặt trùng sẽ gây ra rối và chạy loạn" },
    { ("Xử lý logic", "Continue"), "Lệnh continue" +
                "\n Được sự dụng trong for" +
                "\n dùng để bỏ qua for hiện tại và chuyển sang for khác" +
                "\n hoặc bỏ qua các lệnh trong vòng lặp" +
                "\n Ví dụ" +
                "\n for =main, end=100" +
                "\n {" +
                "\n Wait(1000)" +
                "\n continue" +
                "\n Wait(4000)" +
                "\n }" +
                "\n lệnh continue bỏ qua lệnh 4000" },
    { ("Xử lý logic","BREAK"),"Lệnh break" +
                "\n Dùng để dừng vòng lặp" +
                "\n Hoặc thoát script nếu không để trong vòng lặp" },
    { ("Xử lý logic", "Stop Script"), "Lệnh stop script " +
                "\n Dùng để dừng script ngay lập tức"},
    { ("Xử lý logic","Return"), "Trong quá trình chạy " +
                "\n Gặp lệnh này sẽ dừng hoặc chuyển script nếu chạy nhiều lần " },
    { ("Xử lý logic", "Comment"), "Sự dụng lệnh này để ghi chú lại script " +
                "\n những gì được ghi sau Comment sẽ không được thực thi "},
    { ("Xử lý logic", "Show status"), "Lệnh Show status" +
                "\n Sẽ hiện thị log lên hiện thị quá trình chạy đến lệnh nào "},
    { ("Search text image","Tìm đúng && click"), "Lệnh này tương tự lệnh tìm đúng click " +
                "\n Nhưng nó sẽ tìm trên hình ảnh xử lý những nơi k thể lấy được text"},
    { ("Search text image","Tìm đúng && wait"), "Lệnh này tương tự lệnh tìm đúng và đợi " +
                "\n Nhưng nó sẽ tìm trên hình ảnh xử lý những nơi k thể lấy được text"},
    { ("Search text image","Tìm đúng && tiếp tục"), "Lệnh này tương tự lệnh tìm đúng tiếp tục " +
                "\n Nhưng nó sẽ tìm trên hình ảnh xử lý những nơi k thể lấy được text"}
};


        private ITextAppender textAppender;
        public ClickToolbox(ITextAppender appender)
        {
            InitializeComponent();
            textAppender = appender;
            BuildUI();
        }
        private void BuildUI()
        {
            this.Size = new Size(427, 408); // Fit panel
            this.BackColor = Color.WhiteSmoke;

            var rootPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            rootPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            rootPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));

            // LEFT SIDE
            var leftPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,

                AutoScroll = true
            };

            // RIGHT SIDE
            var rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoScroll = true
            };

            // LEFT
            leftPanel.Controls.Add(CreateGroup("Click tọa độ", new string[] { "ClickXY", "Swipe", "Random Click", "Wait" }, 370));
            leftPanel.Controls.Add(CreateGroup("Search text click", new string[] {
            "Tìm đúng && click",
            "Tìm đúng && wait",
            "Tìm đúng && tiếp tục"
        }, 370));
            leftPanel.Controls.Add(CreateGroup("Search text image", new string[] {
            "Tìm đúng && click", "Tìm đúng && wait", "Tìm đúng && tiếp tục"
        }, 370));

            // RIGHT
            rightPanel.Controls.Add(CreateGroupRight("Xử lý logic", new string[] {
            "FOR LOOP", "IF", "BREAK", "Continue",
            "Stop Script", "Return", "Comment", "Show status"
        }, 140));
      
            rootPanel.Controls.Add(leftPanel, 0, 0);
            rootPanel.Controls.Add(rightPanel, 1, 0);

            this.Controls.Add(rootPanel);
        }

        private GroupBox CreateGroup(string title, string[] buttons, int maxWidth)
        {
            var group = new GroupBox
            {
                Text = title,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(5),
                Margin = new Padding(10, 5, 0, 5),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                MaximumSize = new Size(maxWidth, 0),
                MinimumSize = new Size(maxWidth, 0),
                Padding = new Padding(3),
            };

            foreach (var text in buttons)
            {
                var btn = new Button
                {
                    Text = text,
                    AutoSize = true,
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
                btn.Tag = new ButtonContext
                {
                    GroupName = title,
                    ButtonText = text
                };

                if (buttonTooltips.TryGetValue((title, text), out var tooltip))
                {
                    buttonToolTip.SetToolTip(btn, tooltip);
                }
                else
                {
                    buttonToolTip.SetToolTip(btn, $"Chức năng nút: {text}");
                }

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
            group.Controls.Add(flow);
            return group;
        }
        private GroupBox CreateGroupRight(string title, string[] buttons, int maxWidth)
        {
            var group = new GroupBox
            {
                Text = title,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(5),
                Margin = new Padding(0, 5, 0, 5),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                MaximumSize = new Size(maxWidth, 0),
                Padding = new Padding(3),
            };

            foreach (var text in buttons)
            {
                var btn = new Button
                {
                    Text = text,
                    Width = 120,
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

                btn.Tag = new ButtonContext
                {
                    GroupName = title,
                    ButtonText = text
                };

                if (buttonTooltips.TryGetValue((title, text), out var tooltip))
                {
                    buttonToolTip.SetToolTip(btn, tooltip);
                }
                else
                {
                    buttonToolTip.SetToolTip(btn, $"Chức năng nút: {text}");
                }

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

            group.Controls.Add(flow);
            return group;
        }

        private string GetMappedText(string groupName, string buttonText)
        {
            if (groupName == "Search text click")
            {
                switch (buttonText)
                {
                    case "Tìm đúng && click":
                        return "SearchAndClick(\"\")";
                    case "Tìm gần đúng && click":
                        return "";
                    case "Tìm đúng && wait":
                        return "SearchWaitClick(\"\", 1000)";
                    case "Tìm gần đúng && wait":
                        return "";
                    case "Tìm đúng && tiếp tục":
                        return "SearchAndContinue(\"\")";
                    case "Tìm gần đúng && tiếp tục":
                        return "";
                }


            }
            else if (groupName == "Search text image")
            {
                switch (buttonText)
                {
                    case "Tìm đúng && click":
                        return "FindAndClick(\"\")";
                    case "Tìm đúng && wait":
                        return "findWaitClick(\"\", 1000)";
                    case "Tìm đúng && tiếp tục":
                        return "FindAndContinue(\"\")";
                }
            }
            else if (groupName == "Click tọa độ")
            {
                switch (buttonText)
                {
                    case "ClickXY":
                        return "ClickXY(100 100)";
                    case "Swipe":
                        return "Swipe(500 500 5 10 500)";
                    case "Random Click":
                        return "RandomClick(100 100, 900 1900)";
                    case "Wait":
                        return "Wait(1000)";
                }
            }
            else if (groupName == "Xử lý logic")
            {
                switch (buttonText)
                {
                    case "FOR LOOP":
                        return "for=,end=100 \n{ \n\n }";
                    case "IF":
                        return "if= \n { \n\n }";
                    case "GOTO":
                        return "goto";
                    case "BREAK":
                        return "break";
                    case "Continue":
                        return "continue";
                    case "Stop Script":
                        return "StopScript()";
                    case "Return":
                        return "return";
                    case "Comment":
                        return "//";
                    case "Show status":
                        return "Log(\"\")";
                }
            }

            return buttonText;
        }

    }
}
