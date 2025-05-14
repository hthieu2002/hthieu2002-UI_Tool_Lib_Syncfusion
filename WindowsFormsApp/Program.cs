using AccountCreatorForm.Views;
using Syncfusion.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
            SyncfusionLicenseProvider.RegisterLicense("Mzg2MTI1OUAzMjM5MmUzMDJlMzAzYjMyMzkzYkMvelVybFp0WHVXT25wd0tFNHhXR3RlM2VvalpjLzhxOFBERDN2S1RFanc9");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
