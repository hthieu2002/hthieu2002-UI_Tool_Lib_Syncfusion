using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Model.Static;

namespace WindowsFormsApp
{
    public partial class DataChangeInfoToolbox: UserControl
    {
        private ITextAppender textAppender;
        public DataChangeInfoToolbox(ITextAppender appender)
        {
            InitializeComponent();
            textAppender = appender;
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

            // Thêm vào panel chính
            roolPanel.Controls.Add(CreateMultiColumnButtonGroup(
                ScriptAutomationStatic.TitleProcessPhoneActions,
                new List<string[]>
                {
                    new string[] { ScriptAutomationStatic.ControlBackup, ScriptAutomationStatic.ControlRestore, ScriptAutomationStatic.ControlLoginGmail,ScriptAutomationStatic.ControlLoadPlayStore},
                    new string[] { ScriptAutomationStatic.ControlChangeInfo, ScriptAutomationStatic.ControlChangeSIM, ScriptAutomationStatic.ControlWipeAccount, ScriptAutomationStatic.ControlWaitReboot, ScriptAutomationStatic.ControlWaitInternet },
                    new string[] { ScriptAutomationStatic.ControlPushFileToPhone, ScriptAutomationStatic.ControlPullFIleToPC }
                }
            ));

            this.Controls.Add(roolPanel);
        }

        private GroupBox CreateMultiColumnButtonGroup(string title, List<string[]> columns, int fixedWidth = 550, int fixedHeight = 400)
        {
            var group = new GroupBox
            {
                Text = title,
                AutoSize = false,
                Size = new Size(fixedWidth, fixedHeight),
                Padding = new Padding(5),
                Margin = new Padding(10, 5, 0, 5),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            // Flow chính (trái qua phải)
            var mainFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(10)
            };

            // Tạo từng cột
            foreach (var columnButtons in columns)
            {
                var columnPanel = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = true,
                    Margin = new Padding(10)
                };

                foreach (var text in columnButtons)
                {
                    var btn = CreateStyledButton(text);
                    columnPanel.Controls.Add(btn);
                }

                mainFlow.Controls.Add(columnPanel);
            }

            // Căn giữa layout chính
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
            btn.MouseEnter += (s, e) => btn.BackColor = Color.SlateBlue;
            // Hover ra: trả về màu gốc
            btn.MouseLeave += (s, e) => btn.BackColor = Color.MediumSlateBlue;
            btn.Paint += RoundedButtonPainter.PaintButton;
            // Gán sự kiện Click riêng từng nút
            btn.Click += (s, e) =>
            {
                var textResult =  HandleButtonClick(text);
                textAppender?.AppendText(textResult);
            };
            return btn;
        }

        private string HandleButtonClick(string action)
        {
            if (action == ScriptAutomationStatic.ControlBackup)
            {
                return "";
            }
            else if (action == ScriptAutomationStatic.ControlRestore)
            {
                return "";
            }
            else if (action == ScriptAutomationStatic.ControlLoginGmail)
            {
                return "";
            }
            else if (action == ScriptAutomationStatic.ControlLoadPlayStore)
            {
                return "";
            }
            else if (action == ScriptAutomationStatic.ControlChangeInfo)
            {
                return "";
            }
            else if (action == ScriptAutomationStatic.ControlChangeSIM)
            {
                return "";
            }
            else if (action == ScriptAutomationStatic.ControlWipeAccount)
            {
                return "";
            }
            else if (action == ScriptAutomationStatic.ControlWaitReboot)
            {
                return "WaitReboot()";
            }
            else if (action == ScriptAutomationStatic.ControlWaitInternet)
            {
                return "WaitInternet()";
            }
            else if (action == ScriptAutomationStatic.ControlPushFileToPhone)
            {
                return "PushFile(\"FromPC\", \"SendToPhone\")";
            }
            else if (action == ScriptAutomationStatic.ControlPullFIleToPC)
            {
                return "PullFile(\"FromPhone\", \"SendToPC\")";
            }
            else
            {
                return action;
            }

        }
    }
}

