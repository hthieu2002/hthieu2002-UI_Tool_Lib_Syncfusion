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
        private void init()
        {
            sfbtnEditScript.Image = Properties.Resources.fileEdit;
            sfbtnCapture.Image = Properties.Resources.capture;

            sfbtnEditScript.Paint += BtnCommon_Paint;
            sfbtnLoadDevice.Paint += BtnCommon_Paint;
            sfbtnSend.Paint += BtnCommon_Paint;
            sfbtnTest.Paint += BtnCommon_Paint;
            sfbtnCapture.Paint += BtnCommon_Paint;

            btnLoadFile.Paint += BtnCommon_Paint;
            btnDelete.Paint += BtnCommon_Paint;
            btnCreate.Paint += BtnCommon_Paint;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            clickToolStripMenuItem_Click(clickToolStripMenuItem, EventArgs.Empty);
        }
        private void BtnCommon_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            int radius = 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(
                btn.ClientRectangle.X + 1,
                btn.ClientRectangle.Y + 1,
                btn.ClientRectangle.Width - 2,
                btn.ClientRectangle.Height - 2
            );
         
            btn.Region = new Region(GetRoundedRect(rect, radius));
            rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            e.Graphics.FillRectangle(new SolidBrush(btn.BackColor), rect);

            Pen borderPen = GetButtonBorderPen(btn);
            e.Graphics.DrawPath(borderPen, GetRoundedRect(rect, radius));

            Color textColor = GetButtonTextColor(btn);

            int iconSize = 24;                      // Kích thước icon (vuông)
            int iconPadding = 5;                    // Khoảng cách icon với viền và text
            int textOffsetX = 0;

            // === BỔ SUNG: Vẽ icon nếu có ===
            if (btn.Image != null)
            {
                // Giữ nguyên aspect ratio (nếu cần)
                int drawWidth = iconSize;
                int drawHeight = iconSize;

                // Nếu icon không phải hình vuông thì scale theo chiều nhỏ nhất
                if (btn.Image.Width != btn.Image.Height)
                {
                    float scale = Math.Min((float)iconSize / btn.Image.Width, (float)iconSize / btn.Image.Height);
                    drawWidth = (int)(btn.Image.Width * scale);
                    drawHeight = (int)(btn.Image.Height * scale);
                }

                Rectangle iconRect = new Rectangle(
                    rect.X + iconPadding,
                    rect.Y + (rect.Height - drawHeight) / 2,
                    drawWidth,
                    drawHeight
                );

                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImage(btn.Image, iconRect);
                textOffsetX = drawWidth + iconPadding * 2;
            }

            // === Phần vẽ text vẫn GIỮ NGUYÊN như bạn viết, chỉ cộng thêm offset icon ===
            Rectangle textRect = new Rectangle(
                rect.X + textOffsetX,
                rect.Y + 2,
                rect.Width - textOffsetX - 4,
                rect.Height - 4
            );

            TextRenderer.DrawText(
                e.Graphics,
                btn.Text,
                btn.Font,
                textRect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private Pen GetButtonBorderPen(Button btn)
        {
            if (!btn.Enabled)
            {
                return new Pen(Color.Gray);
            }
            else if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                return new Pen(Color.Blue);
            }
            else if (btn.Focused)
            {
                return new Pen(Color.Green);
            }
            else
            {
                return new Pen(Color.Gray);
            }
        }

        private Color GetButtonTextColor(Button btn)
        {
            if (btn.ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                return Color.Blue;
            }
            return btn.ForeColor;
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); // Top-left corner
            graphicsPath.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y); // Top edge
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90); // Top-right corner
            graphicsPath.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius); // Right edge
            graphicsPath.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90); // Bottom-right corner
            graphicsPath.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom); // Bottom edge
            graphicsPath.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90); // Bottom-left corner
            graphicsPath.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius); // Left edge
            graphicsPath.CloseFigure();
            return graphicsPath;
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
        private void SetActiveMenu(ToolStripMenuItem selectedItem)
        {
            // Duyệt toàn bộ các item cấp 1 trong MenuStrip
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                // Reset về trạng thái bình thường
                item.BackColor = Color.WhiteSmoke;
                item.ForeColor = Color.Black;
                item.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            }

            // Set trạng thái cho item đang được chọn
            selectedItem.BackColor = Color.WhiteSmoke;// Nền tím
            selectedItem.ForeColor = Color.Black;              // Chữ trắng
            selectedItem.Font = new Font("Segoe UI", 9f, FontStyle.Bold); // In đậm
        }

        private void BuildDataTableUI()
        {
            var dataGridView = new DataGridView
            {
                Width = 565,
                Height = 364,
                ColumnCount = 2,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 9f),
                AllowUserToResizeRows = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.MediumSlateBlue,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.WhiteSmoke,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.LightSteelBlue,
                    SelectionForeColor = Color.Black
                }
            };
            dataGridView.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray; 
                }
            };
            dataGridView.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0) 
                {
                    dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke; 
                }
            };
            // Đặt tên cột
            dataGridView.Columns[0].Name = "Key";
            dataGridView.Columns[1].Name = "Value";

            // Cho cột tự dãn ra đều nhau:
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // (Tuỳ chỉnh nếu muốn Key nhỏ hơn)
            dataGridView.Columns[0].FillWeight = 40;
            dataGridView.Columns[1].FillWeight = 60;

            // Nội dung các dòng theo như bạn gửi:
            string[] keys = {
        "index", "text", "resource-id", "class", "package", "content-desc", "checkable",
        "checked", "clickable", "enabled", "focusable", "focused",
        "scrollable", "long-clickable", "password", "selected", "bounds"
    };

            foreach (var key in keys)
            {
                dataGridView.Rows.Add(key, "");
            }

            datagrid.Controls.Add(dataGridView); // datagrid là panel hoặc groupbox bạn đặt sẵn trong Designer
        }


        private void editText()
        {
            richTextBox1.Font = new Font("Consolas", 12);
            richTextBox1.BackColor = Color.FromArgb(40, 40, 40);
            richTextBox1.ForeColor = Color.White;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.WordWrap = false; // Rất quan trọng để scroll đúng
            richTextBox1.Multiline = true;
            richTextBox1.SelectAll();
            richTextBox1.SelectionIndent = 40;
            richTextBox1.DeselectAll();

            panel5.Paint += Panel1_Paint; 
            richTextBox1.VScroll += (s, e) => panel5.Invalidate();
            richTextBox1.TextChanged += (s, e) => panel5.Invalidate();
            richTextBox1.Resize += (s, e) => panel5.Invalidate();
            richTextBox1.KeyUp += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    panel5.Invalidate();
                }
            };
            richTextBox1.TextChanged += (s, e) =>
            {
                HighlightSyntax();
            };

        }
        public void AppendText(string textToAdd)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                richTextBox1.Text += " ";
            }
            richTextBox1.Text += textToAdd;
        }
        private void HighlightSyntax()
        {
            int selectionStart = richTextBox1.SelectionStart;
            int selectionLength = richTextBox1.SelectionLength;

            // Reset toàn bộ về mặc định
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.White;

            // Regex: tìm hàm dạng TênHàm( ... )
            var regex = new Regex(@"\w+\(.*?\)");
            var matches = regex.Matches(richTextBox1.Text);

            foreach (Match match in matches)
            {
                // Tô màu vàng cho toàn bộ hàm + ()
                richTextBox1.Select(match.Index, match.Length);
                richTextBox1.SelectionColor = Color.Gold;

                // Xử lý bên trong ()
                int openParen = richTextBox1.Text.IndexOf('(', match.Index);
                int closeParen = richTextBox1.Text.IndexOf(')', openParen);
                if (openParen >= 0 && closeParen > openParen)
                {
                    int paramStart = openParen + 1;
                    int paramLength = closeParen - paramStart;
                    string paramText = richTextBox1.Text.Substring(paramStart, paramLength);

                    // Quét từng token bên trong ()
                    var paramRegex = new Regex(@"\d+|\"".*?\""|\w+");
                    var paramMatches = paramRegex.Matches(paramText);

                    foreach (Match paramMatch in paramMatches)
                    {
                        int tokenIndex = paramStart + paramMatch.Index;
                        int tokenLength = paramMatch.Length;

                        richTextBox1.Select(tokenIndex, tokenLength);

                        if (Regex.IsMatch(paramMatch.Value, @"^\d+$")) // số
                        {
                            richTextBox1.SelectionColor = Color.DeepSkyBlue;
                        }
                        else if (Regex.IsMatch(paramMatch.Value, "^\".*\"$")) // chuỗi trong dấu "
                        {
                            richTextBox1.SelectionColor = Color.LightPink;
                        }
                        else // từ bình thường
                        {
                            richTextBox1.SelectionColor = Color.Pink;
                        }
                    }
                }
            }

            // Khôi phục lại selection đang gõ
            richTextBox1.Select(selectionStart, selectionLength);
            richTextBox1.SelectionColor = Color.White;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(panel1.BackColor);

            int firstIndex = richTextBox1.GetCharIndexFromPosition(new Point(0, 0));
            int firstLine = richTextBox1.GetLineFromCharIndex(firstIndex);

            int lineHeight;
            try
            {
                lineHeight = richTextBox1.GetPositionFromCharIndex(firstIndex + 1).Y
                             - richTextBox1.GetPositionFromCharIndex(firstIndex).Y;
            }
            catch
            {
                lineHeight = (int)richTextBox1.Font.GetHeight() + 4;
            }

            if (lineHeight <= 0) lineHeight = (int)richTextBox1.Font.GetHeight() + 4;

            int visibleLines = (richTextBox1.Height / lineHeight) + 1;

            using (Brush brush = new SolidBrush(Color.Black))
            {
                for (int i = 0; i <= visibleLines; i++)
                {
                    int lineNumber = firstLine + i + 1;

                    int charIndex = richTextBox1.GetFirstCharIndexFromLine(lineNumber - 1);
                    if (charIndex == -1) break; // Không có dòng tiếp theo

                    int y = richTextBox1.GetPositionFromCharIndex(charIndex).Y;

                    e.Graphics.DrawString(lineNumber.ToString(),
                        richTextBox1.Font, brush, new PointF(5, y));
                }
            }
        }

        private void sfbtnEditScript_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                // Đang ở trạng thái "Edit", chuyển sang "Save"
                sfbtnEditScript.Text = "Save script";
               // btnEditSave.Image = Properties.Resources.icon_save; // Đổi icon nếu có
                isEditing = true;
                richTextBox1.ReadOnly = false;
            }
            else
            {
                sfbtnEditScript.Text = "Edit script";
                isEditing = false;
                richTextBox1.ReadOnly = true;
              //  SaveScriptToFile(); // Nếu bạn muốn lưu luôn
            }
        }
    }
}
