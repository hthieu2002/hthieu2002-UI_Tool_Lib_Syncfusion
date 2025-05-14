using Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp.Script.RoslynTest;

public class EventRoslyn
{
    RoslynStringProcessing roslyn = new RoslynStringProcessing();
    public EventRoslyn()
    {
        // Khởi tạo tài nguyên nếu có
    }

    // Phương thức xử lý ClickXY
    public async Task HandleClickXY(string input, string deviceId)
    {
        try
        {
            var coordinates = roslyn.getParseClickCoordinates(input);
            string scrcpyPath = Path.Combine(Application.StartupPath, "Resources", "scrcpy.exe");

            if (File.Exists(scrcpyPath))
            {
                string command = $"adb -s {deviceId} shell input tap {coordinates.Item1} {coordinates.Item2}";
                await ADBService.ExecuteAdbCommand(command);
            }
            else
            {
                Console.WriteLine("scrcpy.exe không tìm thấy.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi thực thi lệnh ClickXY: " + ex.Message);
        }
    }
    public async Task HandleSwipe(string input , string deviceId)
    {

        var coordinates = roslyn.getParseSwipeCoordinates(input);
        string scrcpyPath = Path.Combine(Application.StartupPath, "Resources", "scrcpy.exe");
        try
        {
            if (File.Exists(scrcpyPath))
            {
                string command = $"adb -s {deviceId} shell input swipe {coordinates.Item1} {coordinates.Item2} {coordinates.Item3} {coordinates.Item4} {coordinates.Item5}";
                await ADBService.ExecuteAdbCommand(command);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi thực thi lệnh adb swipe: " + ex.Message);
        }
    }
}
