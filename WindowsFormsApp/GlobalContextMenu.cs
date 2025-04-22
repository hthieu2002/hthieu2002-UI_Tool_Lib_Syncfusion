using AccountCreatorForm.Views;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WindowsFormsApp;
using WindowsFormsApp.Properties;

public static class GlobalContextMenu
{
    public static ContextMenuStrip ContextMenu = new ContextMenuStrip();
    private static Home homeForm;
    private static int countDevice;
    public static void SetHomeForm(Home form)
    {
        homeForm = form;
        UpdateContextMenu();
    }

    static GlobalContextMenu()
    {
        UpdateContextMenu();
    }

    public static void setCountDevice(int count)
    {
        countDevice = count;
    }
    public static void UpdateContextMenu()
    {
        ToolStripMenuItem reloadItem;
        ToolStripMenuItem restartItem;
        ToolStripMenuItem apkItem;
        ToolStripMenuItem screenshotItem;
        ToolStripMenuItem adbItem;
        ToolStripMenuItem deleteItem;

        ContextMenu.Items.Clear();

        if (homeForm?.currentChildForm is ScreenView screenView)
        {
            int deviceCount = screenView.deviceDisplays.Count;

            reloadItem = new ToolStripMenuItem("Reload");
            restartItem = new ToolStripMenuItem($"Khởi động lại {deviceCount} thiết bị");
            apkItem = new ToolStripMenuItem($"Cài đặt APK cho {deviceCount} thiết bị");
            screenshotItem = new ToolStripMenuItem($"Chụp màn hình cho {deviceCount} thiết bị");
            adbItem = new ToolStripMenuItem($"Thực hiện lệnh ADB cho {deviceCount} thiết bị");
            deleteItem = new ToolStripMenuItem($"Reload {deviceCount} thiết bị");

            reloadItem.Click += (sender, e) => { MessageBox.Show("Reload clicked in ScreenView"); };
            restartItem.Click += (sender, e) => screenView.RestartAllDevices_Click(screenView);
            apkItem.Click += (sender, e) => screenView.InstallAPK_Click(screenView);
            screenshotItem.Click += (sender, e) => screenView.CaptureScreenshot_Click(screenView);
            adbItem.Click += (sender, e) => screenView.ExecuteAdbCommand_Click(screenView);
            deleteItem.Click += (sender, e) => screenView.DeleteAllDevices_Click(screenView);

            reloadItem.Image = Resources.reload;
            restartItem.Image = Resources.resartPhone;
            apkItem.Image = Resources.apk;
            screenshotItem.Image = Resources.chupmanhinh;
            adbItem.Image = Resources.adb1;
            deleteItem.Image = Resources.reload;

            ContextMenu.Items.AddRange(new ToolStripItem[] { restartItem, apkItem, screenshotItem, adbItem, deleteItem });

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
