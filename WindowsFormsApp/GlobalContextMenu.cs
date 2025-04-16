using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public static class GlobalContextMenu
    {
        // Tạo một ContextMenuStrip chung
        public static ContextMenuStrip ContextMenu = new ContextMenuStrip();

        static GlobalContextMenu()
        {
            // Thêm các mục vào ContextMenu
            ToolStripMenuItem reloadItem = new ToolStripMenuItem("Reload");
            ToolStripMenuItem autoItem = new ToolStripMenuItem("Tự động hóa cho 0 thiết bị");
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy ID 0 devices");
            ToolStripMenuItem restartItem = new ToolStripMenuItem("Khởi động lại 0 thiết bị");
            ToolStripMenuItem apkItem = new ToolStripMenuItem("Cài đặt APK");
            ToolStripMenuItem screenshotItem = new ToolStripMenuItem("Chụp màn hình");
            ToolStripMenuItem proxyItem = new ToolStripMenuItem("Thay đổi proxy");
            ToolStripMenuItem adbItem = new ToolStripMenuItem("Lệnh Adb");
            ToolStripMenuItem textShortcutItem = new ToolStripMenuItem("Gõ nhanh văn bản");
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Xóa 0 thiết bị");
            ToolStripMenuItem changeImageItem = new ToolStripMenuItem("Thay đổi hình nền theo số thứ tự");
            ToolStripMenuItem changeOrderItem = new ToolStripMenuItem("Thay đổi số thứ tự thiết bị");
            ToolStripMenuItem addToGroupItem = new ToolStripMenuItem("Thêm vào nhóm");

            // Gán sự kiện cho các mục menu
            reloadItem.Click += (sender, e) => { MessageBox.Show("Reload clicked"); };
            autoItem.Click += (sender, e) => { MessageBox.Show("Auto clicked"); };
            copyItem.Click += (sender, e) => { MessageBox.Show("Copy clicked"); };
            restartItem.Click += (sender, e) => { MessageBox.Show("Restart clicked"); };
            apkItem.Click += (sender, e) => { MessageBox.Show("APK clicked"); };
            screenshotItem.Click += (sender, e) => { MessageBox.Show("Screenshot clicked"); };
            proxyItem.Click += (sender, e) => { MessageBox.Show("Proxy clicked"); };
            adbItem.Click += (sender, e) => { MessageBox.Show("ADB clicked"); };
            textShortcutItem.Click += (sender, e) => { MessageBox.Show("Text shortcut clicked"); };
            deleteItem.Click += (sender, e) => { MessageBox.Show("Delete clicked"); };
            changeImageItem.Click += (sender, e) => { MessageBox.Show("Change image clicked"); };
            changeOrderItem.Click += (sender, e) => { MessageBox.Show("Change order clicked"); };
            addToGroupItem.Click += (sender, e) => { MessageBox.Show("Add to group clicked"); };

            // Thêm các mục vào ContextMenu
            ContextMenu.Items.AddRange(new ToolStripItem[]
            {
            reloadItem, autoItem, copyItem, restartItem, apkItem, screenshotItem,
            proxyItem, adbItem, textShortcutItem, deleteItem, changeImageItem,
            changeOrderItem, addToGroupItem
            });
        }
    }
}
