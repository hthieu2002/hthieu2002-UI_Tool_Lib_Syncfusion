using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Model.Static
{
    public static class ViewChangeStatic
    {
        public static string Info { get; set; } = "Info";
        public static string Error { get; set; } = "Error";
        public static string Warning { get; set; } = "Warning";

        public static string logMessageEditItem { get; set; } = "Vui lòng chọn một thiết bị để chỉnh sửa.";
        public static string logMessageDeleteItem { get; set; } = "Please select a device to delete.";
        public static string logMessageStartDevice { get; set; } = "Không có thiết bị mới nào để chạy.";
        public static string logMessageStartChane { get; set; } = "Are you sure to proceed with these changes and reboot ?";
        public static string TitleMessageStartChane { get; set; } = "Changes Confirmation";
        public static string logMessageKeyBox { get; set; } = "Are you push file keybox.xml to phone ?";
        public static string logErrorMessageKeyBox { get; set; } = "Vui lòng chọn file có tên đúng là keybox.xml";
        public static string logMessagePif { get; set; } = "Are you push file pif.json to phone ?";
        public static string logErrorMessagePif { get; set; } = "Vui lòng chọn file có tên đúng là pif.json";
        public static string ErrorRandomChange { get; set; } = "Devices not existed, please try again";
        public static string logChangeError { get; set; } = "This selected device cannot be changed, please check your rom and developer setting and try loading again.";
        public static string ChangeError { get; set; } = "Device Error";
        public static string titleLocationForm { get; set; } = "Nhập tọa độ";
        public static string logLatitude { get; set; } = "Giá trị Latitude phải nằm trong khoảng từ -180.0 đến 180.0";
        public static string logErrorLatitude { get; set; } = "Giá trị không hợp lệ.";
        public static string logLongitude { get; set; } = "Giá trị Longitude phải nằm trong khoảng từ -90.0 đến 90.0";
        public static string logErrorLongitude { get; set; } = "Giá trị không hợp lệ.";
        public static string titlepProxyForm { get; set; } = "Input Proxy Socks5";
        public static string logProxyForm { get; set; } = "Thiết bị hiện không hoạt động hoặc chưa xác thực.";
        public static string logProxyForm1 { get; set; } = "Vui lòng chọn một thiết bị.";
        public static string logErrorFakeTimeZone { get; set; } = "Failed to connect to";
        public static string logErrorFakeTimeZone1 { get; set; } = "port";
        public static string TitleErrorFakeTimeZone { get; set; } = "Exception";
        public static string logFakeTimeZone { get; set; } = "Invalid Proxy";
        public static string infoDevice { get; set; } = "Thông tin thiết bị";


        
    }
}
