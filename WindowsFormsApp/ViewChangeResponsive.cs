using Syncfusion.WinForms.Input;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountCreatorForm.Controls;
using Syncfusion.Windows.Forms.Tools;

namespace WindowsFormsApp
{
    public partial class ViewChangeResponsive : Form
    {
        private Panel mainPanel;
        private Panel panelMenu;
        private Panel panelContent;
        private Panel panelGrid;
        private Panel panelBottom;
        private SfDataGrid sfDataGrid;
        private TextBoxExt txtDeviceID, txtBrand;
        private SfButton btnRandomDevice, btnBackup, btnOpenUrl;
        private HeaderViewCommon headerView;
        public ViewChangeResponsive()
        {
            InitializeComponent();
            SetLayout();
            SetGridView();
        }

        
        // Set Layout using standard Panel and TextBoxExt
        private void SetLayout()
        {
            // Main Panel for Layout
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(mainPanel);

            // Panel for Menu (50px)
            headerView = new HeaderViewCommon
            {
                Height = 50,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 20)
            };
            headerView.SetTitle("Devices");
            mainPanel.Controls.Add(headerView);

            // Panel for Content (Grid and Bottom part)
            panelContent = new Panel
            {
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(panelContent);

            // TableLayoutPanel for Content (split into 2 columns for DataGrid and Panels)
            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2
            };
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70));  // DataGrid
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30));  // Panels and Buttons

            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));  // Column 1 for Panels
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));  // Column 2 for Buttons

            panelContent.Controls.Add(tableLayout);

            // Panel for DataGrid (first row, full width)
            panelGrid = new Panel
            {
                Dock = DockStyle.Fill
            };
            tableLayout.Controls.Add(panelGrid, 0, 0);
            tableLayout.SetColumnSpan(panelGrid, 2);

            // Panel for Inputs and Buttons (second row, full width)
            panelBottom = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true // Enable scroll when content overflows
            };
            tableLayout.Controls.Add(panelBottom, 0, 1);

            // Add Textboxes and Buttons
            AddInputFields();
            AddButtons();
        }
        private void SetGridView()
        {
            sfDataGrid = new SfDataGrid
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill,
                AllowEditing = false,
                AllowSorting = true,
                ShowGroupDropArea = false
            };

            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "STT", HeaderText = "#", Width = 30 });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "DeviceID", HeaderText = "Device ID", Width = 200 });
            sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 100 });

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("DeviceID", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));

            sfDataGrid.DataSource = dataTable;

            panelGrid.Controls.Add(sfDataGrid);
        }
        private void AddInputFields()
        {
            // Panel for Column 1
            // Panel cho cột 1, nơi chứa các ô nhập liệu
            var panelInputColumn1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown, // Các nhóm control sẽ xếp dọc
                BorderStyle = BorderStyle.FixedSingle  // Thêm viền cho panel
            };

            // Cột 1 chứa các panel con, mỗi panel con sẽ chứa một label và một textbox

            // Panel cho "Device ID"
            var panelDeviceID = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight, // Điều này giúp label và textbox xếp ngang
                Dock = DockStyle.Top,
                AutoSize = true,
                BorderStyle = BorderStyle.FixedSingle  // Thêm viền cho panel
            };
            var labelDeviceID = new Label { Text = "Device ID", Width = 100, Dock = DockStyle.Left };
            txtDeviceID = new TextBoxExt { Dock = DockStyle.Fill };
            panelDeviceID.Controls.Add(labelDeviceID);
            panelDeviceID.Controls.Add(txtDeviceID);
            panelInputColumn1.Controls.Add(panelDeviceID);

            // Panel cho "Brand"
            var panelBrand = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight, // Điều này giúp label và textbox xếp ngang
                Dock = DockStyle.Top,
                AutoSize = true,
                BorderStyle = BorderStyle.FixedSingle  // Thêm viền cho panel
            };
            var labelBrand = new Label { Text = "Brand", Width = 100, Dock = DockStyle.Left };
            txtBrand = new TextBoxExt { Dock = DockStyle.Fill };
            panelBrand.Controls.Add(labelBrand);
            panelBrand.Controls.Add(txtBrand);
            panelInputColumn1.Controls.Add(panelBrand);

            // Add the input column (panelInputColumn1) to the bottom panel
            panelBottom.Controls.Add(panelInputColumn1);
        }

        private void AddButtons()
        {
            // Panel for Column 2
            var panelButtonColumn2 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, // Đảm bảo panel chiếm toàn bộ chiều rộng của panelBottom
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                BorderStyle = BorderStyle.FixedSingle // Thêm viền cho panel
            };

            // Buttons
            btnRandomDevice = new SfButton { Text = "Random Device", Dock = DockStyle.Fill };
            btnBackup = new SfButton { Text = "Backup", Dock = DockStyle.Fill };
            btnOpenUrl = new SfButton { Text = "Open URL", Dock = DockStyle.Fill };

            panelButtonColumn2.Controls.Add(btnRandomDevice);
            panelButtonColumn2.Controls.Add(btnBackup);
            panelButtonColumn2.Controls.Add(btnOpenUrl);

            // Add the button panel to the layout
            panelBottom.Controls.Add(panelButtonColumn2);
        }

    }
}

