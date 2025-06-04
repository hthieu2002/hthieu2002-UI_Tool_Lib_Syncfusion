using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp.Model;

namespace WindowsFormsApp
{
    public partial class ScriptAutomation : Form, ITextAppender
    {
        private bool isEditing = false;
        List<string> dataFileScript;

        private XmlDocument _xmlDoc;
        private List<UiElement> _uiElements;
        private UiElement currentElement = null;
        private Process scrcpyProcess = null;

        DataGridView dataGridView;

        //data text test 
        private int clickCount = 0;
        private int x1, y1;
        private int x2, y2;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETREDRAW = 0x000B;

        private const int SB_VERT = 1;
        private const int WM_VSCROLL = 0x0115;
        private const int SB_THUMBPOSITION = 4;

        [DllImport("user32.dll")]
        private static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        private static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        public ScriptAutomation()
        {
            InitializeComponent();

            _uiElements = new List<UiElement>();
            lbNew.Visible = false;
            init();
            BuildDataTableUI();
            editText();
            lbLog.Text = "";
            sfCbModelDump.Text = "Nomal";
            this.Load += Form1_Load;
            sfCbFile.SelectedIndexChanged += sfCbFile_SelectedIndexChanged;
            pictureBoxScreen.MouseClick += pictureBoxScreen_MouseClick;
            pictureBoxScreen.MouseMove += pictureBoxScreen_MouseMove;

            if (string.IsNullOrEmpty(sfCbFile.Text))
            {
                sfbtnEditScript.Enabled = false;
            }
            else
            {
                sfbtnEditScript.Enabled = true;
            }

            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
            sfCbLoadDevices.SelectedIndexChanged += sfCbLoadDevices_SelectedIndexChanged;
        }
        private void BeginUpdate()
        {
            SendMessage(richTextBox1.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        private void EndUpdate()
        {
            SendMessage(richTextBox1.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            richTextBox1.Invalidate();
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
            LoadContent(new DataChangeInfoToolbox(this));
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
            LoadContent(new KeyButtonToolbox(this));
        }

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActiveMenu((ToolStripMenuItem)sender);
            LoadContent(new GeneralToolbox(this));
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


                SaveScriptToFile();
            }
        }
        private void SaveScriptToFile()
        {
            string fileName = sfCbFile.SelectedItem?.ToString();
            if (fileName == null)
            {
                MessageBox.Show("Vui lòng tạo file hoặc chọn file");
                return;
            }
            string scriptFolderPath = Path.Combine(Application.StartupPath, "Resources", "script");
            string filePath = Path.Combine(scriptFolderPath, fileName);

            try
            {
                string scriptContent = richTextBox1.Text;

                if (!Directory.Exists(scriptFolderPath))
                {
                    Directory.CreateDirectory(scriptFolderPath);
                }

                System.IO.File.WriteAllText(filePath, scriptContent);
                //MessageBox.Show("Dữ liệu đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lbNew.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            lbNew.Visible = true;
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
                string selectedFileName = sfCbFile.SelectedItem?.ToString();
                if(selectedFileName == null)
                {
                    return;
                }
                string scriptFolderPath = Path.Combine(Application.StartupPath, "Resources", "script");
                string filePath = Path.Combine(scriptFolderPath, selectedFileName);
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        string fileContent = System.IO.File.ReadAllText(filePath);  // Đọc nội dung file
                        richTextBox1.Text = fileContent;  // Hiển thị nội dung vào richTextBox1
                        lbNew.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void sfbtnLoadDevice_Click(object sender, EventArgs e)
        {
            var devices = ADBService.GetDevices();

            sfCbLoadDevices.DataSource = devices;

            if (devices.Count > 0)
            {
                sfCbLoadDevices.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Không có thiết bị nào kết nối.");
            }
        }

        private void sfbtnCapture_Click(object sender, EventArgs e)
        {
            string deviceId = sfCbLoadDevices.Text;

            if (sfCbLoadDevices.Text == "")
            {
                MessageBox.Show("Hãy load devices và chọn thiết bị ");
                return;
            }
            lbLog.Text = "Chụp màn hình";
            CaptureScreenshot(deviceId);
            lbLog.Text = "Dump xml";
            DumpUIDetails(deviceId);
            lbLog.Text = "Success";
            ShowScreenshot();
            lbLog.Text = "";
        }
        private void CaptureScreenshot(string deviceId)
        {
            string imagePath = Path.Combine(Application.StartupPath, "screen.png");
            pictureBoxScreen.Image?.Dispose();
            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        fs.Close();
                        System.IO.File.Delete(imagePath);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Ảnh đang bị khóa. Đang đợi để giải phóng...");
                    Thread.Sleep(500);
                    return;
                }
            }

            var processStartInfo = new ProcessStartInfo("./Resources/adb", $"-s {deviceId} shell screencap -p /sdcard/screen.png")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(processStartInfo);
            if (process != null)
            {
                process.WaitForExit();
            }


            processStartInfo = new ProcessStartInfo("./Resources/adb", $"-s {deviceId} pull /sdcard/screen.png {imagePath}")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process = Process.Start(processStartInfo);
            if (process != null)
            {
                process.WaitForExit();
            }
        }

        private void DumpUIDetails(string deviceId)
        {
            var processStartInfo = new ProcessStartInfo("./Resources/adb", $"-s {deviceId} shell uiautomator dump /sdcard/ui_dump.xml")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(processStartInfo);
            process.WaitForExit();

            processStartInfo = new ProcessStartInfo("./Resources/adb", $"-s {deviceId} pull /sdcard/ui_dump.xml {Path.Combine(Application.StartupPath, "ui_dump.xml")}")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            process = Process.Start(processStartInfo);
            process.WaitForExit();

            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(Path.Combine(Application.StartupPath, "ui_dump.xml"));

            XmlNodeList elementNodes = _xmlDoc.GetElementsByTagName("node");
            foreach (XmlNode node in elementNodes)
            {
                if (node.Attributes["class"] != null)
                {
                    string className = node.Attributes["class"].Value;

                    if (string.IsNullOrEmpty(node.Attributes["bounds"]?.Value))
                    {
                        continue;
                    }

                    var element = new UiElement
                    {
                        Index = node.Attributes["index"]?.Value,
                        Text = node.Attributes["text"]?.Value,
                        ResourceId = node.Attributes["resource-id"]?.Value,
                        Class = node.Attributes["class"]?.Value,
                        Package = node.Attributes["package"]?.Value,
                        ContentDesc = node.Attributes["content-desc"]?.Value,
                        Checkable = node.Attributes["checkable"]?.Value,
                        Checked = node.Attributes["checked"]?.Value,
                        Clickable = node.Attributes["clickable"]?.Value,
                        Enabled = node.Attributes["enabled"]?.Value,
                        Focusable = node.Attributes["focusable"]?.Value,
                        Focused = node.Attributes["focused"]?.Value,
                        Scrollable = node.Attributes["scrollable"]?.Value,
                        LongClickable = node.Attributes["long-clickable"]?.Value,
                        Password = node.Attributes["password"]?.Value,
                        Selected = node.Attributes["selected"]?.Value,
                        Bounds = node.Attributes["bounds"]?.Value
                    };

                    if (!string.IsNullOrEmpty(element.Bounds))
                    {
                        _uiElements.Add(element);
                    }
                    else
                    {
                        Console.WriteLine($"Skipping element with missing bounds: {element.Text}");
                    }
                }
            }
        }


        private void ShowScreenshot()
        {
            string imagePath = Path.Combine(Application.StartupPath, "screen.png");

            if (System.IO.File.Exists(imagePath))
            {
                pictureBoxScreen.Image?.Dispose();
                pictureBoxScreen.Image = null;

                Bitmap bitmap = new Bitmap(imagePath);

                pictureBoxScreen.Image = bitmap;
                pictureBoxScreen.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Hãy load devices và chọn thiết bị ");
            }
        }
        private System.Drawing.Point ConvertToImageCoordinates(int mouseX, int mouseY, PictureBox pictureBox, Bitmap image)
        {
            try
            {
                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image), "The image cannot be null.");
                }

