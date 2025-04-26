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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataChangeInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panelContent);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(1018, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 976);
            this.panel1.TabIndex = 0;
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
            this.textToolStripMenuItem.Size = new System.Drawing.Size(50, 24);
            this.textToolStripMenuItem.Text = "Text";
            this.textToolStripMenuItem.Click += new System.EventHandler(this.textToolStripMenuItem_Click);
            // 
            // keyButtonToolStripMenuItem
            // 
            this.keyButtonToolStripMenuItem.Name = "keyButtonToolStripMenuItem";
            this.keyButtonToolStripMenuItem.Size = new System.Drawing.Size(95, 26);
            this.keyButtonToolStripMenuItem.Text = "Key button";
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
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Location = new System.Drawing.Point(-1, 31);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(574, 473);
            this.panelContent.TabIndex = 1;
            // 
            // ScriptAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1599, 983);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScriptAutomation";
            this.Text = "ScriptAutomation";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

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
    }
}