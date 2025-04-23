using AccountCreatorForm.Controls;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using WindowsFormsApp.Model;



namespace WindowsFormsApp
{
    public partial class ViewChange : Form
    {
        private HeaderViewCommon headerView;
        private SfDataGrid sfDataGrid;
        private List<DeviceDisplay> deviceDisplays = new List<DeviceDisplay>();  // Danh sách các thiết bị hiện tại

        public ViewChange()
        {
            InitializeComponent();
            setInit();
            setMenu();
            setGridView();
            LoadDevicesFromFile();
            StartDeviceCheck();
        }

        public void setInit()
        {
            mainMenu.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tableLayoutPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60)); // panelTop
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 40)); // panelBottom

            panelContextTop.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            panelContextBottom.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            change.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            info.Anchor = AnchorStyles.Left | AnchorStyles.Right;

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
            btnChangeSim.Paint += BtnCommon_Paint;
            btnOpenUrl.Paint += BtnCommon_Paint;
            btnRandomdevice.Paint += BtnCommon_Paint;
            btnRandomSim.Paint += BtnCommon_Paint;
            btnScreenshot.Paint += BtnCommon_Paint;
            sfButton12.Paint += BtnCommon_Paint;
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

        public void setGridView()
        {
            sfDataGrid = new SfDataGrid
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill,
                AllowEditing = false,
                AllowDeleting = false,
                AllowSorting = true,
                ShowGroupDropArea = false
            };

            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "STT", HeaderText = "#", Width = 30 });
            sfDataGrid.Columns.Add(new GridCheckBoxColumn { MappingName = "Checkbox", HeaderText = "Box", AllowEditing = true, Width = 60 });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "DeviceID", HeaderText = "Device ID", Width = 200 });
            sfDataGrid.Columns.Add(new GridProgressBarColumn { MappingName = "Progress", HeaderText = "Progress" });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 100 });
            GridButtonColumn activityColumn = new GridButtonColumn { MappingName = "Activity", HeaderText = "Activity", Width = 80 };
            sfDataGrid.Columns.Add(activityColumn);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("Checkbox", typeof(bool));
            dataTable.Columns.Add("DeviceID", typeof(string));
            dataTable.Columns.Add("Progress", typeof(int));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("Activity", typeof(string));

            sfDataGrid.DataSource = dataTable;
            panelContextTop.Controls.Add(sfDataGrid);

            this.sfDataGrid.RecordContextMenu = new ContextMenuStrip();
            var detailsItem = new ToolStripMenuItem("Details");
            var editItem = new ToolStripMenuItem("Edit");
            var deleteItem = new ToolStripMenuItem("Delete");

            // Attach the event handlers to the menu items
            detailsItem.Click += DetailsItem_Click;
            editItem.Click += EditItem_Click;
            deleteItem.Click += DeleteItem_Click;

            // Add items to the context menu
            this.sfDataGrid.RecordContextMenu.Items.Add(detailsItem);
            this.sfDataGrid.RecordContextMenu.Items.Add(editItem);
            this.sfDataGrid.RecordContextMenu.Items.Add(deleteItem);
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
        public (List<DeviceDisplay> onlineDevices, List<DeviceDisplay> offlineDevices) LoadDevicesFromJson()
        {
            string path = Path.Combine(Application.StartupPath, "devices.json");

            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);

                var devices = JsonConvert.DeserializeObject<List<DeviceDisplay>>(jsonContent);

                var onlineDevices = devices?.Where(d => d.Status == "Online").ToList() ?? new List<DeviceDisplay>();
                var offlineDevices = devices?.Where(d => d.Status == "Offline").ToList() ?? new List<DeviceDisplay>();

                return (onlineDevices, offlineDevices);  
            }
            else
            {
                return (new List<DeviceDisplay>(), new List<DeviceDisplay>());
            }
        }

        private async void StartDeviceCheck()
        {
            var (onlineDevices, offlineDevices) = LoadDevicesFromJson();

            var previousOnlineDevices = onlineDevices.Select(d => d.Serial).ToHashSet();
            var previousOfflineDevices = offlineDevices.Select(d => d.Serial).ToHashSet();

            await LoadUI(onlineDevices, offlineDevices);
            while (true)
            {
                var connectedDevices = GetConnectedDevices().ToHashSet();
                var newDevices = connectedDevices.Except(previousOnlineDevices).ToList();
                var disconnectedDevices = previousOnlineDevices.Except(connectedDevices).ToList();

                int countDevice = newDevices.Count();
                GlobalContextMenu.setCountDevice(countDevice);
                GlobalContextMenu.UpdateContextMenu();

                if (newDevices.Any() || disconnectedDevices.Any())
                {
                    foreach (var device in newDevices)
                    {
                        var existingDevice = deviceDisplays.FirstOrDefault(d => d.Serial == device);

                        if (existingDevice != null)
                        {
                            string currentStatus = IsDeviceOnline(device) ? "Online" : "Offline";

                            if (existingDevice.Status != currentStatus)
                            {
                                UpdateDeviceStatus(device, currentStatus);
                            }
                        }
                        else
                        {
                            if (this.IsHandleCreated)
                            {
                                Invoke((MethodInvoker)(() =>
                                {
                                    AddDeviceView(device);  
                                    UpdateDeviceStatus(device, IsDeviceOnline(device) ? "Online" : "Offline");
                                }));
                            }
                        }
                    }
                    foreach (var disconnectedDevice in disconnectedDevices)
                    {
                        if (this.IsHandleCreated)
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                UpdateDeviceStatus(disconnectedDevice, "Offline");
                            }));
                        }
                    }

                    // Cập nhật lại danh sách thiết bị đã kết nối và mất kết nối
                    previousOnlineDevices = connectedDevices;
                    previousOfflineDevices = previousOfflineDevices.Concat(disconnectedDevices).ToHashSet(); // Cập nhật offline devices
                }

                await Task.Delay(1000);  // Kiểm tra lại mỗi giây
            }
        }
        public async Task LoadUI(List<DeviceDisplay> onlineDevices, List<DeviceDisplay> offlineDevices)
        {
            foreach (var device in onlineDevices)
            {
                var existingDevice = deviceDisplays.FirstOrDefault(d => d.Serial == device.Serial);

                if (existingDevice == null)  
                {
                    await Task.Run(() =>
                    {
                        if (this.IsHandleCreated)
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                AddDeviceView(device.Serial);  
                                UpdateDeviceStatus(device.Serial, "Online");  
                            }));
                        }
                    });
                }
                else
                {
                    UpdateDeviceStatus(device.Serial, "Online");
                }
            }

            foreach (var device in offlineDevices)
            {
                var existingDevice = deviceDisplays.FirstOrDefault(d => d.Serial == device.Serial);

                if (existingDevice == null) 
                {
                    await Task.Run(() =>
                    {
                        if (this.IsHandleCreated)
                        {
                            Invoke((MethodInvoker)(() =>
                            {
                                AddDeviceView(device.Serial);  
                                UpdateDeviceStatus(device.Serial, "Offline");  
                            }));
                        }
                    });
                }
                else
                {
                    UpdateDeviceStatus(device.Serial, "Offline");
                }
            }
        }

        private void AddDeviceView(string deviceId)
        {
            DataTable dataTable = sfDataGrid.DataSource as DataTable;
            int stt = dataTable.Rows.Count + 1;

            dataTable.Rows.Add(stt, false, deviceId, 0, "Offline");

            deviceDisplays.Add(new DeviceDisplay { Serial = deviceId, Status = "Offline", Activity = "YES", Checkbox = false });
            SaveDevicesToFile();
            sfDataGrid.Refresh();
        }


        private void RemoveDeviceView(DeviceDisplay device)
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


        public void SetResertDataInputForm()
        {
            sfCbBrand.SelectedValue = "";
            sfCbOs.SelectedValue = "";
            sfCbCountry.SelectedValue = "";
            sfCbModel.SelectedValue = "";
            sfCbSim.SelectedValue = "";
            sfCbName.SelectedValue = "";

            txtCode.Text = "";
            txtICCID.Text = "";
            txtImei.Text = "";
            txtIMSI.Text = "";
            txtMac.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtSerial.Text = "";
        }
        private void UpdateDeviceStatus(string deviceId, string status)
        {
            var device = deviceDisplays.FirstOrDefault(d => d.Serial == deviceId);

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
                        row["Activity"] = "YES";
                        device.Activity = "YES";
                    }
                    else
                    {
                        row["Progress"] = 0;
                        row["Checkbox"] = false;
                        row["Activity"] = "YES";
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

            string path = Path.Combine(Application.StartupPath, "devices.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(uniqueDevices, Newtonsoft.Json.Formatting.Indented));
        }

        private void LoadDevicesFromFile()
        {
            string path = Path.Combine(Application.StartupPath, "devices.json");
            if (!File.Exists(path))
            {
                SaveDevicesToFile();
            }
            string json = File.ReadAllText(path);
            deviceDisplays = JsonConvert.DeserializeObject<List<DeviceDisplay>>(json) ?? new List<DeviceDisplay>();

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

    }
}