                if (pictureBox.Width == 0 || pictureBox.Height == 0)
                {
                    throw new ArgumentException("The PictureBox dimensions are invalid.");
                }

                if (mouseX < 0 || mouseX >= pictureBox.Width || mouseY < 0 || mouseY >= pictureBox.Height)
                {
                    throw new ArgumentException("Mouse coordinates are outside the bounds of the PictureBox.");
                }

                float scaleX = (float)image.Width / pictureBox.Width;
                float scaleY = (float)image.Height / pictureBox.Height;

                int imageX = (int)(mouseX * scaleX);
                int imageY = (int)(mouseY * scaleY);

                return new System.Drawing.Point(imageX, imageY);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new System.Drawing.Point(0, 0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new System.Drawing.Point(0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return new System.Drawing.Point(0, 0);
            }
        }



        private void pictureBoxScreen_MouseClick(object sender, MouseEventArgs e)
        {
            var bitmap = pictureBoxScreen.Image as Bitmap;

            if (bitmap != null)
            {
                var position = ConvertToImageCoordinates(e.X, e.Y, pictureBoxScreen, bitmap);

                //x.Text = $"[ {position.X} ]";
                //y.Text = $": {position.Y} ]";
                // import dữ liệu cho txt test
                ImportDataTextTest(position.X, position.Y);


                var element = GetElementAtPosition(position.X, position.Y);

                if (element != null)
                {
                    Console.WriteLine($"Mouse moved to: ({e.X}, {e.Y}) - Element: {element.Text}");
                    Console.WriteLine($"Bounds: {element.Bounds}");

                    DisplayElementInfo(element);
                    DrawElementBorder(element, pictureBoxScreen, bitmap);
                }
                else
                {
                    // Xóa thông tin nếu không có phần tử nào
                    ClearElementInfo();
                    pictureBoxScreen.Invalidate();
                }
            }
        }
        private void pictureBoxScreen_MouseMove(object sender, MouseEventArgs e)
        {
            var bitmap = pictureBoxScreen.Image as Bitmap;

            if (bitmap != null)
            {
                var position = ConvertToImageCoordinates(e.X, e.Y, pictureBoxScreen, bitmap);

                x.Text = $"[ {position.X} ";
                y.Text = $": {position.Y} ]";
            }
        }

