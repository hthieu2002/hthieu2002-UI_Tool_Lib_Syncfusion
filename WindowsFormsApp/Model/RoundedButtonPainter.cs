using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class RoundedButtonPainter
{
    public static void PaintButton(object sender, PaintEventArgs e)
    {
        Button btn = sender as Button;
        if (btn == null) return;

        int radius = 5;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = new Rectangle(
            btn.ClientRectangle.X + 1,
            btn.ClientRectangle.Y + 1,
            btn.ClientRectangle.Width - 2,
            btn.ClientRectangle.Height - 2
        );

        btn.Region = new Region(GetRoundedRect(rect, radius));
        rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

        // VẼ NỀN (không đổi màu khi hover)
        e.Graphics.FillRectangle(new SolidBrush(btn.BackColor), rect);

        // Viền giữ nguyên không đổi (chỉ khác khi Disabled hoặc Focused)
        Pen borderPen = GetButtonBorderPen(btn);
        e.Graphics.DrawPath(borderPen, GetRoundedRect(rect, radius));

        // Màu chữ giữ nguyên không đổi
        Color textColor = btn.Enabled ? btn.ForeColor : Color.Gray;

        Rectangle textRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
        TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private static Pen GetButtonBorderPen(Button btn)
    {
        if (!btn.Enabled)
        {
            return new Pen(Color.Gray);
        }
        else if (btn.Focused)
        {
            return new Pen(Color.Green); // Chỉ viền xanh nếu Focused (Enter, Tab vào)
        }
        else
        {
            return new Pen(Color.Gray);  // Không đổi màu khi hover
        }
    }

    private static GraphicsPath GetRoundedRect(Rectangle rect, int radius)
    {
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
        graphicsPath.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
        graphicsPath.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
        graphicsPath.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
        graphicsPath.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
        graphicsPath.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
        graphicsPath.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
        graphicsPath.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);
        graphicsPath.CloseFigure();
        return graphicsPath;
    }
}
