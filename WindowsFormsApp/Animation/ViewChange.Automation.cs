using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Styles;
using Services;
using WindowsFormsApp.Script;

namespace WindowsFormsApp
{
    public partial class ViewChange : Form
    {
        private Model.WindowMode _previousWindowMode = Model.WindowMode.Normal;
        private void ApplyPanelInputMargin()
        {
            var current = Model.AppState.CurrentWindowMode;
            var previous = _previousWindowMode;
            if (current == previous)
                return;
            bool isTransitionBetweenNormalAndMaximized =
                (previous == Model.WindowMode.Normal && current == Model.WindowMode.Maximized) ||
                (previous == Model.WindowMode.Maximized && current == Model.WindowMode.Normal);
            if (!isTransitionBetweenNormalAndMaximized)
            {
                _previousWindowMode = current;
                return;
            }
            tableLayoutPanel1.ColumnStyles.Clear();
            if (current == Model.WindowMode.Maximized)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            }
            else if (current == Model.WindowMode.Normal)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            }
            _previousWindowMode = current;
        }
        private void SetInputAddLayoutInput()
        {
            // Tạo các điều khiển input
            txtBrand = new TextBoxExt {  Width = 130, Height = 35 };
            txtOS = new TextBoxExt {  Width = 130, Height = 35 };
            txtOS_version = new TextBoxExt {  Width = 60, Height = 35 };
            txtSerial = new TextBoxExt { Width = 130, Height = 35 };
            txtImei = new TextBoxExt { Width = 130, Height = 35 };
            txtMac = new TextBoxExt { Width = 130, Height = 35 };
            txtName = new TextBoxExt {  Width = 130, Height = 35 };
            txtCountry = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 130, Height = 35 };
            txtCode = new TextBoxExt { Width = 130, Height = 35 };
            txtImsi = new TextBoxExt { Width = 130, Height = 35 };
            txtModel = new TextBoxExt { Width = 130, Height = 35 };
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
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 5, 0),
                Height = 35,
                Width = 100,
                AutoSize = false,
            };
            osPanel.Controls.Add(osLabel);
            osPanel.Controls.Add(txtOS);
            //osPanel.Controls.Add(txtOS_version);


            // Thêm các FlowLayoutPanel vào PanelInput
            PanelInput.Controls.Add(CreateInputPanel("BRAND", txtBrand));
            PanelInput.Controls.Add(CreateInputPanel("NAME", txtName));
            PanelInput.Controls.Add(CreateInputPanel("MODEL", txtModel));
            PanelInput.Controls.Add(osPanel);
            PanelInput.Controls.Add(CreateInputPanel("COUNTRY", txtCountry));
            txtCountry.SelectedIndexChanged += txtCountry_SelectedIndexChanged;
            PanelInput.Controls.Add(CreateInputPanel("SIM", txtSim));
            PanelInput.Controls.Add(CreateInputPanel("SERIAL", txtSerial));
            PanelInput.Controls.Add(CreateInputPanel("CODE", txtCode));
            PanelInput.Controls.Add(CreateInputPanel("PHONE", txtPhone));
            PanelInput.Controls.Add(CreateInputPanel("IMEI", txtImei));
            PanelInput.Controls.Add(CreateInputPanel("IMSI", txtImsi));
            PanelInput.Controls.Add(CreateInputPanel("ICCID", txtIccId));
            PanelInput.Controls.Add(CreateInputPanel("MAC", txtMac));

            checkSiml = new CheckBox();
            checkSiml.Text = "Change sim";
            checkSiml.Checked = false;
            checkSiml.Font = new Font(checkSiml.Font, FontStyle.Bold);
            checkSiml.Width = 200;
            checkSiml.Margin = new Padding(20,10,5,0);

            PanelInput.Controls.Add(checkSiml);

            this.checkSiml.CheckedChanged += checkSiml_CheckedChanged;
        }
        private void checkSiml_CheckedChanged(object sender, EventArgs e)
        {
            Util.checkSim = checkSiml.Checked;
        }
        private FlowLayoutPanel CreateInputPanel(string labelText, Control inputControl)
        {
            panel = new FlowLayoutPanel
            {
                //  Dock = DockStyle.Top,
                FlowDirection = FlowDirection.LeftToRight,
                Height = 35,
                Width = 250,
            };

            // Tạo AutoLabel với tên label
            label = new AutoLabel
            {
                Text = labelText,
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 5, 0),
                Height = 35,
                Width = 100,
            };

            panel.Controls.Add(label);
            panel.Controls.Add(inputControl);

            return panel;
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
        private void SetupButtonCancelStyle(SfButton button)
        {

            button.Cursor = Cursors.Hand;

            button.MouseEnter += (s, e) =>
            {
                button.Style.BackColor = Color.OrangeRed;
                button.Style.ForeColor = Color.White;
            };

            button.MouseLeave += (s, e) =>
            {
                button.Style.BackColor = Color.LightBlue;
                button.Style.ForeColor = Color.White;
            };
        }
        private void SetButtonAddLayoutButton()
        {
            SfButton[] buttons = {
            btnRandomdevice = new SfButton { Text = "Random Device" },
          //  btnAutoBackup = new SfButton { Text = "Auto Backup" },
            btnAutochangeFull = new SfButton { Text = "Auto Change Full" },
            btnAutoChangeSim = new SfButton { Text = "Auto Change Sim" },
          //  btnBackup = new SfButton { Text = "Backup" },
          //  btnBackup2 = new SfButton { Text = "Backup " },
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
      //  btnBackup,
      //  btnBackup2,
        btnOpenUrl,
       // btnAutoBackup,
        btnScreenshot

        );
           

            PanelButton.Controls.Add(btnFakeLocation);
            PanelButton.Controls.Add(txtRestore);
            PanelButton.Controls.Add(btnRestore);
            setupDisableButtonChange();
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

        public void setInit()
        {
            PanelInput.BorderStyle = BorderStyle.FixedSingle;
            PanelButton.BorderStyle = BorderStyle.FixedSingle;

            btnRandomdevice.TextAlign = ContentAlignment.MiddleLeft;
          //  btnAutoBackup.TextAlign = ContentAlignment.MiddleLeft;
            btnAutochangeFull.TextAlign = ContentAlignment.MiddleLeft;
            btnAutoChangeSim.TextAlign = ContentAlignment.MiddleLeft;
         //   btnBackup.TextAlign = ContentAlignment.MiddleLeft;
         //   btnBackup2.TextAlign = ContentAlignment.MiddleLeft;
            btnChangeDevice.TextAlign = ContentAlignment.MiddleLeft;
            btnChangeSim.TextAlign = ContentAlignment.MiddleLeft;
            btnOpenUrl.TextAlign = ContentAlignment.MiddleLeft;
            btnRandomSim.TextAlign = ContentAlignment.MiddleLeft;
            btnScreenshot.TextAlign = ContentAlignment.MiddleLeft;

          //  btnAutoBackup.Paint += BtnCommon_Paint;
            //
            // btn auto change full
            //
            btnAutochangeFull.Paint += BtnCommon_Paint;
            btnAutochangeFull.Click += btnChangeFull_Click;
            //
            // btn auto change sim
            //
            btnAutoChangeSim.Paint += BtnCommon_Paint;
            btnAutoChangeSim.Click += btnChangeSimAll_Click;
            //
            // btn backup  
            //
         //   btnBackup.Paint += BtnCommon_Paint;
            //
            // btn backup 2
            //
        //    btnBackup2.Paint += BtnCommon_Paint;
            //
            // btn change device 
            //
            btnChangeDevice.Paint += BtnCommon_Paint;
            btnChangeDevice.Click += btnChange_Click;
            //
            // btn change sim
            //
            btnChangeSim.Paint += BtnCommon_Paint;
            btnChangeSim.Click += btnChangeSim_Click;

            btnOpenUrl.Paint += BtnCommon_Paint;
            //
            // btn random device
            //
            btnRandomdevice.Paint += BtnCommon_Paint;
            btnRandomdevice.Click += btnRandom_Click;
            //
            // btn random sim
            // 
            btnRandomSim.Paint += BtnCommon_Paint;
            btnRandomSim.Click += BtnRandomSim_Click;
            // 
            // btn screen shot
            //
            btnScreenshot.Paint += BtnCommon_Paint;
            btnScreenshot.Click += btnScreenShot_Click;

            btnRestore.Paint += BtnCommon_Paint;
            //
            // btn fake location
            //
            btnFakeLocation.Paint += BtnCommon_Paint;
            btnFakeLocation.Click += btnFakeLocation_Click;
        }

        public void setupDisableButtonChange()
        {
            btnRandomdevice.Enabled = false;
            btnRandomSim.Enabled = false;
            btnChangeDevice.Enabled = false;
            btnChangeSim.Enabled = false;
            btnAutoChangeSim.Enabled = false;
            btnAutochangeFull.Enabled = false;

            btnRandomdevice.BackColor = Color.DarkGray;
            btnRandomSim.BackColor = Color.DarkGray;
            btnChangeDevice.BackColor = Color.DarkGray;
            btnChangeSim.BackColor = Color.DarkGray;
            btnAutochangeFull.BackColor = Color.DarkGray;
            btnAutoChangeSim.BackColor = Color.DarkGray;
        }
        public void setupEnableButtonRandom()
        {
            btnRandomdevice.Enabled = true;
            btnRandomSim.Enabled = true;
            btnAutoChangeSim.Enabled = true;
            btnAutochangeFull.Enabled = true;

            btnRandomdevice.BackColor = Color.LightBlue;
            btnRandomSim.BackColor = Color.LightBlue;

            btnAutochangeFull.BackColor = Color.LightBlue;
            btnAutoChangeSim.BackColor = Color.LightBlue;
        }
        public void setupDisableButtonRandom()
        {
            btnRandomdevice.Enabled = false;
            btnRandomdevice.BackColor = Color.DarkGray;
        }
        public void setupDisableButtonRandomSim()
        {
            btnRandomSim.Enabled = false;
            btnRandomSim.BackColor = Color.DarkGray;
        }
        public void setupEnableButtonChangeSim()
        {
            btnChangeSim.Enabled = true;
            btnAutoChangeSim.Enabled = true;
            btnRandomSim.Enabled = true;
            btnRandomSim.BackColor = Color.LightBlue;
            btnChangeSim.BackColor = Color.LightBlue;
            btnAutoChangeSim.BackColor = Color.LightBlue;
        }
        public void setupEnableButtonChangeDevice()
        {
            btnChangeDevice.Enabled = true;
            btnAutochangeFull.Enabled = true;
            btnRandomdevice.Enabled = true;
            btnRandomdevice.BackColor = Color.LightBlue;
            btnChangeDevice.BackColor = Color.LightBlue;
            btnAutochangeFull.BackColor = Color.LightBlue;
        }
        public void setGridView()
        {
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
            sfDataGrid.Columns.Add(new GridCheckBoxColumn { MappingName = "Checkbox", HeaderText = "Box", Width = 80, AllowEditing = true });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "NameID", HeaderText = "Name", Width = 120 });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "DeviceID", HeaderText = "Device ID", Width = 200 });

            var progressCol = new GridProgressBarColumn
            {
                MappingName = "Progress",
                HeaderText = "%",
                Width = 150,
                Minimum = 0,
                Maximum = 100,
                ValueMode = ProgressBarValueMode.Percentage
            };
            progressCol.CellStyle.TextColor = Color.White;
            progressCol.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
            sfDataGrid.Columns.Add(progressCol);

            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "ProgressText", HeaderText = "Progress" });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 100 });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "Activity", HeaderText = "Active", Width = 80 });

            _deviceTable = new DataTable();
            _deviceTable.Columns.Add("STT", typeof(int));
            _deviceTable.Columns.Add("Checkbox", typeof(bool));
            _deviceTable.Columns.Add("NameID", typeof(string));
            _deviceTable.Columns.Add("DeviceID", typeof(string));
            _deviceTable.Columns.Add("Progress", typeof(int));
            _deviceTable.Columns.Add("ProgressText", typeof(string));
            _deviceTable.Columns.Add("Status", typeof(string));
            _deviceTable.Columns.Add("Activity", typeof(string));

            this.sfDataGrid.Style.HeaderStyle.Borders.All = new GridBorder(GridBorderStyle.Dotted, Color.Blue, GridBorderWeight.Thin);
            this.sfDataGrid.Style.CellStyle.Borders.All = new GridBorder(GridBorderStyle.Dotted, Color.Blue, GridBorderWeight.Thin);
            (this.sfDataGrid.Columns["Checkbox"] as GridCheckBoxColumn).AllowCheckBoxOnHeader = true;
            sfDataGrid.QueryCellStyle += sfDataGrid_QueryCellStyle;

            sfDataGrid.DataSource = _deviceTable;
            //    this.sfDataGrid.ShowBusyIndicator = true;
            tableLayoutPanel.Controls.Add(sfDataGrid, 0, 0);
            /*
             * menu context data table 
             */
            this.sfDataGrid.RecordContextMenu = new ContextMenuStrip();
            var copyDeviceID = new ToolStripMenuItem("Copy Device ID");
            var detailsItem = new ToolStripMenuItem("Details");
            var editItem = new ToolStripMenuItem("Edit");
            var fakeRedsocks = new ToolStripMenuItem("Fake Proxy");
            var deleteItem = new ToolStripMenuItem("Delete");
            copyDeviceID.Click += CopyDeviceID_Click;
            detailsItem.Click += DetailsItem_Click;
            editItem.Click += EditItem_Click;
            fakeRedsocks.Click += FakeRedsocks_Click;
            deleteItem.Click += DeleteItem_Click;
            this.sfDataGrid.RecordContextMenu.Items.AddRange(
                new ToolStripItem[] { copyDeviceID, detailsItem, editItem, fakeRedsocks, deleteItem }
            );
        }
        private void sfDataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.Column.MappingName == "Activity")
            {
                if (e.DisplayText == "NO")
                {
                    // e.Style.BackColor = Color.Coral;
                    e.Style.TextColor = Color.Red;
                    //   this.e.Style.ButtonStyle.TextColor = Color.DarkBlue;

              //      this.sfDataGrid.Style.ButtonStyle.BackColor = Color.LightPink;
                   // e.Style.ButtonStyle.TextColor = Color.Red;
                }
                else if (e.DisplayText == "YES")
                {
                    //  e.Style.BackColor = Color.LightSkyBlue;
                     e.Style.TextColor = Color.DarkSlateBlue;
                   // this.sfDataGrid.Style.ButtonStyle.TextColor = Color.Blue;
                }
            }
            if (e.Column.MappingName == "Status")
            {
                if(e.DisplayText == "Online")
                {
                    e.Style.TextColor = Color.Blue;
                }
                else
                {
                    e.Style.TextColor = Color.Red;
                }
            }
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
    }
}