        private void DrawElementBorder(UiElement element, PictureBox pictureBox, Bitmap image)
        {
            var bounds = element.Bounds.Trim('[', ']').Split(new[] { "][" }, StringSplitOptions.None);
            var topLeft = bounds[0].Split(',');
            var bottomRight = bounds[1].Split(',');

            int left = int.Parse(topLeft[0]);
            int top = int.Parse(topLeft[1]);
            int right = int.Parse(bottomRight[0]);
            int bottom = int.Parse(bottomRight[1]);

            float scaleX = (float)pictureBox.Width / image.Width;
            float scaleY = (float)pictureBox.Height / image.Height;

            int pictureBoxLeft = (int)(left * scaleX);
            int pictureBoxTop = (int)(top * scaleY);
            int pictureBoxRight = (int)(right * scaleX);
            int pictureBoxBottom = (int)(bottom * scaleY);

            using (Graphics g = pictureBox.CreateGraphics())
            {
                Pen pen = new Pen(Color.Red, 1);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                g.DrawRectangle(pen, new Rectangle(pictureBoxLeft, pictureBoxTop, pictureBoxRight - pictureBoxLeft, pictureBoxBottom - pictureBoxTop));
            }
        }



        private void ClearElementInfo()
        {
            dataGridView.Rows.Clear();
        }
        private UiElement GetElementAtPosition(int x, int y)
        {
            pictureBoxScreen.Refresh();
            UiElement selectedElement = null;
            double minDistance = double.MaxValue;

            foreach (var element in _uiElements)
            {
                if (element == null || string.IsNullOrEmpty(element.Bounds))
                {
                    Console.WriteLine("Element or Bounds is null or empty.");
                    continue;
                }

                if (element.Class.ToLower().Contains("viewgroup"))
                {
                    Console.WriteLine($"Skipping element with class containing 'ViewGroup': {element.Text}");
                    continue;
                }

                var bounds = element.Bounds.Trim('[', ']').Split(new[] { "][" }, StringSplitOptions.None);
                if (bounds.Length != 2)
                {
                    Console.WriteLine("Invalid bounds format.");
                    continue;
                }

                var topLeft = bounds[0].Split(',');
                var bottomRight = bounds[1].Split(',');

                if (topLeft.Length != 2 || bottomRight.Length != 2)
                {
                    Console.WriteLine("Bounds format is invalid.");
                    continue;
                }

                int left = int.Parse(topLeft[0]);
                int top = int.Parse(topLeft[1]);
                int right = int.Parse(bottomRight[0]);
                int bottom = int.Parse(bottomRight[1]);

                int centerX = (left + right) / 2;
                int centerY = (top + bottom) / 2;

                double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));

