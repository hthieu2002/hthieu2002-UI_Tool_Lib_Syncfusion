using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public enum WindowMode
    {
        Normal,
        Minimized,
        Maximized,
        FullScreen
    }
    public static class AppState
    {
        /// <summary>
        /// Chứa trạng thái hiện tại của Form.
        /// </summary>
        public static WindowMode CurrentWindowMode { get; set; } = WindowMode.Normal;
    }
    public static class FormHelper
    {
        public static WindowMode GetWindowMode(this Form form)
        {
            if (form.WindowState == FormWindowState.Minimized)
                return WindowMode.Minimized;

            // Nếu đang ở chế độ fullscreen (tối đa và không có border)
            if (form.WindowState == FormWindowState.Maximized && form.FormBorderStyle == FormBorderStyle.None)
                return WindowMode.FullScreen;

            if (form.WindowState == FormWindowState.Maximized)
                return WindowMode.Maximized;

            return WindowMode.Normal;
        }

    }

}
