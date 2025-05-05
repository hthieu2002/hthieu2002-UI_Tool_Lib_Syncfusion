using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid;
using System.Data;

namespace WindowsFormsApp
{
	public partial class ViewAutomation : Form
	{
		private void setControl()
		{
			btnLoadFile.Paint += BtnCommon_Paint;
			btnRun.Paint += BtnCommon_Paint;
			btnScript.Click += Script_Click;
			btnScript.Paint += BtnCommon_Paint;

			flowLayoutPanel3.Controls.Add(nudNumber);
			flowLayoutPanel3.Controls.Add(cbAuto);
			flowLayoutPanel3.Controls.Add(btnRun);
			flowLayoutPanel3.Controls.Add(btnScript);

		}
		private void setControlRight()
		{
			btnAutoRun.Paint += BtnCommon_Paint;
			btnBackup.Paint += BtnCommon_Paint;
			btnScreen.Paint += BtnCommon_Paint;
			btnRestore.Paint += BtnCommon_Paint;
			panel.Controls.Add(txtRestore);
			panel.Controls.Add(btnRestore);
			flowLayoutPanel1.Controls.Add(cbNomal);
			flowLayoutPanel1.Controls.Add(btnAutoRun);
			flowLayoutPanel1.Controls.Add(btnBackup);
			flowLayoutPanel1.Controls.Add(btnScreen);
			flowLayoutPanel1.Controls.Add(panel);
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
			sfDataGrid.Columns.Add(new GridCheckBoxColumn { MappingName = "Checkbox", HeaderText = "Box", Width = 60, AllowEditing = true });
			sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "DeviceID", HeaderText = "Device ID", Width = 200 });

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

			sfDataGrid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 100 });
			sfDataGrid.Columns.Add(new GridButtonColumn { MappingName = "Activity", HeaderText = "Active", Width = 80 });

			_deviceTable = new DataTable();
			_deviceTable.Columns.Add("STT", typeof(int));
			_deviceTable.Columns.Add("Checkbox", typeof(bool));
			_deviceTable.Columns.Add("DeviceID", typeof(string));
			_deviceTable.Columns.Add("Progress", typeof(int));   
			_deviceTable.Columns.Add("Status", typeof(string));
			_deviceTable.Columns.Add("Activity", typeof(string));

			sfDataGrid.DataSource = _deviceTable;
			tableLayoutPanel1.Controls.Add(sfDataGrid, 0, 0);

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
