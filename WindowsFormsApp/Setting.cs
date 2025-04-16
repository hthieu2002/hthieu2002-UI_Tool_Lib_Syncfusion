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
    public partial class Setting : Form
    {

        public Setting()
        {
            InitializeComponent();
            panel2.Visible = true;
           
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;

            this.PerformLayout();
            this.Refresh();
        }

        private void sfButton2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            this.Height = this.Height - panel2.Height;
            this.PerformLayout(); 
            this.Refresh();
        }

        private void sfButton3_Click(object sender, EventArgs e)
        {
          
        }
    }
}

