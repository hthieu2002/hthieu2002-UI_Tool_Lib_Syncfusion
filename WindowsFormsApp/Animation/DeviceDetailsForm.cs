using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.Animation
{
    public partial class DeviceDetailsForm : Form
    {
        public DeviceDetailsForm()
        {
            InitializeComponent();
        }

        public DeviceDetailsForm(string device)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.StartPosition = FormStartPosition.CenterScreen;

            string brand = GetDeviceInfoFromADB(device, "getprop ro.product.brand");
            string name = GetDeviceInfoFromADB(device, "getprop ro.android.board");
            string model = GetDeviceInfoFromADB(device, "getprop ro.product.model");
            string os1 = GetDeviceInfoFromADB(device, "getprop ro.build.version.release");
            string os2 = GetDeviceInfoFromADB(device, "getprop ro.build.version.sdk");
            string country = GetDeviceInfoFromADB(device, "settings get global mi_sim_operator_country");
            string sim = GetDeviceInfoFromADB(device, "settings get global mi_sim_operator_name");
            string serial = GetDeviceInfoFromADB(device, "getprop ro.serialno");
            string codeSim = GetDeviceInfoFromADB(device, "settings get global mi_sim_operator_numeric");
            string phone = GetDeviceInfoFromADB(device, "settings get global mi_line1_number");
            string imei = GetDeviceInfoFromADB(device, "settings get global mi_imei_number");
            string imsi = GetDeviceInfoFromADB(device, "settings get global mi_imsi");
            string iccid = GetDeviceInfoFromADB(device, "settings get global mi_iccid");
            string mac = GetDeviceMACAddress(device);

            this.Text = $"Thông tin thiết bị {device}";
            txtBrand.Text = brand;
            txtName.Text = name;
            txtModel.Text = model;
            txtOs1.Text = os1;
            txtOs2.Text = os2;
            txtCountry.Text = country;
            txtSIm.Text = sim;
            txtSerial.Text = serial;
            txtCodeSim.Text = codeSim;
            txtPhone.Text = phone;
            txtImei.Text = imei;
            txtImsi.Text = imsi;
            txtIccid.Text = iccid;
            txtMac.Text = mac;
        }

        private string GetDeviceInfoFromADB(string deviceID, string property)
        {
            string result = ADBService.ExecuteADBCommandDetail(deviceID, $"shell {property}");
            return result.Trim();
        }
        private string GetDeviceMACAddress(string deviceID)
        {
            string result = ADBService.ExecuteADBCommandDetail(deviceID, "shell cat /sys/class/net/wlan0/address");
            return result.Trim();
        }


    }
}
