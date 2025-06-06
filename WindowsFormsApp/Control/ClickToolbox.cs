using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Model;
using WindowsFormsApp.Model.Static;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{
    public partial class ClickToolbox : UserControl
    {
        private System.Windows.Forms.ToolTip buttonToolTip = new System.Windows.Forms.ToolTip();
        private readonly Dictionary<(string groupName, string buttonText), string> buttonTooltips = new Dictionary<(string, string), string>
{
    { (ScriptAutomationStatic.TitleGroupClickToolbox1, ScriptAutomationStatic.ControlGroup1Click), ScriptAutomationStatic.ClickXY },
    { (ScriptAutomationStatic.TitleGroupClickToolbox1, ScriptAutomationStatic.ControlGroup1Swipe), ScriptAutomationStatic.Swipe },
    { (ScriptAutomationStatic.TitleGroupClickToolbox1, ScriptAutomationStatic.ControlGroup1RandomClick), ScriptAutomationStatic.RandomClick },
    { (ScriptAutomationStatic.TitleGroupClickToolbox1, ScriptAutomationStatic.ControlGroup1Wait), ScriptAutomationStatic.Wait },
    { (ScriptAutomationStatic.TitleGroupClickToolbox2, ScriptAutomationStatic.ControlGroup2SearchAndClick), ScriptAutomationStatic.SearchAndClick },
    { (ScriptAutomationStatic.TitleGroupClickToolbox2, ScriptAutomationStatic.ControlGroup2SearchWaitClick), ScriptAutomationStatic.SearchWaitClick },
    { (ScriptAutomationStatic.TitleGroupClickToolbox2, ScriptAutomationStatic.ControlGroup2SearchAndContinue), ScriptAutomationStatic.SearchAndContinue },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "IF"), ScriptAutomationStatic.If },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "FOR LOOP"), ScriptAutomationStatic.ForLoop },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "Continue"), ScriptAutomationStatic.Continue },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "BREAK"), ScriptAutomationStatic.Break },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "Stop Script"), ScriptAutomationStatic.StopScript },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "Return"), ScriptAutomationStatic.Return },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "Comment"), ScriptAutomationStatic.Comment },
    { (ScriptAutomationStatic.TitleGroupClickToolbox4, "Show status"), ScriptAutomationStatic.ShowStatus },
    { (ScriptAutomationStatic.TitleGroupClickToolbox3, ScriptAutomationStatic.ControlGroup3FindAndClick), ScriptAutomationStatic.SearchImageAndClick },
    { (ScriptAutomationStatic.TitleGroupClickToolbox3, ScriptAutomationStatic.ControlGroup3findWaitClick), ScriptAutomationStatic.SearchImageWaitClick },
    { (ScriptAutomationStatic.TitleGroupClickToolbox3, ScriptAutomationStatic.ControlGroup3FindAndContinue), ScriptAutomationStatic.SearchImageAndContinue }
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
            leftPanel.Controls.Add(CreateGroup(ScriptAutomationStatic.TitleGroupClickToolbox1, new string[] { ScriptAutomationStatic.ControlGroup1Click
                , ScriptAutomationStatic.ControlGroup1Swipe, ScriptAutomationStatic.ControlGroup1RandomClick, ScriptAutomationStatic.ControlGroup1Wait }, 370));
            leftPanel.Controls.Add(CreateGroup(ScriptAutomationStatic.TitleGroupClickToolbox2, new string[] {
            ScriptAutomationStatic.ControlGroup2SearchAndClick,
            ScriptAutomationStatic.ControlGroup2SearchWaitClick,
            ScriptAutomationStatic.ControlGroup2SearchAndContinue
        }, 370));
            leftPanel.Controls.Add(CreateGroup(ScriptAutomationStatic.TitleGroupClickToolbox3, new string[] {
            ScriptAutomationStatic.ControlGroup3FindAndClick,
            ScriptAutomationStatic.ControlGroup3findWaitClick,
            ScriptAutomationStatic.ControlGroup3FindAndContinue
        }, 370));

            // RIGHT
            rightPanel.Controls.Add(CreateGroupRight(ScriptAutomationStatic.TitleGroupClickToolbox4, new string[] {
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
                var btn = new System.Windows.Forms.Button
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
                    var clickedButton = s as System.Windows.Forms.Button;
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
                var btn = new System.Windows.Forms.Button
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
                    var clickedButton = s as System.Windows.Forms.Button;
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
            if (groupName == ScriptAutomationStatic.TitleGroupClickToolbox2)
            {
                if (buttonText == ScriptAutomationStatic.ControlGroup2SearchAndClick)
                {
                    return "SearchAndClick(\"\")";
                }
                else if (buttonText == "Tìm gần đúng && click")
                {
                    return "";
                }
                else if (buttonText == ScriptAutomationStatic.ControlGroup2SearchWaitClick)
                {
                    return "SearchWaitClick(\"\", 1000)";
                }
                else if (buttonText == "Tìm gần đúng && wait")
                {
                    return "";
                }
                else if (buttonText == ScriptAutomationStatic.ControlGroup2SearchAndContinue)
                {
                    return "SearchAndContinue(\"\")";
                }
                else if (buttonText == "Tìm gần đúng && tiếp tục")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
            else if (groupName == ScriptAutomationStatic.TitleGroupClickToolbox3)
            {
                if (buttonText == ScriptAutomationStatic.ControlGroup3FindAndClick)
                {
                    return "FindAndClick(\"\")";
                }
                else if (buttonText == ScriptAutomationStatic.ControlGroup3findWaitClick)
                {
                    return "findWaitClick(\"\", 1000)";
                }
                else if (buttonText == ScriptAutomationStatic.ControlGroup3FindAndContinue)
                {
                    return "FindAndContinue(\"\")";
                }
                else
                {
                    return "";
                }

            }
            else if (groupName == ScriptAutomationStatic.TitleGroupClickToolbox1)
            {
                if (buttonText == ScriptAutomationStatic.ControlGroup1Click)
                {
                    return "ClickXY(100 100)";
                }
                else if (buttonText == ScriptAutomationStatic.ControlGroup1Swipe)
                {
                    return "Swipe(500 500 5 10 500)";
                }
                else if (buttonText == ScriptAutomationStatic.ControlGroup1RandomClick)
                {
                    return "RandomClick(100 100, 900 1900)";
                }
                else if (buttonText == ScriptAutomationStatic.ControlGroup1Wait)
                {
                    return "Wait(1000)";
                }
                else
                {
                    return "";
                }

            }
            else if (groupName == ScriptAutomationStatic.TitleGroupClickToolbox4)
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
