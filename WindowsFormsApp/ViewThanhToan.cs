using AccountCreatorForm.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AccountCreatorForm.Views
{
    public partial class ViewThanhToan : Form
    {
        private HeaderViewCommon headerView;
        private Panel wrapperPanel;

        public ViewThanhToan()
        {
            InitializeComponent();
            InitLayout();
            LoadPricingCards();
            this.BackColor = Color.White;
        }

        private void InitLayout()
        {
            this.BackColor = ColorTranslator.FromHtml("#F5F7FC");

            // ----- Header ----- 
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 70;
            panelHeader.BackColor = Color.White;

            headerView = new HeaderViewCommon();
            headerView.Dock = DockStyle.Fill;
            headerView.SetTitle("Thanh toán");
            panelHeader.Controls.Add(headerView);

            // ----- Label "Bảng Giá" -----
            lblBangGia.Dock = DockStyle.Top;
            lblBangGia.BackColor = Color.White;
            lblBangGia.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBangGia.Text = "Bảng Giá";
            lblBangGia.Height = 90;
            lblBangGia.TextAlign = ContentAlignment.MiddleCenter;
            lblBangGia.Padding = new Padding(20, 10, 0, 0);
            // lblBangGia.BackColor = ColorTranslator.FromHtml("#F5F7FC");
            lblBangGia.AutoSize = true;

            // ----- FlowLayout chứa các card -----
            flowLayoutPanelCards = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Margin = new Padding(0,20,0,0),
                Padding = new Padding(0, 50, 0, 30),
                BackColor = Color.Transparent
            };

            // ----- Wrapper Panel để căn giữa flow -----
            Panel centerWrapper = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 20, 0, 20),
                BackColor = Color.Transparent
            };

            centerWrapper.Controls.Add(flowLayoutPanelCards);

            // ✅ Responsive CĂN GIỮA + tự điều chỉnh theo chiều ngang khi resize
            centerWrapper.Resize += (s, e) =>
            {
                int flowWidth = flowLayoutPanelCards.PreferredSize.Width;
                int wrapperWidth = centerWrapper.ClientSize.Width;

                // Điều chỉnh Left để căn giữa
                flowLayoutPanelCards.Left = (wrapperWidth - flowWidth) / 2;
            };


            // ----- Layout tổng -----
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // label bảng giá
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // nội dung card

            mainLayout.Controls.Add(panelHeader, 0, 0);
            mainLayout.Controls.Add(lblBangGia, 0, 1);
            mainLayout.Controls.Add(centerWrapper, 0, 2);

            // Add vào Form
            this.Controls.Clear();
            this.Controls.Add(mainLayout);
        }

        private void LoadPricingCards()
        {
            flowLayoutPanelCards.Controls.Clear();

            var cards = new List<UcPricingCard>();

            var goiFree = new UcPricingCard
            {
                Title = "Dùng thử",
                Price = "Miễn phí",
                Subtitle = "dùng thử 7 ngày",
                ButtonText = "Bắt đầu",
                Features = new[]
                {
            "Stream thiết bị không giới hạn",
            "Sử dụng với trình giả lập (LDPlayer, MumuPlayer, Nox, Bluestack, ...)",
            "Chuyển đổi thiết bị & Sao lưu thiết bị với Genboxphone",
            "Tự động hóa không cần mã trong Trình chỉnh sửa",
            "Đồng bộ chuột / bàn phím",
            "Độ phân giải màn hình cao",
            "Tải lên / tải xuống tệp",
            "Cài đặt / gỡ cài đặt ứng dụng hàng loạt",
            "Thêm chức năng proxy hàng loạt với bộ định tuyến",
            "Bán/mua mini-app trên store"
        }
            };
            cards.Add(goiFree);

            var featuresPro = new[]
            {
        
        "Mọi thứ trong Dùng thử, cộng với:",
        "Lập lịch tự động hóa tác vụ",
        "Tạo tác vụ, Chạy tác vụ tự động",
        "Truy cập vào các kịch bản sẵn có trên Automation Store",
        "API địa phương",
        "Hỗ trợ VIP"
    };

            cards.Add(new UcPricingCard
            {
                Title = "Pro – 1 tháng",
                Price = "650,000đ",
                Subtitle = "/ máy",
                ButtonText = "Mua ngay",
                Features = featuresPro
            });

            cards.Add(new UcPricingCard
            {
                Title = "Pro – 6 tháng",
                Price = "3,200,000đ",
                Subtitle = "/ máy",
                ButtonText = "Mua ngay",
                Features = featuresPro
            });

            cards.Add(new UcPricingCard
            {
                Title = "Pro – 1 năm",
                Price = "6,000,000đ",
                Subtitle = "/ máy",
                ButtonText = "Mua ngay",
                Features = featuresPro
            });

            cards.Add(new UcPricingCard
            {
                Title = "Pro – trọn đời",
                Price = "12,000,000đ",
                Subtitle = "/ máy",
                ButtonText = "Mua ngay",
                Features = featuresPro
            });

            foreach (var card in cards)
            {
                flowLayoutPanelCards.Controls.Add(card);
            }

            flowLayoutPanelCards.PerformLayout();
            foreach (var card in cards)
            {
                card.PerformLayout();
            }

            int maxHeight = cards.Max(c => c.Height);

            foreach (var card in cards)
            {
                card.Height = maxHeight;
            }
        }

    }
}