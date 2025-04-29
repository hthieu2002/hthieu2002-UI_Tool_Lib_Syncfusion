using AccountCreatorForm.Controls;
using Newtonsoft.Json;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Model;



namespace WindowsFormsApp
{
    public partial class ViewChange : Form
    {
        private HeaderViewCommon headerView;
        private SfDataGrid sfDataGrid;
        private List<WindowsFormsApp.Model.DeviceDisplay> deviceDisplays = new List<WindowsFormsApp.Model.DeviceDisplay>();  // Danh sách các thiết bị hiện tại
        private CancellationTokenSource _deviceCheckCancellationTokenSource;
        private Task _deviceCheckTask;
        public static ViewChange Instance { get; private set; }
        private readonly Dictionary<string, string> _progressTextMap = new Dictionary<string, string>();
        private readonly HashSet<string> _animatingDevices = new HashSet<string>();

        private List<DeviceConfig> _configs;
        private HashSet<int> _usedIndices = new HashSet<int>();
        private int _randomCount = 0;
        private Random _rnd = new Random();

        private ComboBox txtBrand;
        private ComboBox txtOS;
        private ComboBox txtOS_version;
        private TextBoxExt txtSerial;
        private TextBoxExt txtImei;
        private TextBoxExt txtMac;
        private ComboBox txtName;
        private ComboBox txtCountry;
        private TextBoxExt txtCode;
        private TextBoxExt txtImsi;
        private ComboBox txtModel;
        private ComboBox txtSim;
        private TextBoxExt txtPhone;
        private TextBoxExt txtIccId;

        // button
        private SfButton btnRandomdevice;
        private SfButton btnAutoBackup;
        private SfButton btnAutochangeFull;
        private SfButton btnAutoChangeSim;
        private SfButton btnBackup;
        private SfButton btnBackup2;
        private SfButton btnChangeDevice;
        private SfButton btnChangeSim;
        private SfButton btnOpenUrl;
        private SfButton btnRandomSim;
        private SfButton btnScreenshot;
        //
        private SfButton btnRestore;
        private SfButton btnFakeLocation;
        private TextBoxExt txtRestore;
        private DataTable _deviceTable;
        private object copyContent = null;
        //
        FlowLayoutPanel panel;
        AutoLabel label;
        FlowLayoutPanel osPanel;
        public ViewChange()
        {
            InitializeComponent();
            SetButtonAddLayoutButton();
            SetInputAddLayoutInput();
            setInit();
            setMenu();
            setGridView();
            Instance = this;
            LoadDevicesFromFile();
            _ = InitializeDeviceStatus();
            StartDeviceCheck();
            this.Resize += MyForm_OnSizeChange;
            this.SizeChanged += MyForm_OnSizeChange;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;

            //
            LoadConfigs();

        }

        private void LoadConfigs()
        {
            var path = Path.Combine(System.Windows.Forms.Application.StartupPath, "./Resources/data.json");
            var json = File.ReadAllText(path);
            _configs = JsonConvert.DeserializeObject<List<DeviceConfig>>(json);

            InitComboBoxes();
        }

        private void InitComboBoxes()
        {
            var brands = _configs.Select(c => c.brand).Distinct().ToList();
            var oss = _configs.Select(c => c.os).Distinct().ToList();
            var osVers = _configs.Select(c => c.os_version).Distinct().ToList();
            var names = _configs.Select(c => c.name).Distinct().ToList();
            var countries = _configs.Select(c => c.country).Distinct().ToList();
            var models = _configs.Select(c => c.model).Distinct().ToList();
            var sims = _configs.Select(c => c.sim).Distinct().ToList();

            txtBrand.DataSource = brands;
            txtOS.DataSource = oss;
            txtOS_version.DataSource = osVers;
            txtName.DataSource = names;
            txtCountry.DataSource = countries;
            txtModel.DataSource = models;
            txtSim.DataSource = sims;

        }
        private void MyForm_OnSizeChange(object sender, EventArgs e)
        {
            ApplyPanelInputMargin();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyPanelInputMargin();
        }
        public void setMenu()
        {
            headerView = new HeaderViewCommon
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 20)
            };
            headerView.SetTitle("Devices");
            mainMenu.Controls.Add(headerView);
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

        private bool IsDeviceOnline(string deviceId)
        {
            var process = new Process();
            process.StartInfo.FileName = "adb";
            process.StartInfo.Arguments = $"-s {deviceId} shell getprop sys.boot_completed";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return !string.IsNullOrEmpty(output) && output.Contains("1");
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
        private bool IsDeviceActive(string deviceId)
        {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = "adb";
                process.StartInfo.Arguments = $"-s {deviceId} shell getprop sys.boot_completed";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return !string.IsNullOrEmpty(output) && output.Contains("1");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking device status: {ex.Message}");
                return false;
            }
        }
        public void StartDeviceCheck()
        {
            _deviceCheckCancellationTokenSource?.Cancel();
            _deviceCheckCancellationTokenSource = new CancellationTokenSource();

            _deviceCheckTask = Task.Run(() => DeviceCheckLoop(_deviceCheckCancellationTokenSource.Token));
        }

