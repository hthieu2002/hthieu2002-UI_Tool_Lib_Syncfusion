using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class ScreenView : Form
    {
        public class DeviceDisplay
        {
            public string Serial { get; set; }
            public Process ScrcpyProcess { get; set; }
            public IntPtr ScrcpyWindow { get; set; }
            public Panel DevicePanel { get; set; }
        }

        private FlowLayoutPanel flowLayoutPanel;
        private FlowLayoutPanel rightPanel;
        private const int panelWidth = 200;
        private const float aspectRatio = 0.4615f;
        private const int panelHeight = (int)(panelWidth / aspectRatio);
        private int scale = 150;
        private int maxSize = 1280;
        private bool isTurnScreenOff = true;
        public List<DeviceDisplay> deviceDisplays = new List<DeviceDisplay>();
        List<string> activeDevices = new List<string>();
        List<Form> openedForms = new List<Form>();
        string[] deviceIds = { };
        private Label selectedDevicesLabel;
        private CheckBox cbTurnOffScreen;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);
        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOWNORMAL = 1;
        private const int SW_HIDE = 0;

        [DllImport("user32.dll")]
        private static extern bool SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_APPWINDOW = 0x00040000;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public const uint LWA_COLORKEY = 0x00000001;
        public const uint LWA_ALPHA = 0x00000002;
        public const int WS_EX_TRANSPARENT = 0x20;


        public ScreenView()
        {
            InitializeComponent();
            load();
            this.FormClosing += (s, e) =>
            {
                foreach (var process in Process.GetProcessesByName("scrcpy"))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch { /* Có thể bị lỗi nếu process đã chết, bỏ qua */ }
                }
            };
        }

        private void load()
        {
            init();
            StartDeviceCheck();
        }

        private void init()
        {
            this.Text = "Device Management";

            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.SplitterDistance = (int)(this.Width * 0.75); // 75% bên trái
            splitContainer.IsSplitterFixed = true; // Cho phép kéo nếu muốn


            flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.WrapContents = true;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            splitContainer.Panel1.Controls.Add(flowLayoutPanel);


            rightPanel = new FlowLayoutPanel();
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.BackColor = SystemColors.Control; // Màu mặc định

            // Tạo đường kẻ dọc full chiều cao
            Panel leftBorder = new Panel();
            leftBorder.Width = 1;
            leftBorder.Dock = DockStyle.Left; // Gắn sát trái
            leftBorder.BackColor = Color.DarkGray;

            // Thêm đường kẻ vào Panel2 trước khi thêm rightPanel (để không bị che)
            splitContainer.Panel2.Controls.Add(leftBorder);
            splitContainer.Panel2.Controls.Add(rightPanel);

            this.Controls.Add(splitContainer);
            CreateActionButtons();
        }
        private void CreateActionButtons()
        {
            AddTrackBar(rightPanel, "Tỉ lệ khung hình(%): ", 100, 200, 150, (s, e) =>
            {
                scale = ((TrackBar)s).Value;
            });

            AddTrackBar(rightPanel, "Độ phân giải (Sắc nét)", 240, 2200, 1280, (s, e) =>
            {
                maxSize = ((TrackBar)s).Value;
            });

            // Tạo checkbox tắt màn hình khi view
            cbTurnOffScreen = new CheckBox();
            cbTurnOffScreen.Text = "Tắt màn hình khi xem";
            cbTurnOffScreen.AutoSize = true;
            cbTurnOffScreen.Checked = true;
            cbTurnOffScreen.Margin = new Padding(20, 10, 10, 10);
            cbTurnOffScreen.CheckedChanged += CbTurnOffScreen_CheckedChanged;
            // Thêm checkbox vào parent panel
            rightPanel.Controls.Add(cbTurnOffScreen);

            AddActionPanel(rightPanel);

            selectedDevicesLabel = new Label();
            selectedDevicesLabel.AutoSize = true;
            selectedDevicesLabel.Margin = new Padding(10);
            selectedDevicesLabel.Text = $"Số thiết bị được chọn: {activeDevices.Count}"; // Hiển thị số lượng ban đầu
            // Thêm Label vào parent panel
            rightPanel.Controls.Add(selectedDevicesLabel);


            deviceIds = GetConnectedDevices();
            AddDeviceButtons(rightPanel, deviceIds);
        }
        private void CbTurnOffScreen_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbTurnOffScreen.Checked;
            isTurnScreenOff = isChecked;
        }
        private void UpdateSelectedDevicesLabel()
        {
            selectedDevicesLabel.Text = $"Số thiết bị được chọn: {activeDevices.Count}";
        }
        private void AddActionPanel(Panel parent)
        {
            Panel bottomPanel = new Panel();
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 100;
            bottomPanel.Padding = new Padding(10);
            bottomPanel.Width = 300;

            System.Windows.Forms.Button btnView = new System.Windows.Forms.Button();
            btnView.Text = "View";
            btnView.AutoSize = true;
            btnView.Margin = new Padding(5);
            btnView.Click += (s, e) =>
            {
                int startX = 100;
                int startY = 100;
                int offsetX = 50;
                int index = 0;

                foreach (string deviceId in activeDevices)
                {
                    Form deviceForm = new Form();
                    deviceForm.Text = $"Device {deviceId}";
                    deviceForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    deviceForm.MaximizeBox = false;

                    float scaleScreen = scale / 100f;
                    int width = (int)(200 * scaleScreen) + 180;
                    int height = (int)(453 * scaleScreen) + 130;

                    width = Math.Min(width, Screen.PrimaryScreen.WorkingArea.Width);
                    height = Math.Min(height, Screen.PrimaryScreen.WorkingArea.Height);

                    deviceForm.Size = new System.Drawing.Size(width, height);
                    deviceForm.StartPosition = FormStartPosition.CenterScreen;
                    deviceForm.TopMost = false;

                    Panel scrcpyPanel = new Panel();
                    scrcpyPanel.Dock = DockStyle.Fill;
                    scrcpyPanel.BackColor = Color.Blue;

                    Panel controlPanel = new Panel();
                    controlPanel.Dock = DockStyle.Right;
                    controlPanel.Width = (int)(width * 0.2);
                    controlPanel.BackColor = Color.LightGray;

                    Button btnHome = new Button { Text = "Home", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                    Button btnBack = new Button { Text = "Back", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                    Button btnReboot = new Button { Text = "Reboot Android", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                    Button btnPowerOff = new Button { Text = "Power Off Android", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                    Button btnIncreaseVolume = new Button { Text = "Increase Volume", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                    Button btnDecreaseVolume = new Button { Text = "Decrease Volume", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };

                    btnHome.Click += BtnHome_Click;
                    btnBack.Click += BtnBack_Click;
                    btnReboot.Click += BtnReboot_Click;
                    btnPowerOff.Click += BtnPowerOff_Click;
                    btnIncreaseVolume.Click += BtnIncreaseVolume_Click;
                    btnDecreaseVolume.Click += BtnDecreaseVolume_Click;

                    void BtnHome_Click(object senderHome, EventArgs e1) => ExecuteAdbCommand("input keyevent 3", deviceId); // Home
                    void BtnBack_Click(object senderBack, EventArgs e2) => ExecuteAdbCommand("input keyevent 4", deviceId); // Back
                    void BtnReboot_Click(object senderReboot, EventArgs e3)
                    {
                        deviceForm.Close();
                        // Gọi lệnh ADB để reboot Android
                        ExecuteAdbCommand("reboot", deviceId);
                    }

                    void BtnPowerOff_Click(object senderPowerOff, EventArgs e4) => ExecuteAdbCommand("input keyevent 26", deviceId); // Power Off
                    void BtnIncreaseVolume_Click(object senderIncreaseVolume, EventArgs e5) => ExecuteAdbCommand("input keyevent 24", deviceId); // Increase Volume
                    void BtnDecreaseVolume_Click(object senderDecreaseVolume, EventArgs e6) => ExecuteAdbCommand("input keyevent 25", deviceId); // Decrease Volume

                    void ExecuteAdbCommand(string command, string device)
                    {
                        ProcessStartInfo startInfoFormScreen = new ProcessStartInfo()
                        {
                            FileName = "./Resources/adb.exe",
                            Arguments = $"-s {device} shell {command}",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };

                        Process adbProcess = Process.Start(startInfoFormScreen);
                        adbProcess.WaitForExit();
                    }

                    controlPanel.Controls.Add(btnHome);
                    controlPanel.Controls.Add(btnBack);
                    controlPanel.Controls.Add(btnReboot);
                    controlPanel.Controls.Add(btnPowerOff);
                    controlPanel.Controls.Add(btnIncreaseVolume);
                    controlPanel.Controls.Add(btnDecreaseVolume);

                    deviceForm.Controls.Add(scrcpyPanel);
                    deviceForm.Controls.Add(controlPanel);

                    Process scrcpyProcessZoom = null;
                    IntPtr scrcpyWindowZoom = IntPtr.Zero;

                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        FileName = "./Resources/scrcpy.exe",
                        Arguments = $"-s {deviceId} --max-size {maxSize} --max-fps 15 --video-bit-rate 2M " +
                                    $"{(isTurnScreenOff ? "--turn-screen-off " : "")}" +
                                    $"--window-borderless --window-x 3000 --window-y 3000 --fullscreen",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    bool scrcpyStarted = false;
                    while (!scrcpyStarted)
                    {

                        scrcpyProcessZoom = Process.Start(startInfo);

                        while (scrcpyWindowZoom == IntPtr.Zero)
                        {
                            foreach (Process p in Process.GetProcessesByName("scrcpy"))
                            {
                                if (p.Id == scrcpyProcessZoom.Id)
                                {
                                    foreach (ProcessThread thread in p.Threads)
                                    {
                                        EnumThreadWindows(thread.Id, (hWnd, lParam) =>
                                        {
                                            scrcpyWindowZoom = hWnd;
                                            return false;
                                        }, IntPtr.Zero);
                                    }
                                }
                            }
                            Task.Delay(500);
                        }

                        if (scrcpyWindowZoom != IntPtr.Zero)
                        {
                            Thread.Sleep(1000);
                            scrcpyPanel.Resize += (sender, args) =>
                            {
                                if (scrcpyWindowZoom != IntPtr.Zero)
                                {
                                    MoveWindow(scrcpyWindowZoom, 0, 0, scrcpyPanel.Width, scrcpyPanel.Height, true);
                                }
                            };

                            ShowWindow(scrcpyWindowZoom, SW_HIDE);
                            SetParent(scrcpyWindowZoom, scrcpyPanel.Handle);
                            ShowWindow(scrcpyWindowZoom, SW_SHOWNORMAL);
                            MoveWindow(scrcpyWindowZoom, 0, 0, scrcpyPanel.Width, scrcpyPanel.Height, true);

                            scrcpyStarted = true;
                        }
                    }

                    MonitorDeviceConnection(deviceId, deviceForm);

                    deviceForm.Show();
                    openedForms.Add(deviceForm);

                    async void MonitorDeviceConnection(string device, Form deviceForm1)
                    {
                        while (true)
                        {
                            bool isDeviceConnected = await IsDeviceConnected(device);

                            if (!isDeviceConnected)
                            {
                                if (deviceForm.IsHandleCreated)
                                {
                                    deviceForm.Invoke((Action)(() =>
                                    {
                                        deviceForm.Close();
                                    }));
                                }
                                else
                                {
                                    deviceForm.HandleCreated += (sender, args) =>
                                    {
                                        deviceForm.Close();
                                    };
                                }

                                // Thoát vòng lặp sau khi đóng form
                                break;
                            }
                            await Task.Delay(1000);  // Đợi 1 giây trước khi kiểm tra lại
                        }
                    }


                    async Task<bool> IsDeviceConnected(string device)
                    {
                        ProcessStartInfo startInfo1 = new ProcessStartInfo()
                        {
                            FileName = "./Resources/adb.exe",
                            Arguments = "devices",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };

                        Process adbProcess = Process.Start(startInfo1);
                        string output = await adbProcess.StandardOutput.ReadToEndAsync();

                        return output.Contains(device);
                    }
                    deviceForm.StartPosition = FormStartPosition.Manual;
                    deviceForm.Location = new Point(startX + (width + offsetX) * index, startY);
                    index++;
                    Thread.Sleep(1000);
                }
                AddDeviceButtons(rightPanel, deviceIds);
                activeDevices.Clear();
                UpdateSelectedDevicesLabel();
            };
            StyleButton(btnView);

            Button btnCloseAll = new Button();
            btnCloseAll.Text = "Close All";
            btnCloseAll.AutoSize = true;
            btnCloseAll.Margin = new Padding(5);
            btnCloseAll.Click += (s, e) =>
            {
                foreach (var form in openedForms.ToList())
                {
                    if (!form.IsDisposed)
                    {
                        form.Invoke((Action)(() =>
                        {
                            form.Close();
                        }));
                    }
                }

                openedForms.Clear();
            };

            StyleButton(btnCloseAll);

            Button btnRefreshDevice = new Button();
            btnRefreshDevice.Text = "Refresh";
            btnRefreshDevice.AutoSize = true;
            btnRefreshDevice.Margin = new Padding(5);
            btnRefreshDevice.Click += (s, e) =>
            {
                deviceIds = GetConnectedDevices();
                AddDeviceButtons(rightPanel, deviceIds);
                activeDevices.Clear();
                UpdateSelectedDevicesLabel();
            };
            StyleButton(btnRefreshDevice);

            Button btnPushFile = new Button();
            btnPushFile.Text = "Push File";
            btnPushFile.AutoSize = true;
            btnPushFile.Margin = new Padding(5);
            btnPushFile.Click += (s, e) =>
            {
                MessageBox.Show("Push File clicked");
            };
            StyleButton(btnPushFile);

            Button btnInstallAPK = new Button();
            btnInstallAPK.Text = "Install APK";
            btnInstallAPK.AutoSize = true;
            btnInstallAPK.Margin = new Padding(5);
            btnInstallAPK.Click += (s, e) =>
            {
                MessageBox.Show("Install APK clicked");
            };
            StyleButton(btnInstallAPK);

            FlowLayoutPanel buttonGroup = new FlowLayoutPanel();
            buttonGroup.Dock = DockStyle.Fill;
            buttonGroup.FlowDirection = FlowDirection.LeftToRight;
            buttonGroup.WrapContents = true; // Cho phép xuống dòng
            buttonGroup.AutoSize = true;
            buttonGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            buttonGroup.Controls.Add(btnView);
            buttonGroup.Controls.Add(btnCloseAll);
            buttonGroup.Controls.Add(btnRefreshDevice);
            buttonGroup.Controls.Add(btnPushFile);
            buttonGroup.Controls.Add(btnInstallAPK);

            bottomPanel.Controls.Add(buttonGroup);
            parent.Controls.Add(bottomPanel);
        }
        private void StyleButton(Button button)
        {
            button.Cursor = Cursors.Hand;
            button.BackColor = Color.LightBlue;
            button.ForeColor = Color.Black;

            button.FlatStyle = FlatStyle.Flat; // Cho đẹp, phẳng
            button.FlatAppearance.BorderSize = 0;

            button.MouseEnter += (s, e) =>
            {
                button.BackColor = Color.DodgerBlue;
                button.ForeColor = Color.White;
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor = Color.LightBlue;
                button.ForeColor = Color.Black;
            };
        }
        private void AddTrackBar(Panel parent, string labelText, int min, int max, int value, EventHandler onChange)
        {
            // Tạo label mô tả
            Label label = new Label();
            label.Text = labelText;
            label.AutoSize = true;
            label.Width = parent.ClientSize.Width;
            label.Margin = new Padding(20, 10, 0, 0); // Thêm khoảng cách trên
            parent.Controls.Add(label);

            // Tạo panel chứa TrackBar và giá trị
            Panel trackBarPanel = new Panel();
            trackBarPanel.Height = 40;
            trackBarPanel.Dock = DockStyle.Top;
            trackBarPanel.Padding = new Padding(0, 0, 0, 10);
            trackBarPanel.Width = 250;
            parent.Controls.Add(trackBarPanel);

            // TrackBar
            TrackBar trackBar = new TrackBar();
            trackBar.Minimum = min;
            trackBar.Maximum = max;
            trackBar.Value = value;
            trackBar.TickStyle = TickStyle.None;
            trackBar.Width = 180;
            trackBar.Left = 20;
            trackBar.Top = 5;
            trackBar.Scroll += onChange;
            trackBarPanel.Controls.Add(trackBar);

            // Label hiển thị giá trị
            Label valueLabel = new Label();
            valueLabel.Text = value.ToString();
            valueLabel.Left = trackBar.Right + 10;
            valueLabel.Top = 10;
            valueLabel.AutoSize = true;
            trackBarPanel.Controls.Add(valueLabel);

            // Cập nhật giá trị khi kéo
            trackBar.Scroll += (s, e) => valueLabel.Text = trackBar.Value.ToString();
        }
        private class DeviceButtonState
        {
            public string Id { get; set; }
            public bool IsActive { get; set; }
        }

        private void AddDeviceButtons(FlowLayoutPanel parent, string[] deviceIds)
        {
            // Tìm FlowLayoutPanel hiện tại nếu có
            FlowLayoutPanel buttonPanel = parent.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();

            if (buttonPanel == null)
            {
                // Nếu chưa có FlowLayoutPanel, tạo mới
                buttonPanel = new FlowLayoutPanel();
                buttonPanel.Dock = DockStyle.Top;
                buttonPanel.Height = 500;
                buttonPanel.Width = 350;
                buttonPanel.AutoScroll = true;
                buttonPanel.WrapContents = true;
                buttonPanel.Padding = new Padding(10);
                buttonPanel.Margin = new Padding(0, 10, 0, 0);
                buttonPanel.FlowDirection = FlowDirection.LeftToRight;
                buttonPanel.BackColor = Color.LightGray;

                parent.Controls.Add(buttonPanel);
            }
            else
            {
                // Nếu đã có FlowLayoutPanel, xóa các button cũ
                buttonPanel.Controls.Clear();
            }

            // Thêm các button mới từ danh sách deviceIds
            foreach (string deviceId in deviceIds)
            {
                Button btn = new Button();
                btn.Text = $"{deviceId}";
                btn.AutoSize = true;
                btn.Margin = new Padding(5);
                btn.Tag = new DeviceButtonState
                {
                    Id = deviceId,
                    IsActive = false
                };
                btn.BackColor = SystemColors.Control; // Mặc định

                btn.Click += (s, e) =>
                {
                    Button clickedBtn = (Button)s;
                    var state = (DeviceButtonState)clickedBtn.Tag;

                    // Toggle trạng thái
                    state.IsActive = !state.IsActive;

                    // Cập nhật giao diện
                    clickedBtn.BackColor = state.IsActive ? Color.LightGreen : SystemColors.Control;

                    // In ra ID nếu muốn xử lý
                    string clickedId = state.Id;

                    if (state.IsActive)
                    {
                        // Thêm ID vào danh sách khi active
                        if (!activeDevices.Contains(clickedId))
                        {
                            activeDevices.Add(clickedId);
                        }
                    }
                    else
                    {
                        // Xóa ID khỏi danh sách khi unactive
                        if (activeDevices.Contains(state.Id))
                        {
                            activeDevices.Remove(state.Id);
                        }
                    }
                    UpdateSelectedDevicesLabel();
                };

                buttonPanel.Controls.Add(btn);
            }
        }

        private async void StartDeviceCheck()
        {
            while (true)
            {
                var devices = GetConnectedDevices();

                var newDevices = devices.Except(deviceDisplays.Select(d => d.Serial)).ToList();
                int countDevice = newDevices.Count();

                GlobalContextMenu.setCountDevice(countDevice);
                GlobalContextMenu.UpdateContextMenu();
                foreach (var device in newDevices)
                {
                    if (device != "accd8f87")
                    {
                        await Task.Run(() =>
                        {
                            if (this.IsHandleCreated)
                            {
                                Invoke((MethodInvoker)(() => AddDeviceView(device)));
                            }
                        });
                        await Task.Delay(3000);
                    }
                }

                var connectedDevices = devices.ToHashSet();
                var disconnectedDevices = deviceDisplays.Where(d => !connectedDevices.Contains(d.Serial)).ToList();
                foreach (var disconnectedDevice in disconnectedDevices)
                {
                    if (this.IsHandleCreated)
                    {
                        Invoke((MethodInvoker)(() => RemoveDeviceView(disconnectedDevice)));
                    }
                }
                await Task.Delay(1000);
            }
        }


        private void AddDeviceView(string deviceId)
        {
            var existingDevicePanel = flowLayoutPanel.Controls.Cast<Control>()
                .FirstOrDefault(c => c is Panel panel && panel.Controls.OfType<Label>().FirstOrDefault()?.Text == deviceId);

            if (existingDevicePanel != null)
            {
                flowLayoutPanel.Controls.Remove(existingDevicePanel);
                existingDevicePanel.Dispose();
            }


            if (existingDevicePanel == null)
            {
                Panel devicePanel = new Panel();
                devicePanel.Size = new System.Drawing.Size(panelWidth, panelHeight + 20);
                devicePanel.Margin = new Padding(10);
                devicePanel.BackColor = System.Drawing.Color.LightGray;



                flowLayoutPanel.Controls.Add(devicePanel);

                devicePanel.MouseClick += (s, e) =>
                {
                    flowLayoutPanel.Controls.Remove(devicePanel);
                    devicePanel.Dispose();
                };
                StartScrcpyForDeviceAsync(deviceId, devicePanel);
            }
        }

        private void RemoveDeviceView(DeviceDisplay deviceDisplay)
        {
            flowLayoutPanel.Controls.Remove(deviceDisplay.DevicePanel);
            deviceDisplay.DevicePanel.Dispose();
            deviceDisplays.Remove(deviceDisplay);
        }

        private string[] GetConnectedDevices()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = @"./Resources/adb.exe",
                Arguments = "devices",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            var devices = output.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                                 .Where(line => !line.StartsWith("List") && !line.StartsWith("---------"))
                                 .Select(line => line.Split('\t')[0])
                                 .ToArray();

            return devices;
        }

        private async void StartScrcpyForDeviceAsync(string deviceId, Panel devicePanel)
        {
            string scrcpyExePath = "./Resources/scrcpy.exe";
            Process scrcpyProcess = null;
            IntPtr scrcpyWindow = IntPtr.Zero;
            int attempts = 0;

            Label deviceLabel = new Label();
            deviceLabel.Text = $"View Device: {deviceId}";
            deviceLabel.TextAlign = ContentAlignment.MiddleCenter;
            deviceLabel.BackColor = ColorTranslator.FromHtml("#5677FE");
            deviceLabel.Dock = DockStyle.Bottom;
            deviceLabel.ForeColor = Color.White;
            devicePanel.Controls.Add(deviceLabel);
            deviceLabel.BringToFront();

            deviceLabel.Cursor = Cursors.Hand;
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(deviceLabel, "View");

            deviceLabel.Click += async (sender, e) =>
            {

                Form deviceForm = new Form();
                deviceForm.Text = $"Device {deviceId}";
                deviceForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                deviceForm.MaximizeBox = false;
                float scaleScreen = scale / 100f;
                int width = (int)(devicePanel.Width * scaleScreen) + 180;
                int height = (int)(devicePanel.Height * scaleScreen) + 130;

                width = Math.Min(width, Screen.PrimaryScreen.WorkingArea.Width);
                height = Math.Min(height, Screen.PrimaryScreen.WorkingArea.Height);

                deviceForm.Size = new Size(width, height);
                deviceForm.StartPosition = FormStartPosition.CenterScreen;
                deviceForm.TopMost = false;

                Panel scrcpyPanel = new Panel();
                scrcpyPanel.Dock = DockStyle.Fill;
                scrcpyPanel.BackColor = Color.Blue;

                Panel controlPanel = new Panel();
                controlPanel.Dock = DockStyle.Right;
                controlPanel.Width = (int)(width * 0.2);
                controlPanel.BackColor = Color.LightGray;

                Button btnHome = new Button { Text = "Home", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                Button btnBack = new Button { Text = "Back", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                Button btnReboot = new Button { Text = "Reboot Android", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                Button btnPowerOff = new Button { Text = "Power Off Android", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                Button btnIncreaseVolume = new Button { Text = "Increase Volume", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
                Button btnDecreaseVolume = new Button { Text = "Decrease Volume", Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };

                btnHome.Click += BtnHome_Click;
                btnBack.Click += BtnBack_Click;
                btnReboot.Click += BtnReboot_Click;
                btnPowerOff.Click += BtnPowerOff_Click;
                btnIncreaseVolume.Click += BtnIncreaseVolume_Click;
                btnDecreaseVolume.Click += BtnDecreaseVolume_Click;

                void BtnHome_Click(object senderHome, EventArgs e1) => ExecuteAdbCommand("input keyevent 3", deviceId); // Home
                void BtnBack_Click(object senderBack, EventArgs e2) => ExecuteAdbCommand("input keyevent 4", deviceId); // Back
                void BtnReboot_Click(object senderReboot, EventArgs e3)
                {
                    deviceForm.Close();
                    // Gọi lệnh ADB để reboot Android
                    ExecuteAdbCommand("reboot", deviceId);
                }

                void BtnPowerOff_Click(object senderPowerOff, EventArgs e4) => ExecuteAdbCommand("input keyevent 26", deviceId); // Power Off
                void BtnIncreaseVolume_Click(object senderIncreaseVolume, EventArgs e5) => ExecuteAdbCommand("input keyevent 24", deviceId); // Increase Volume
                void BtnDecreaseVolume_Click(object senderDecreaseVolume, EventArgs e6) => ExecuteAdbCommand("input keyevent 25", deviceId); // Decrease Volume

                void ExecuteAdbCommand(string command, string device)
                {
                    ProcessStartInfo startInfoFormScreen = new ProcessStartInfo()
                    {
                        FileName = "./Resources/adb.exe",
                        Arguments = $"-s {device} shell {command}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    Process adbProcess = Process.Start(startInfoFormScreen);
                    adbProcess.WaitForExit();
                }

                controlPanel.Controls.Add(btnHome);
                controlPanel.Controls.Add(btnBack);
                controlPanel.Controls.Add(btnReboot);
                controlPanel.Controls.Add(btnPowerOff);
                controlPanel.Controls.Add(btnIncreaseVolume);
                controlPanel.Controls.Add(btnDecreaseVolume);

                deviceForm.Controls.Add(scrcpyPanel);
                deviceForm.Controls.Add(controlPanel);

                Process scrcpyProcessZoom = null;
                IntPtr scrcpyWindowZoom = IntPtr.Zero;

                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = scrcpyExePath,
                    Arguments = $"-s {deviceId} --max-size {maxSize} --max-fps 15 " +
                                $"--window-borderless --window-x 3000 --window-y 3000  --fullscreen",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                bool scrcpyStarted = false;
                while (!scrcpyStarted)
                {
                    scrcpyProcessZoom = Process.Start(startInfo);

                    while (scrcpyWindowZoom == IntPtr.Zero)
                    {
                        foreach (Process p in Process.GetProcessesByName("scrcpy"))
                        {
                            if (p.Id == scrcpyProcessZoom.Id)
                            {
                                foreach (ProcessThread thread in p.Threads)
                                {
                                    EnumThreadWindows(thread.Id, (hWnd, lParam) =>
                                    {
                                        scrcpyWindowZoom = hWnd;
                                        return false;
                                    }, IntPtr.Zero);
                                }
                            }
                        }

                        await Task.Delay(500);
                    }

                    if (scrcpyWindowZoom != IntPtr.Zero)
                    {
                        ShowWindow(scrcpyWindowZoom, SW_HIDE);
                        SetParent(scrcpyWindowZoom, scrcpyPanel.Handle);
                        ShowWindow(scrcpyWindowZoom, SW_SHOWNORMAL);
                        MoveWindow(scrcpyWindowZoom, 0, 0, scrcpyPanel.Width, scrcpyPanel.Height, true);
                        scrcpyStarted = true;
                    }
                }

                //var connectionCheckTimer = new System.Threading.Timer(async _ =>
                //{
                //    bool isDeviceConnected = await IsDeviceConnected(deviceId);
                //    if (!isDeviceConnected)
                //    {
                //        deviceForm.Invoke((Action)(() =>
                //        {
                //            deviceForm.Close();
                //        }));
                //    }
                //}, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));


                //deviceForm.FormClosing += (senderForm, closingArgs) =>
                //{
                //    // Kiểm tra xem tiến trình scrcpyProcessZoom có đang chạy không trước khi gọi Kill()
                //    if (scrcpyProcessZoom != null && !scrcpyProcessZoom.HasExited)
                //    {
                //        scrcpyProcessZoom.Kill();  // Giết tiến trình nếu nó vẫn đang chạy
                //    }

                //    // Giải phóng timer kết nối
                //  //  connectionCheckTimer.Dispose();
                //};
                MonitorDeviceConnection(deviceId, deviceForm);

                deviceForm.Show();
            };

            async void MonitorDeviceConnection(string device, Form deviceForm)
            {
                while (true)
                {
                    bool isDeviceConnected = await IsDeviceConnected(deviceId);

                    if (!isDeviceConnected)
                    {
                        if (deviceForm.IsHandleCreated)
                        {
                            deviceForm.Invoke((Action)(() =>
                            {
                                deviceForm.Close();
                            }));
                        }
                        else
                        {
                            deviceForm.HandleCreated += (sender, args) =>
                            {
                                deviceForm.Close();
                            };
                        }

                        // Thoát vòng lặp sau khi đóng form
                        break;
                    }
                    await Task.Delay(1000);  // Đợi 1 giây trước khi kiểm tra lại
                }
            }


            async Task<bool> IsDeviceConnected(string device)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "./Resources/adb.exe",
                    Arguments = "devices",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                Process adbProcess = Process.Start(startInfo);
                string output = await adbProcess.StandardOutput.ReadToEndAsync();

                return output.Contains(device);
            }

            async Task<bool> TryStartScrcpyProcess()
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = scrcpyExePath,
                    Arguments = $"-s {deviceId} --max-size {Math.Min(1080, 2220)} --max-fps 15 " +
                                $"--window-borderless --window-x 3000 --window-y 3000 --window-width {panelWidth} --window-height {panelHeight} --no-control",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                scrcpyProcess = Process.Start(startInfo);
                await Task.Delay(3000);
                return scrcpyProcess != null && !scrcpyProcess.HasExited;
            }

            while (scrcpyWindow == IntPtr.Zero && attempts < 5)
            {
                bool started = await TryStartScrcpyProcess();
                if (!started)
                {
                    if (devicePanel?.Parent != null)
                    {
                        devicePanel.Parent.Controls.Remove(devicePanel);
                        devicePanel.Dispose();
                    }

                    attempts++;
                    await Task.Delay(2000);

                }
                else
                {
                    break;
                }
            }

            if (scrcpyProcess == null || scrcpyProcess.HasExited)
            {
                return;
            }

            int maxAttempts = 5;
            while (scrcpyWindow == IntPtr.Zero && attempts < maxAttempts)
            {
                foreach (Process p in Process.GetProcessesByName("scrcpy"))
                {
                    if (p.Id == scrcpyProcess.Id)
                    {
                        foreach (ProcessThread thread in p.Threads)
                        {
                            EnumThreadWindows(thread.Id, (hWnd, lParam) =>
                            {
                                scrcpyWindow = hWnd;
                                return false;
                            }, IntPtr.Zero);
                        }
                    }
                }

                if (scrcpyWindow == IntPtr.Zero)
                {
                    await Task.Delay(500);
                    attempts++;
                }
            }
            if (scrcpyWindow != IntPtr.Zero)
            {
                ShowWindow(scrcpyWindow, SW_HIDE);
                int exStyle = GetWindowLong(scrcpyWindow, GWL_EXSTYLE);
                exStyle &= ~WS_EX_APPWINDOW;
                exStyle |= WS_EX_TOOLWINDOW;
                SetWindowLong(scrcpyWindow, GWL_EXSTYLE, exStyle);
                if (devicePanel != null && !devicePanel.IsDisposed)
                {
                    SetParent(scrcpyWindow, devicePanel.Handle);
                    ShowWindow(scrcpyWindow, SW_SHOWNORMAL);
                    MoveWindow(scrcpyWindow, 0, 0, panelWidth, panelHeight, true);


                    devicePanel.BringToFront();

                    devicePanel.Resize += (s, e) =>
                    {
                        if (!devicePanel.IsDisposed)
                        {
                            MoveWindow(scrcpyWindow, 0, 0, devicePanel.Width, devicePanel.Height, true);
                        }
                    };

                    deviceDisplays.Add(new DeviceDisplay
                    {
                        Serial = deviceId,
                        ScrcpyProcess = scrcpyProcess,
                        ScrcpyWindow = scrcpyWindow,
                        DevicePanel = devicePanel
                    });

                    ExecuteAdbCommand("shell input swipe 500 1150 500 1000", deviceId);

                }
            }
        }
        public void RestartAllDevices_Click(ScreenView screenView)
        {
            foreach (var device in screenView.deviceDisplays)
            {
                ExecuteAdbCommand("reboot", device.Serial); // Thực thi lệnh reboot cho thiết bị
            }
            MessageBox.Show("Tất cả các thiết bị đã được khởi động lại.");
        }

        public void InstallAPK_Click(ScreenView screenView)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "APK Files|*.apk",
                Title = "Chọn file APK"
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string apkFilePath = fileDialog.FileName;
                foreach (var device in screenView.deviceDisplays)
                {
                    string command = $"install {apkFilePath}";
                    ExecuteAdbCommand(command, device.Serial);
                }
                MessageBox.Show("APK đã được cài đặt cho tất cả các thiết bị.");
            }
        }

        public void CaptureScreenshot_Click(ScreenView screenView)
        {
            foreach (var device in screenView.deviceDisplays)
            {
                string command = $"shell screencap -p /sdcard/screenshot{device.Serial}.png";
                ExecuteAdbCommand(command, device.Serial);
                ExecuteAdbCommand($"pull /sdcard/screenshot{device.Serial}.png", device.Serial);  // Tải ảnh về máy tính
            }
            MessageBox.Show("Chụp màn hình đã được thực hiện cho tất cả các thiết bị.");
        }

        public void ExecuteAdbCommand_Click(ScreenView screenView)
        {
            string adbCommand = PromptForAdbCommand();  // Hàm này có thể yêu cầu người dùng nhập lệnh ADB

            if (!string.IsNullOrEmpty(adbCommand))
            {
                foreach (var device in screenView.deviceDisplays)
                {
                    ExecuteAdbCommand(adbCommand, device.Serial);
                }
                MessageBox.Show("Lệnh ADB đã được thực hiện cho tất cả các thiết bị.");
            }
        }

        public void DeleteAllDevices_Click(ScreenView screenView)
        {
            foreach (var device in screenView.deviceDisplays)
            {
                ExecuteAdbCommand("disconnect", device.Serial);  // Ngắt kết nối
                flowLayoutPanel.Controls.Remove(device.DevicePanel);
                device.DevicePanel.Dispose();
            }
            screenView.deviceDisplays.Clear();  // Xóa tất cả các thiết bị khỏi danh sách
            MessageBox.Show("Đã reload views.");
        }

        public static void ExecuteAdbCommand(string command, string deviceId)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "./Resources/adb.exe",
                Arguments = $"-s {deviceId} {command}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            Process adbProcess = Process.Start(startInfo);
            adbProcess.WaitForExit();
        }
        public static string PromptForAdbCommand()
        {
            using (var inputBox = new Form())
            {
                inputBox.Text = "Nhập lệnh ADB";

                // Tạo TextBox và đặt chiều cao cho TextBox nếu cần
                var textBox = new TextBox { Dock = DockStyle.Fill, Multiline = true, Height = 100 };

                // Tạo Button và đặt kích thước cho nó
                var button = new Button { Text = "OK", Dock = DockStyle.Bottom, Height = 40 };

                // Thêm TextBox và Button vào form
                inputBox.Controls.Add(textBox);
                inputBox.Controls.Add(button);

                // Xử lý sự kiện click cho button OK
                button.Click += (sender, e) =>
                {
                    inputBox.Close();
                };

                // Căn chỉnh form với các điều khiển đã thêm
                inputBox.AutoSize = true;
                inputBox.StartPosition = FormStartPosition.CenterScreen;
                inputBox.ShowDialog();

                return textBox.Text;
            }
        }

    }
}
