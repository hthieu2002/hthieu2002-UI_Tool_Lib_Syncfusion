using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class ScriptAutomation : Form, ITextAppender
    {
        private bool isEditing = false;

        public ScriptAutomation()
        {
            InitializeComponent();
            init();
            BuildDataTableUI();
            editText();
            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            clickToolStripMenuItem_Click(clickToolStripMenuItem, EventArgs.Empty);
        }
        private void LoadContent(UserControl control)
        {
            panelContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Add(control);
        }
        private void dataChangeInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveMenu((ToolStripMenuItem)sender);
            LoadContent(new DataChangeInfoToolbox());
        }

        private void clickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveMenu((ToolStripMenuItem)sender);
            LoadContent(new ClickToolbox(this));
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveMenu((ToolStripMenuItem)sender);
            LoadContent(new TextToolbox(this));
        }

        private void keyButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveMenu((ToolStripMenuItem)sender);
            LoadContent(new KeyButtonToolbox());
        }

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveMenu((ToolStripMenuItem)sender);
            LoadContent(new GeneralToolbox());
        }
        private void sfbtnEditScript_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                sfbtnEditScript.Text = "Save script";
               // btnEditSave.Image = Properties.Resources.icon_save;
                isEditing = true;
                richTextBox1.ReadOnly = false;
            }
            else
            {
                sfbtnEditScript.Text = "Edit script";
                isEditing = false;
                richTextBox1.ReadOnly = true;
              //  SaveScriptToFile(); 
            }
        }
    }
}
