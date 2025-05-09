using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace WindowsFormsApp
{
    public partial class ScriptAutomation : Form, ITextAppender
    {
        private bool isEditing = false;
        List<string> dataFileScript;
        public ScriptAutomation()
        {
            InitializeComponent();
            init();
            BuildDataTableUI();
            editText();
            this.Load += Form1_Load;
            sfCbFile.SelectedIndexChanged += sfCbFile_SelectedIndexChanged;

            if (string.IsNullOrEmpty(sfCbFile.Text))
            {
                sfbtnEditScript.Enabled = false;
            }
            else
            {
                sfbtnEditScript.Enabled = true;
            }
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

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            dataFileScript = new List<string>();
            dataFileScript.Clear();

            string scriptFolderPath = Path.Combine(Application.StartupPath, "Resources", "script");
            if (!Directory.Exists(scriptFolderPath))
            {
                Directory.CreateDirectory(scriptFolderPath);
            }
            string[] txtFiles = Directory.GetFiles(scriptFolderPath, "*.txt");

            if (txtFiles.Length == 0)
            {
                MessageBox.Show("Không có file .txt nào trong thư mục 'script'.", "Thông báo");
                sfCbFile.DataSource = null;
                sfCbFile.Text = "";
                return;
            }

            foreach (string filePath in txtFiles)
            {
                string fileName = Path.GetFileName(filePath);
                dataFileScript.Add(fileName);
                // cbLoadFile.CheckedItems.Add(fileName);
            }
            sfCbFile.DataSource = dataFileScript;

            sfCbFile.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            string fileName = $"run_{currentDate}.txt";
            string filePath = Path.Combine("Resources", "script", fileName);

            if (!string.IsNullOrEmpty(sfCbFile.Text))
            {
                string fileNameFromComboBox = sfCbFile.Text.EndsWith(".txt")
                    ? sfCbFile.Text.Substring(0, sfCbFile.Text.Length - 4) 
                    : sfCbFile.Text;

                fileNameFromComboBox = $"{fileNameFromComboBox}.txt"; 

                string filePathFromComboBox = Path.Combine("Resources", "script", fileNameFromComboBox);

                if (System.IO.File.Exists(filePathFromComboBox))
                {
                    MessageBox.Show("File đã tồn tại trong thư mục Resources/script.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                try
                {
                    using (StreamWriter sw = System.IO.File.CreateText(filePathFromComboBox))
                    {
                       // sw.WriteLine(sfCbFile.Text);
                    }
                    MessageBox.Show($"Tạo file mới với nội dung từ ComboBox: {fileNameFromComboBox}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tạo file với nội dung: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string fileNameWithTime = $"script_{currentDate}_{DateTime.Now.ToString("HHmmss")}.txt"; // Thêm thời gian vào tên file
                string filePathWithTime = Path.Combine("Resources", "script", fileNameWithTime);

                if (System.IO.File.Exists(filePathWithTime))
                {
                    MessageBox.Show("File đã tồn tại trong thư mục Resources/script.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                try
                {
                    using (StreamWriter sw = System.IO.File.CreateText(filePathWithTime))
                    {
                        sw.WriteLine("");
                    }
                    MessageBox.Show($"Tạo file mới: {fileNameWithTime}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tạo file: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sfCbFile.Text))
            {
                MessageBox.Show("Vui lòng load file để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string fileNameToDelete = sfCbFile.Text;
                string filePathToDelete = Path.Combine("Resources", "script", fileNameToDelete);

                if (System.IO.File.Exists(filePathToDelete))
                {
                    try
                    {
                        System.IO.File.Delete(filePathToDelete);
                        MessageBox.Show($"Đã xóa file: {fileNameToDelete}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa file: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("File không tồn tại trong thư mục Resources/script.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void sfCbFile_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            if (string.IsNullOrEmpty(sfCbFile.Text))
            {
                sfbtnEditScript.Enabled = false;

            }
            else
            {
                sfbtnEditScript.Enabled = true;
                string selectedFileName = sfCbFile.SelectedItem.ToString();
                string scriptFolderPath = Path.Combine(Application.StartupPath, "Resources", "script");
                string filePath = Path.Combine(scriptFolderPath, selectedFileName);
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        string fileContent = System.IO.File.ReadAllText(filePath);  // Đọc nội dung file
                        richTextBox1.Text = fileContent;  // Hiển thị nội dung vào richTextBox1
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
    }
}
