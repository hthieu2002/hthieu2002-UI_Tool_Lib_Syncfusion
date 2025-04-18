using AccountCreatorForm.Views;
using System.Windows.Forms;
using WindowsFormsApp;

public static class GlobalContextMenu
{
    public static ContextMenuStrip ContextMenu = new ContextMenuStrip();
    private static Home homeForm;

    public static void SetHomeForm(Home form)
    {
        homeForm = form;
        UpdateContextMenu();
    }

    static GlobalContextMenu()
    {
        UpdateContextMenu();
    }

    private static void UpdateContextMenu()
    {
        ToolStripMenuItem reloadItem;
        ToolStripMenuItem restartItem;
        ToolStripMenuItem apkItem;
        ToolStripMenuItem screenshotItem;
        ToolStripMenuItem adbItem;
        ToolStripMenuItem deleteItem;

        ContextMenu.Items.Clear();

        if (homeForm?.currentChildForm is ScreenView)
        {
            reloadItem = new ToolStripMenuItem("Reload");
            restartItem = new ToolStripMenuItem("Khởi động lại 0 thiết bị");
            apkItem = new ToolStripMenuItem("Cài đặt APK");
            screenshotItem = new ToolStripMenuItem("Chụp màn hình");
            adbItem = new ToolStripMenuItem("Lệnh Adb");
            deleteItem = new ToolStripMenuItem("Xóa 0 thiết bị");

            reloadItem.Click += (sender, e) => { MessageBox.Show("Reload clicked in ScreenView"); };
            restartItem.Click += (sender, e) => { MessageBox.Show("Restart clicked in ScreenView"); };
            apkItem.Click += (sender, e) => { MessageBox.Show("APK clicked in ScreenView"); };
            screenshotItem.Click += (sender, e) => { MessageBox.Show("Screenshot clicked in ScreenView"); };
            adbItem.Click += (sender, e) => { MessageBox.Show("ADB clicked in ScreenView"); };
            deleteItem.Click += (sender, e) => { MessageBox.Show("Delete clicked in ScreenView"); };

            ContextMenu.Items.AddRange(new ToolStripItem[] { reloadItem, restartItem, apkItem, screenshotItem, adbItem, deleteItem });
        }
        else if (homeForm?.currentChildForm is ViewChange)
        {
            reloadItem = new ToolStripMenuItem("Reset data");
            var autoItem = new ToolStripMenuItem("Tự động hóa cho 0 thiết bị");
            var copyItem = new ToolStripMenuItem("Copy ID 0 devices");
            restartItem = new ToolStripMenuItem("Khởi động lại 0 thiết bị");
            apkItem = new ToolStripMenuItem("Cài đặt APK");
            screenshotItem = new ToolStripMenuItem("Chụp màn hình");
            var proxyItem = new ToolStripMenuItem("Thay đổi proxy");
            adbItem = new ToolStripMenuItem("Lệnh Adb");
            var textShortcutItem = new ToolStripMenuItem("Gõ nhanh văn bản");
            deleteItem = new ToolStripMenuItem("Xóa 0 thiết bị");
            var changeImageItem = new ToolStripMenuItem("Thay đổi hình nền theo số thứ tự");
            var changeOrderItem = new ToolStripMenuItem("Thay đổi số thứ tự thiết bị");
            var addToGroupItem = new ToolStripMenuItem("Thêm vào nhóm");

            reloadItem.Click += (sender, e) => { MessageBox.Show("Reset data clicked in ViewChange"); };
            autoItem.Click += (sender, e) => { MessageBox.Show("Auto clicked in ViewChange"); };
            copyItem.Click += (sender, e) => { MessageBox.Show("Copy clicked in ViewChange"); };

            ContextMenu.Items.AddRange(new ToolStripItem[] { reloadItem, autoItem, copyItem, restartItem, apkItem, screenshotItem, proxyItem, adbItem, textShortcutItem, deleteItem, changeImageItem, changeOrderItem, addToGroupItem });
        }
    }
}
