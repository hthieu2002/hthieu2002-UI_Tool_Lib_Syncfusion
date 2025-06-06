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
            this.cbLoadFile = new Syncfusion.WinForms.ListView.SfComboBox();
            this.btnLoadFile = new Syncfusion.WinForms.Controls.SfButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.nudNumber = new System.Windows.Forms.NumericUpDown();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.btnRun = new Syncfusion.WinForms.Controls.SfButton();
            this.btnStopRun = new Syncfusion.WinForms.Controls.SfButton();
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
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLoadFile)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNomal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRestore)).BeginInit();
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
            this.flowLayoutPanel2.Controls.Add(this.cbLoadFile);
            this.flowLayoutPanel2.Controls.Add(this.btnLoadFile);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(10, 20);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(10, 20, 3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(243, 15);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // cbLoadFile
            // 
            this.cbLoadFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbLoadFile.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.cbLoadFile.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.cbLoadFile.Location = new System.Drawing.Point(3, 3);
            this.cbLoadFile.Name = "cbLoadFile";
            this.cbLoadFile.Size = new System.Drawing.Size(250, 31);
            this.cbLoadFile.Style.EditorStyle.WatermarkForeColor = System.Drawing.Color.Blue;
            this.cbLoadFile.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbLoadFile.TabIndex = 0;
            this.cbLoadFile.Watermark = "Choose a file";
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnLoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLoadFile.ForeColor = System.Drawing.Color.White;
            this.btnLoadFile.Location = new System.Drawing.Point(3, 40);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(120, 35);
            this.btnLoadFile.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnLoadFile.Style.ForeColor = System.Drawing.Color.White;
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "Load file";
            this.btnLoadFile.UseVisualStyleBackColor = false;
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
            // nudNumber
            // 
            this.nudNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudNumber.Location = new System.Drawing.Point(0, 0);
            this.nudNumber.Name = "nudNumber";
            this.nudNumber.Size = new System.Drawing.Size(60, 30);
            this.nudNumber.TabIndex = 0;
            this.nudNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbAuto
            // 
            this.cbAuto.AutoSize = true;
            this.cbAuto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbAuto.Location = new System.Drawing.Point(0, 0);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(104, 30);
            this.cbAuto.TabIndex = 0;
            this.cbAuto.Text = "Vô hạn";
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.Teal;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(0, 0);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(120, 35);
            this.btnRun.Style.BackColor = System.Drawing.Color.Teal;
            this.btnRun.Style.ForeColor = System.Drawing.Color.White;
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run Script";
            this.btnRun.UseVisualStyleBackColor = false;
            // 
            // btnStopRun
            // 
            this.btnStopRun.BackColor = System.Drawing.Color.OrangeRed;
            this.btnStopRun.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnStopRun.ForeColor = System.Drawing.Color.White;
            this.btnStopRun.Location = new System.Drawing.Point(0, 0);
            this.btnStopRun.Name = "btnStopRun";
            this.btnStopRun.Size = new System.Drawing.Size(120, 35);
            this.btnStopRun.Style.BackColor = System.Drawing.Color.OrangeRed;
            this.btnStopRun.Style.ForeColor = System.Drawing.Color.White;
            this.btnStopRun.TabIndex = 0;
            this.btnStopRun.Text = "Stop script";
            this.btnStopRun.UseVisualStyleBackColor = false;
            this.btnStopRun.Visible = false;
            // 
            // btnScript
            // 
            this.btnScript.BackColor = System.Drawing.Color.MediumPurple;
            this.btnScript.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnScript.ForeColor = System.Drawing.Color.White;
            this.btnScript.Location = new System.Drawing.Point(0, 0);
            this.btnScript.Name = "btnScript";
            this.btnScript.Size = new System.Drawing.Size(120, 35);
            this.btnScript.Style.BackColor = System.Drawing.Color.MediumPurple;
            this.btnScript.Style.ForeColor = System.Drawing.Color.White;
            this.btnScript.TabIndex = 0;
            this.btnScript.Text = "Code script";
            this.btnScript.UseVisualStyleBackColor = false;
            // 
            // cbNomal
            // 
            this.cbNomal.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.cbNomal.DropDownStyle = Syncfusion.WinForms.ListView.Enums.DropDownStyle.DropDownList;
            this.cbNomal.Location = new System.Drawing.Point(0, 0);
            this.cbNomal.Name = "cbNomal";
            this.cbNomal.Size = new System.Drawing.Size(250, 31);
            this.cbNomal.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbNomal.TabIndex = 0;
            // 
            // btnAutoRun
            // 
            this.btnAutoRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnAutoRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoRun.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAutoRun.ForeColor = System.Drawing.Color.White;
            this.btnAutoRun.Location = new System.Drawing.Point(0, 0);
            this.btnAutoRun.Name = "btnAutoRun";
            this.btnAutoRun.Size = new System.Drawing.Size(120, 35);
            this.btnAutoRun.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnAutoRun.Style.ForeColor = System.Drawing.Color.White;
            this.btnAutoRun.TabIndex = 0;
            this.btnAutoRun.Text = "Load devices";
            this.btnAutoRun.UseVisualStyleBackColor = false;
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnBackup.ForeColor = System.Drawing.Color.White;
            this.btnBackup.Location = new System.Drawing.Point(0, 0);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(120, 35);
            this.btnBackup.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnBackup.Style.ForeColor = System.Drawing.Color.White;
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = false;
            // 
            // btnScreen
            // 
            this.btnScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScreen.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnScreen.ForeColor = System.Drawing.Color.White;
            this.btnScreen.Location = new System.Drawing.Point(0, 0);
            this.btnScreen.Name = "btnScreen";
            this.btnScreen.Size = new System.Drawing.Size(120, 35);
            this.btnScreen.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnScreen.Style.ForeColor = System.Drawing.Color.White;
            this.btnScreen.TabIndex = 0;
            this.btnScreen.Text = "Screenshoot";
            this.btnScreen.UseVisualStyleBackColor = false;
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 100);
            this.panel.TabIndex = 0;
            // 
            // txtRestore
            // 
            this.txtRestore.BeforeTouchSize = new System.Drawing.Size(200, 30);
            this.txtRestore.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRestore.ForeColor = System.Drawing.Color.Black;
            this.txtRestore.Location = new System.Drawing.Point(0, 0);
            this.txtRestore.Margin = new System.Windows.Forms.Padding(5, 8, 10, 0);
            this.txtRestore.Name = "txtRestore";
            this.txtRestore.Size = new System.Drawing.Size(200, 30);
            this.txtRestore.TabIndex = 0;
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(0, 0);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(120, 35);
            this.btnRestore.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(140)))), ((int)(((byte)(250)))));
            this.btnRestore.Style.ForeColor = System.Drawing.Color.White;
            this.btnRestore.TabIndex = 0;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = false;
            // 
            // ViewAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ViewAutomation";
            this.Text = "ViewAutomation";
            this.Load += new System.EventHandler(this.ViewAutomation_Load);
            this.VisibleChanged += new System.EventHandler(this.ViewAutoamtion_VisibleChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbLoadFile)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNomal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRestore)).EndInit();
            this.ResumeLayout(false);

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
        private Syncfusion.WinForms.Controls.SfButton btnStopRun;
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