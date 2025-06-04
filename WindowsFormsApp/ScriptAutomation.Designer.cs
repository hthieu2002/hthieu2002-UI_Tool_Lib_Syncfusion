namespace WindowsFormsApp
{
    partial class ScriptAutomation
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.y = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.x = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.lbLog = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.datagrid = new System.Windows.Forms.Panel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.sfbtnCapture = new Syncfusion.WinForms.Controls.SfButton();
            this.sfbtnLoadDevice = new Syncfusion.WinForms.Controls.SfButton();
            this.sfCbModelDump = new Syncfusion.WinForms.ListView.SfComboBox();
            this.sfCbLoadDevices = new Syncfusion.WinForms.ListView.SfComboBox();
            this.panelContent = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataChangeInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sfbtnEditScript = new Syncfusion.WinForms.Controls.SfButton();
            this.sfbtnSend = new Syncfusion.WinForms.Controls.SfButton();
            this.sfbtnTest = new Syncfusion.WinForms.Controls.SfButton();
            this.txtTest = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panelTest = new System.Windows.Forms.Panel();
            this.pictureBoxScreen = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbNew = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelete = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCreate = new Syncfusion.WinForms.Controls.SfButton();
            this.btnLoadFile = new Syncfusion.WinForms.Controls.SfButton();
            this.sfCbFile = new Syncfusion.WinForms.ListView.SfComboBox();
            this.sfView = new Syncfusion.WinForms.Controls.SfButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfCbModelDump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sfCbLoadDevices)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTest)).BeginInit();
            this.panelTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfCbFile)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.y);
            this.panel1.Controls.Add(this.x);
            this.panel1.Controls.Add(this.lbLog);
            this.panel1.Controls.Add(this.datagrid);
            this.panel1.Controls.Add(this.autoLabel1);
            this.panel1.Controls.Add(this.sfbtnCapture);
            this.panel1.Controls.Add(this.sfbtnLoadDevice);
            this.panel1.Controls.Add(this.sfCbModelDump);
            this.panel1.Controls.Add(this.sfCbLoadDevices);
            this.panel1.Controls.Add(this.panelContent);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(1018, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 929);
            this.panel1.TabIndex = 0;
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(58, 525);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(35, 16);
            this.y.TabIndex = 10;
            this.y.Text = "999 ]";
            // 
            // x
            // 
            this.x.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.x.Location = new System.Drawing.Point(15, 525);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(42, 16);
            this.x.TabIndex = 9;
            this.x.Text = "[ 9999";
            // 
            // lbLog
            // 
            this.lbLog.ForeColor = System.Drawing.Color.Red;
            this.lbLog.Location = new System.Drawing.Point(341, 573);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(26, 16);
            this.lbLog.TabIndex = 8;
            this.lbLog.Text = "log";
            // 
            // datagrid
            // 
            this.datagrid.Location = new System.Drawing.Point(3, 607);
            this.datagrid.Name = "datagrid";
            this.datagrid.Size = new System.Drawing.Size(565, 364);
            this.datagrid.TabIndex = 7;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Location = new System.Drawing.Point(24, 573);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(82, 16);
            this.autoLabel1.TabIndex = 6;
            this.autoLabel1.Text = "Model dump";
            // 
            // sfbtnCapture
            // 
            this.sfbtnCapture.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfbtnCapture.Location = new System.Drawing.Point(457, 513);
            this.sfbtnCapture.Name = "sfbtnCapture";
            this.sfbtnCapture.Size = new System.Drawing.Size(111, 28);
            this.sfbtnCapture.Style.Image = global::WindowsFormsApp.Properties.Resources.capture;
            this.sfbtnCapture.TabIndex = 5;
            this.sfbtnCapture.Text = "Capture";
            this.sfbtnCapture.Click += new System.EventHandler(this.sfbtnCapture_Click);
            // 
            // sfbtnLoadDevice
            // 
            this.sfbtnLoadDevice.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfbtnLoadDevice.Location = new System.Drawing.Point(331, 513);
            this.sfbtnLoadDevice.Name = "sfbtnLoadDevice";
            this.sfbtnLoadDevice.Size = new System.Drawing.Size(110, 28);
            this.sfbtnLoadDevice.TabIndex = 4;
            this.sfbtnLoadDevice.Text = "Load devices";
            this.sfbtnLoadDevice.Click += new System.EventHandler(this.sfbtnLoadDevice_Click);
            // 
            // sfCbModelDump
            // 
            this.sfCbModelDump.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.sfCbModelDump.Location = new System.Drawing.Point(128, 558);
            this.sfCbModelDump.Name = "sfCbModelDump";
            this.sfCbModelDump.Size = new System.Drawing.Size(197, 31);
            this.sfCbModelDump.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sfCbModelDump.TabIndex = 3;
            this.sfCbModelDump.TabStop = false;
            // 
            // sfCbLoadDevices
            // 
            this.sfCbLoadDevices.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.sfCbLoadDevices.Location = new System.Drawing.Point(128, 510);
            this.sfCbLoadDevices.Name = "sfCbLoadDevices";
            this.sfCbLoadDevices.Size = new System.Drawing.Size(197, 31);
            this.sfCbLoadDevices.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sfCbLoadDevices.TabIndex = 2;
            this.sfCbLoadDevices.TabStop = false;
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Location = new System.Drawing.Point(3, 31);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(570, 473);
            this.panelContent.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clickToolStripMenuItem,
            this.textToolStripMenuItem,
            this.keyButtonToolStripMenuItem,
            this.dataChangeInfoToolStripMenuItem,
            this.generalToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(576, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clickToolStripMenuItem
            // 
            this.clickToolStripMenuItem.Name = "clickToolStripMenuItem";
            this.clickToolStripMenuItem.Size = new System.Drawing.Size(54, 26);
            this.clickToolStripMenuItem.Text = "Click";
            this.clickToolStripMenuItem.Click += new System.EventHandler(this.clickToolStripMenuItem_Click);
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(50, 26);
            this.textToolStripMenuItem.Text = "Text";
            this.textToolStripMenuItem.Click += new System.EventHandler(this.textToolStripMenuItem_Click);
            // 
            // keyButtonToolStripMenuItem
            // 
            this.keyButtonToolStripMenuItem.Name = "keyButtonToolStripMenuItem";
            this.keyButtonToolStripMenuItem.Size = new System.Drawing.Size(95, 26);
            this.keyButtonToolStripMenuItem.Text = "Key button";
            this.keyButtonToolStripMenuItem.Click += new System.EventHandler(this.keyButtonToolStripMenuItem_Click);
            // 
            // dataChangeInfoToolStripMenuItem
            // 
            this.dataChangeInfoToolStripMenuItem.Name = "dataChangeInfoToolStripMenuItem";
            this.dataChangeInfoToolStripMenuItem.Size = new System.Drawing.Size(141, 26);
            this.dataChangeInfoToolStripMenuItem.Text = "Data/Change info";
            this.dataChangeInfoToolStripMenuItem.Click += new System.EventHandler(this.dataChangeInfoToolStripMenuItem_Click);
            // 
            // generalToolStripMenuItem
            // 
            this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            this.generalToolStripMenuItem.Size = new System.Drawing.Size(74, 26);
            this.generalToolStripMenuItem.Text = "General";
            this.generalToolStripMenuItem.Click += new System.EventHandler(this.generalToolStripMenuItem_Click);
            // 
            // sfbtnEditScript
            // 
            this.sfbtnEditScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sfbtnEditScript.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfbtnEditScript.Location = new System.Drawing.Point(596, 12);
            this.sfbtnEditScript.Name = "sfbtnEditScript";
            this.sfbtnEditScript.Size = new System.Drawing.Size(104, 44);
            this.sfbtnEditScript.Style.Image = global::WindowsFormsApp.Properties.Resources.fileEdit;
            this.sfbtnEditScript.TabIndex = 1;
            this.sfbtnEditScript.Text = "Edit script";
            this.sfbtnEditScript.Click += new System.EventHandler(this.sfbtnEditScript_Click);
            // 
            // sfbtnSend
            // 
            this.sfbtnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sfbtnSend.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfbtnSend.Location = new System.Drawing.Point(900, 12);
            this.sfbtnSend.Name = "sfbtnSend";
            this.sfbtnSend.Size = new System.Drawing.Size(101, 44);
            this.sfbtnSend.TabIndex = 3;
            this.sfbtnSend.Text = "Send";
            this.sfbtnSend.Click += new System.EventHandler(this.sfbtnSend_Click);
            // 
            // sfbtnTest
            // 
            this.sfbtnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sfbtnTest.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfbtnTest.Location = new System.Drawing.Point(900, 62);
            this.sfbtnTest.Name = "sfbtnTest";
            this.sfbtnTest.Size = new System.Drawing.Size(101, 28);
            this.sfbtnTest.TabIndex = 4;
            this.sfbtnTest.Text = "Test";
            this.sfbtnTest.Click += new System.EventHandler(this.sfbtnTest_Click);
            // 
            // txtTest
            // 
            this.txtTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTest.BeforeTouchSize = new System.Drawing.Size(298, 27);
            this.txtTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTest.Location = new System.Drawing.Point(596, 63);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(298, 27);
            this.txtTest.TabIndex = 5;
            // 
            // panelTest
            // 
            this.panelTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTest.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelTest.Controls.Add(this.pictureBoxScreen);
            this.panelTest.Location = new System.Drawing.Point(596, 111);
            this.panelTest.Name = "panelTest";
            this.panelTest.Size = new System.Drawing.Size(416, 822);
            this.panelTest.TabIndex = 6;
            // 
            // pictureBoxScreen
            // 
            this.pictureBoxScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxScreen.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxScreen.Name = "pictureBoxScreen";
            this.pictureBoxScreen.Size = new System.Drawing.Size(410, 815);
            this.pictureBoxScreen.TabIndex = 0;
            this.pictureBoxScreen.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(587, 928);
            this.panel2.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.richTextBox1);
            this.panel4.Location = new System.Drawing.Point(3, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(579, 825);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lbNew);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(40, 825);
            this.panel5.TabIndex = 1;
            // 
            // lbNew
            // 
            this.lbNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNew.Location = new System.Drawing.Point(24, 0);
            this.lbNew.Name = "lbNew";
            this.lbNew.Size = new System.Drawing.Size(16, 20);
            this.lbNew.TabIndex = 0;
            this.lbNew.Text = "*";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(579, 825);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnCreate);
            this.panel3.Controls.Add(this.btnLoadFile);
            this.panel3.Controls.Add(this.sfCbFile);
            this.panel3.Location = new System.Drawing.Point(17, 860);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(565, 39);
            this.panel3.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnDelete.Location = new System.Drawing.Point(464, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 28);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCreate.Location = new System.Drawing.Point(362, 6);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(96, 28);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnLoadFile.Location = new System.Drawing.Point(260, 6);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(96, 28);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "Load file";
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // sfCbFile
            // 
            this.sfCbFile.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.sfCbFile.Location = new System.Drawing.Point(3, 3);
            this.sfCbFile.Name = "sfCbFile";
            this.sfCbFile.Size = new System.Drawing.Size(251, 31);
            this.sfCbFile.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sfCbFile.TabIndex = 0;
            this.sfCbFile.TabStop = false;
            // 
            // sfView
            // 
            this.sfView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sfView.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfView.Location = new System.Drawing.Point(706, 12);
            this.sfView.Name = "sfView";
            this.sfView.Size = new System.Drawing.Size(188, 44);
            this.sfView.TabIndex = 11;
            this.sfView.Text = "View device";
            this.sfView.Click += new System.EventHandler(this.sfView_Click);
            // 
            // ScriptAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1599, 936);
            this.Controls.Add(this.sfView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelTest);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.sfbtnTest);
            this.Controls.Add(this.sfbtnSend);
            this.Controls.Add(this.sfbtnEditScript);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScriptAutomation";
            this.Text = "ScriptAutomation";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfCbModelDump)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sfCbLoadDevices)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTest)).EndInit();
            this.panelTest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfCbFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataChangeInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalToolStripMenuItem;
        private System.Windows.Forms.Panel panelContent;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.WinForms.Controls.SfButton sfbtnCapture;
        private Syncfusion.WinForms.Controls.SfButton sfbtnLoadDevice;
        private Syncfusion.WinForms.ListView.SfComboBox sfCbModelDump;
        private Syncfusion.WinForms.ListView.SfComboBox sfCbLoadDevices;
        private System.Windows.Forms.Panel datagrid;
        private Syncfusion.WinForms.Controls.SfButton sfbtnEditScript;
        private Syncfusion.WinForms.Controls.SfButton sfbtnSend;
        private Syncfusion.WinForms.Controls.SfButton sfbtnTest;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtTest;
        private System.Windows.Forms.Panel panelTest;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.WinForms.Controls.SfButton btnDelete;
        private Syncfusion.WinForms.Controls.SfButton btnCreate;
        private Syncfusion.WinForms.Controls.SfButton btnLoadFile;
        private Syncfusion.WinForms.ListView.SfComboBox sfCbFile;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbLog;
        private System.Windows.Forms.PictureBox pictureBoxScreen;
        private Syncfusion.Windows.Forms.Tools.AutoLabel y;
        private Syncfusion.Windows.Forms.Tools.AutoLabel x;
        private Syncfusion.WinForms.Controls.SfButton sfView;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbNew;
    }
}