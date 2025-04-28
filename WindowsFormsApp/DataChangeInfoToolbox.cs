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
    public partial class DataChangeInfoToolbox: UserControl
    {
        public DataChangeInfoToolbox()
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

            // Thêm vào panel chính
            roolPanel.Controls.Add(CreateMultiColumnButtonGroup(
                "Process Phone Actions",
                new List<string[]>
                {
                    new string[] { "Backup", "Restore", "Login Gmail", "Load play store" },
                    new string[] { "Change Info", "Change SIM", "Wipe Account", "Wait reboot", "Wait internet" },
                    new string[] { "Push File To Phone", "Pull FIle To PC" }
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
            btn.Click += (s, e) => HandleButtonClick(text);
            return btn;
        }

        private void HandleButtonClick(string action)
        {
            switch (action)
            {
                case "Backup":
                    BackupAction();
                    break;
                case "Restore":
                    RestoreAction();
                    break;
                case "Login Gmail":
                    LoginGmailAction();
                    break;
                case "Load play store":
                    LoadPlayStoreAction();
                    break;
                case "Change Info":
                    ChangeInfoAction();
                    break;
                case "Change SIM":
                    ChangeSimAction();
                    break;
                case "Wipe Account":
                    WipeAccountAction();
                    break;
                case "Wait reboot":
                    WaitRebootAction();
                    break;
                case "Wait internet":
                    WaitInternetAction();
                    break;
                case "Push File To Phone":
                    PushFileToPhoneAction();
                    break;
                case "Pull FIle To PC":
                    PullFileToPCAction();
                    break;
                default:
                    MessageBox.Show($"No action defined for: {action}");
                    break;
            }
        }

        // ====== Các hàm xử lý từng action ======
        private void BackupAction() => MessageBox.Show("Backup started...");
        private void RestoreAction() => MessageBox.Show("Restore started...");
        private void LoginGmailAction() => MessageBox.Show("Logging into Gmail...");
        private void LoadPlayStoreAction() => MessageBox.Show("Loading Play Store...");
        private void ChangeInfoAction() => MessageBox.Show("Changing device info...");
        private void ChangeSimAction() => MessageBox.Show("Changing SIM...");
        private void WipeAccountAction() => MessageBox.Show("Wiping account...");
        private void WaitRebootAction() => MessageBox.Show("Waiting for reboot...");
        private void WaitInternetAction() => MessageBox.Show("Waiting for internet...");
        private void PushFileToPhoneAction() => MessageBox.Show("Pushing file to phone...");
        private void PullFileToPCAction() => MessageBox.Show("Pulling file to PC...");
    }
}

