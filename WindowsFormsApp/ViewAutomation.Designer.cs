using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.ListView;
using Syncfusion.WinForms.ListView.Enums;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    partial class ViewAutomation
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbLoadFile = new Syncfusion.WinForms.ListView.SfComboBox();
            this.btnLoadFile = new Syncfusion.WinForms.Controls.SfButton();
            this.nudNumber = new System.Windows.Forms.NumericUpDown();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.btnRun = new Syncfusion.WinForms.Controls.SfButton();
            this.btnScript = new Syncfusion.WinForms.Controls.SfButton();
            this.cbNomal = new Syncfusion.WinForms.ListView.SfComboBox();
            this.btnAutoRun = new Syncfusion.WinForms.Controls.SfButton();
            this.btnBackup = new Syncfusion.WinForms.Controls.SfButton();
            this.btnScreen = new Syncfusion.WinForms.Controls.SfButton();
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtRestore = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.btnRestore = new Syncfusion.WinForms.Controls.SfButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.99946F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.00054F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel2.Controls.Add(this.panelInfo, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 362);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 85);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panelInfo
            // 
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.tableLayoutPanel3);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(3, 3);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(258, 79);
            this.panelInfo.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(256, 77);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(10, 41);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(243, 33);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(10, 20);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(10, 20, 3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(243, 15);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(267, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 79);
            this.panel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(522, 77);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ViewAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ViewAutomation";
            this.Text = "ViewAutomation";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            //
            // cbLoadFile
            //
            this.cbLoadFile.Width = 250;
            this.cbLoadFile.DropDownStyle = DropDownStyle.DropDownList;
            this.cbLoadFile.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.cbLoadFile.Watermark = "Choose a file";
            this.cbLoadFile.Style.EditorStyle.WatermarkForeColor = Color.Blue;
            flowLayoutPanel2.Controls.Add(cbLoadFile);
            //
            // btnLoadFile
            //
            this.btnLoadFile.Text = "Load file";
            this.btnLoadFile.Width = 120;
            this.btnLoadFile.Height = 35;
            this.btnLoadFile.BackColor = Color.FromArgb(150, 140, 250);
            this.btnLoadFile.ForeColor = Color.White;
            this.btnLoadFile.FlatStyle = FlatStyle.Flat;

            flowLayoutPanel2.Controls.Add(btnLoadFile);
            //
            //nudNumber
            //
            this.nudNumber.Value = 1;
            this.nudNumber.Width = 60;
            this.nudNumber.Height = 30;
            this.nudNumber.Font = new Font("Segoe UI", 10);
            //
            //cbAuto
            //
            this.cbAuto.Text = "Vô hạn";
            this.cbAuto.AutoSize = true;
            this.cbAuto.Font = new Font("Segoe UI", 10);
            this.cbAuto.Height = 30;
            //
            //btnRun
            //
            this.btnRun.Text = "Run Script";
            this.btnRun.Width = 120;
            this.btnRun.Height = 35;
            this.btnRun.BackColor = Color.Teal;
            this.btnRun.ForeColor = Color.White;
            this.btnRun.TextImageRelation = TextImageRelation.ImageBeforeText;
            //
            //btnScript
            //
            this.btnScript.Text = "Code script";
            this.btnScript.Width = 120;
            this.btnScript.Height = 35;
            this.btnScript.BackColor = Color.MediumPurple;
            this.btnScript.ForeColor = Color.White;
            this.btnScript.TextImageRelation = TextImageRelation.ImageBeforeText;
            //
            //cbNomal
            //
            this.cbNomal.Width = 250;
            this.cbNomal.DropDownStyle = DropDownStyle.DropDownList;
            //
            //btnAutoRun
            //
            this.btnAutoRun.Text = "Auto run";
            this.btnAutoRun.Width = 120;
            this.btnAutoRun.Height = 35;
            this.btnAutoRun.BackColor = Color.FromArgb(150, 140, 250);
            this.btnAutoRun.ForeColor = Color.White;
            this.btnAutoRun.FlatStyle = FlatStyle.Flat;
            //
            //btnBackup
            //
            this.btnBackup.Text = "Backup";
            this.btnBackup.Width = 120;
            this.btnBackup.Height = 35;
            this.btnBackup.BackColor = Color.FromArgb(150, 140, 250);
            this.btnBackup.ForeColor = Color.White;
            this.btnBackup.FlatStyle = FlatStyle.Flat;
            //
            //btnScreen
            //
            this.btnScreen.Text = "Screenshoot";
            this.btnScreen.Width = 120;
            this.btnScreen.Height = 35;
            this.btnScreen.BackColor = Color.FromArgb(150, 140, 250);
            this.btnScreen.ForeColor = Color.White;
            this.btnScreen.FlatStyle = FlatStyle.Flat;
            //
            //panel
            //
            panel.AutoSize = true;
            //
            //txtRestore
            //
            this.txtRestore.Width = 200;
            this.txtRestore.ForeColor = Color.Black;
            this.txtRestore.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.txtRestore.Padding = new Padding(10);
            this.txtRestore.Margin = new Padding(5, 8, 10, 0);
            this.txtRestore.Height = 40;
            //
            //btnRestore
            //
            this.btnRestore.Text = "Restore";
            this.btnRestore.Width = 120;
            this.btnRestore.Height = 35;
            this.btnRestore.BackColor = Color.FromArgb(150, 140, 250);
            this.btnRestore.ForeColor = Color.White;
            this.btnRestore.FlatStyle = FlatStyle.Flat;
          
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Syncfusion.WinForms.ListView.SfComboBox cbLoadFile;
        private Syncfusion.WinForms.Controls.SfButton btnLoadFile;
        private System.Windows.Forms.NumericUpDown nudNumber;
        private System.Windows.Forms.CheckBox cbAuto;
        private Syncfusion.WinForms.Controls.SfButton btnRun;
        private Syncfusion.WinForms.Controls.SfButton btnScript;
        //
        private Syncfusion.WinForms.ListView.SfComboBox cbNomal;
        private Syncfusion.WinForms.Controls.SfButton btnAutoRun;
        private Syncfusion.WinForms.Controls.SfButton btnBackup;
        private Syncfusion.WinForms.Controls.SfButton btnScreen;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtRestore;
        private Syncfusion.WinForms.Controls.SfButton btnRestore;

        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDataGrid;
        private System.Data.DataTable _deviceTable;

        private System.Windows.Forms.FlowLayoutPanel panel;
    }
}