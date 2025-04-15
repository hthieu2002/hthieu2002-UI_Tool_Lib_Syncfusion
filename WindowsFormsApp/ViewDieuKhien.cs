using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AccountCreatorForm.Views
{
    public partial class ViewDieuKhien : Form
    {
        public ViewDieuKhien()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
           // panel1.Height = 100;
            init();
        }
        public void init()
        {
            DataTable dataTable = new DataTable();

            // Thêm các cột vào DataTable
            dataTable.Columns.Add("Index", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Serial", typeof(string));
            dataTable.Columns.Add("Active", typeof(string));
            dataTable.Columns.Add("HTTP Proxy", typeof(string));
            dataTable.Columns.Add("Stats", typeof(string));
            dataTable.Columns.Add("Today", typeof(string));
            dataTable.Columns.Add("Progress", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("Auto", typeof(string));

            // Thêm dữ liệu mẫu vào DataTable
            dataTable.Rows.Add(1, "G7H1V310EM", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(2, "66FSPCNT20", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(3, "IC63QUJY3Y", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(4, "98AY15771", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(5, "98HAY14YQ2", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(6, "1X86YIF8ZJ", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(7, "8Z5G54PT1", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(8, "Z9BYBBNNL3", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(9, "8Q1EL05192", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            dataTable.Rows.Add(10, "93DAYS0016M", "OK", "OK", "N/A", "N/A", "0/0", "Ready...", "Offline", "START");
            // Gán DataTable vào SfDataGrid
            dataTable1.DataSource = dataTable;
            // Panel chứa các điều khiển phía trên (ví dụ: tiêu đề hoặc các điều khiển khác)
            panel1.Dock = DockStyle.Top;
            panel1.Height = 100;  // Kích thước cố định cho panel

            dataTable1.Dock = DockStyle.Fill;
            dataTable1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sfButton2.Anchor = AnchorStyles.Right;
            sfButton3.Anchor =  AnchorStyles.Right;
            sfButton4.Anchor =  AnchorStyles.Right;
            sfButton5.Anchor =  AnchorStyles.Right;
            sfButton6.Anchor =  AnchorStyles.Right;



            dataTable1.AllowResizingColumns = true;
            dataTable1.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
        }
        private void sfButton2_Click(object sender, EventArgs e)
        {

        }
    }
}