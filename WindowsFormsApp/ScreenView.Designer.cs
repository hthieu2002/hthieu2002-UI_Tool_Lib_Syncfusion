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
            this.SuspendLayout();
            // 
            // ScreenView
            // 
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.rightPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cbTurnOffScreen = new System.Windows.Forms.CheckBox();
         //   this.selectedDevicesLabel = new System.Windows.Forms.Label();
            this.leftBorder = new Panel();
            this.bottomPanel = new Panel();
            this.btnCloseAll = new Button();
            this.btnRefreshDevice = new Button();
            this.btnPushFile = new Button();
            this.btnInstallAPK = new Button();
            this.buttonGroup = new FlowLayoutPanel();
            this.btnView = new Button();
            this.label = new Label();
            this.label2 = new Label();
            this.trackBarPanel = new Panel();
            this.trackBarPanel2 = new Panel();
            this.trackBar = new TrackBar();
            this.trackBar2 = new TrackBar();
            this.valueLabel = new Label();
            this.valueLabel2 = new Label();
            this.buttonPanel = new FlowLayoutPanel();
            this.deviceLabel = new Label();
            this.toolTip = new ToolTip();
            this.devicePanel = new Panel();
            //
            // splitContainer
            //
            this.splitContainer.Dock = DockStyle.Fill;
            this.splitContainer.Orientation = Orientation.Vertical;
            this.splitContainer.IsSplitterFixed = true; 
            //
            // flowlayoutPanel
            //
            this.flowLayoutPanel.Dock = DockStyle.Fill;
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.WrapContents = true;
            this.flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            this.splitContainer.Panel1.Controls.Add(flowLayoutPanel);
            //
            // rightPanel
            //
            this.rightPanel.Dock = DockStyle.Fill;
            this.rightPanel.BackColor = SystemColors.Control;
            //
            //cbTurnOffScreen
            //
            this.cbTurnOffScreen.Text = "Tắt màn hình khi xem";
            this.cbTurnOffScreen.AutoSize = true;
            this.cbTurnOffScreen.Checked = true;
            this.cbTurnOffScreen.Margin = new Padding(20, 10, 10, 10);
            this.cbTurnOffScreen.CheckedChanged += CbTurnOffScreen_CheckedChanged;
           
            //
            //selectedDevicesLabel
            //
            this.selectedDevicesLabel = new Label();
            this.selectedDevicesLabel.AutoSize = true;
            this.selectedDevicesLabel.Margin = new Padding(20,10,10,10);
            this.selectedDevicesLabel.Text = "Thiết bị 0";
            //
            //leftBorder
            //
            this.leftBorder.Width = 1;
            this.leftBorder.Dock = DockStyle.Left; // Gắn sát trái
            this.leftBorder.BackColor = Color.DarkGray;
            //
            //bottomPanel
            //
            this.bottomPanel.Dock = DockStyle.Bottom;
            this.bottomPanel.Height = 100;
            this.bottomPanel.Padding = new Padding(10);
            this.bottomPanel.Width = 300;
            //
            //btnCloseAll
            //
            this.btnCloseAll.Text = "Close All";
            this.btnCloseAll.AutoSize = true;
            this.btnCloseAll.Margin = new Padding(5);
            //
            //btnRefreshDevice
            //
            this.btnRefreshDevice.Text = "Refresh";
            this.btnRefreshDevice.AutoSize = true;
            this.btnRefreshDevice.Margin = new Padding(5);
            //
            //btnPushFile
            //
            this.btnPushFile.Text = "Push File";
            this.btnPushFile.AutoSize = true;
            this.btnPushFile.Margin = new Padding(5);
            //
            //btnInstallAPK
            //
            this.btnInstallAPK.Text = "Install APK";
            this.btnInstallAPK.AutoSize = true;
            this.btnInstallAPK.Margin = new Padding(5);
            //
            //buttonGroup
            //
            this.buttonGroup.Dock = DockStyle.Fill;
            this.buttonGroup.FlowDirection = FlowDirection.LeftToRight;
            this.buttonGroup.WrapContents = true; // Cho phép xuống dòng
            this.buttonGroup.AutoSize = true;
            this.buttonGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.buttonGroup.Controls.Add(btnView);
            this.buttonGroup.Controls.Add(btnCloseAll);
            this.buttonGroup.Controls.Add(btnRefreshDevice);
            this.buttonGroup.Controls.Add(btnPushFile);
            this.buttonGroup.Controls.Add(btnInstallAPK);
            //
            //btnView
            //
            this.btnView.Text = "View";
            this.btnView.AutoSize = true;
            this.btnView.Margin = new Padding(5);
            //
            //label
            //
            this.label.Text = "";
            this.label.AutoSize = true;
            this.label.Margin = new Padding(20, 10, 0, 0);
            //
            //label2
            //
            this.label2.Text = "";
            this.label2.AutoSize = true;
            this.label2.Margin = new Padding(20, 10, 0, 0);
            //
            //trackBarPanel
            //
            this.trackBarPanel.Height = 40;
            this.trackBarPanel.Dock = DockStyle.Top;
            this.trackBarPanel.Padding = new Padding(0, 0, 0, 10);
            this.trackBarPanel.Width = 250;
            //
            //trackBarPanel2
            //
            this.trackBarPanel2.Height = 40;
            this.trackBarPanel2.Dock = DockStyle.Top;
            this.trackBarPanel2.Padding = new Padding(0, 0, 0, 10);
            this.trackBarPanel2.Width = 250;
            //
            //trackBar
            //
            this.trackBar.TickStyle = TickStyle.None;
            this.trackBar.Width = 180;
            this.trackBar.Left = 20;
            this.trackBar.Top = 5;
            //
            //trackBar2
            //
            this.trackBar2.TickStyle = TickStyle.None;
            this.trackBar2.Width = 180;
            this.trackBar2.Left = 20;
            this.trackBar2.Top = 5;

            //
            //valueLabel
            //
            this.valueLabel.Text = "";
            this.valueLabel.Left = trackBar.Right + 10;
            this.valueLabel.Top = 10;
            this.valueLabel.AutoSize = true;
            //
            //valueLabel2
            //
            this.valueLabel2.Text = "";
            this.valueLabel2.Left = trackBar2.Right + 10;
            this.valueLabel2.Top = 10;
            this.valueLabel2.AutoSize = true;
            //
            //deviceLabel
            //
            this.deviceLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.deviceLabel.BackColor = ColorTranslator.FromHtml("#5677FE");
            this.deviceLabel.Dock = DockStyle.Bottom;
            this.deviceLabel.ForeColor = Color.White;
            //
            //devicePanel
            //
            this.devicePanel.Margin = new Padding(10);
            this.devicePanel.BackColor = System.Drawing.Color.LightGray;

            this.splitContainer.Panel2.Controls.Add(leftBorder);
            this.splitContainer.Panel2.Controls.Add(rightPanel);

            this.rightPanel.Controls.Add(label);
            this.trackBarPanel.Controls.Add(valueLabel);
            this.trackBarPanel.Controls.Add(trackBar);
            this.rightPanel.Controls.Add(trackBarPanel);

            this.rightPanel.Controls.Add(label2);
            this.trackBarPanel2.Controls.Add(valueLabel2);
            this.trackBarPanel2.Controls.Add(trackBar2);
            this.rightPanel.Controls.Add(trackBarPanel2);

            this.rightPanel.Controls.Add(cbTurnOffScreen);
            this.rightPanel.Controls.Add(selectedDevicesLabel);

            this.Controls.Add(splitContainer);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ScreenView";
            this.Text = "ScreenView";
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
        private Label deviceLabel;
        private ToolTip toolTip;
        private Panel devicePanel;
    }
}