using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Model;
using WindowsFormsApp.Model.Static;

namespace WindowsFormsApp
{
    public partial class ScreenView : Form
    {
        public static ScreenView Instance { get; private set; }
        private const int panelWidth = 200;
        private const float aspectRatio = 0.4615f;
        private const int panelHeight = (int)(panelWidth / aspectRatio);
        private int scale = 150;
        private int maxSize = 1280;
        private bool isTurnScreenOff = true;
        public List<DeviceDisplay> deviceDisplays = new List<DeviceDisplay>();
        List<string> activeDevices = new List<string>();
        List<Form> openedForms = new List<Form>();
        HashSet<string> viewedDevices = new HashSet<string>();
        string[] deviceIds = { };

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

        private LanguageManager lang;
        public ScreenView()
        {
            InitializeComponent();
            Instance = this;
            this.Text = "Device Management";
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
            StartDeviceCheck();
            CreateActionButtons();
            
            this.splitContainer.SplitterDistance = (int)(this.Width * 0.75);
        }
        private void CreateActionButtons()
        {
            AddTrackBar(rightPanel, "Độ phân giải (Sắc nét)", 240, 2200, 1280, (s, e) =>
            {
                maxSize = ((TrackBar)s).Value;
            });

            AddTrackBar2(rightPanel, "Tỉ lệ khung hình(%): ", 100, 200, 150, (s, e) =>
            {
                scale = ((TrackBar)s).Value;
            });

            AddActionPanel(rightPanel);
            selectedDevicesLabel.Text = $"Số thiết bị được chọn: {activeDevices.Count}";
           
            deviceIds = ADBService.GetConnectedDevices();
            AddDeviceButtons(rightPanel, deviceIds);
        }
        private void CbTurnOffScreen_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbTurnOffScreen.Checked;
            isTurnScreenOff = isChecked;
        }
        private void UpdateSelectedDevicesLabel()
        {
            selectedDevicesLabel.Text = $"{lang.Get("numberOfSelected")} {activeDevices.Count}";
        }
        public void RestartAllDevices_Click(ScreenView screenView)
        {
            foreach (var device in screenView.deviceDisplays)
            {
               ADBService.ExecuteAdbCommand("reboot", device.Serial); // Thực thi lệnh reboot cho thiết bị
            }
            MessageBox.Show(ScreenViewStatic.logRestartAllDevices);
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
                    ADBService.ExecuteAdbCommand(command, device.Serial);
                }
                MessageBox.Show(ScreenViewStatic.logInstallAPK);
            }
        }
        private void btnPushFileAllDevice_Click(object render, EventArgs args)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Filter = "All Files|*.*",
                    Title = "Chọn file bất kỳ"
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = fileDialog.FileName;
                    foreach (var deviceId in activeDevices)
                    {
                        string command = $"push {filePath} /sdcard/";
                        ADBService.ExecuteAdbCommand(command, deviceId);
                    }
                    MessageBox.Show(ScreenViewStatic.logPushFileAllDevice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ScreenViewStatic.logErrorPushFileAllDevice + ex.Message, ViewChangeStatic.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void btnInstallAPKAllDevice_Click(object render , EventArgs args)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Filter = "APK Files|*.apk",
                    Title = "Chọn file APK"
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string apkFilePath = fileDialog.FileName;
                    foreach (var deviceId in activeDevices)
                    {
                        string command = $"install {apkFilePath}";
                        ADBService.ExecuteAdbCommand(command, deviceId);
                    }
                    MessageBox.Show(ScreenViewStatic.logInstallAPKAllDevice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ScreenViewStatic.logErrorInstallAPKAllDevice + ex.Message, ViewChangeStatic.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        public void CaptureScreenshot_Click(ScreenView screenView)
        {
            foreach (var device in screenView.deviceDisplays)
            {
                string command = $"shell screencap -p /sdcard/screenshot{device.Serial}.png";
                ADBService.ExecuteAdbCommand(command, device.Serial);
                string directoryPath = $".\\{device.Serial}";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                ADBService.ExecuteAdbCommand($"pull /sdcard/screenshot{device.Serial}.png ./{device.Serial}/screenshot.png", device.Serial); 
            }
            MessageBox.Show(ScreenViewStatic.logCaptureScreenshot);
        }
        public void ExecuteAdbCommand_Click(ScreenView screenView)
        {
            string adbCommand = PromptForAdbCommand(); 

            if (!string.IsNullOrEmpty(adbCommand))
            {
                foreach (var device in screenView.deviceDisplays)
                {
                    ADBService.ExecuteAdbCommand(adbCommand, device.Serial);
                }
                MessageBox.Show(ScreenViewStatic.logExecuteAdbCommand);
            }
        }
        public void DeleteAllDevices_Click(ScreenView screenView)
        {
            foreach (var device in screenView.deviceDisplays)
            {
                ADBService.ExecuteAdbCommand("disconnect", device.Serial);  // Ngắt kết nối
                flowLayoutPanel.Controls.Remove(device.DevicePanel);
                device.DevicePanel.Dispose();
            }
            screenView.deviceDisplays.Clear();  // Xóa tất cả các thiết bị khỏi danh sách
         //   MessageBox.Show("Đã reload views.");
        }

        private void ScreenView_Load(object sender, EventArgs e)
        {
            LoadLanguageScreenView();
        }
        public void LoadLanguageScreenView()
        {
            lang = new LanguageManager(FormVisibilityManager.IsLanguage);

            this.Text = lang.Get("screenView");
            label.Text = lang.Get("resolution");
            label2.Text = lang.Get("ratio");
            selectedDevicesLabel.Text = $"{lang.Get("numberOfSelected")} {activeDevices.Count}";
            cbTurnOffScreen.Text = lang.Get("turnOffScreen");

            ScreenViewStatic.logRestartAllDevices = lang.Get("logRestartAllDevices");
            ScreenViewStatic.logInstallAPK = lang.Get("logInstallAPK");
            ScreenViewStatic.logPushFileAllDevice = lang.Get("logPushFileAllDevice");
            ScreenViewStatic.logErrorPushFileAllDevice = lang.Get("logErrorPushFileAllDevice");
            ScreenViewStatic.logInstallAPKAllDevice = lang.Get("logInstallAPKAllDevice");
            ScreenViewStatic.logErrorInstallAPKAllDevice = lang.Get("logErrorInstallAPKAllDevice");
            ScreenViewStatic.logCaptureScreenshot = lang.Get("logCaptureScreenshot");
            ScreenViewStatic.logExecuteAdbCommand = lang.Get("logExecuteAdbCommand");
        }
    }
}
