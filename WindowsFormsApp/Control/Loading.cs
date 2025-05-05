using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Loading : UserControl
    {
        private int angle = 0;
        private Timer timer;

        public Loading()
        {
            this.BackColor = Color.White;  
            this.Dock = DockStyle.Fill; 

            Label label = new Label();
            label.Text = "Đang tải...";
            label.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label.ForeColor = Color.Black;  
            label.Dock = DockStyle.Top;  
            label.TextAlign = ContentAlignment.MiddleCenter;

            this.Controls.Add(label);

            timer = new Timer();
            timer.Interval = 30; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen pen = new Pen(Color.Gray, 5))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.ResetTransform();
                e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2); 

                e.Graphics.RotateTransform(angle); 
                e.Graphics.DrawArc(pen, -30, -30, 60, 60, 0, 270); 

            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            angle += 5;  
            if (angle >= 360)
            {
                angle = 0;  
            }

            this.Invalidate();  
        }

        public void StopLoading()
        {
            timer.Stop();
        }
    }

}


