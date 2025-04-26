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
    public partial class ScriptAutomation: Form
    {
        public ScriptAutomation()
        {
            InitializeComponent();
            //var screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            //var screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            //this.Width = (int)(screenWidth * 0.9);
            //this.Height = (int)(screenHeight * 0.9);

            //this.StartPosition = FormStartPosition.CenterScreen;

            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
        }
        private void LoadContent(UserControl control)
        {
            panelContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Add(control);
        }
        private void dataChangeInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent(new ClickToolbox());
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent(new TextToolbox());
        }
    }
}
