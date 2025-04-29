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
    public class ButtonContext
    {
        public string GroupName { get; set; }
        public string ButtonText { get; set; }
    }

    public partial class ClickToolbox: UserControl
    {

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
            leftPanel.Controls.Add(CreateGroup("Click tọa độ", new string[] { "ClickXY", "Swipe up", "Random Click", "Swipe down", "Wait" }, 370));
            leftPanel.Controls.Add(CreateGroup("Search text click", new string[] {
            "Tìm đúng && click", "Tìm gần đúng && click",
            "Tìm đúng && wait", "Tìm gần đúng && wait",
            "Tìm đúng && tiếp tục", "Tìm gần đúng && tiếp tục"
        }, 370));
            leftPanel.Controls.Add(CreateGroup("Search text image", new string[] {
            "Tìm đúng && click", "Tìm đúng && wait", "Tìm đúng && tiếp tục"
        }, 370));

            // RIGHT
            rightPanel.Controls.Add(CreateGroupRight("Xử lý logic", new string[] {
            "FOR LOOP", "IF", "GOTO", "BREAK", "Continue",
            "Stop Script", "Return()", "Comment", "Show status"
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
                Margin = new Padding(10,5,0,5),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                MaximumSize = new Size(maxWidth, 0),
                MinimumSize = new Size(maxWidth , 0),
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
                    GroupName = "Search text click",
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
                        return "SearchOfAndClick(\"\")";
                    case "Tìm đúng && wait":
                        return "SearchWaitClick(\"\", 1000)";
                    case "Tìm gần đúng && wait":
                        return "SearchOfWaitClick(\"\", 1000)";
                    case "Tìm đúng && tiếp tục":
                        return "SearchAndContinue(\"\")";
                    case "Tìm gần đúng && tiếp tục":
                        return "SearchOfAndContinue(\"\")";
                }
            }
            else if (groupName == "Search text image")
            {
                switch (buttonText)
                {
                    case "Tìm đúng && click":
                        return "FindAndClick(\"\")";
                    case "Tìm đúng && wait":
                        return "FindWaitClick(\"\", 1000)";
                    case "Tìm đúng && tiếp tục":
                        return "FindAndContinue(\"\")";
                }
            }
            else if (groupName == "Click tọa độ")
            {
                switch (buttonText)
                {
                    case "ClickXY":
                        return "ClickXY()";
                    case "Swipe up":
                        return "Swipeup(500 500 5 10)";
                    case "Random Click":
                        return "RandomClick(100 100, 900 1900)";
                    case "Swipe down":
                        return "Swipedown(500 500 5 10)";
                    case "Wait":
                        return "Wait(1000)";
                }
            }
            return buttonText; 
        }

    }
}
