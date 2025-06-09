using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Animation;
using WindowsFormsApp.Model.Static;

namespace WindowsFormsApp
{
    public partial class GeneralToolbox: UserControl
    {
        private ITextAppender textAppender;
        public GeneralToolbox(ITextAppender appender)
        {
            InitializeComponent();
            textAppender = appender;
            BuildFullUI();
        }

        private void BuildFullUI()
        {
            this.BackColor = Color.WhiteSmoke;

            var roolPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Cột bên trái (WiFi, Proxy, Shell...)
            var leftPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                Padding = new Padding(10)
            };

            string[] leftButtons = {
        ScriptAutomationStatic.ControlWiFiON, ScriptAutomationStatic.ControlWiFiOFF, ScriptAutomationStatic.ControlOpenURL /*, "ON Bproxy", "Auto proxy", "Check SIM"*/, ScriptAutomationStatic.ControlCommand
    };

            foreach (var text in leftButtons)
            {
                var btn = CreateStyledButton(text);
                leftPanel.Controls.Add(btn);
            }

            // GroupBox: Package
            var packageGroup = CreateMultiColumnButtonGroup(
                ScriptAutomationStatic.TitlePackage,
                new List<string[]>
                {
            new string[] { ScriptAutomationStatic.ControlOpenApp, ScriptAutomationStatic.ControlCloseApp, ScriptAutomationStatic.ControlEnableApp, ScriptAutomationStatic.ControlDisableApp },
            new string[] { ScriptAutomationStatic.ControlInstallApp, ScriptAutomationStatic.ControlUninstall, ScriptAutomationStatic.ControlClearData, ScriptAutomationStatic.ControlSwipeCloseApp, ScriptAutomationStatic.ControlLoadApp }
                },
                fixedWidth: 370,
                fixedHeight: 300
            );

            // Thêm vào roolPanel
            roolPanel.Controls.Add(leftPanel);
            roolPanel.Controls.Add(packageGroup);

            this.Controls.Add(roolPanel);
        }

        private GroupBox CreateMultiColumnButtonGroup(string title, List<string[]> columns, int fixedWidth = 600, int fixedHeight = 400)
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

            var mainFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(10)
            };

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
                    var btn = CreateStyledButtonGroup(text);
                    columnPanel.Controls.Add(btn);
                }

                mainFlow.Controls.Add(columnPanel);
            }

            mainFlow.PerformLayout();
            int centerX = (fixedWidth - mainFlow.PreferredSize.Width) / 2;
            mainFlow.Location = new Point(centerX, 30);

            group.Controls.Add(mainFlow);
            return group;
        }
        private Button CreateStyledButtonGroup(string text)
        {
            var btn = new Button
            {
                Text = text,
                Width = 130,
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
            btn.Click += (s, e) =>
            {
                if (text.Equals("Load app"))
                {
                    LoadAppForm loadAppFrm = new LoadAppForm();
                    loadAppFrm.Show();
                }
                else
                {
                    var textResult = GetTextHandleByButton(text);
                    textAppender?.AppendText(textResult);
                }
            };
            return btn;
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
                Font = new Font("Segoe UI", 8f)  ,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Paint += RoundedButtonPainter.PaintButton;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.SlateBlue;
            // Hover ra: trả về màu gốc
            btn.MouseLeave += (s, e) => btn.BackColor = Color.MediumSlateBlue;
            btn.Click += (s, e) => {
                var textResult = GetTextHandleByButton(text);
                textAppender?.AppendText(textResult);
            };
            return btn;
        }
        private string GetTextHandleByButton(string action)
        {
            if (action == ScriptAutomationStatic.ControlWiFiON)
                return "WiFiON()";
            else if (action == ScriptAutomationStatic.ControlWiFiOFF)
                return "WiFiOFF()";
            else if (action == ScriptAutomationStatic.ControlOpenURL)
                return "OpenURL(\"YOU_URL\")";
            else if (action == ScriptAutomationStatic.ControlCommand)
                return "RunCommandShell(\"command\")";
            else if (action == ScriptAutomationStatic.ControlOpenApp)
                return "OpenApp(\"package_app\")";
            else if (action == ScriptAutomationStatic.ControlCloseApp)
                return "CloseApp(\"package_app\")";
            else if (action == ScriptAutomationStatic.ControlEnableApp)
                return "EnableApp(\"package_app\")";
            else if (action == ScriptAutomationStatic.ControlDisableApp)
                return "DisbledApp(\"package_app\")";
            else if (action == ScriptAutomationStatic.ControlInstallApp)
                return "InstallApp(\"path_to_apk\")";
            else if (action == ScriptAutomationStatic.ControlUninstall)
                return "UninstallApp(\"package_app\")";
            else if (action == ScriptAutomationStatic.ControlClearData)
                return "ClearDataApp(\"package_app\")";
            else if (action == ScriptAutomationStatic.ControlSwipeCloseApp)
                return "SwipeCloseApp()";
            else
                return action;

        }
    }
}
