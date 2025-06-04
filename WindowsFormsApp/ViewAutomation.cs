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
using System.Xml.Linq;
using WindowsFormsApp.Script.RoslynScript;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp
{
    public partial class ViewAutomation : Form
    {
        List<string> dataFileScript;
        private Task _deviceCheckTask;
        private CancellationTokenSource _cts;

        private static CancellationTokenSource _deviceCheckCancellationTokenSource;
        private readonly HashSet<string> _animatingDevices = new HashSet<string>();
        public static ViewAutomation Instance { get; private set; }
        public ViewAutomation()
        {
            InitializeComponent();
            Instance = this;
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
        public async Task UpdateNameGridView(string id, string name)
        {
            var dt = sfDataGrid.DataSource as System.Data.DataTable;
            if (dt == null) return;

            // Tìm kiếm chỉ mục dòng dựa trên DeviceID
            int rowIndex = dt.AsEnumerable().ToList().FindIndex(r => r.Field<string>("DeviceID") == id);

            if (rowIndex >= 0 && rowIndex < dt.Rows.Count)
            {
                var row = dt.Rows[rowIndex];
                row["NameID"] = name;

                // Lưu thay đổi vào dictionary
                FormVisibilityManager._updatedNames[id] = name;

                // Nếu form đang được mở, làm mới giao diện
                if (this.Visible)
                {
                    sfDataGrid.Refresh();
                }
               
            }
        }
     
        public async Task UpdateProgressGridView(string id, string text, int p = 1)
        {
            var dt = sfDataGrid.DataSource as System.Data.DataTable;
            if (dt == null) return;

            int rowIndex = dt.AsEnumerable().ToList().FindIndex(r => r.Field<string>("DeviceID") == id);

            Console.WriteLine(text);
            if (rowIndex >= 0 && rowIndex < dt.Rows.Count)
            {
                var row = dt.Rows[rowIndex];
                if (sfDataGrid.InvokeRequired)
                {
                    sfDataGrid.Invoke(new Action(() =>
                    {
                        row.BeginEdit();
                        row["Progress"] = p;
                        row["ProgressText"] = text;
                        row.EndEdit();
                        dt.AcceptChanges();
                        sfDataGrid.View.Refresh();
                    }));
                }
                else
                {
                    row.BeginEdit();
                    row["Progress"] = p;
                    row["ProgressText"] = text;
                    row.EndEdit();
                    dt.AcceptChanges();
                    sfDataGrid.View.Refresh();
                }

            }
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
        public async Task StartDeviceCheck()
        {
            _deviceCheckCancellationTokenSource?.Cancel();
            _deviceCheckCancellationTokenSource = new CancellationTokenSource();
            await DeviceCheckLoop(_deviceCheckCancellationTokenSource.Token);
        }
        private async Task DeviceCheckLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!FormVisibilityManager.IsFormViewChangeVisible)
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
                                string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "devices.json");
                                if (!File.Exists(path))
                                {
                                    SaveDevicesToFile();
                                }
                                string json = File.ReadAllText(path);
                                List<Model.DeviceDisplay> devices = JsonConvert.DeserializeObject<List<Model.DeviceDisplay>>(json);
                                var deviceName = devices.FirstOrDefault(d => d.Serial == device);
                                //AddDeviceView(device, "", 1);
                                UpdateDeviceStatus(device, "Online");
                                sfDataGrid.Refresh();
                            }));
                        }
                    }
                    await Task.Delay(1000);
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
        private void btnAutoRun_Click(object sender, EventArgs e)
        {
            _deviceTable = sfDataGrid.DataSource as DataTable;

            if (_deviceTable == null)
            {
                Console.WriteLine("DataSource chưa được gán hoặc không đúng kiểu DataTable.");
                return;
            }

            // Lưu trạng thái Checkbox và Activity của các thiết bị online
            var onlineDeviceStates = new Dictionary<string, (bool IsChecked, string Activity)>();
            foreach (System.Data.DataRow row in _deviceTable.Rows)
            {
                bool isChecked = row["Checkbox"] is bool val && val;
                string status = row["Status"]?.ToString()?.ToLower();
                string serial = row["DeviceID"]?.ToString();
                string activity = row["Activity"]?.ToString() ?? "";

                if (!string.IsNullOrEmpty(serial) && status == "online")
                {
                    onlineDeviceStates[serial.ToLower()] = (isChecked, activity);
                }
            }

            _deviceTable.Rows.Clear();

            string path = Path.Combine(Application.StartupPath, "devices.json");
            if (!File.Exists(path))
            {
                SaveDevicesToFile();
            }

            string json = File.ReadAllText(path);
            deviceDisplays = JsonConvert.DeserializeObject<List<WindowsFormsApp.Model.DeviceDisplay>>(json)
                              ?? new List<WindowsFormsApp.Model.DeviceDisplay>();

            foreach (var device in deviceDisplays)
            {
                int stt = _deviceTable.Rows.Count + 1;

                string serialLower = device.Serial.ToLower();
                bool isChecked = false;
                string activity = "";

                if (onlineDeviceStates.TryGetValue(serialLower, out var state))
                {
                    isChecked = state.IsChecked;
                    activity = state.Activity;
                }

                _deviceTable.Rows.Add(stt, isChecked, device.Name, device.Serial, 0, "", device.Status, activity);
            }

            sfDataGrid.Refresh();
        }


        private async void RunScript_Click(object sender, EventArgs e)
        {
            if (cbLoadFile.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng load file script.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var dt = sfDataGrid.DataSource as System.Data.DataTable;
            if (dt == null) return;

            var selectedRows = dt.Rows
                                   .Cast<System.Data.DataRow>()
                                   .Where(r => r.Field<bool>("Checkbox"))
                                   .ToList();

            var toAnimate = selectedRows
                  .Select(r => r.Field<string>("DeviceID"))
                  .Where(id => !_animatingDevices.Contains(id))
                  .ToList();

            if (!toAnimate.Any())
            {
                MessageBox.Show("Không có thiết bị mới nào để chạy.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var deviceIds = selectedRows
                .Select(r => r.Field<string>("DeviceID")) 
                .ToList();

            var tasks = new List<Task>();

            btnRun.Visible = false;
            btnStopRun.Visible = true;

            if (cbAuto.Checked)
            {
               _cts = new CancellationTokenSource();
                int i = 0;
                while (!_cts.Token.IsCancellationRequested)
                {
                    i++;
                    foreach (var deviceId in deviceIds)
                    {
                        var task = Task.Run(async () =>
                        {
                            await UpdateProgressGridView(deviceId, $"Start script {cbLoadFile.Text} vô hạn lần {i}", 1);
                            RoslynScriptAutomation.Run($"./Resources/script/{cbLoadFile.Text}", deviceId, this);

                            await UpdateProgressGridView(deviceId, $"Success", 100);
                        });
                        tasks.Add(task);

                    }
                    await Task.WhenAll(tasks);

                    await Task.Delay(1000);
                }
            }
            else
            {
                int numberOfRuns = (int)nudNumber.Value;
                for (int i = 0; i < numberOfRuns; i++)
                {
                    foreach (var deviceId in deviceIds)
                    {
                        var task = Task.Run(async () =>
                        {
                            await UpdateProgressGridView(deviceId, $"Start script {cbLoadFile.Text} lần {i+1}", 1);
                            RoslynScriptAutomation.Run($"./Resources/script/{cbLoadFile.Text}", deviceId, this);

                            await UpdateProgressGridView(deviceId, $"Success", 100);

                        });
                        tasks.Add(task);

                    }

                    await Task.WhenAll(tasks);
                }
                btnRun.Invoke((MethodInvoker)(() =>
                {
                    btnRun.Visible = true;
                    btnStopRun.Visible = false;
                }));

                await Task.Delay(1000);
            }
        }
        private async void StopRunScript_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                btnRun.Visible = true;
                btnStopRun.Visible = false;
            }
        }

        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAuto.Checked)
            {
                nudNumber.Enabled = false;
                nudNumber.Value = 0;
            }
            else
            {
                nudNumber.Enabled = true;
            }
        }
        private void ViewAutoamtion_VisibleChanged(object sender, EventArgs e)
        {
            FormVisibilityManager.IsFormViewAutomationVisible = this.Visible;

            if (FormVisibilityManager.IsFormViewAutomationVisible)
            {
                if (FormVisibilityManager._updatedNames.Count > 0)
                {
                    foreach (var item in FormVisibilityManager._updatedNames)
                    {
                        var dt = sfDataGrid.DataSource as DataTable;
                        if (dt != null)
                        {
                            var row = dt.Rows.Cast<System.Data.DataRow>().FirstOrDefault(r => r["DeviceID"].ToString() == item.Key);
                            if (row != null)
                            {
                                row["NameID"] = item.Value;
                            }
                        }
                    }
                    FormVisibilityManager._updatedNames.Clear();
                    sfDataGrid.Refresh();
                }
            }
        }
    }
}

