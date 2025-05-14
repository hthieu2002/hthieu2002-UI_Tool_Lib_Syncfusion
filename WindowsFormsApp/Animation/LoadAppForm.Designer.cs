using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp.Animation
{
    partial class LoadAppForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sfComboBox1 = new Syncfusion.WinForms.ListView.SfComboBox();
            this.btnLoadDevice = new Syncfusion.WinForms.Controls.SfButton();
            this.btnLoadAllApp = new Syncfusion.WinForms.Controls.SfButton();
            this.btnLoadAppInstaller = new Syncfusion.WinForms.Controls.SfButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sfComboBox1
            // 
            // Kích thước và vị trí
            this.sfComboBox1.Location = new Point(12, 12);
            this.sfComboBox1.Size = new Size(220, 40);
            this.sfComboBox1.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.sfComboBox1.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.sfComboBox1.Name = "sfComboBox1";
            this.sfComboBox1.TabIndex = 0;
            this.sfComboBox1.TabStop = false;

            // Font chữ hiện đại
            this.sfComboBox1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // Token style (nếu dùng chế độ MultiSelect)
            this.sfComboBox1.Style.TokenStyle.BackColor = Color.AliceBlue;
            this.sfComboBox1.Style.TokenStyle.BorderColor = Color.LightSteelBlue;
            this.sfComboBox1.Style.TokenStyle.ForeColor = Color.DarkSlateGray;
            this.sfComboBox1.Style.TokenStyle.Font = new Font("Segoe UI Semibold", 9F);
            this.sfComboBox1.Style.TokenStyle.CloseButtonBackColor = Color.Transparent;

            // 
            // btnLoadDevice
            // 
            this.btnLoadDevice.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLoadDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadDevice.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLoadDevice.ForeColor = System.Drawing.Color.White;
            this.btnLoadDevice.Location = new System.Drawing.Point(240, 12);
            this.btnLoadDevice.Name = "btnLoadDevice";
            this.btnLoadDevice.Size = new System.Drawing.Size(146, 41);
            this.btnLoadDevice.Style.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLoadDevice.Style.ForeColor = System.Drawing.Color.White;
            this.btnLoadDevice.TabIndex = 1;
            this.btnLoadDevice.Text = "Load Device";
            this.btnLoadDevice.UseVisualStyleBackColor = false;
            this.btnLoadDevice.Click += new System.EventHandler(this.btnLoadDevice_Click);
            // 
            // btnLoadAllApp
            // 
            this.btnLoadAllApp.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLoadAllApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadAllApp.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLoadAllApp.ForeColor = System.Drawing.Color.White;
            this.btnLoadAllApp.Location = new System.Drawing.Point(416, 12);
            this.btnLoadAllApp.Name = "btnLoadAllApp";
            this.btnLoadAllApp.Size = new System.Drawing.Size(146, 41);
            this.btnLoadAllApp.Style.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLoadAllApp.Style.ForeColor = System.Drawing.Color.White;
            this.btnLoadAllApp.TabIndex = 2;
            this.btnLoadAllApp.Text = "Load all app";
            this.btnLoadAllApp.UseVisualStyleBackColor = false;
            this.btnLoadAllApp.Click += new System.EventHandler(this.btnLoadAllApp_Click);
            // 
            // btnLoadAppInstaller
            // 
            this.btnLoadAppInstaller.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLoadAppInstaller.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadAppInstaller.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLoadAppInstaller.ForeColor = System.Drawing.Color.White;
            this.btnLoadAppInstaller.Location = new System.Drawing.Point(584, 12);
            this.btnLoadAppInstaller.Name = "btnLoadAppInstaller";
            this.btnLoadAppInstaller.Size = new System.Drawing.Size(204, 41);
            this.btnLoadAppInstaller.Style.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnLoadAppInstaller.Style.ForeColor = System.Drawing.Color.White;
            this.btnLoadAppInstaller.TabIndex = 3;
            this.btnLoadAppInstaller.Text = "Load app installer";
            this.btnLoadAppInstaller.UseVisualStyleBackColor = false;
            this.btnLoadAppInstaller.Click += new System.EventHandler(this.btnLoadAppInstaller_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(776, 364);
            this.dataGridView1.TabIndex = 4;

            this.dataGridView1.ColumnHeadersHeight = 30; // Chiều cao tiêu đề
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(106, 90, 205); // Màu tím đậm
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Chữ trắng
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold); // Font tiêu đề
            this.dataGridView1.EnableHeadersVisualStyles = false; // Tắt style mặc định

            // Vô hiệu hóa kéo giãn cột và hàng
            this.dataGridView1.AllowUserToResizeColumns = false; // Không cho kéo giãn chiều rộng cột
            this.dataGridView1.AllowUserToResizeRows = false; // Không cho kéo giãn chiều cao hàng
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho kéo giãn chiều cao tiêu đề

            // Vô hiệu hóa chỉnh sửa giá trị
            this.dataGridView1.ReadOnly = true; // Chỉ cho phép đọc, không cho chỉnh sửa

            this.dataGridView1.DefaultCellStyle.BackColor = Color.White; // Nền ô trắng
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black; // Chữ đen
            this.dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9f); // Font ô
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGray; // Màu chọn
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Dòng kẻ
            this.dataGridView1.GridColor = Color.FromArgb(200, 200, 200); // Màu xám nhạt
            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single; 

            // Giao diện tổng thể
            this.dataGridView1.BackgroundColor = Color.White; // Nền trắng
            this.dataGridView1.BorderStyle = BorderStyle.None; // Bỏ viền ngoài
            this.dataGridView1.RowHeadersVisible = false; // Ẩn cột tiêu đề dòng
            this.dataGridView1.AllowUserToAddRows = false; // Không cho thêm dòng mới
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            // 
            // LoadAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnLoadAppInstaller);
            this.Controls.Add(this.btnLoadAllApp);
            this.Controls.Add(this.btnLoadDevice);
            this.Controls.Add(this.sfComboBox1);
            this.Name = "LoadAppForm";
            this.Text = "Load App";
            ((System.ComponentModel.ISupportInitialize)(this.sfComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.ListView.SfComboBox sfComboBox1;
        private Syncfusion.WinForms.Controls.SfButton btnLoadDevice;
        private Syncfusion.WinForms.Controls.SfButton btnLoadAllApp;
        private Syncfusion.WinForms.Controls.SfButton btnLoadAppInstaller;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}