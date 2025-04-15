using AccountCreatorForm.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Properties;
using System.Windows.Forms;

namespace AccountCreatorForm.Views
{
    public partial class ViewCuaHang : Form
    {
        private Panel panelHeader;
        private HeaderViewCommon headerView;
        private Label lblStore;
        private FlowLayoutPanel flowLayoutPanelItems;

        public ViewCuaHang()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            InitLayout(); // Initialize the layout
            LoadStoreItems(); // Load the store items
        }

        private void InitLayout()
        {
            this.BackColor = ColorTranslator.FromHtml("#FFFFFF");

            // ----- Initialize Header (Syncfusion SfPanel) -----
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80
                
            };

            headerView = new HeaderViewCommon
            {
                Dock = DockStyle.Fill,
                // BackColor = Color.Red,
                Margin = new Padding(0, 0, 0, 20)
            };
            headerView.SetTitle("Cửa hàng");
            panelHeader.Controls.Add(headerView);

            // ----- Initialize Label "Store" (Syncfusion SfLabel) -----
            lblStore = new Label
            {
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Text = "Cửa hàng",
                Height = 70,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(20, 10, 10, 10),
                AutoSize = false
            };

            lblStore.Paint += (sender, e) =>
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            };

            // ----- Initialize FlowLayoutPanel for items (Syncfusion SfFlowLayoutPanel) -----
            flowLayoutPanelItems = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(30),
                Margin = new Padding(0,40,0,0),
                WrapContents = true, // Allows for wrapping when the content overflows
                FlowDirection = FlowDirection.LeftToRight 
            };
            
            // ----- Main layout (Syncfusion TableLayoutPanel) -----
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // store title
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 400)); // content area

            mainLayout.Controls.Add(panelHeader, 0, 0);
            mainLayout.Controls.Add(lblStore, 0, 1);
            mainLayout.Controls.Add(flowLayoutPanelItems, 0, 2);

            // Add to form
            this.Controls.Clear();
            this.Controls.Add(mainLayout);
        }
       

        private void LoadStoreItems()
        {
            // Clear existing items
            flowLayoutPanelItems.Controls.Clear();
            flowLayoutPanelItems.FlowDirection = FlowDirection.LeftToRight; // Lọc theo chiều ngang
            flowLayoutPanelItems.WrapContents = true; // Cho phép xuống dòng khi không đủ không gian
            flowLayoutPanelItems.AutoSize = true;
            flowLayoutPanelItems.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanelItems.Dock = DockStyle.Fill;
            // Sample items
            var item1 = new StoreItem
            {
                Title = "[COMBO 1] NUÔI CLONE + VIỆT NAM PROMAX SIÊU CẤP PRO",
                UserName = "Hiếu đẹp trai",
                Description = "Nuôi facebook trên NewFeed, Reels..., cân mọi thứ miễn đưa tiền đây",
                ButtonText = "facebook",
                ItemImage = Resources.cuahang_0
            };

            var item2 = new StoreItem
            {
                Title = "[COMBO 1] NUÔI CLONE + VIỆT NAM PROMAX SIÊU CẤP PRO",
                UserName = "Hiếu đẹp trai",
                Description = "Nuôi facebook trên NewFeed, Reels..., cân mọi thứ miễn đưa tiền đây",
                ButtonText = "facebook",
                ItemImage = Resources.cuahang_0
            };
            var item3 = new StoreItem
            {
                Title = "[COMBO 1] NUÔI CLONE + VIỆT NAM PROMAX SIÊU CẤP PRO",
                UserName = "Hiếu đẹp trai",
                Description = "Nuôi facebook trên NewFeed, Reels..., cân mọi thứ miễn đưa tiền đây",
                ButtonText = "facebook",
                ItemImage = Resources.cuahang_0
            };
            var item4 = new StoreItem
            {
                Title = "[COMBO 1] NUÔI CLONE + VIỆT NAM PROMAX SIÊU CẤP PRO",
                UserName = "Hiếu đẹp trai",
                Description = "Nuôi facebook trên NewFeed, Reels..., cân mọi thứ miễn đưa tiền đây",
                ButtonText = "facebook",
                ItemImage = Resources.cuahang_0
            };
            var item5 = new StoreItem
            {
                Title = "[COMBO 1] NUÔI CLONE + VIỆT NAM PROMAX SIÊU CẤP PRO",
                UserName = "Hiếu đẹp trai",
                Description = "Nuôi facebook trên NewFeed, Reels..., cân mọi thứ miễn đưa tiền đây",
                ButtonText = "facebook",
                ItemImage = Resources.cuahang_0
            };
            var item6 = new StoreItem
            {
                Title = "[COMBO 1] NUÔI CLONE + VIỆT NAM PROMAX SIÊU CẤP PRO",
                UserName = "Hiếu đẹp trai",
                Description = "Nuôi facebook trên NewFeed, Reels..., cân mọi thứ miễn đưa tiền đây",
                ButtonText = "facebook",
                ItemImage = Resources.cuahang_0
            };

            // Add items to SfFlowLayoutPanel
            flowLayoutPanelItems.Controls.Add(item1);
            flowLayoutPanelItems.Controls.Add(item2);
            flowLayoutPanelItems.Controls.Add(item3);
            flowLayoutPanelItems.Controls.Add(item4);
            flowLayoutPanelItems.Controls.Add(item5);
            flowLayoutPanelItems.Controls.Add(item6);

            int maxHeight = flowLayoutPanelItems.Controls.Cast<Control>().Max(c => c.Height);
            foreach (Control item in flowLayoutPanelItems.Controls)
            {
                item.Height = maxHeight; // Đảm bảo tất cả items có chiều cao đều nhau
            }

            flowLayoutPanelItems.Resize += (s, e) =>
            {
                // Lấy chiều rộng của flowLayoutPanelItems sau khi được tính toán
                int flowWidth = flowLayoutPanelItems.PreferredSize.Width;
                int wrapperWidth = flowLayoutPanelItems.Parent.ClientSize.Width;

                // Cập nhật vị trí Left để căn giữa FlowLayoutPanel
                // Kiểm tra nếu chiều rộng của flowLayoutPanelItems lớn hơn vùng chứa

                if (flowWidth < wrapperWidth)
                {
                    flowLayoutPanelItems.Left = (wrapperWidth - flowWidth) / 2;
                }
                else
                {
                    flowLayoutPanelItems.Left = 70; // Nếu chiều rộng quá lớn thì không căn giữa nữa
                }
            };


        }

        private void ViewCuaHang_Load(object sender, EventArgs e)
        {
            flowLayoutPanelItems.Refresh();
        }
    }
}
