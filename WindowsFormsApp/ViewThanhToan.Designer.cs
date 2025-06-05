namespace AccountCreatorForm.Views
{
    partial class ViewThanhToan
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
            this.panelHeader = new System.Windows.Forms.Panel();
          //  this.lblBangGia = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.flowLayoutPanelCards = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1030, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // lblBangGia
            // 
            //this.lblBangGia.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.lblBangGia.Location = new System.Drawing.Point(487, 103);
            //this.lblBangGia.Name = "lblBangGia";
            //this.lblBangGia.Size = new System.Drawing.Size(108, 31);
            //this.lblBangGia.TabIndex = 1;
          //  this.lblBangGia.Text = "Bảng giá";
            // 
            // flowLayoutPanelCards
            // 
            this.flowLayoutPanelCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelCards.Location = new System.Drawing.Point(0, 80);
            this.flowLayoutPanelCards.Name = "flowLayoutPanelCards";
            this.flowLayoutPanelCards.Size = new System.Drawing.Size(1030, 546);
            this.flowLayoutPanelCards.TabIndex = 2;
            // 
            // ViewThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 626);
            //this.Controls.Add(this.flowLayoutPanelCards);
           // this.Controls.Add(this.lblBangGia);
           // this.Controls.Add(this.panelHeader);
            this.Name = "ViewThanhToan";
            this.Text = "ViewThanhToan";
            this.Load += new System.EventHandler(this.ViewThanhToan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
       // private Syncfusion.Windows.Forms.Tools.AutoLabel lblBangGia;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCards;
    }
}