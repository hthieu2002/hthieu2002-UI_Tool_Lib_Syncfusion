﻿using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class ScreenView : Form
    {
        private class DeviceDisplay
        {
            public string Serial { get; set; }
            public Process ScrcpyProcess { get; set; }
            public IntPtr ScrcpyWindow { get; set; }
            public Panel DevicePanel { get; set; }
        }

        private FlowLayoutPanel flowLayoutPanel;
        private const int panelWidth = 200;
        private const float aspectRatio = 0.4615f;
        private const int panelHeight = (int)(panelWidth / aspectRatio);
        private List<DeviceDisplay> deviceDisplays = new List<DeviceDisplay>();

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
        }

        private void load()
        {
            init();
            StartDeviceCheck(); 
        }

        private void init()
        {
            this.Text = "Device Management";

            flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.WrapContents = true;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            this.Controls.Add(flowLayoutPanel);
        }

        private async void StartDeviceCheck()
        {
            while (true) 
            {
                var devices = GetConnectedDevices();

                var newDevices = devices.Except(deviceDisplays.Select(d => d.Serial)).ToList();
                foreach (var device in newDevices)
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

                var connectedDevices = devices.ToHashSet();
                var disconnectedDevices = deviceDisplays.Where(d => !connectedDevices.Contains(d.Serial)).ToList();
                foreach (var disconnectedDevice in disconnectedDevices)
                {
                    if (this.IsHandleCreated)
                    {
                        Invoke((MethodInvoker)(() => RemoveDeviceView(disconnectedDevice)));
                    }
                }

                await Task.Delay(2000); 
            }
        }


        private void AddDeviceView(string deviceId)
        {
            var existingDevicePanel = flowLayoutPanel.Controls.Cast<Control>()
                .FirstOrDefault(c => c is Panel panel && panel.Controls.OfType<Label>().FirstOrDefault()?.Text == deviceId);

            if (existingDevicePanel == null)
            {
                Panel devicePanel = new Panel();
                devicePanel.Size = new System.Drawing.Size(panelWidth, panelHeight + 20);
                devicePanel.Margin = new Padding(10);
                devicePanel.BackColor = System.Drawing.Color.LightGray;

                devicePanel.MouseClick += (s, e) =>
                {
                    MessageBox.Show($"Đã click vào thiết bị: {deviceId}");
                };

                flowLayoutPanel.Controls.Add(devicePanel);
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
            deviceLabel.Text = $"Device: {deviceId}";  
            deviceLabel.TextAlign = ContentAlignment.MiddleCenter;
            deviceLabel.BackColor = Color.Transparent;  
            deviceLabel.Dock = DockStyle.Bottom;
            devicePanel.Controls.Add(deviceLabel);
            deviceLabel.BringToFront();

            deviceLabel.Cursor = Cursors.Hand;

            deviceLabel.Click += async (sender, e) =>
            {
                Form deviceForm = new Form();
                deviceForm.Text = $"Device {deviceId}";

                int width = (int)(devicePanel.Width * 1.5) + 250;
                int height = (int)(devicePanel.Height * 1.5) + 100;

                width = Math.Min(width, Screen.PrimaryScreen.WorkingArea.Width);
                height = Math.Min(height, Screen.PrimaryScreen.WorkingArea.Height);

                deviceForm.Size = new Size(width, height); 
                deviceForm.StartPosition = FormStartPosition.CenterScreen;

                deviceForm.TopMost = true;

                Panel scrcpyPanel = new Panel();
                scrcpyPanel.Dock = DockStyle.Fill;  
                scrcpyPanel.BackColor = Color.Black;  

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
                void BtnReboot_Click(object senderReboot, EventArgs e3) => ExecuteAdbCommand("reboot", deviceId); // Reboot
                void BtnPowerOff_Click(object senderPowerOff, EventArgs e4) => ExecuteAdbCommand("poweroff", deviceId); // Power Off
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
                    Arguments = $"-s {deviceId} --max-size {Math.Min(1080, 2220)} --max-fps 15 " +
                                $"--window-borderless --window-x 0 --window-y 0 --fullscreen",  
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
                    }
                }

                var connectionCheckTimer = new System.Threading.Timer(async _ =>
                {
                    bool isDeviceConnected = await IsDeviceConnected(deviceId);
                    if (!isDeviceConnected)
                    {
                        deviceForm.Invoke((Action)(() =>
                        {
                            deviceForm.Close();  
                        }));
                    }
                }, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

                deviceForm.FormClosing += (senderForm, closingArgs) =>
                {
                    scrcpyProcessZoom?.Kill();  
                    connectionCheckTimer.Dispose();  
                };

                deviceForm.Show();
            };

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
                }
            }
        }
    }
}
