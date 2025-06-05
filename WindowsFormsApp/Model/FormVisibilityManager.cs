using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Model
{
    public static class FormVisibilityManager
    {
        // Biến static để theo dõi trạng thái của các form
        public static bool IsFormViewAutomationVisible { get; set; } = false;
        public static bool IsFormViewChangeVisible { get; set; } = false;

        public static Dictionary<string, string> _updatedNames = new Dictionary<string, string>();

        public static string IsLanguage = Properties.Settings.Default.Language ?? "en";

    }
}
