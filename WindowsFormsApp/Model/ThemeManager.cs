using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.Model
{
    public static class ThemeManager
    {
        public static bool IsDarkMode { get; set; } = false;

        public static void ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            ApplyTheme();
        }

        public static void ApplyTheme()
        {
            // Màu nền và màu chữ cơ bản cho Dark Mode
            Color backgroundColor = IsDarkMode ? Color.FromArgb(33, 33, 33) : Color.White;
            Color textColor = IsDarkMode ? Color.White : Color.Black;
            Color buttonBackColor = IsDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;
            Color buttonForeColor = IsDarkMode ? Color.White : Color.Black;
            Color hoverBackColor = IsDarkMode ? Color.FromArgb(67, 67, 70) : Color.FromArgb(240, 240, 240);
            Color controlBackgroundColor = IsDarkMode ? Color.FromArgb(45, 45, 48) : Color.White;

            // Lặp qua tất cả các form đang mở và áp dụng theme
            foreach (Form form in Application.OpenForms)
            {
                ApplyFormTheme(form, backgroundColor, textColor, buttonBackColor, buttonForeColor, hoverBackColor, controlBackgroundColor);
            }
        }

        private static void ApplyFormTheme(Form form, Color backgroundColor, Color textColor, Color buttonBackColor, Color buttonForeColor, Color hoverBackColor, Color controlBackgroundColor)
        {
            // Thay đổi màu sắc của form
            form.BackColor = backgroundColor;

            // Lặp qua các control trong form và thay đổi màu sắc của chúng
            foreach (Control control in form.Controls)
            {
                ApplyControlTheme(control, backgroundColor, textColor, buttonBackColor, buttonForeColor, hoverBackColor, controlBackgroundColor);
            }
        }

        private static void ApplyControlTheme(Control control, Color backgroundColor, Color textColor, Color buttonBackColor, Color buttonForeColor, Color hoverBackColor, Color controlBackgroundColor)
        {
            // Kiểm tra nếu control là Button
            if (control is Button btn)
            {
                // Nếu đúng, ta có thể truy cập các thuộc tính của Button, bao gồm FlatAppearance
                btn.BackColor = buttonBackColor;
                btn.ForeColor = buttonForeColor;
                btn.FlatAppearance.MouseOverBackColor = hoverBackColor;  // Đặt màu hover
                btn.FlatAppearance.MouseDownBackColor = hoverBackColor;  // Đặt màu khi click
            }
            else if (control is Label lbl)
            {
                lbl.ForeColor = textColor;  // Thay đổi màu chữ cho Label
            }
            else if (control is TextBox || control is RichTextBox)
            {
                control.BackColor = controlBackgroundColor;
                control.ForeColor = textColor;
            }
            else if (control is PictureBox)
            {
                control.BackColor = controlBackgroundColor;
            }
            else if (control is Panel)
            {
                control.BackColor = controlBackgroundColor;
            }

            // Áp dụng theme cho các control con
            foreach (Control childControl in control.Controls)
            {
                ApplyControlTheme(childControl, backgroundColor, textColor, buttonBackColor, buttonForeColor, hoverBackColor, controlBackgroundColor);
            }
        }

    }

}
