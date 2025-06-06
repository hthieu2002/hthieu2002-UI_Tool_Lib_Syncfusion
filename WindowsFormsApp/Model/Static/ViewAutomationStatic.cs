using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Model.Static
{
    public static class ViewAutomationStatic
    {
        public static string logLoadFileScript { get; set; } = "Không có file .txt nào trong thư mục 'script'.";
        public static string logRunScript { get; set; } = "Vui lòng load file script.";
        public static string logDeviceRunScript { get; set; } = "Không có thiết bị mới nào để chạy.";
    }
}
