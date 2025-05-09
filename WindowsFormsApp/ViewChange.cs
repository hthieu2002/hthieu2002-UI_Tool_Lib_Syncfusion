using AccountCreatorForm;
using AccountCreatorForm.Constants;
using AccountCreatorForm.Controls;
using AuthenticationService;
using MiHttpClient;
using Newtonsoft.Json;
using POCO.Models;
using Services;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using System;
using System.CodeDom;
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
using System.Xml.Linq;
using WindowsFormsApp.Animation;
using WindowsFormsApp.Model;
using WindowsFormsApp.Script;
using Xamarin.Forms;


namespace WindowsFormsApp
{
    public partial class ViewChange : Form
    {
        private HeaderViewCommon headerView;
        private SfDataGrid sfDataGrid;
        private List<WindowsFormsApp.Model.DeviceDisplay> deviceDisplays = new List<WindowsFormsApp.Model.DeviceDisplay>();  // Danh sách các thiết bị hiện tại
        private Task _deviceCheckTask;
        public static ViewChange Instance { get; private set; }
        private readonly Dictionary<string, string> _progressTextMap = new Dictionary<string, string>();
        private readonly HashSet<string> _animatingDevices = new HashSet<string>();

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
        //
        FlowLayoutPanel panel;
        AutoLabel label;
        FlowLayoutPanel osPanel;
        //
        private MiChangerGraphQLClient miChangerGraphQLClient;
        private DeviceModel tempDeviceAll;

        private List<SimCarrier> simCarriers;
        public DeviceConfigModel _deviceConfig;
        private List<SimCarrier> telecomDataSource = new List<SimCarrier>();
        public ViewChange()
        {
            this.SuspendLayout();
            InitializeComponent();

            ConfigureForm(); // Cấu hình cơ bản
            Instance = this; // ← Phải set ở Constructor luôn để mọi nơi khác dùng được Instance
            setGridView();
            // Delay load UI logic sang sự kiện Load
            this.Load += async (s, e) => await LoadAsync();
            this.ResumeLayout(false);
        }
        private void ConfigureForm()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Resize += MyForm_OnSizeChange;
            this.SizeChanged += MyForm_OnSizeChange;
        }
        private async Task LoadAsync()
        {

            SetInputAddLayoutInput();
            SetButtonAddLayoutButton();

            setInit();
            setMenu();
            LoadDevicesFromFile();
            await InitializeDeviceStatus();
            StartDeviceCheck();
            LoadGUI();
            setupEnableButtonRandom();
        }
        private void LoadGUI()
        {
            var uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var taskLoadCarrierJson = Task.Run(() => JsonService<SimCarrier>.loadConfigurationFromResource("carriers.json"))
                .ContinueWith(task =>
                {
                    telecomDataSource = task.Result;
                    //load list carrier to dropdownlist once loading progress (from json) done
                    simCarriers = telecomDataSource
                                    .GroupBy(c => c.CountryName)
                                    .Select(c => c.First())
                                    .OrderBy(c => c.CountryName)
                                    .ToList();

                    txtCountry.DisplayMember = "CountryName";

                    txtCountry.DataSource = simCarriers;
                    if (true)
                    {
                        try
                        {
                            var simCarrier = simCarriers.FirstOrDefault(x => x.CountryName.Equals("Abkhazia"));
                            if (simCarrier != null)
                            {
                                txtCountry.Text = simCarrier.CountryName;
                            }
                        }
                        catch
                        {
                            //ignored
                        }
                    }

                    return telecomDataSource;
                },
                uiThreadScheduler);
            // var taskLoadDeviceModelJson = Task.Run(() => JsonService<DeviceModel>.loadConfigurationFromResource("devices.json"))
            //    .ContinueWith(task => { deviceModelDataSource = task.Result; return deviceModelDataSource; });
            //comboBoxSourceInfo.SelectedIndex = 2; //0: wadoge, 1: samsung, 2: all
            //  comboBoxOpenFrom.SelectedIndex = 0;  //0: vending, 1: setting
            //  comboBoxRecovery.SelectedIndex = 1;  //0: NONE, 1: All, 2: yahoo.com, 3: outlook.com
        }
        private void txtCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentValue = txtCountry.SelectedValue as SimCarrier;

            var carrierList = telecomDataSource.FindAll(c => currentValue.Attribute.Mcc.Equals(c.Attribute.Mcc))
                .Select(c => new ComboBoxItem { Name = c.Name + "-" + c.Attribute.Mnc, Value = c.Attribute.Mnc })
                .ToList();

