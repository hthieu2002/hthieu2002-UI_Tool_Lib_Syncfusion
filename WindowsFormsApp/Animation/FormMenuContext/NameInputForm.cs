using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp.Animation
{
    public partial class NameInputForm : Form
    {
        // Khai báo các control
        private TextBox txtName;
        private Button btnOk;
        private Button btnCancel;

        public string NewName { get; private set; }

        public NameInputForm(string currentName, string nameForm)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = nameForm;
            // Thiết lập kích thước cho form
            this.Width = 350;  // Chiều rộng form
            this.Height = 180;  // Chiều cao form
            this.StartPosition = FormStartPosition.CenterParent;  // Đặt form ở giữa parent form

            // Tạo TextBox
            txtName = new TextBox();
            txtName.Width = 280;  // Chiều rộng TextBox
            txtName.Height = 25;  // Chiều cao TextBox
            txtName.Text = currentName;  // Hiển thị tên hiện tại trong TextBox
            txtName.Location = new Point(30, 30);  
            this.Controls.Add(txtName); 

            // Tạo Button OK
            btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Width = 80;  
            btnOk.Height = 30; 
            btnOk.Location = new Point(30, 80);  
            btnOk.Click += btnOk_Click; 
            this.Controls.Add(btnOk); 

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Width = 80; 
            btnCancel.Height = 30;
            btnCancel.Location = new Point(130, 80);  
            btnCancel.Click += btnCancel_Click;  
            this.Controls.Add(btnCancel);  
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            NewName = txtName.Text;

            if (string.IsNullOrWhiteSpace(NewName) && Text == "Name device")
            {
                MessageBox.Show("Tên không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(NewName) && Text == "Input Proxy Socks5")
            {
                MessageBox.Show("Proxy Trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
