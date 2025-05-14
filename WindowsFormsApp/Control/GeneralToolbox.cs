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
        "WiFi ON", "WiFi OFF", "Open URL", "ON Bproxy", "Auto proxy", "Check SIM", "Command(shell)"
    };

            foreach (var text in leftButtons)
            {
                var btn = CreateStyledButton(text);
                leftPanel.Controls.Add(btn);
            }

            // GroupBox: Package
            var packageGroup = CreateMultiColumnButtonGroup(
                "Package",
                new List<string[]>
                {
            new string[] { "Open App", "Close App", "Enable App", "Disable App" },
            new string[] { "Install app", "Uninstall", "Clear data", "Swipe close app", "Load app" }
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
            switch (action)
            {
                case "WiFi ON":
                    return "WiFiON()";
                case "WiFi OFF":
                    return "WiFiOFF()";
                case "Open URL":
                    return "OpenURL(\"YOU_URL\")";
                case "ON Bproxy":
                    return "ConnectBproxy()";
                case "Auto proxy":
                    return "AutoProxy()";
                case "Check SIM":
                    return "CheckSimOnline()";
                case "Command(shell)":
                    return "RunCommandShell(\"command\")";
                case "Open App":
                    return "OpenApp(\"package_app\")";
                case "Close App":
                    return "CloseApp(\"package_app\")";
                case "Enable App":
                    return "EnableApp(\"package_app\")";
                case "Disable App":
                    return "DisbledApp(\"package_app\")";
                case "Install app":
                    return "InstallApp(\"path_to_apk\")";
                case "Uninstall":
                    return "UninstallApp(\"package_app\")";
                case "Clear data":
                    return "ClearDataApp(\"package_app\")";
                case "Swipe close app":
                    return "SwipeCloseApp()";
                default:
                    return action;
            }
        }
    }
}
