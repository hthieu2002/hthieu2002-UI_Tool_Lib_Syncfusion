using AccountCreatorForm.Controls;
using Syncfusion.WinForms.DataGrid;
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

namespace WindowsFormsApp
{
    public partial class ViewChange : Form
    {
        private HeaderViewCommon headerView;
        private SfDataGrid sfDataGrid;
        public ViewChange()
        {
            InitializeComponent();
            setInit();
            setMenu();
            setGridView();
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
            //

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
                // BackColor = Color.Red,
                Margin = new Padding(0, 0, 0, 20)
            };
            headerView.SetTitle("Devices");
            mainMenu.Controls.Add(headerView);
        }

        private void setGridView()
        {
            // Create and configure the SfDataGrid
            sfDataGrid = new SfDataGrid
            {
                Dock = DockStyle.Fill,  // Ensure the DataGrid takes up the full space of the form
                AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill,  // Auto-size columns to fill available space
                AllowEditing = false,  // Disable editing in the grid
                AllowDeleting = false,  // Disable row deletion
                AllowSorting = true,  // Enable sorting
                ShowGroupDropArea = false,  // Hide the grouping area
            };

            // Add columns to the grid
            sfDataGrid.Columns.Add(new GridColumn { MappingName = "STT", HeaderText = "#", Width = 50 });
            sfDataGrid.Columns.Add(new GridCheckBoxColumn { MappingName = "Checkbox", HeaderText = "Checkbox", Width = 70, AllowEditing = true });
            sfDataGrid.Columns.Add(new GridColumn { MappingName = "DeviceID", HeaderText = "Device ID", Width = 150 });
            sfDataGrid.Columns.Add(new GridProgressBarColumn { MappingName = "Progress", HeaderText = "Progress", Width = 200 });

            // Add "Activity" column as a GridButtonColumn and ensure it's the last column
            GridButtonColumn activityColumn = new GridButtonColumn { MappingName = "Activity", HeaderText = "Activity", Width = 100 };
            sfDataGrid.Columns.Add(activityColumn); // "Activity" column will automatically be the last one

            // Create DataTable to hold the dynamic data
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("STT", typeof(string));
            dataTable.Columns.Add("Checkbox", typeof(bool));
            dataTable.Columns.Add("DeviceID", typeof(string));
            dataTable.Columns.Add("Progress", typeof(int));
            dataTable.Columns.Add("Activity", typeof(string));

            // Add rows to the DataTable with values for Activity
            dataTable.Rows.Add(1, true, "520003edb295a44b", 50, "YES");
            dataTable.Rows.Add(2, false, "520015abe3f5411", 30, "NO");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");
            dataTable.Rows.Add(3, false, "520015abe3f5411", 100, "YES");

            // Assign the DataTable to the DataSource of SfDataGrid
            sfDataGrid.DataSource = dataTable;
            panelContextTop.Controls.Add(sfDataGrid);
            sfDataGrid.QueryCellStyle += (sender, e) =>
            {
                // Add any customization to the cell styles if necessary
            };
        }

        private void autoLabel9_Click(object sender, EventArgs e)
        {

        }

        private void BtnCommon_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button; // Lấy thông tin Button đang được vẽ
            if (btn == null) return;

            int radius = 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Lấy rectangle cho nút hiện tại
            Rectangle rect = new Rectangle(
                btn.ClientRectangle.X + 1,
                btn.ClientRectangle.Y + 1,
                btn.ClientRectangle.Width - 2,
                btn.ClientRectangle.Height - 2
            );

            // Cập nhật vùng cho nút
            btn.Region = new Region(GetRoundedRect(rect, radius));
            rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            // Xóa chữ cũ trước khi vẽ lại
            e.Graphics.FillRectangle(new SolidBrush(btn.BackColor), rect); // Xóa chữ và nền bằng màu nền của Button

            // Xác định màu viền dựa trên trạng thái nút
            Pen borderPen = GetButtonBorderPen(btn);

            // Vẽ viền cho nút
            e.Graphics.DrawPath(borderPen, GetRoundedRect(rect, radius));

            // Xác định màu chữ dựa trên trạng thái
            Color textColor = GetButtonTextColor(btn);

            // Vẽ văn bản của nút
            Rectangle textRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4); // Điều chỉnh phạm vi để tránh chữ bị đè lên
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // Phương thức xác định màu viền của nút
        private Pen GetButtonBorderPen(Button btn)
        {
            if (!btn.Enabled) // Disabled state
            {
                return new Pen(Color.Gray); // Màu viền cho Disabled
            }
            else if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position))) // Hover state
            {
                return new Pen(Color.Blue); // Màu viền cho Hover
            }
            else if (btn.Focused) // Focused state
            {
                return new Pen(Color.Green); // Màu viền cho Focused
            }
            else // Default state
            {
                return new Pen(Color.Gray); // Màu viền mặc định
            }
        }

        // Phương thức xác định màu chữ của nút
        private Color GetButtonTextColor(Button btn)
        {
            if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position))) // Hover state
            {
                return Color.Blue; // Màu chữ cho Hover
            }
            return btn.ForeColor; // Màu chữ mặc định
        }

        // Phương thức vẽ góc bo tròn cho nút
        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();

            // Vẽ các góc bo tròn
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

        private void btnScreenshot_Click(object sender, EventArgs e)
        {
            
        }

        private void autoLabel11_Click(object sender, EventArgs e)
        {

        }
    }
}