        private async Task DeviceCheckLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var connectedDevices = GetConnectedDevices().ToHashSet();
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
                        string currentStatus = IsDeviceOnline(device) ? "Online" : "Offline";
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
                            AddDeviceView(device);
                            UpdateDeviceStatus(device, "Online");
                            sfDataGrid.Refresh();
                        }));
                    }
                }
                await Task.Delay(1000);
            }
        }
        private void AddDeviceView(string deviceId)
        {
            DataTable dataTable = sfDataGrid.DataSource as DataTable;
            int stt = dataTable.Rows.Count + 1;

            dataTable.Rows.Add(stt, false, deviceId, 0, "Offline");

            deviceDisplays.Add(new WindowsFormsApp.Model.DeviceDisplay { Serial = deviceId, Status = "Offline", Activity = "YES", Checkbox = false });
            SaveDevicesToFile();
            sfDataGrid.Refresh();
        }
        private void RemoveDeviceView(WindowsFormsApp.Model.DeviceDisplay device)
        {
            DataTable dataTable = sfDataGrid.DataSource as DataTable;
            var rows = dataTable.Select($"DeviceID = '{device.Serial}'");
            if (rows.Length > 0)
            {
                dataTable.Rows.Remove(rows[0]);
                deviceDisplays.Remove(device);
                DeleteDeviceById(device.Serial);
            }
            sfDataGrid.Refresh();
        }

        private void DeleteDeviceById(string deviceId)
        {
            var deviceToRemove = deviceDisplays.FirstOrDefault(d => d.Serial == deviceId);

            if (deviceToRemove != null)
            {
                deviceDisplays.Remove(deviceToRemove);
                SaveDevicesToFile();
                sfDataGrid.Refresh();
            }
        }
        private void UpdateDeviceStatus(string deviceId, string status)
        {
            var device = deviceDisplays.FirstOrDefault(d => d.Serial == deviceId);
            bool isActive = IsDeviceActive(deviceId);
            if (device != null)
            {
                device.Status = status;
                DataTable dataTable = sfDataGrid.DataSource as DataTable;
                var row = dataTable.Select($"DeviceID = '{deviceId}'").FirstOrDefault();
                if (row != null)
                {
                    row["Status"] = status;
                    if (status == "Online")
                    {
                        row["Progress"] = 0;
                        row["Checkbox"] = true;
                        row["Activity"] = isActive ? "NO" : "YES";
                        device.Activity = "YES";
                    }
                    else
                    {
                        row["Progress"] = 0;
                        row["Checkbox"] = false;
                        row["Activity"] = "---";
                        device.Activity = "YES";
                    }
                }
                SaveDevicesToFile();
                sfDataGrid.Refresh();
            }
        }
        private void SaveDevicesToFile()
        {
            var uniqueDevices = deviceDisplays
                .GroupBy(d => d.Serial)
                .Select(g => g.First())
                .ToList();

            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "devices.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(uniqueDevices, Newtonsoft.Json.Formatting.Indented));
        }

        private void LoadDevicesFromFile()
        {
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "devices.json");
            if (!File.Exists(path))
            {
                SaveDevicesToFile();
            }
            string json = File.ReadAllText(path);
            deviceDisplays = JsonConvert.DeserializeObject<List<WindowsFormsApp.Model.DeviceDisplay>>(json) ?? new List<WindowsFormsApp.Model.DeviceDisplay>();

            foreach (var device in deviceDisplays.ToList())
            {
                AddDeviceView(device.Serial);
                UpdateDeviceStatus(device.Serial, device.Status);
            }
        }
        public void CountSelectedDevices()
        {
            int selectedCount = 0;
            foreach (System.Data.DataRow row in (sfDataGrid.DataSource as DataTable).Rows)
            {
                bool isChecked = Convert.ToBoolean(row["Checkbox"]);
                if (isChecked)
                {
                    selectedCount++;
                }
            }
            GlobalContextMenu.UpdateContextMenu(selectedCount);
        }
        private void CopyDeviceID_Click(object sender, EventArgs e)
        {
            var colIndex = sfDataGrid.Columns.IndexOf(sfDataGrid.Columns["DeviceID"]);
            var valueCurrentRow = ((DataRowView)sfDataGrid.CurrentItem).Row.ItemArray;
            var deviceID = valueCurrentRow[colIndex];
            Clipboard.SetText(deviceID.ToString());
        }
        private void DetailsItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Details option clicked.");
        }

        private void EditItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Edit option clicked.");
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            var selectedRow = sfDataGrid.SelectedItems.Cast<DataRowView>().FirstOrDefault();
            if (selectedRow != null)
            {
                string deviceId = selectedRow["DeviceID"].ToString();

                var deviceToRemove = deviceDisplays.FirstOrDefault(d => d.Serial == deviceId);

                if (deviceToRemove != null)
                {
                    RemoveDeviceView(deviceToRemove);
                }
            }
            else
            {
                MessageBox.Show("Please select a device to delete.");
            }
        }

        //data
        public void SetResertDataInputForm()
        {
            txtCode.Text = "";
            txtIccId.Text = "";
            txtImei.Text = "";
            txtImsi.Text = "";
            txtMac.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtSerial.Text = "";
        }
        private void btnRandom_Click(object sender, EventArgs e)
        {
            int idx;
            if (_randomCount < 3)
            {
                var available = Enumerable.Range(0, _configs.Count)
                                          .Where(i => !_usedIndices.Contains(i))
                                          .ToArray();
                if (available.Length == 0)
                {
                    _usedIndices.Clear();
                    available = Enumerable.Range(0, _configs.Count).ToArray();
                }
                idx = available[_rnd.Next(available.Length)];
                _usedIndices.Add(idx);
            }
            else
            {
                idx = _rnd.Next(_configs.Count);
            }
            _randomCount++;

            FillControls(_configs[idx]);
        }
        private void FillControls(DeviceConfig c)
        {
            txtBrand.SelectedItem = c.brand;
            txtOS.SelectedItem = c.os;
            txtOS_version.SelectedItem = c.os_version;
            txtName.SelectedItem = c.name;
            txtCountry.SelectedItem = c.country;
            txtModel.SelectedItem = c.model;
            txtSim.SelectedItem = c.sim;
            txtSerial.Text = c.serial;
            txtCode.Text = c.code;
            txtPhone.Text = c.phone;
            txtImei.Text = c.imei;
            txtImsi.Text = c.imsi;
            txtIccId.Text = c.iccid;
            txtMac.Text = c.mac;
        }
        // ví dụ gọi:
        private async void btnChange_Click(object sender, EventArgs e)
        {
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

            var details = selectedRows
                .Select(r => r.Field<string>("DeviceID"))
                .Where(id => toAnimate.Contains(id))
                .Select(id => $"ID: {id}, Active: { /* lấy từ DataTable nếu cần */ ""}")
                .ToList();

            string message = $"Sẽ chạy progress cho {toAnimate.Count} thiết bị:";
            message += "\n" + string.Join("\n", details);
            MessageBox.Show(message);

            var tasks = toAnimate
                .Select(id => AnimateProgressAsync(id, delayMs: 30))
                .ToArray();

            foreach (var id in toAnimate)
                ViewChange.Instance.StartBackgroundProgress(id, 30);
        }
        private async Task AnimateProgressAsync(string deviceId, int delayMs = 50)
        {
            if (_animatingDevices.Contains(deviceId))
                return;

            _animatingDevices.Add(deviceId);

            for (int p = 1; p <= 100; p++)
            {
                ViewChange.Instance.UpdateProgress(deviceId, p, $"{p}%");
                await Task.Delay(delayMs);
            }

            _animatingDevices.Remove(deviceId);
        }
        public void StartBackgroundProgress(string deviceId, int intervalMs = 50)
        {
            if (!_animatingDevices.Add(deviceId))
                return;

            Task.Run(async () =>
            {
                try
                {
                    for (int p = 1; p <= 100; p++)
                    {
                        if (sfDataGrid.InvokeRequired)
                        {
                            sfDataGrid.Invoke(new Action(() =>
                                UpdateProgress(deviceId, p, $"{p}%")));
                        }
                        else
                        {
                            UpdateProgress(deviceId, p, $"{p}%");
                        }
                        await Task.Delay(intervalMs);
                    }
                }
                finally
                {
                    _animatingDevices.Remove(deviceId);
                }
            });
        }
        // UpdateProgress
        public void UpdateProgress(string deviceId, int percent, string displayText = null)
        {
            if (sfDataGrid.InvokeRequired)
            {
                sfDataGrid.Invoke(new Action(() => UpdateProgress(deviceId, percent, displayText)));
                return;
            }
            var dt = sfDataGrid.DataSource as System.Data.DataTable;
            if (dt == null) return;
            var row = dt.Select($"DeviceID = '{deviceId.Replace("'", "''")}'").FirstOrDefault();
            if (row == null) return;

            row["Progress"] = Math.Max(0, Math.Min(100, percent));
            _progressTextMap[deviceId] = displayText ?? $"{percent}%";
            sfDataGrid.Refresh();
        }

    }
}
