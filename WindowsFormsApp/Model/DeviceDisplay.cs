using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public class DeviceDisplay
    {
        public string Serial { get; set; }
        public Process ScrcpyProcess { get; set; }
        public IntPtr ScrcpyWindow { get; set; }
        public Panel DevicePanel { get; set; }
    }
}
