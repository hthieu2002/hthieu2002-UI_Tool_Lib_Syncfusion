using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Model.Static
{
    public static class ScreenViewStatic
    {
        public static string logRestartAllDevices { get; set; } = "Tất cả các thiết bị đã được khởi động lại.";
        public static string logInstallAPK { get; set; } = "APK đã được cài đặt cho tất cả các thiết bị.";
        public static string logPushFileAllDevice { get; set; } = "File đã được push và SDCARD cho tất cả các thiết bị.";
        public static string logErrorPushFileAllDevice { get; set; } = "Đã xảy ra lỗi khi chọn file:";
        public static string logInstallAPKAllDevice { get; set; } = "APK đã được cài đặt cho tất cả các thiết bị.";
        public static string logErrorInstallAPKAllDevice { get; set; } = "Đã xảy ra lỗi khi chọn file:";
        public static string logCaptureScreenshot { get; set; } = "Chụp màn hình đã được thực hiện cho tất cả các thiết bị.";
        public static string logExecuteAdbCommand { get; set; } = "Lệnh ADB đã được thực hiện cho tất cả các thiết bị.";
    }
}
