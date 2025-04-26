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
        private void ApplyPanelInputMargin()
        {
            if (AppState.CurrentWindowMode == WindowMode.Maximized)
            {
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));  // mặc định 60/40
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            }
            else
            {
                tableLayoutPanel1.ColumnStyles.Clear();
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));  // mặc định 60/40
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            }
        }
        private FlowLayoutPanel CreateInputPanel(string labelText, Control inputControl)
        {
            // Tạo FlowLayoutPanel chứa Label và Input (TextBox hoặc ComboBox)
            panel = new FlowLayoutPanel
            {
                //  Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                Height = 35,
                Width = 250,
                //BackColor = Color.Aqua
                // BackColor = Color.Aqua,
                //  AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Tạo AutoLabel với tên label
            label = new AutoLabel
            {
                Text = labelText,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 5, 0),
                Height = 35,
                Width = 100,
            };

            panel.Controls.Add(label);
            panel.Controls.Add(inputControl);

            return panel;
        }

        private void SetInputAddLayoutInput()
        {
            // Tạo các điều khiển input
            txtBrand = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130, Height = 35 };
            txtOS = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 60, Height = 3 };
            txtOS_version = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 60, Height = 35 };
            txtSerial = new TextBoxExt { Width = 130, Height = 35 };
            txtImei = new TextBoxExt { Width = 130, Height = 35 };
            txtMac = new TextBoxExt { Width = 130, Height = 35 };
            txtName = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130, Height = 35 };
            txtCountry = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130, Height = 35 };
            txtCode = new TextBoxExt { Width = 130, Height = 35 };
            txtImsi = new TextBoxExt { Width = 130, Height = 35 };
            txtModel = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130, Height = 35 };
            txtSim = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130, Height = 35 };
            txtPhone = new TextBoxExt { Width = 130, Height = 35 };
            txtIccId = new TextBoxExt { Width = 130, Height = 35 };

            osPanel = new FlowLayoutPanel
            {
                //    Dock = DockStyle.Top,
                Height = 35,
                Width = 250,
                FlowDirection = FlowDirection.LeftToRight,
                // AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            AutoLabel osLabel = new AutoLabel
            {
                Text = "OS",
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 5, 0),
                Height = 35,
                Width = 100,
                AutoSize = false,
            };
            osPanel.Controls.Add(osLabel);
            osPanel.Controls.Add(txtOS);
            osPanel.Controls.Add(txtOS_version);


            // Thêm các FlowLayoutPanel vào PanelInput
            PanelInput.Controls.Add(CreateInputPanel("BRAND", txtBrand));
            PanelInput.Controls.Add(CreateInputPanel("NAME", txtName));
            PanelInput.Controls.Add(CreateInputPanel("MODEL", txtModel));
            PanelInput.Controls.Add(osPanel);
            PanelInput.Controls.Add(CreateInputPanel("COUNTRY", txtCountry));
            PanelInput.Controls.Add(CreateInputPanel("SIM", txtSim));
            PanelInput.Controls.Add(CreateInputPanel("SERIAL", txtSerial));
            PanelInput.Controls.Add(CreateInputPanel("CODE", txtCode));
            PanelInput.Controls.Add(CreateInputPanel("PHONE", txtPhone));
            PanelInput.Controls.Add(CreateInputPanel("IMEI", txtImei));
            PanelInput.Controls.Add(CreateInputPanel("IMSI", txtImsi));
            PanelInput.Controls.Add(CreateInputPanel("ICCID", txtIccId));
            PanelInput.Controls.Add(CreateInputPanel("MAC", txtMac));
        }
        private void SetupButtonStyle(SfButton button)
        {
            button.Cursor = Cursors.Hand;

            button.MouseEnter += (s, e) =>
            {
                button.Style.BackColor = Color.DodgerBlue;
                button.Style.ForeColor = Color.White;
            };

            button.MouseLeave += (s, e) =>
            {
                button.Style.BackColor = Color.LightBlue;
                button.Style.ForeColor = Color.Black;
            };
        }
        private void SetButtonAddLayoutButton()
        {
            SfButton[] buttons = {
            btnRandomdevice = new SfButton { Text = "Random Device" },
            btnAutoBackup = new SfButton { Text = "Auto Backup" },
            btnAutochangeFull = new SfButton { Text = "Auto Change Full" },
            btnAutoChangeSim = new SfButton { Text = "Auto Change Sim" },
            btnBackup = new SfButton { Text = "Backup" },
            btnBackup2 = new SfButton { Text = "Backup " },
            btnChangeDevice = new SfButton { Text = "Change Device" },
            btnChangeSim = new SfButton { Text = "Change Sim" },
            btnOpenUrl = new SfButton { Text = "Open URL" },
            btnRandomSim = new SfButton { Text = "Random Sim" },
            btnScreenshot = new SfButton { Text = "Screenshot" },
            btnRestore = new SfButton {
                Text = "Restore",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightBlue,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10),
                Margin = new Padding(5),
                Height = 40,
                Width = 160
                },
            btnFakeLocation = new SfButton
            {
                Text = "Fake Location",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightBlue,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10),
                Margin = new Padding(5),
                Height = 40,
                Width = 160
                },
            };

            foreach (var btn in buttons)
            {
                SetupButtonStyle(btn);
            }

            txtRestore = new TextBoxExt
            {
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Padding = new Padding(10),
                Margin = new Padding(5, 8, 10, 0),
                Height = 40,
                Width = 320,
            };


            ConfigureButtons(
        btnRandomdevice,
        btnChangeDevice,
        btnAutochangeFull,
        btnRandomSim,
        btnChangeSim,
        btnAutoChangeSim,
        btnBackup,
        btnBackup2,
        btnOpenUrl,
        btnAutoBackup,
        btnScreenshot

        );
            PanelButton.Controls.Add(btnRestore);
            PanelButton.Controls.Add(txtRestore);
            PanelButton.Controls.Add(btnFakeLocation);
        }
        private void ConfigureButtons(params SfButton[] buttons)
        {
            foreach (var button in buttons)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.Style.BackColor = Color.LightBlue;
                button.Style.ForeColor = Color.Black;
                button.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                button.TextAlign = ContentAlignment.MiddleLeft;
                button.Padding = new Padding(10);
                button.Margin = new Padding(5);
                button.Height = 40;
                button.Width = 160;

                PanelButton.Controls.Add(button);
            }
        }

        public async Task InitializeDeviceStatus()
        {
            var (onlineDevices, offlineDevices) = LoadDevicesFromJson();
            var connectedDevices = GetConnectedDevices().ToHashSet();

            foreach (var device in onlineDevices)
            {
                if (!connectedDevices.Contains(device.Serial))
                {
                    UpdateDeviceStatus(device.Serial, "Offline");
                }
                else
                {
                    string currentStatus = IsDeviceOnline(device.Serial) ? "Online" : "Offline";
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

        public void setInit()
        {
            PanelInput.BorderStyle = BorderStyle.FixedSingle;
            PanelButton.BorderStyle = BorderStyle.FixedSingle;

            btnRandomdevice.TextAlign = ContentAlignment.MiddleLeft;
            btnAutoBackup.TextAlign = ContentAlignment.MiddleLeft;
            btnAutochangeFull.TextAlign = ContentAlignment.MiddleLeft;
            btnAutoChangeSim.TextAlign = ContentAlignment.MiddleLeft;
            btnBackup.TextAlign = ContentAlignment.MiddleLeft;
            btnBackup2.TextAlign = ContentAlignment.MiddleLeft;
            btnChangeDevice.TextAlign = ContentAlignment.MiddleLeft;
            btnChangeSim.TextAlign = ContentAlignment.MiddleLeft;
            btnOpenUrl.TextAlign = ContentAlignment.MiddleLeft;
            btnRandomSim.TextAlign = ContentAlignment.MiddleLeft;
            btnScreenshot.TextAlign = ContentAlignment.MiddleLeft;

            btnAutoBackup.Paint += BtnCommon_Paint;
            btnAutochangeFull.Paint += BtnCommon_Paint;
            btnAutoChangeSim.Paint += BtnCommon_Paint;
            btnBackup.Paint += BtnCommon_Paint;
            btnBackup2.Paint += BtnCommon_Paint;
            btnChangeDevice.Paint += BtnCommon_Paint;
            btnChangeDevice.Click += btnChange_Click;
            btnChangeSim.Paint += BtnCommon_Paint;
            btnOpenUrl.Paint += BtnCommon_Paint;
            btnRandomdevice.Paint += BtnCommon_Paint;
            btnRandomdevice.Click += btnRandom_Click;
            btnRandomSim.Paint += BtnCommon_Paint;
            btnScreenshot.Paint += BtnCommon_Paint;
            btnRestore.Paint += BtnCommon_Paint;
            btnFakeLocation.Paint += BtnCommon_Paint;
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

        //public void setGridView()
        //{
        //    sfDataGrid = new SfDataGrid
        //    {
        //        Dock = DockStyle.Fill,
        //        AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill,
        //        AllowEditing = false,
        //        AllowDeleting = false,
        //        AllowSorting = true,
        //        ShowGroupDropArea = false
        //    };

        //    sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "STT", HeaderText = "#", Width = 30 });
        //    sfDataGrid.Columns.Add(new GridCheckBoxColumn { MappingName = "Checkbox", HeaderText = "Box", AllowEditing = true, Width = 60 });
        //    sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "DeviceID", HeaderText = "Device ID", Width = 200 });
        //    sfDataGrid.Columns.Add(new GridProgressBarColumn { MappingName = "Progress", HeaderText = "Progress" });
        //    sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 100 });
        //    GridButtonColumn activityColumn = new GridButtonColumn { MappingName = "Activity", HeaderText = "Active", Width = 80 };
        //    sfDataGrid.Columns.Add(activityColumn);

        //    var progressCol = new GridProgressBarColumn
        //    {
        //        MappingName = "Progress",
        //        HeaderText = "Progress",
        //        Minimum = 0,
        //        Maximum = 100,
        //        ValueMode = ProgressBarValueMode.Percentage  // ← show “xx%” based on the 0–100 int :contentReference[oaicite:0]{index=0}
        //    };
        //    // chữ trắng căn giữa
        //    progressCol.CellStyle.TextColor = Color.White;
        //    progressCol.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;


        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("STT", typeof(int));
        //    dataTable.Columns.Add("Checkbox", typeof(bool));
        //    dataTable.Columns.Add("DeviceID", typeof(string));

        //    dataTable.Columns.Add(progressCol, typeof(int));
        //    dataTable.Columns.Add("Status", typeof(string));
        //    dataTable.Columns.Add("Activity", typeof(string));

        //    sfDataGrid.DataSource = dataTable;
        //    tableLayoutPanel.Controls.Add(sfDataGrid, 0, 0);

        //  //  sfDataGrid.QueryCellStyle += SfDataGrid_QueryCellStyle;
        //    this.sfDataGrid.RecordContextMenu = new ContextMenuStrip();
        //    var detailsItem = new ToolStripMenuItem("Details");
        //    var editItem = new ToolStripMenuItem("Edit");
        //    var deleteItem = new ToolStripMenuItem("Delete");

        //    detailsItem.Click += DetailsItem_Click;
        //    editItem.Click += EditItem_Click;
        //    deleteItem.Click += DeleteItem_Click;

        //    this.sfDataGrid.RecordContextMenu.Items.Add(detailsItem);
        //    this.sfDataGrid.RecordContextMenu.Items.Add(editItem);
        //    this.sfDataGrid.RecordContextMenu.Items.Add(deleteItem);
        //}
        private DataTable _deviceTable;
        private object copyContent = null;
        public void setGridView()
        {
            // 1. Khởi SfDataGrid và các cột STT, Checkbox, DeviceID
            sfDataGrid = new SfDataGrid
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = AutoSizeColumnsMode.Fill,
                AllowEditing = false,
                AllowDeleting = false,
                AllowSorting = true,
                ShowGroupDropArea = false
            };
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "STT", HeaderText = "#", Width = 30 });
            sfDataGrid.Columns.Add(new GridCheckBoxColumn { MappingName = "Checkbox", HeaderText = "Box", Width = 60, AllowEditing = true });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "DeviceID", HeaderText = "Device ID", Width = 200 });

            // 2. Thêm 1 lần duy nhất cột Progress với Percentage & style chữ trắng
            var progressCol = new GridProgressBarColumn
            {
                MappingName = "Progress",
                HeaderText = "Progress",
                Minimum = 0,
                Maximum = 100,
                ValueMode = ProgressBarValueMode.Percentage
            };
            progressCol.CellStyle.TextColor = Color.White;
            progressCol.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
            sfDataGrid.Columns.Add(progressCol);

            // 3. Thêm các cột còn lại
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 100 });
            sfDataGrid.Columns.Add(new GridButtonColumn { MappingName = "Activity", HeaderText = "Active", Width = 80 });

            // 4. Khởi và bind DataTable gốc (_deviceTable) với đúng 6 cột
            _deviceTable = new DataTable();
            _deviceTable.Columns.Add("STT", typeof(int));
            _deviceTable.Columns.Add("Checkbox", typeof(bool));
            _deviceTable.Columns.Add("DeviceID", typeof(string));
            _deviceTable.Columns.Add("Progress", typeof(int));    // phải đúng tên và kiểu int
            _deviceTable.Columns.Add("Status", typeof(string));
            _deviceTable.Columns.Add("Activity", typeof(string));

            sfDataGrid.DataSource = _deviceTable;
            tableLayoutPanel.Controls.Add(sfDataGrid, 0, 0);

            // 5. Thiết lập context menu nếu cần
            this.sfDataGrid.RecordContextMenu = new ContextMenuStrip();
            var copyDeviceID = new ToolStripMenuItem("Copy Device ID");
            var detailsItem = new ToolStripMenuItem("Details");
            var editItem = new ToolStripMenuItem("Edit");
            var deleteItem = new ToolStripMenuItem("Delete");
            copyDeviceID.Click += CopyDeviceID_Click;
            detailsItem.Click += DetailsItem_Click;
            editItem.Click += EditItem_Click;
            deleteItem.Click += DeleteItem_Click;
            this.sfDataGrid.RecordContextMenu.Items.AddRange(
                new ToolStripItem[] { copyDeviceID, detailsItem, editItem, deleteItem }
            );
        }

        //private void SfDataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        //{
        //    if (e.Column.MappingName == "Progress" &&
        //        e.DataRow.RowType == RowType.DefaultRow)
        //    {
        //        var drv = e.DataRow.RowData as System.Data.DataRowView;
        //        if (drv != null)
        //        {
        //            e.DisplayText = drv["Progress"].ToString() + "%";
        //        }

        //        e.Style.TextColor = Color.White;
        //        e.Style.HorizontalAlignment = HorizontalAlignment.Center;
        //    }
        //}

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
                // Khởi tạo tiến trình ADB để kiểm tra thiết bị
                var process = new Process();
                process.StartInfo.FileName = "adb";
                process.StartInfo.Arguments = $"-s {deviceId} shell getprop sys.boot_completed";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                // Đọc kết quả đầu ra từ lệnh adb
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Nếu kết quả là "1", thiết bị đã hoàn tất khởi động và active
                return !string.IsNullOrEmpty(output) && output.Contains("1");
            }
            catch (Exception ex)
            {
                // In ra lỗi nếu không thể kiểm tra trạng thái thiết bị
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

        private void BtnCommon_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            int radius = 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(
                btn.ClientRectangle.X + 1,
                btn.ClientRectangle.Y + 1,
                btn.ClientRectangle.Width - 2,
                btn.ClientRectangle.Height - 2
            );

            btn.Region = new Region(GetRoundedRect(rect, radius));
            rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            e.Graphics.FillRectangle(new SolidBrush(btn.BackColor), rect);

            Pen borderPen = GetButtonBorderPen(btn);

            e.Graphics.DrawPath(borderPen, GetRoundedRect(rect, radius));

            Color textColor = GetButtonTextColor(btn);

            Rectangle textRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4); // Điều chỉnh phạm vi để tránh chữ bị đè lên
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private Pen GetButtonBorderPen(Button btn)
        {
            if (!btn.Enabled)
            {
                return new Pen(Color.Gray);
            }
            else if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                return new Pen(Color.Blue);
            }
            else if (btn.Focused)
            {
                return new Pen(Color.Green);
            }
            else
            {
                return new Pen(Color.Gray);
            }
        }

        private Color GetButtonTextColor(Button btn)
        {
            if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                return Color.Blue;
            }
            return btn.ForeColor;
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); // Top-left corner
            graphicsPath.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y); // Top edge
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90); // Top-right corner
            graphicsPath.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius); // Right edge
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90); // Bottom-right corner
            graphicsPath.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom); // Bottom edge
            graphicsPath.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90); // Bottom-left corner
            graphicsPath.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius); // Left edge
            graphicsPath.CloseFigure();
            return graphicsPath;
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
