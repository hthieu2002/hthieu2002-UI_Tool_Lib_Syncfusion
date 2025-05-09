using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp.Model
{
    public static class DeviceCheckManager
    {
        private static CancellationTokenSource _deviceCheckCancellationTokenSource;

        public static void StartDeviceCheck(Func<CancellationToken, Task> deviceCheckLoop)
        {
            _deviceCheckCancellationTokenSource?.Cancel();
            _deviceCheckCancellationTokenSource = new CancellationTokenSource();

            // Đảm bảo phương thức được chạy dưới dạng Task và chờ đợi bất đồng bộ
            Task.Run(() => deviceCheckLoop(_deviceCheckCancellationTokenSource.Token));
        }

        public static void StopDeviceCheck()
        {
            _deviceCheckCancellationTokenSource?.Cancel();
        }
    }

}
