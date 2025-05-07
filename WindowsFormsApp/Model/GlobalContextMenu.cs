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
    public static void UpdateContextMenu(int count = 0)
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
        else if (homeForm?.currentChildForm is ViewChange viewChange)
        {
            reloadItem = new ToolStripMenuItem("Reset data");
            reloadItem.Click += (sender, e) => viewChange.SetResertDataInputForm();
            var loadPage = new ToolStripMenuItem("Load");
            //var (onlineDevices, offlineDevices) = viewChange.LoadDevicesFromJson();
            loadPage.Click += (sender, e) => viewChange.LoadContextMenu();
            var autoItem = new ToolStripMenuItem($"Tự động hóa cho {count} thiết bị");
            var copyItem = new ToolStripMenuItem($"Copy ID {count} devices");
            restartItem = new ToolStripMenuItem($"Khởi động lại {count} thiết bị");
            var proxyItem = new ToolStripMenuItem($"Thay đổi proxy cho {count} thiết bị");
            adbItem = new ToolStripMenuItem("Lệnh Adb");
            var textShortcutItem = new ToolStripMenuItem("Gõ nhanh văn bản");
            deleteItem = new ToolStripMenuItem($"Xóa {count} thiết bị");
            var changeImageItem = new ToolStripMenuItem("Thay đổi hình nền theo số thứ tự");
            var changeOrderItem = new ToolStripMenuItem("Thay đổi số thứ tự thiết bị");
            var addToGroupItem = new ToolStripMenuItem("Thêm vào nhóm");

            ContextMenu.Items.AddRange(new ToolStripItem[] { reloadItem, loadPage, autoItem, copyItem, restartItem, proxyItem, adbItem, textShortcutItem, deleteItem, changeImageItem, changeOrderItem, addToGroupItem });
        }

        
    }

}
