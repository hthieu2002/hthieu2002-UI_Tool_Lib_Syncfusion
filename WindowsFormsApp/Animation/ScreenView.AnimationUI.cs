using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class ScreenView : Form
    {
        private void AddActionPanel(Panel parent)
        {
            btnView.Click += (s, e) =>
            {
                int startX = 100;
                int startY = 100;
                int offsetX = 50;
                int index = 0;

                foreach (string deviceId in activeDevices)
                {
                    if (viewedDevices.Contains(deviceId))
                        continue;

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
                    viewedDevices.Add(deviceId);
                    deviceForm.StartPosition = FormStartPosition.Manual;
                    deviceForm.Location = new Point(startX + (width + offsetX) * index, startY);
                    index++;
                    Thread.Sleep(1000);
                    deviceForm.FormClosed += (object sender, FormClosedEventArgs _) =>
                    {
                        viewedDevices.Remove(deviceId);
                    };
                }
                AddDeviceButtons(rightPanel, deviceIds);
                activeDevices.Clear();
                UpdateSelectedDevicesLabel();
            };
            StyleButton(btnView);


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


            btnRefreshDevice.Click += (s, e) =>
            {
                deviceIds = GetConnectedDevices();
                AddDeviceButtons(rightPanel, deviceIds);
                activeDevices.Clear();
                UpdateSelectedDevicesLabel();
            };
            StyleButton(btnRefreshDevice);


            btnPushFile.Click += (s, e) =>
            {
                MessageBox.Show("Push File clicked");
            };
            StyleButton(btnPushFile);


            btnInstallAPK.Click += (s, e) =>
            {
                MessageBox.Show("Install APK clicked");
            };
            StyleButton(btnInstallAPK);

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
            label.Text = labelText;
            label.Width = parent.ClientSize.Width;
          //  parent.Controls.Add(label);
          //  parent.Controls.Add(trackBarPanel);
            trackBar.Minimum = min;
            trackBar.Maximum = max;
            trackBar.Value = value;
            trackBar.Scroll += onChange;
            valueLabel.Text = value.ToString();
            //trackBarPanel.Controls.Add(valueLabel);
            trackBar.Scroll += (s, e) => valueLabel.Text = trackBar.Value.ToString();

        }
        private void AddTrackBar2(Panel parent, string labelText, int min, int max, int value, EventHandler onChange)
        {
            label2.Text = labelText;
            label2.Width = parent.ClientSize.Width;
            //  parent.Controls.Add(label);
            //  parent.Controls.Add(trackBarPanel);
            trackBar2.Minimum = min;
            trackBar2.Maximum = max;
            trackBar2.Value = value;
            trackBar2.Scroll += onChange;
            valueLabel2.Text = value.ToString();
            //trackBarPanel.Controls.Add(valueLabel);
            trackBar2.Scroll += (s, e) => valueLabel2.Text = trackBar2.Value.ToString();

        }
        private void AddDeviceButtons(FlowLayoutPanel parent, string[] deviceIds)
        {
            buttonPanel = parent.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();

            if (buttonPanel == null)
            {
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
                buttonPanel.Controls.Clear();
            }

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
                flowLayoutPanel.Controls.Add(devicePanel);

                devicePanel.MouseClick += (s, e) =>
                {
                    flowLayoutPanel.Controls.Remove(devicePanel);
                    devicePanel.Dispose();
                };
                StartScrcpyForDeviceAsync(deviceId, devicePanel);
            }
        }
        private async void StartScrcpyForDeviceAsync(string deviceId, Panel devicePanel)
        {
            string scrcpyExePath = "./Resources/scrcpy.exe";
            Process scrcpyProcess = null;
            IntPtr scrcpyWindow = IntPtr.Zero;
            int attempts = 0;

            deviceLabel.Text = $"View Device: {deviceId}";

            devicePanel.Controls.Add(deviceLabel);
            deviceLabel.BringToFront();
            deviceLabel.Cursor = Cursors.Hand;

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
                    Arguments = $"-s {deviceId} --max-size {maxSize} --max-fps 15 " + $"{(isTurnScreenOff ? "--turn-screen-off " : "")}" +
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
        private void RemoveDeviceView(DeviceDisplay deviceDisplay)
        {
            flowLayoutPanel.Controls.Remove(deviceDisplay.DevicePanel);
            deviceDisplay.DevicePanel.Dispose();
            deviceDisplays.Remove(deviceDisplay);
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