                Console.WriteLine($"Element: {element.Text}, Bounds: {element.Bounds}, Distance: {distance}");

                if (distance < minDistance)
                {
                    minDistance = distance;
                    selectedElement = element;
                }
            }

            if (selectedElement != null)
            {
                Console.WriteLine($"Selected Element: {selectedElement.Text}, with Bounds: {selectedElement.Bounds}");
            }
            else
            {
                Console.WriteLine("No element selected.");
                return null;
            }

            return selectedElement;

        }
        private void ImportDataTextTest(int x, int y)
        {
            string command = txtTest.Text.Split('(')[0].Trim();
            clickCount++;

            if (command == "ClickXY")
            {
                txtTest.Text = "";
                txtTest.Text = $"ClickXY({x} {y})";
            }
            if (command == "Swipe")
            {
                if (clickCount % 2 == 0)
                {
                    //chẵn
                    x2 = x;
                    y2 = y;
                }
                else
                {
                    // lẻ
                    x1 = x;
                    y1 = y;
                }

                if (x2 == 0 && y2 == 0)
                {
                    txtTest.Text = "";
                    txtTest.Text = $"Swipe({x1} {y1} null null 500)";
                }
                else
                {
                    txtTest.Text = "";
                    txtTest.Text = $"Swipe({x1} {y1} {x2} {y2} 500)";
                }
            }
        }
        private void DisplayElementInfo(UiElement element)
        {
            dataGridView.Rows.Clear();

            var bounds = element.Bounds.Trim('[', ']').Split(new[] { "][" }, StringSplitOptions.None);
            var topLeft = bounds[0].Split(',');
            var bottomRight = bounds[1].Split(',');
            int left = int.Parse(topLeft[0]);
            int top = int.Parse(topLeft[1]);
            int right = int.Parse(bottomRight[0]);
            int bottom = int.Parse(bottomRight[1]);
            int centerX = (left + right) / 2;
            int centerY = (top + bottom) / 2;

            var xY = $"[ {centerX} : {centerY} ]";

            dataGridView.Rows.Add("index", element.Index);
            dataGridView.Rows.Add("[X : Y ]", xY);
            dataGridView.Rows.Add("text", element.Text);
            dataGridView.Rows.Add("resource-id", element.ResourceId);
            dataGridView.Rows.Add("class", element.Class);
            dataGridView.Rows.Add("package", element.Package);
            dataGridView.Rows.Add("content-desc", element.ContentDesc);
            dataGridView.Rows.Add("checkable", element.Checkable);
            dataGridView.Rows.Add("checked", element.Checked);
            dataGridView.Rows.Add("clickable", element.Clickable);
            dataGridView.Rows.Add("enabled", element.Enabled);
            dataGridView.Rows.Add("focusable", element.Focusable);
            dataGridView.Rows.Add("focused", element.Focused);
            dataGridView.Rows.Add("scrollable", element.Scrollable);
            dataGridView.Rows.Add("long-clickable", element.LongClickable);
            dataGridView.Rows.Add("password", element.Password);
            dataGridView.Rows.Add("selected", element.Selected);

        }

        private void sfView_Click(object sender, EventArgs e)
        {
            if (sfCbLoadDevices.Text == "")
            {
                MessageBox.Show("Hãy load và chọn thiết bị view");
                return;
            }
            try
            {
                string deviceId = sfCbLoadDevices.Text;
                string scrcpyPath = Path.Combine(Application.StartupPath, "Resources", "scrcpy.exe");

                if (File.Exists(scrcpyPath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = scrcpyPath,
                        Arguments = $"-s {deviceId} --always-on-top --window-x 0 --window-y 30",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    scrcpyProcess = Process.Start(startInfo);
                }
                else
                {
                    MessageBox.Show("scrcpy.exe không được tìm thấy trong thư mục Resources.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gọi scrcpy: " + ex.Message);
            }
        }

        private void sfbtnSend_Click(object sender, EventArgs e)
        {
            string message = txtTest.Text;
            if (sfCbFile.Text == "")
            {
                // 
                MessageBox.Show("Load file cần xử lý.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(message))
            {
                richTextBox1.AppendText("\n" + message);

                // txtTest.Clear();
            }
            else
            {
                MessageBox.Show("Click các chức năng trước khi gửi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void sfCbLoadDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sfCbLoadDevices.SelectedItem != null)
            {
                sfView.Text = "View device: " + sfCbLoadDevices.SelectedItem.ToString();
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.F)
            {
                FormatBraces();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveScriptToFile();
            }

            if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAllText();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                richTextBox1.Copy();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                richTextBox1.Paste();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                richTextBox1.Cut();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (richTextBox1.CanUndo)
                {
                    richTextBox1.Undo();
                }
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Tab)
            {
                IndentCurrentLine();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.Y)
            {
                if (richTextBox1.CanRedo)
                    richTextBox1.Redo();
                e.SuppressKeyPress = true;
            }
        }
        private void IndentCurrentLine()
        {
            int selectionStart = richTextBox1.SelectionStart;
            int currentLine = richTextBox1.GetLineFromCharIndex(selectionStart);
            int lineStart = richTextBox1.GetFirstCharIndexFromLine(currentLine);

            richTextBox1.SelectionStart = lineStart;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectedText = "\t"; // Thêm tab đầu dòng
        }

        private void SelectAllText()
        {
            richTextBox1.SelectAll();
            richTextBox1.Focus();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (sfCbFile.Text == "")
            {
                return;
            }
            lbNew.Visible = true;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (scrcpyProcess != null && !scrcpyProcess.HasExited)
            {
                scrcpyProcess.Kill();
            }
        }

        private async void sfbtnTest_Click(object sender, EventArgs e)
        {
            // test
            if (txtTest.Text == "")
            {
                MessageBox.Show("Vui lòng điền chức năng test");
                return;
            }
            if (sfCbLoadDevices.Text == "")
            {
                MessageBox.Show("Chọn load và click thiết bị");
                return;
            }
            try
            {
                sfbtnTest.Text = "Running";
                Script.Roslyn.ScriptAutomation scriptRolyn = new Script.Roslyn.ScriptAutomation();
                await scriptRolyn.TestFunction(txtTest.Text, sfCbLoadDevices.Text);

                await Task.Delay(1000);
                lbLog.Text = "Chụp màn hình";
                CaptureScreenshot(sfCbLoadDevices.Text);
                lbLog.Text = "Dump xml";
                DumpUIDetails(sfCbLoadDevices.Text);
                lbLog.Text = "Success";
                ShowScreenshot();
                lbLog.Text = "";

                sfbtnTest.Text = "Test";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            sfbtnTest.Text = "Test";
        }
    }
}

