using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.ListView;
using Syncfusion.WinForms.ListView.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.IO;
using Syncfusion.WinForms.DataGrid.Events;
using Services;
using Newtonsoft.Json;
using System.Threading;
using WindowsFormsApp.Model;

namespace WindowsFormsApp
{
    public partial class ViewAutomation : Form
    {
        List<string> dataFileScript;
        private Task _deviceCheckTask;
        public ViewAutomation()
        {
            InitializeComponent();
            setControl();
            setControlRight();
            setGridView();
            LoadDevicesFromFile();
            this.Load += async (s, e) => await LoadAsync();
        }
        public async Task LoadAsync()
        {
            StartDeviceCheck();
            await InitializeDeviceStatus();
        }
        private void Script_Click(object sender, EventArgs e)
        {
            ScriptAutomation script = new ScriptAutomation();
            script.Show();
        }
        public async Task InitializeDeviceStatus()
        {
            var (onlineDevices, offlineDevices) = LoadDevicesFromJson();
            var connectedDevices = ADBService.GetConnectedDevices().ToHashSet();
            foreach (var device in onlineDevices)
            {
                if (!connectedDevices.Contains(device.Serial))
                {
                    UpdateDeviceStatus(device.Serial, "Offline");
                }
                else
                {
                    string currentStatus = ADBService.IsDeviceOnline(device.Serial) ? "Online" : "Offline";
                    UpdateDeviceStatus(device.Serial, currentStatus);
                }
            }
            foreach (var device in offlineDevices)
            {
                if (connectedDevices.Contains(device.Serial))
                {
                    UpdateDeviceStatus(device.Serial, "Online");
                }
            }
            await Task.Delay(1000);
        }
        public (List<WindowsFormsApp.Model.DeviceDisplay> onlineDevices, List<WindowsFormsApp.Model.DeviceDisplay> offlineDevices) LoadDevicesFromJson()
        {
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "devices.json");

            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);

                var devices = JsonConvert.DeserializeObject<List<WindowsFormsApp.Model.DeviceDisplay>>(jsonContent);

                var onlineDevices = devices?.Where(d => d.Status == "Online").ToList() ?? new List<WindowsFormsApp.Model.DeviceDisplay>();
                var offlineDevices = devices?.Where(d => d.Status == "Offline").ToList() ?? new List<WindowsFormsApp.Model.DeviceDisplay>();

                return (onlineDevices, offlineDevices);
            }
            else
            {
                return (new List<WindowsFormsApp.Model.DeviceDisplay>(), new List<WindowsFormsApp.Model.DeviceDisplay>());
            }
        }
        public void StartDeviceCheck()
        {
            DeviceCheckManager.StartDeviceCheck(DeviceCheckLoop);
        }
        private async Task DeviceCheckLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var connectedDevices = ADBService.GetConnectedDevices().ToHashSet();
                var (onlineDevices, offlineDevices) = LoadDevicesFromJson();
                foreach (var device in onlineDevices)
                {
                    if (!connectedDevices.Contains(device.Serial))
                    {
                        Invoke((MethodInvoker)(() =>
                        {
                            UpdateDeviceStatus(device.Serial, "Offline");
                            sfDataGrid.Refresh();
                        }));
                    }
                }

                foreach (var device in connectedDevices)
                {
                    var existingDevice = deviceDisplays.FirstOrDefault(d => d.Serial == device);

                    if (existingDevice != null)
                    {
                        string currentStatus = ADBService.IsDeviceOnline(device) ? "Online" : "Offline";
                        if (existingDevice.Status != currentStatus)
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                UpdateDeviceStatus(device, currentStatus);
                                sfDataGrid.Refresh();
                            }));
                        }
                    }
                    else
                    {
                        Invoke((MethodInvoker)(() =>
                        {
                            AddDeviceView(device, "", 1);
                            UpdateDeviceStatus(device, "Online");
                            sfDataGrid.Refresh();
                        }));
                    }
                }
                await Task.Delay(1000);
            }
        }
        private void sfDataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.Column.MappingName == "Activity")
            {
                if (e.DisplayText == "NO")
                {
                    e.Style.TextColor = Color.Red;
                }
                else if (e.DisplayText == "YES")
                {
                    e.Style.TextColor = Color.DarkSlateBlue;
                }
            }
            if (e.Column.MappingName == "Status")
            {
                if (e.DisplayText == "Online")
                {
                    e.Style.TextColor = Color.Blue;
                }
                else
                {
                    e.Style.TextColor = Color.Red;
                }
            }
        }
        private void LoadFileScript_Click(object sender, EventArgs e)
        {
            dataFileScript = new List<string>();
            dataFileScript.Clear();

            string scriptFolderPath = Path.Combine(Application.StartupPath, "Resources", "script");
            if (!Directory.Exists(scriptFolderPath))
            {
                Directory.CreateDirectory(scriptFolderPath);
            }
            string[] txtFiles = Directory.GetFiles(scriptFolderPath, "*.txt");

            if (txtFiles.Length == 0)
            {
                MessageBox.Show("Không có file .txt nào trong thư mục 'script'.", "Thông báo");
                return;
            }

            foreach (string filePath in txtFiles)
            {
                string fileName = Path.GetFileName(filePath);
                dataFileScript.Add(fileName);
                // cbLoadFile.CheckedItems.Add(fileName);
            }
            cbLoadFile.DataSource = dataFileScript;

            cbLoadFile.SelectedIndex = 0;

        }
        private void RunScript_Click(object sender, EventArgs e)
        {

        }

    }
}
