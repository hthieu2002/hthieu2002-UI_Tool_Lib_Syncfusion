using Services;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.Animation
{
    public partial class LoadAppForm : Form
    {
        public LoadAppForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimumSize = new Size(820, 500);
            styleHoverButton();
            UpdateComboboxDevices();
        }
        private void styleHoverButton()
        {
            btnLoadDevice.MouseEnter += (s, e) => btnLoadDevice.BackColor = Color.SteelBlue;
            btnLoadDevice.MouseLeave += (s, e) => btnLoadDevice.BackColor = Color.MediumSlateBlue;

            btnLoadAllApp.MouseEnter += (s, e) => btnLoadAllApp.BackColor = Color.SteelBlue;
            btnLoadAllApp.MouseLeave += (s, e) => btnLoadAllApp.BackColor = Color.MediumSlateBlue;

            btnLoadAppInstaller.MouseEnter += (s, e) => btnLoadAppInstaller.BackColor = Color.SteelBlue;
            btnLoadAppInstaller.MouseLeave += (s, e) => btnLoadAppInstaller.BackColor = Color.MediumSlateBlue;

            dataGridView1.DataBindingComplete += (s, e) =>
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (dataGridView1.Columns.Contains("No"))
                {
                    dataGridView1.Columns["No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns["No"].Width = 50;
                    dataGridView1.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dataGridView1.Columns.Contains("Name"))
                {
                    dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            };

            dataGridView1.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    var sb = new System.Text.StringBuilder();

                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (dataGridView1.Columns[cell.ColumnIndex].Name == "Name")
                        {
                            sb.AppendLine(cell.Value?.ToString());
                        }
                    }

                    if (sb.Length > 0)
                    {
                        Clipboard.SetText(sb.ToString());
                        e.Handled = true;
                    }
                }
            };
        }

        private void btnLoadDevice_Click(object sender, EventArgs e)
        {
            UpdateComboboxDevices();
        }
        private void UpdateComboboxDevices()
        {
            sfComboBox1.Text = "";
            sfComboBox1.DataSource = null;

            var getDevices = ADBService.GetConnectedDevices();
            if (getDevices != null && getDevices.Length > 0)
            {
                sfComboBox1.DataSource = getDevices;
                sfComboBox1.SelectedIndex = 0;
                sfComboBox1.Refresh();  
            } else
            {
                sfComboBox1.DataSource = null;
                sfComboBox1.Text = "";  
                sfComboBox1.Refresh();
            }
        }

        private void btnLoadAllApp_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfComboBox1.Text == "")
                {
                    return;
                }

                var getDeviceSelected = sfComboBox1.SelectedItem.ToString();
                if (sfComboBox1.SelectedItem == null || string.IsNullOrWhiteSpace(getDeviceSelected))
                {
                    MessageBox.Show("Vui lòng chọn thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var getPackagesAllApps = ADBService.ExecuteADBCommandDetail(getDeviceSelected, "shell pm list packages");
                    if (!string.IsNullOrEmpty(getPackagesAllApps) && getPackagesAllApps.Length > 0)
                    {
                        var listPkg = getPackagesAllApps.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                                     .Where(line => !line.StartsWith("List") && !line.StartsWith("---------"))
                                     .Select(line => line.Split('\t')[0].Replace("package:", ""))
                                     .OrderBy(x => x.ToLower())
                                     .ToArray();

                        DataTable table = new DataTable();
                        table.Columns.Add("No", typeof(int));
                        table.Columns.Add("Name", typeof(string));

                        int index = 1;
                        foreach (var pkg in listPkg)
                        {
                            table.Rows.Add(index++, pkg);
                        }

                        dataGridView1.DataSource = table;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        UpdateComboboxDevices();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnLoadAppInstaller_Click(object sender, EventArgs e)
        {
            if (sfComboBox1.Text == "")
            {
                return;
            }
            var getDeviceSelected = sfComboBox1.SelectedItem.ToString();

            if (sfComboBox1.SelectedItem == null || string.IsNullOrWhiteSpace(getDeviceSelected))
            {
                MessageBox.Show("Vui lòng chọn thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else {

                var getPackagesUserApps = ADBService.ExecuteADBCommandDetail(getDeviceSelected, "shell pm list packages -3");
                if (!string.IsNullOrEmpty(getPackagesUserApps) && getPackagesUserApps.Length > 0)
                {
                    var listPkg = getPackagesUserApps.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                                 .Where(line => !line.StartsWith("List") && !line.StartsWith("---------"))
                                 .Select(line => line.Split('\t')[0].Replace("package:",""))
                                 .OrderBy(x => x.ToLower())
                                 .ToArray();

                    DataTable table = new DataTable();
                    table.Columns.Add("No", typeof(int));
                    table.Columns.Add("Name", typeof(string));

                    int index = 1;
                    foreach (var pkg in listPkg)
                    {
                        table.Rows.Add(index++, pkg);
                    }

                    dataGridView1.DataSource = table;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    UpdateComboboxDevices();
                }
            }
        }
    }
}
