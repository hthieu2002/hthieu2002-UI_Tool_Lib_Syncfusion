using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WindowsFormsApp
{
    partial class ScreenView
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.leftBorder = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label = new System.Windows.Forms.Label();
            this.trackBarPanel = new System.Windows.Forms.Panel();
            this.valueLabel = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarPanel2 = new System.Windows.Forms.Panel();
            this.valueLabel2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.cbTurnOffScreen = new System.Windows.Forms.CheckBox();
            this.selectedDevicesLabel = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.btnCloseAll = new System.Windows.Forms.Button();
            this.btnRefreshDevice = new System.Windows.Forms.Button();
            this.btnPushFile = new System.Windows.Forms.Button();
            this.btnInstallAPK = new System.Windows.Forms.Button();
            this.buttonGroup = new System.Windows.Forms.FlowLayoutPanel();
            this.btnView = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.trackBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.trackBarPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.buttonGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.flowLayoutPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.leftBorder);
            this.splitContainer.Panel2.Controls.Add(this.rightPanel);
            this.splitContainer.Size = new System.Drawing.Size(800, 450);
            this.splitContainer.SplitterDistance = 266;
            this.splitContainer.TabIndex = 0;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(266, 450);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // leftBorder
            // 
            this.leftBorder.BackColor = System.Drawing.Color.DarkGray;
            this.leftBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftBorder.Location = new System.Drawing.Point(0, 0);
            this.leftBorder.Name = "leftBorder";
            this.leftBorder.Size = new System.Drawing.Size(1, 450);
            this.leftBorder.TabIndex = 0;
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.SystemColors.Control;
            this.rightPanel.Controls.Add(this.label);
            this.rightPanel.Controls.Add(this.trackBarPanel);
            this.rightPanel.Controls.Add(this.label2);
            this.rightPanel.Controls.Add(this.trackBarPanel2);
            this.rightPanel.Controls.Add(this.cbTurnOffScreen);
            this.rightPanel.Controls.Add(this.selectedDevicesLabel);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(530, 450);
            this.rightPanel.TabIndex = 1;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(20, 10);
            this.label.Margin = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 16);
            this.label.TabIndex = 0;
            // 
            // trackBarPanel
            // 
            this.trackBarPanel.Controls.Add(this.valueLabel);
            this.trackBarPanel.Controls.Add(this.trackBar);
            this.trackBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBarPanel.Location = new System.Drawing.Point(23, 3);
            this.trackBarPanel.Name = "trackBarPanel";
            this.trackBarPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.trackBarPanel.Size = new System.Drawing.Size(250, 40);
            this.trackBarPanel.TabIndex = 1;
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(200, 10);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(0, 16);
            this.valueLabel.TabIndex = 0;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(20, 5);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(180, 56);
            this.trackBar.TabIndex = 1;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(20, 10, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 2;
            // 
            // trackBarPanel2
            // 
            this.trackBarPanel2.Controls.Add(this.valueLabel2);
            this.trackBarPanel2.Controls.Add(this.trackBar2);
            this.trackBarPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBarPanel2.Location = new System.Drawing.Point(3, 49);
            this.trackBarPanel2.Name = "trackBarPanel2";
            this.trackBarPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.trackBarPanel2.Size = new System.Drawing.Size(250, 40);
            this.trackBarPanel2.TabIndex = 3;
            // 
            // valueLabel2
            // 
            this.valueLabel2.AutoSize = true;
            this.valueLabel2.Location = new System.Drawing.Point(200, 10);
            this.valueLabel2.Name = "valueLabel2";
            this.valueLabel2.Size = new System.Drawing.Size(0, 16);
            this.valueLabel2.TabIndex = 0;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(20, 5);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(180, 56);
            this.trackBar2.TabIndex = 1;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // cbTurnOffScreen
            // 
            this.cbTurnOffScreen.AutoSize = true;
            this.cbTurnOffScreen.Checked = true;
            this.cbTurnOffScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTurnOffScreen.Location = new System.Drawing.Point(276, 56);
            this.cbTurnOffScreen.Margin = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.cbTurnOffScreen.Name = "cbTurnOffScreen";
            this.cbTurnOffScreen.Size = new System.Drawing.Size(153, 20);
            this.cbTurnOffScreen.TabIndex = 4;
            this.cbTurnOffScreen.Text = "Tắt màn hình khi xem";
            // 
            // selectedDevicesLabel
            // 
            this.selectedDevicesLabel.AutoSize = true;
            this.selectedDevicesLabel.Location = new System.Drawing.Point(459, 56);
            this.selectedDevicesLabel.Margin = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.selectedDevicesLabel.Name = "selectedDevicesLabel";
            this.selectedDevicesLabel.Size = new System.Drawing.Size(61, 16);
            this.selectedDevicesLabel.TabIndex = 5;
            this.selectedDevicesLabel.Text = "Thiết bị 0";
            // 
            // bottomPanel
            // 
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(10);
            this.bottomPanel.Size = new System.Drawing.Size(300, 100);
            this.bottomPanel.TabIndex = 0;
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.AutoSize = true;
            this.btnCloseAll.Location = new System.Drawing.Point(90, 5);
            this.btnCloseAll.Margin = new System.Windows.Forms.Padding(5);
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(75, 26);
            this.btnCloseAll.TabIndex = 1;
            this.btnCloseAll.Text = "Close All";
            // 
            // btnRefreshDevice
            // 
            this.btnRefreshDevice.AutoSize = true;
            this.btnRefreshDevice.Location = new System.Drawing.Point(5, 41);
            this.btnRefreshDevice.Margin = new System.Windows.Forms.Padding(5);
            this.btnRefreshDevice.Name = "btnRefreshDevice";
            this.btnRefreshDevice.Size = new System.Drawing.Size(75, 26);
            this.btnRefreshDevice.TabIndex = 2;
            this.btnRefreshDevice.Text = "Refresh";
            // 
            // btnPushFile
            // 
            this.btnPushFile.AutoSize = true;
            this.btnPushFile.Location = new System.Drawing.Point(90, 41);
            this.btnPushFile.Margin = new System.Windows.Forms.Padding(5);
            this.btnPushFile.Name = "btnPushFile";
            this.btnPushFile.Size = new System.Drawing.Size(75, 26);
            this.btnPushFile.TabIndex = 3;
            this.btnPushFile.Text = "Push File";
            // 
            // btnInstallAPK
            // 
            this.btnInstallAPK.AutoSize = true;
            this.btnInstallAPK.Location = new System.Drawing.Point(5, 77);
            this.btnInstallAPK.Margin = new System.Windows.Forms.Padding(5);
            this.btnInstallAPK.Name = "btnInstallAPK";
            this.btnInstallAPK.Size = new System.Drawing.Size(80, 26);
            this.btnInstallAPK.TabIndex = 4;
            this.btnInstallAPK.Text = "Install APK";
            // 
            // buttonGroup
            // 
            this.buttonGroup.AutoSize = true;
            this.buttonGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonGroup.Controls.Add(this.btnView);
            this.buttonGroup.Controls.Add(this.btnCloseAll);
            this.buttonGroup.Controls.Add(this.btnRefreshDevice);
            this.buttonGroup.Controls.Add(this.btnPushFile);
            this.buttonGroup.Controls.Add(this.btnInstallAPK);
            this.buttonGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGroup.Location = new System.Drawing.Point(0, 0);
            this.buttonGroup.Name = "buttonGroup";
            this.buttonGroup.Size = new System.Drawing.Size(200, 100);
            this.buttonGroup.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.AutoSize = true;
            this.btnView.Location = new System.Drawing.Point(5, 5);
            this.btnView.Margin = new System.Windows.Forms.Padding(5);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 26);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "View";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(200, 100);
            this.buttonPanel.TabIndex = 0;
            // 
            // ScreenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Name = "ScreenView";
            this.Text = "ScreenView";
            this.Load += new System.EventHandler(this.ScreenView_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.trackBarPanel.ResumeLayout(false);
            this.trackBarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.trackBarPanel2.ResumeLayout(false);
            this.trackBarPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.buttonGroup.ResumeLayout(false);
            this.buttonGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel rightPanel;
        private System.Windows.Forms.CheckBox cbTurnOffScreen;
        private System.Windows.Forms.Label selectedDevicesLabel;
        private Panel leftBorder;
        private Panel bottomPanel;
        private Button btnCloseAll;
        private Button btnRefreshDevice;
        private Button btnPushFile;
        private Button btnInstallAPK;
        private FlowLayoutPanel buttonGroup;
        private System.Windows.Forms.Button btnView;
        private Label label;
        private Label label2;
        private Panel trackBarPanel;
        private Panel trackBarPanel2;
        private TrackBar trackBar;
        private TrackBar trackBar2;
        private Label valueLabel;
        private Label valueLabel2;
        private FlowLayoutPanel buttonPanel;
        //private Label deviceLabel;
        private ToolTip toolTip;
      //  private Panel devicePanel;
    }
}