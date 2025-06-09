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
    public partial class KeyButtonToolbox: UserControl
    {
        private ITextAppender textAppender;
        public KeyButtonToolbox(ITextAppender textAppender)
        {
            InitializeComponent();
            this.textAppender = textAppender;
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

            var panelTop = new Panel
            {
                AutoSize = true
            };

            var btnSendKey = CreateStyledButton(ScriptAutomationStatic.ControlSendKey);
            var linkListKey = new LinkLabel
            {
                Text = ScriptAutomationStatic.ControlListKey,
                AutoSize = true,
                Location = new Point(btnSendKey.Width + 10, 10),
                LinkColor = Color.Brown
            };
            linkListKey.Click += (s, e) => System.Diagnostics.Process.Start("https://gist.github.com/arjunv/2bbcca9a1a1c127749f8dcb6d36fb0bc");

            panelTop.Controls.Add(btnSendKey);
            panelTop.Controls.Add(linkListKey);

            var btnCtrlA = CreateStyledButton(ScriptAutomationStatic.ControlCtrlA);

            var btnCheckKeyboard = CreateStyledButton(ScriptAutomationStatic.ControlCheckKeyboard);

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
            btn.Click += (s, e) => OnButtonClick(btn);
            return btn;
        }

        private void OnButtonClick(System.Windows.Forms.Button button)
        {
            if (button.Text == ScriptAutomationStatic.ControlSendKey)
            {
                textAppender?.AppendText("SendKey(66)");
            }
            else if (button.Text == ScriptAutomationStatic.ControlCheckKeyboard)
            {
                textAppender?.AppendText("CheckKeyBoard()");
            }
            else if (button.Text == ScriptAutomationStatic.ControlCtrlA)
            {
                textAppender?.AppendText("CtrlA()");
            }
        }
    }
}

