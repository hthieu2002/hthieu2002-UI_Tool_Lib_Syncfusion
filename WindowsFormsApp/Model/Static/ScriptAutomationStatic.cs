using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Model.Static
{
    public static class ScriptAutomationStatic
    {
        public static string logSaveScript { get; set; } = "Vui lòng tạo file hoặc chọn file";
        public static string logErrorSaveScript { get; set; } = "Lỗi khi lưu file:";
        public static string logLoadFile{ get; set; } = "Không có file .txt nào trong thư mục 'script'.";
        public static string logCreateFile{ get; set; } = "File đã tồn tại trong thư mục Resources/script.";
        public static string logErrorCreateFile{ get; set; } = "Lỗi khi tạo file với nội dung:";
        public static string logCreateSuccessFile{ get; set; } = "Tạo file mới với nội dung từ ComboBox:";
        public static string logCreateFileOn{ get; set; } = "File đã tồn tại trong thư mục Resources/script.";
        public static string logSuccessCreateFileOn{ get; set; } = "Tạo file mới:";
        public static string logErrorCreateFileOn{ get; set; } = "Lỗi khi tạo file: ";
        public static string logDeleteFile{ get; set; } = "Vui lòng load file để xóa.";
        public static string logSuccessDeleteFile{ get; set; } = "Đã xóa file: ";
        public static string logErrorDeleteFile{ get; set; } = "Lỗi khi xóa file: ";
        public static string logErrorDeleteFileOn{ get; set; } = "File không tồn tại trong thư mục Resources/script.";
        public static string logErrorOnLoadDataFile { get; set; } = "Không thể đọc file:";
        public static string logErrorLoadDevice { get; set; } = "Không có thiết bị nào kết nối.";
        public static string logCaptureDevice { get; set; } = "Hãy load devices và chọn thiết bị";
        public static string logCaptureScreenshot { get; set; } = "Ảnh đang bị khóa. Đang đợi để giải phóng...";
        public static string logShowScreenshot { get; set; } = "Hãy load devices và chọn thiết bị ";
        public static string logView { get; set; } = "Hãy load và chọn thiết bị view";
        public static string logSend { get; set; } = "Load file cần xử lý.";
        public static string logErrorSend { get; set; } = "Click các chức năng trước khi gửi.";
        public static string logTestControl { get; set; } = "Vui lòng điền chức năng test";
        public static string logErrorTestControl { get; set; } = "Chọn load và click thiết bị";
        // click tool box
        public static string ClickXY { get; set; } = "Click theo tọa độ x,y \n Ví dụ \n - Click cố định \n ClickXY(300 400) \n Sự dụng capture để chụp ảnh lấy tọa độ";
        public static string Swipe { get; set; } = "Thao tác cuộn \n Cuộn sẽ có 4 giá trị x1 x2 y1 y2 \n x1 x2 là x,y điểm ban đầu \n y1 y2 là điểm kết thúc \n cuộn sẽ bắt đầu từ điểm x đến y theo bất cứ chiều lên xuống nào \n Ví dụ \n Swipe(200 100 600 800) \n 200 100 tương ứng x1 x2 \n 600 800 tương ứng y1 y2 \n Thông số thứ 5 là độ trễ ms(mặc định là 500ms) \n Sự dụng capture để lấy tọa độ";
        public static string RandomClick { get; set; } = "Click random \n - Được ngăn cách 2 giá trị bởi dấu , \n Ví dụ ClickRandom(100 200 , 300 800) \n X được random trong khoảng 100-200\n Y được random trong khoảng 300-800";
        public static string Wait { get; set; } = "Wait \n Là lệnh chờ đợi \n Wait(1000) chờ 1 giây ";

        public static string SearchAndClick { get; set; } = "Tìm đúng text và click \n Lệnh tìm chữ Next nếu nó tồn tại thì click \n SearchAndClick(\"Next\")";
        public static string SearchWaitClick { get; set; } = "Tìm đúng và đợi\n Lệnh này giống lệnh tìm đúng và click nhưng nó sẽ đợi thêm số giây sau khi đã click\n SearchWaitClick(\"Next\", 1000)";
        public static string SearchAndContinue { get; set; } = "Tìm đúng và tiếp tục\n Lệnh này giống lệnh tìm đúng\n Nhưng lệnh sẽ bỏ qua khi tìm thấy\n Phù hợp trong các lệnh điều kiện if\nSearchAndContinue(\"Next\")";

        public static string If { get; set; } = "If dùng để đặt lệnh trong nó khi thỏa mãn điều kiện \n Ví dụ if = điều kiện { run } \n điều kiện là lệnh thỏa mãn if để run được chạy";
        public static string ForLoop { get; set; } = "For \n Vòng lặp for dùng để đặt vòng lặp xử lý cho các lệnh ở trong đó \n Mẫu \n for=main,end=100 \n { \n Wait(1000) \n } \n main là tên bắt buộc phải có giữa các for \n không được đặt trùng sẽ gây ra rối và chạy loạn";
        public static string Continue { get; set; } = "Lệnh continue \n Được sự dụng trong for \n dùng để bỏ qua for hiện tại và chuyển sang for khác \n hoặc bỏ qua các lệnh trong vòng lặp \n Ví dụ \n for =main, end=100 \n { \n Wait(1000) \n continue \n Wait(4000) \n } \n lệnh continue bỏ qua lệnh 4000";
        public static string Break { get; set; } = "Lệnh break \n Dùng để dừng vòng lặp \n Hoặc thoát script nếu không để trong vòng lặp";
        public static string StopScript { get; set; } = "Lệnh stop script \n Dùng để dừng script ngay lập tức";
        public static string Return { get; set; } = "Trong quá trình chạy \n Gặp lệnh này sẽ dừng hoặc chuyển script nếu chạy nhiều lần ";
        public static string Comment { get; set; } = "Sự dụng lệnh này để ghi chú lại script \n những gì được ghi sau Comment sẽ không được thực thi ";
        public static string ShowStatus { get; set; } = "Lệnh Show status \n Sẽ hiện thị log lên hiện thị quá trình chạy đến lệnh nào ";

        public static string SearchImageAndClick { get; set; } = "Lệnh này tương tự lệnh tìm đúng click \n Nhưng nó sẽ tìm trên hình ảnh xử lý những nơi k thể lấy được text";
        public static string SearchImageWaitClick { get; set; } = "Lệnh này tương tự lệnh tìm đúng và đợi \n Nhưng nó sẽ tìm trên hình ảnh xử lý những nơi k thể lấy được text";
        public static string SearchImageAndContinue { get; set; } = "Lệnh này tương tự lệnh tìm đúng tiếp tục \n Nhưng nó sẽ tìm trên hình ảnh xử lý những nơi k thể lấy được text";
        public static string TitleGroupClickToolbox1 { get; set; } = "Click tọa độ";
        public static string TitleGroupClickToolbox2 { get; set; } = "Tim text click";
        public static string TitleGroupClickToolbox3 { get; set; } = "Tim text trong ảnh";
        public static string TitleGroupClickToolbox4 { get; set; } = "Xử lý logic";
        public static string ControlGroup1Click { get; set; } = "Nhấn X Y";
        public static string ControlGroup1Swipe { get; set; } = "Vuốt";
        public static string ControlGroup1RandomClick { get; set; } = "Click ngẫu nhiên";
        public static string ControlGroup1Wait { get; set; } = "Đợi";
        public static string ControlGroup2SearchAndClick { get; set; } = "Tìm đúng && click";
        public static string ControlGroup2SearchWaitClick { get; set; } = "Tìm đúng && đợi";
        public static string ControlGroup2SearchAndContinue { get; set; } = "Tìm đúng && tiếp tục";
        public static string ControlGroup3FindAndClick { get; set; } = "Tìm đúng && click";
        public static string ControlGroup3findWaitClick { get; set; } = "Tìm đúng && đợi";
        public static string ControlGroup3FindAndContinue { get; set; } = "Tìm đúng && tiếp tục";
        // text tool box
        public static string SendText { get; set; } = "Send text \n - Gửi text cố định \n SendText(\"Abcd\")";
        public static string SendTextFromFileDelete { get; set; } = "Gửi văn bản từ file và xóa sau khi gửi.";
        public static string SendTextFrom { get; set; } = "Gửi văn bản lấy từ nguồn xác định.";
        public static string RandomTextAndSend { get; set; } = "Tạo văn bản ngẫu nhiên và gửi.";
        public static string DeleteTextOneChar { get; set; } = "Xóa một ký tự khỏi văn bản.";
        public static string DeleteTextAll { get; set; } = "Xóa toàn bộ văn bản.";

        public static string TitleTextToolBox { get; set; } = "Xử lý văn bản";
        public static string ControlSendText { get; set; } = "Send text";
        public static string ControlSendTextFromFileDelete { get; set; } = "Send text from file(delete)";
        public static string ControlSendTextFrom { get; set; } = "Send text from";
        public static string ControlRandomTextAndSend { get; set; } = "Random text & send";
        public static string ControlDeleteTextOneChar { get; set; } = "Delete text(1 char))";
        public static string ControlDeleteTextAll { get; set; } = "Delete text(Delete all)";
    }
}
