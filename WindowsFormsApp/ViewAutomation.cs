using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.ListView;
using Syncfusion.WinForms.ListView.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;

namespace WindowsFormsApp
{
    public partial class ViewAutomation: Form
    {
        public ViewAutomation()
        {
            InitializeComponent();
            setControl();
            setControlRight();
            setGridView();
        }
        private void Script_Click(object sender, EventArgs e)
        {
            ScriptAutomation script = new ScriptAutomation();
            script.Show();
        }
    }
}