            txtSim.DisplayMember = "Name";
            txtSim.DataSource = carrierList;
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
                        string currentStatus =ADBService.IsDeviceOnline(device) ? "Online" : "Offline";
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
     
        private void AddDeviceView(string deviceId, string name, int check)
        {
            _deviceTable = sfDataGrid.DataSource as DataTable;
            if (_deviceTable == null)
            {
                Console.WriteLine("DataSource is not assigned correctly.");
                return;
            }
            int stt = _deviceTable.Rows.Count + 1;


            _deviceTable.Rows.Add(stt, false, name, deviceId, 0, "", "Offline");
            if (check == 1)
            {
                deviceDisplays.Add(new WindowsFormsApp.Model.DeviceDisplay { Serial = deviceId, Status = "Offline", Activity = "YES", Checkbox = false });
            }
            else
            {
                deviceDisplays.Add(new WindowsFormsApp.Model.DeviceDisplay { Name = name, Serial = deviceId, Status = "Offline", Activity = "YES", Checkbox = false });
            }

            SaveDevicesToFile();
            sfDataGrid.Refresh();
        }
        private void RemoveDeviceView(WindowsFormsApp.Model.DeviceDisplay device)
        {
            _deviceTable = sfDataGrid.DataSource as DataTable;
            var rows = _deviceTable.Select($"DeviceID = '{device.Serial}'");
            if (rows.Length > 0)
            {
                _deviceTable.Rows.Remove(rows[0]);
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
            bool isActive = ADBService.IsDeviceActive(deviceId);
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
                AddDeviceView(device.Serial, device.Name, 0);
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
            var selectedRow = sfDataGrid.SelectedItems.Cast<DataRowView>().FirstOrDefault();
            if (selectedRow != null)
            {
                string deviceId = selectedRow["DeviceID"].ToString();

                DeviceDetailsForm detailsForm = new DeviceDetailsForm(deviceId);

                detailsForm.ShowDialog();
            }
        }
        private void EditItem_Click(object sender, EventArgs e)
        {
            var selectedRow = sfDataGrid.SelectedItems.Cast<DataRowView>().FirstOrDefault();
            if (selectedRow != null)
            {
                string deviceId = selectedRow["DeviceID"].ToString();

                var deviceToUpdate = deviceDisplays.FirstOrDefault(d => d.Serial == deviceId);

                if (deviceToUpdate != null)
                {
                    NameInputForm nameForm = new NameInputForm(deviceToUpdate.Name);
                    if (nameForm.ShowDialog() == DialogResult.OK)
                    {
                        string newName = nameForm.NewName;

                        deviceToUpdate.Name = newName;

                        _deviceTable = sfDataGrid.DataSource as DataTable;
                        var rows = _deviceTable.Select($"DeviceID = '{deviceToUpdate.Serial}'");
                        if (rows.Length > 0)
                        {
                            rows[0]["NameID"] = deviceToUpdate.Name;
                        }

                        SaveDevicesToFile();
                        sfDataGrid.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để chỉnh sửa.");
            }
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
        private void CreateService()
        {
            var poolId = AppConfigService.ReadSetting("poolId");
            var clientId = AppConfigService.ReadSetting("clientId");
            var cognito = new CognitoService("ap-southeast-1_Cha6gy7Ui", "4h21ba0at8flinn9iq351if381");
            var username = AppConfigService.ReadSetting("email");
            var password = AppConfigService.ReadSetting("password");
            var endpoint = "https://nievrqo2rbdtfhmhzc2bg2epka.appsync-api.ap-southeast-1.amazonaws.com/graphql";//AppConfigService.ReadSetting("endpoint");
            var refreshToken = cognito.getIdToken("mistplay@yopmail.com", "12345678");
            if (!string.IsNullOrEmpty(refreshToken))
            {
                miChangerGraphQLClient = new MiChangerGraphQLClient(endpoint, ApiAuthenticationType.TOKEN, refreshToken);
            }
        }
        private async void StartAllRandomChange(int button, int autoChange = 0)
        {
            var result = DialogResult.No;
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
            string message = "Are you sure to proceed with these changes and reboot ?";
            string title = "Changes Confirmation";
            if (button == 1 || button == 2)
            {
                result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if (result == DialogResult.Yes || button > 2)
            {
                var tasks = toAnimate.Select(async id =>
                {
                    int rowIndex = dt.AsEnumerable().ToList().FindIndex(r => r.Field<string>("DeviceID") == id);

                    Console.WriteLine($"DeviceID: {id}, rowIndex: {rowIndex}, Total rows: {dt.Rows.Count}");

                    if (rowIndex >= 0 && rowIndex < dt.Rows.Count)
                    {
                        var row = dt.Rows[rowIndex];
                        if (row != null)
                        {
                            _animatingDevices.Add(id);

                            switch (button)
                            {
                                case 1: // change device
                                    await StartChange(id, row, autoChange);
                                    break;
                                case 2: // change sim
                                    await StartChangeSim(id, row, autoChange);
                                    break;
                                case 3: // fake location
                                    await StartFakeLocation(id, row);
                                    break;
                                case 4: // screen shot
                                    await StartScreenShot(id, row);
                                    break;
                                case 5:
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Dòng không hợp lệ.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Không tìm thấy dòng hợp lệ cho DeviceID: {id}.");
                    }
                }).ToArray();
                await Task.WhenAll(tasks);
            }
            else
            {
                //do nothing
            }
        }

        private async void btnRandom_Click(object sender, EventArgs e)
        {
            setupDisableButtonRandom();
            if (miChangerGraphQLClient == null)
            {
                CreateService();
            }
            var currentSelectedCarrier = txtSim.SelectedValue as ComboBoxItem;
            var currentSelectedCountry = txtCountry.SelectedValue as SimCarrier;
            var mcc = currentSelectedCountry.Attribute.Mcc;
            var mnc = currentSelectedCarrier.Value;

            Console.WriteLine("Country Code = {0}. MCC = {1} while carrier name = {2} MNC = {3}"
                , currentSelectedCountry.CountryCode
                , mcc
                , currentSelectedCarrier.Name
                , mnc);

            try
            {
                tempDeviceAll = await miChangerGraphQLClient.GetRandomDeviceV3(sdkMin: 30);
                if (tempDeviceAll.Model == null)
                {
                    MessageBox.Show("Devices not existed, please try again");
                    throw new Exception("Devices not existed, please try again");
                }

                txtName.DataSource = new List<string> { tempDeviceAll.Board };
                txtName.SelectedItem = tempDeviceAll.Board;
                txtOS.DataSource = new List<string> { tempDeviceAll.Release };
                txtOS.SelectedItem = tempDeviceAll.Release;
                txtOS_version.DataSource = new List<string> { tempDeviceAll.SDK };
                txtOS_version.SelectedItem = tempDeviceAll.SDK;
                txtBrand.DataSource = new List<string> { tempDeviceAll.Manufacturer };
                txtBrand.SelectedItem = tempDeviceAll.Manufacturer;
                txtModel.DataSource = new List<string> { tempDeviceAll.Model };
                txtModel.SelectedItem = tempDeviceAll.Model;

                txtImsi.Text = tempDeviceAll.IMSI = RandomService.generateIMSI(mcc, mnc);
                txtIccId.Text = tempDeviceAll.ICCID = RandomService.generateICCID(currentSelectedCountry.CountryCode, mnc);
                txtImei.Text = tempDeviceAll.Imei;
                tempDeviceAll.SerialNo = RandomService.getRandomStringHex16Digit().Substring(0, RandomService.randomInRange(8, 13));
                txtSerial.Text = tempDeviceAll.SerialNo;
                txtPhone.Text = tempDeviceAll.SimPhoneNumber = string.Format("+{0}{1}", currentSelectedCountry.CountryCode, RandomService.generatePhoneNumber());
                txtModel.Text = tempDeviceAll.Model;
                tempDeviceAll.SimOperatorNumeric = string.Concat(mcc, mnc);
                txtCode.Text = tempDeviceAll.SimOperatorNumeric;
                tempDeviceAll.SimOperatorCountry = currentSelectedCountry.CountryIso;
                tempDeviceAll.SimOperatorName = currentSelectedCarrier.Name.Substring(0, currentSelectedCarrier.Name.LastIndexOf("-")).Replace("&", "^&");
                tempDeviceAll.AndroidId = RandomService.getRandomStringHex16Digit();
                tempDeviceAll.WifiMacAddress = RandomService.generateWifiMacAddress();
                tempDeviceAll.BlueToothMacAddress = RandomService.generateMacAddress();
                txtMac.Text = RandomService.generateMacAddress();
            }
            catch (Exception ex)
            {
                //ignored
                Console.WriteLine(ex);
            }
            finally
            {
                setupEnableButtonChangeDevice();
            }
        }
        private async void BtnRandomSim_Click(object sender, EventArgs e)
        {
            setupDisableButtonRandomSim(); // off 
            if (miChangerGraphQLClient == null)
            {
                CreateService();
            }
            var currentSelectedCarrier = txtSim.SelectedValue as ComboBoxItem;
            var currentSelectedCountry = txtCountry.SelectedValue as SimCarrier;
            var mcc = currentSelectedCountry.Attribute.Mcc;
            var mnc = currentSelectedCarrier.Value;

            tempDeviceAll = await miChangerGraphQLClient.GetRandomDeviceV3(sdkMin: 30);
            if (tempDeviceAll.Model == null)
            {
                MessageBox.Show("Devices not existed, please try again");
                throw new Exception("Devices not existed, please try again");
            }
            tempDeviceAll.IMSI = RandomService.generateIMSI(mcc, mnc);
            txtImsi.Text = tempDeviceAll.IMSI;
            tempDeviceAll.ICCID = RandomService.generateICCID(currentSelectedCountry.CountryCode, mnc);
            txtIccId.Text = tempDeviceAll.ICCID;
            tempDeviceAll.SimPhoneNumber = string.Format("+{0}{1}", currentSelectedCountry.CountryCode, RandomService.generatePhoneNumber());
            txtPhone.Text = tempDeviceAll.SimPhoneNumber;
            tempDeviceAll.SimOperatorNumeric = string.Concat(mcc, mnc);
            txtCode.Text = tempDeviceAll.SimOperatorNumeric;
            tempDeviceAll.SimOperatorCountry = currentSelectedCountry.CountryIso;
            tempDeviceAll.SimOperatorName = currentSelectedCarrier.Name.Substring(0, currentSelectedCarrier.Name.LastIndexOf("-")).Replace("&", "^&");
            setupEnableButtonChangeSim(); // on
        }
        public void btnChange_Click(object sender, EventArgs e)
        {
            StartAllRandomChange(1, 1);
        }
        public void btnChangeFull_Click(object sender, EventArgs e)
        {
            StartAllRandomChange(1);
        }
        private void btnChangeSim_Click(object sender, EventArgs e)
        {
            StartAllRandomChange(2, 1);
        }
        private void btnChangeSimAll_Click(object sender, EventArgs e)
        {
            StartAllRandomChange(2);
        }
        private void btnFakeLocation_Click(object sender, EventArgs e)
        {
            StartAllRandomChange(3);
        }
        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            StartAllRandomChange(4);
        }
        public async Task<DeviceModel> RandomDevice()
        {
            DeviceModel tempDevice = null;
            if (miChangerGraphQLClient == null)
            {
                CreateService();
            }

            var currentSelectedCarrier = txtSim.SelectedValue as ComboBoxItem;
            var currentSelectedCountry = txtCountry.SelectedValue as SimCarrier;
            var mcc = currentSelectedCountry.Attribute.Mcc;
            var mnc = currentSelectedCarrier.Value;

            Console.WriteLine("Country Code = {0}. MCC = {1} while carrier name = {2} MNC = {3}"
                , currentSelectedCountry.CountryCode
                , mcc
                , currentSelectedCarrier.Name
                , mnc);

            try
            {
                tempDevice = await miChangerGraphQLClient.GetRandomDeviceV3(sdkMin: 30);
                if (tempDevice.Model == null)
                {
                    MessageBox.Show("Devices not existed, please try again");
                    throw new Exception("Devices not existed, please try again");
                }

                tempDevice.IMSI = RandomService.generateIMSI(mcc, mnc);
                tempDevice.ICCID = RandomService.generateICCID(currentSelectedCountry.CountryCode, mnc);
                tempDevice.SerialNo = RandomService.getRandomStringHex16Digit().Substring(0, RandomService.randomInRange(8, 13));
                tempDevice.SimPhoneNumber = string.Format("+{0}{1}", currentSelectedCountry.CountryCode, RandomService.generatePhoneNumber());
                tempDevice.SimOperatorNumeric = string.Concat(mcc, mnc);
                tempDevice.SimOperatorCountry = currentSelectedCountry.CountryIso;
                tempDevice.SimOperatorName = currentSelectedCarrier.Name.Substring(0, currentSelectedCarrier.Name.LastIndexOf("-")).Replace("&", "^&");
                tempDevice.AndroidId = RandomService.getRandomStringHex16Digit();
                tempDevice.WifiMacAddress = RandomService.generateWifiMacAddress();
                tempDevice.BlueToothMacAddress = RandomService.generateMacAddress();
            }
            catch (Exception ex)
            {
                //ignored
                Console.WriteLine(ex);
            }
            finally
            {

            }
            return tempDevice;
        }
        public async Task<DeviceModel> RandomSim()
        {
            DeviceModel tempDevice = null;
            if (miChangerGraphQLClient == null)
            {
                CreateService();
            }

            var currentSelectedCarrier = txtSim.SelectedValue as ComboBoxItem;
            var currentSelectedCountry = txtCountry.SelectedValue as SimCarrier;
            var mcc = currentSelectedCountry.Attribute.Mcc;
            var mnc = currentSelectedCarrier.Value;

            Console.WriteLine("Country Code = {0}. MCC = {1} while carrier name = {2} MNC = {3}"
                , currentSelectedCountry.CountryCode
                , mcc
                , currentSelectedCarrier.Name
                , mnc);

            try
            {
                tempDevice = await miChangerGraphQLClient.GetRandomDeviceV3(sdkMin: 30);
                if (tempDevice.Model == null)
                {
                    MessageBox.Show("Devices not existed, please try again");
                    throw new Exception("Devices not existed, please try again");
                }

                tempDevice.IMSI = RandomService.generateIMSI(mcc, mnc);
                tempDevice.ICCID = RandomService.generateICCID(currentSelectedCountry.CountryCode, mnc);
                tempDevice.SimPhoneNumber = string.Format("+{0}{1}", currentSelectedCountry.CountryCode, RandomService.generatePhoneNumber());
                tempDevice.SimOperatorNumeric = string.Concat(mcc, mnc);
                tempDevice.SimOperatorCountry = currentSelectedCountry.CountryIso;
                tempDevice.SimOperatorName = currentSelectedCarrier.Name.Substring(0, currentSelectedCarrier.Name.LastIndexOf("-")).Replace("&", "^&");
            }
            catch (Exception ex)
            {
                //ignored
                Console.WriteLine(ex);
            }
            finally
            {

            }
            return tempDevice;
        }
        public async Task StartChange(string device, System.Data.DataRow row, int autoChange)
        {
            DeviceModel deviceTemp = null;
            if (autoChange != 1)
            {
                await ViewChange.Instance.updateProgress(row, "Auto random device", 1);
                deviceTemp = await RandomDevice();
            }

            var uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var saveResult = true;
            await ViewChange.Instance.updateProgress(row, "Start change device", 2);
            await Task.Run(async () =>
            {
                //Firstly, save
                BeginInvoke(new Action(() =>
                {
                    if (autoChange != 1)
                    {
                        // auto
                        btnAutochangeFull.Text = "Running";
                        btnAutochangeFull.Enabled = false;
                        btnAutochangeFull.BackColor = System.Drawing.Color.OrangeRed;
                    }
                    else
                    {
                        btnChangeDevice.Text = "Running";
                        btnChangeDevice.Enabled = false;
                        btnChangeDevice.BackColor = System.Drawing.Color.OrangeRed;
                    }
                }));
                await ViewChange.Instance.updateProgress(row, "Disable wifi", 5);
                ADBService.enableWifi(false, device);
                await Task.Delay(2000);
                if (autoChange == 1)
                {
                    saveResult = Util.SaveDeviceInfo(tempDeviceAll, device, System.Windows.Forms.Application.StartupPath, false, row);
                }
                else
                {
                    saveResult = Util.SaveDeviceInfo(deviceTemp, device, System.Windows.Forms.Application.StartupPath, false, row);
                }
                if (saveResult)
                {
                    _ = ViewChange.Instance.updateProgress(row, "Save info ...", 92);
                    // Wipe
                    BeginInvoke(new Action(() =>
                    {

                    }));
                    _ = ViewChange.Instance.updateProgress(row, "Save info success", 93);

                    _ = ViewChange.Instance.updateProgress(row, "Wipe data", 94);
                    var packagesWipeAfterChanger = loadWipeListConfig();
                    wipePackagesChanger(packagesWipeAfterChanger, device);
                    _ = ViewChange.Instance.updateProgress(row, "Clean GMS ...", 95);
                    _ = ViewChange.Instance.updateProgress(row, "Clean GMS ...", 96);
                    ADBService.cleanGMSPackagesAndAccounts(device);
                    _ = ViewChange.Instance.updateProgress(row, "Clean GMS success", 98);
                    BeginInvoke(new Action(() =>
                    {
                        _ = ViewChange.Instance.updateProgress(row, "Change device success", 99);
                        if (autoChange != 1)
                        {
                            // auto
                            btnAutochangeFull.Text = "Auto Change Full";
                            btnAutochangeFull.Enabled = true;
                            btnAutochangeFull.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            btnChangeDevice.Text = "Change Device";
                            btnChangeDevice.Enabled = true;
                            btnChangeDevice.BackColor = System.Drawing.Color.LightBlue;
                        }
                    }));
                    if (device.Length >= 12)
                    {
                        await ViewChange.Instance.updateProgress(row, "Rebooting ... ", 100);
                        _animatingDevices.Remove(device);
                        ADBService.restartDevice(device);
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        await ViewChange.Instance.updateProgress(row, "Rebooting ...", 100);
                        _animatingDevices.Remove(device);
                        ADBService.restartDevice(device);
                        Thread.Sleep(10000);
                        // FakeDevicePixelAction(device, checkBoxFakeSimInfo.Checked);
                    }
                }
            }).ContinueWith(task =>
            {
                if (!saveResult)
                {

                    _animatingDevices.Remove(device);
                    MessageBox.Show("This selected device cannot be changed, please check your rom and developer setting and try loading again."
                                            , "Device Error"
                                            , MessageBoxButtons.OK
                                            , MessageBoxIcon.Error);
                }
            }, uiThreadScheduler);


        }
        public async Task StartChangeSim(string device, System.Data.DataRow row, int autoChange)
        {
            DeviceModel deviceTemp = null;
            if (autoChange != 1)
            {
                await ViewChange.Instance.updateProgress(row, "Auto random sim", 1);
                deviceTemp = await RandomSim();
            }

            var uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var saveResult = true;
            await ViewChange.Instance.updateProgress(row, "Start change sim", 2);
            await Task.Run(async () =>
            {
                //Firstly, save
                BeginInvoke(new Action(() =>
                {
                    if (autoChange != 1)
                    {
                        // auto
                        btnAutoChangeSim.Text = "Running";
                        btnAutoChangeSim.Enabled = false;
                        btnAutoChangeSim.BackColor = System.Drawing.Color.OrangeRed;
                    }
                    else
                    {
                        btnChangeSim.Text = "Running";
                        btnChangeSim.Enabled = false;
                        btnChangeSim.BackColor = System.Drawing.Color.OrangeRed;
                    }
                }));
                await ViewChange.Instance.updateProgress(row, "Disable wifi", 5);
                ADBService.enableWifi(false, device);
                await Task.Delay(2000);
                if (autoChange == 1)
                {
                    await ViewChange.Instance.updateProgress(row, "Start change sim", 7);
                    saveResult = Util.SaveDeviceSIm(tempDeviceAll, device, System.Windows.Forms.Application.StartupPath, row);
                }
                else
                {
                    await ViewChange.Instance.updateProgress(row, "Start change sim full", 7);
                    saveResult = Util.SaveDeviceSIm(deviceTemp, device, System.Windows.Forms.Application.StartupPath, row);
                }
                if (saveResult)
                {
                    // Wipe
                    BeginInvoke(new Action(() =>
                    {

                    }));
                    //await ViewChange.Instance.updateProgress(row, "Wipe change", 92);
                    //var packagesWipeAfterChanger = loadWipeListConfig();
                    //wipePackagesChanger(packagesWipeAfterChanger, device);
                    //ADBService.cleanGMSPackagesAndAccounts(device);
                    //await ViewChange.Instance.updateProgress(row, "Success Wipe change", 94);
                    BeginInvoke(new Action(() =>
                    {
                        if (autoChange != 1)
                        {
                            // auto
                            btnAutoChangeSim.Text = "Auto Change Sim";
                            btnAutoChangeSim.Enabled = true;
                            btnAutoChangeSim.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            btnChangeSim.Text = "Change Sim";
                            btnChangeSim.Enabled = true;
                            btnChangeSim.BackColor = System.Drawing.Color.LightBlue;
                        }
                    }));
                    if (device.Length >= 12)
                    {
                        await ViewChange.Instance.updateProgress(row, "Rebooting ...", 100);
                        _animatingDevices.Remove(device);
                        ADBService.restartDevice(device);
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        await ViewChange.Instance.updateProgress(row, "Rebooting ...", 100);
                        _animatingDevices.Remove(device);
                        ADBService.restartDevice(device);
                        Thread.Sleep(10000);
                        // FakeDevicePixelAction(device, checkBoxFakeSimInfo.Checked);
                    }
                }
            }).ContinueWith(task =>
            {
                if (!saveResult)
                {

                    _animatingDevices.Remove(device);
                    MessageBox.Show("This selected device cannot be changed, please check your rom and developer setting and try loading again."
                                            , "Device Error"
                                            , MessageBoxButtons.OK
                                            , MessageBoxIcon.Error);
                }
            }, uiThreadScheduler);
        }
        public async Task StartFakeLocation(string device, System.Data.DataRow row)
        {
            await ViewChange.Instance.updateProgress(row, "Fake location", 1);
            try
            {
                // Tạo form nhập tọa độ
                Form inputForm = new Form()
                {
                    Width = 300,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Nhập tọa độ",
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                await ViewChange.Instance.updateProgress(row, "Info form location", 1);
                AutoLabel lblX = new AutoLabel() { Left = 20, Top = 20, Text = "Latitude:", AutoSize = true };
                TextBoxExt txtX = new TextBoxExt() { Left = 100, Top = 20, Width = 160 };

                txtX.KeyPress += (s, e) =>
                {
                    TextBox tb = s as TextBox;

                    if (char.IsControl(e.KeyChar))
                        return;

                    if (char.IsDigit(e.KeyChar))
                        return;

                    if (e.KeyChar == '.' && !tb.Text.Contains('.'))
                        return;

                    if ((e.KeyChar == '-' || e.KeyChar == '+') && tb.SelectionStart == 0 && !tb.Text.Contains("-") && !tb.Text.Contains("+"))
                        return;
                    e.Handled = true;
                };
                txtX.Leave += (s, e) =>
                {
                    if (double.TryParse(txtX.Text, out double value))
                    {
                        if (value < -180.0 || value > 180.0)
                        {
                            MessageBox.Show("Giá trị Latitude phải nằm trong khoảng từ -180.0 đến 180.0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtX.Focus();
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(txtX.Text))
                    {
                        MessageBox.Show("Giá trị không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtX.Focus();
                    }
                };
                AutoLabel lblY = new AutoLabel() { Left = 20, Top = 60, Text = "Longitude:", AutoSize = true };
                TextBoxExt txtY = new TextBoxExt() { Left = 100, Top = 60, Width = 160 };
                txtY.KeyPress += (s, e) =>
                {
                    TextBox tb = s as TextBox;

                    if (char.IsControl(e.KeyChar))
                        return;

                    if (char.IsDigit(e.KeyChar))
                        return;

                    if (e.KeyChar == '.' && !tb.Text.Contains('.'))
                        return;

                    if ((e.KeyChar == '-' || e.KeyChar == '+') && tb.SelectionStart == 0 && !tb.Text.Contains("-") && !tb.Text.Contains("+"))
                        return;
                    e.Handled = true;
                };
                txtY.Leave += (s, e) =>
                {
                    if (double.TryParse(txtY.Text, out double value))
                    {
                        if (value < -90.0 || value > 90.0)
                        {
                            MessageBox.Show("Giá trị Longitude phải nằm trong khoảng từ -90.0 đến 90.0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtY.Focus();
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(txtY.Text))
                    {
                        MessageBox.Show("Giá trị không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtY.Focus();
                    }
                };
                SfButton btnOK = new SfButton() { Text = "OK", Left = 70, Width = 80, Top = 110, DialogResult = DialogResult.OK };
                SfButton btnCancel = new SfButton() { Text = "Cancel", Left = 170, Width = 80, Top = 110, DialogResult = DialogResult.Cancel };

                btnOK.Paint += BtnCommon_Paint;
                btnCancel.Paint += BtnCommon_Paint;
                btnOK.Style.BackColor = System.Drawing.Color.LightBlue;
                btnOK.Style.ForeColor = System.Drawing.Color.White;
                btnCancel.Style.BackColor = System.Drawing.Color.LightBlue;
                btnCancel.Style.ForeColor = System.Drawing.Color.White;
                SetupButtonStyle(btnOK);
                SetupButtonCancelStyle(btnCancel);


                inputForm.Controls.Add(lblX);
                inputForm.Controls.Add(txtX);
                inputForm.Controls.Add(lblY);
                inputForm.Controls.Add(txtY);
                inputForm.Controls.Add(btnOK);
                inputForm.Controls.Add(btnCancel);

                inputForm.AcceptButton = btnOK;
                inputForm.CancelButton = btnCancel;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    await ViewChange.Instance.updateProgress(row, "Success info form location", 5);
                    string x = txtX.Text;
                    string y = txtY.Text;
                    await ViewChange.Instance.updateProgress(row, "Start fake location", 10);
                    btnFakeLocation.Enabled = false;
                    btnFakeLocation.Text = "Running";
                    btnFakeLocation.BackColor = System.Drawing.Color.OrangeRed;
                    await ViewChange.Instance.updateProgress(row, "Fake location", 30);
                    ADBService.FakeLocation(x, y, device);
                    await ViewChange.Instance.updateProgress(row, "Fake location", 99);
                    btnFakeLocation.Enabled = true;
                    btnFakeLocation.Text = "Fake location";
                    btnFakeLocation.BackColor = System.Drawing.Color.LightBlue;
                    await ViewChange.Instance.updateProgress(row, "Success", 100);
                    _animatingDevices.Remove(device);
                }
                _animatingDevices.Remove(device);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            await ViewChange.Instance.updateProgress(row, "", 0);
            _animatingDevices.Remove(device);

        }
        public async Task StartScreenShot(string device, System.Data.DataRow row)
        {
            try
            {
                btnScreenshot.Text = "Running";
                btnScreenshot.Enabled = false;
                btnScreenshot.BackColor = System.Drawing.Color.OrangeRed;
                await ViewChange.Instance.updateProgress(row, "Running Screen shot device", 50);
                //
                ADBService.ScreenShotDevice(device);
                //
                await ViewChange.Instance.updateProgress(row, "Success Screen shot device", 99);

                btnScreenshot.Text = "ScreenShot";
                btnScreenshot.Enabled = true;
                btnScreenshot.BackColor = System.Drawing.Color.LightBlue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            await ViewChange.Instance.updateProgress(row, "Success", 100);
            _animatingDevices.Remove(device);
        }
        private string[] loadWipeListConfig()
        {
            var defaultConfigPath = string.Format("{0}/config/wipe-packages.config", System.Windows.Forms.Application.StartupPath);
            try
            {
                LocalFileService.createFileIfNotExist(defaultConfigPath);
                return LocalFileService.readAllLinesTextFile(defaultConfigPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new string[] { };
            }
        }
        private void wipePackagesChanger(string[] packages, string deviceId)
        {
            var packageXmlPathInAndroid = "/data/system/packages.xml";
            var pathXml = Path.Combine(System.Windows.Forms.Application.StartupPath, "packages.xml");


            ADBService.pullOrPushFile(FileTransferAction.PULL, packageXmlPathInAndroid, System.Windows.Forms.Application.StartupPath, deviceId);

            foreach (string pack in packages)
            {
                ADBService.forceStopPackage(pack, deviceId);
                Thread.Sleep(1000);
                ADBService.wipePackage(pack, deviceId);
                var base64Str = RandomService.generateBase64String();
                XmlService.editPackagesInfo(pathXml, base64Str, pack);
                var source = string.Format("/data/app/$(ls /data/app | grep {0})", pack);
                var destination = string.Format("/data/app/{0}-{1}/", pack, base64Str);
                ADBService.moveFile(source, destination, deviceId);
            }
            ADBService.pullOrPushFile(FileTransferAction.PUSH, pathXml, "/data/system/", deviceId);
            File.Delete(pathXml);
        }
        public async Task updateProgress(System.Data.DataRow row, string text, int p)
        {
            int progressValue = Math.Max(0, Math.Min(100, p));

            //if (row["Progress"] != DBNull.Value && (int)row["Progress"] == 100)
            //{
            //    Console.WriteLine($"Tiến độ đã đạt 100% cho DeviceID: {row["DeviceID"]}, không cần cập nhật nữa.");
            //    return;
            //}

            row["Progress"] = progressValue;
            row["ProgressText"] = text;
            _progressTextMap[row["DeviceID"].ToString()] = $"{text} - {p}%";

            sfDataGrid.Refresh();
            Console.WriteLine($"Đã cập nhật tiến độ cho DeviceID: {row["DeviceID"]} với giá trị {p}%.");
        }
        public void LoadContextMenu()
        {
            sfDataGrid.Refresh();
        }
    }
}


