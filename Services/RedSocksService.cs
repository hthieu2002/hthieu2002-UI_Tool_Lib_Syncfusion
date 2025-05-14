using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace Services
{
    [Obfuscation(Exclude = false)]
    public class RedSocksService
    {
        // install redsocks and iptables first
        private static string defaultRedsocksFileName = "redsocks.conf";
        private static string defaultRedsocksLocalPort = "12345";

        public static void setUpRedSocksOnDevice(string redsocksDir, string deviceId)
        {
            // copy redsocks binary to device
            // copy iptables binary to device if not existed
            Dictionary<string, string> files = new Dictionary<string, string>();
            files.Add(string.Concat(Directory.GetCurrentDirectory(), @"\Resources\iptables"), redsocksDir);
            files.Add(string.Concat(Directory.GetCurrentDirectory(), @"\Resources\redsocks"), redsocksDir);
            ADBService.pushFiles(files, deviceId);
            ADBService.setPermission("555", redsocksDir + "/redsocks", deviceId);
        }

        public static void start(string proxyHostIP, int proxyHostPort, string redsocksDir, string deviceId, string username = "", string password = "", string defaultConnectionMethod = "socks5")
        {
            string hostIP = Dns.GetHostAddresses(proxyHostIP)[0].ToString();
            // 1. create a textfile redsocks.conf with IP and Port. By default, local redsocks port = 12345, type = socks5
            string config = string.Format("base {{log_debug = off;log_info = off;log = \"stderr\";daemon = on;redirector = iptables;}}\n" +
                "redsocks {{ local_ip = 0.0.0.0; local_port = {0}; ip = {1}; port = {2}; type = {3}; }}\n" +
                "redudp {{ local_ip = 0.0.0.0; local_port = 10053; ip = {1}; port = {2}; dest_ip = 8.8.8.8; dest_port = 53; udp_timeout = 30; udp_timeout_stream = 180; }} "
                , defaultRedsocksLocalPort
                , hostIP
                , proxyHostPort
                , defaultConnectionMethod);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                config = string.Format("base {{log_debug = off;log_info = off;log = \"stderr\";daemon = on;redirector = iptables; }}\n" +
                "redsocks {{ local_ip = 0.0.0.0; local_port = {0}; ip = {1}; port = {2}; type = {3}; login = \"{4}\"; password = \"{5}\"; }}\n" +
                "redudp {{ local_ip = 0.0.0.0; local_port = 10053; ip = {1}; port = {2}; login = \"{4}\"; password = \"{5}\"; dest_ip = 8.8.8.8; dest_port = 53; udp_timeout = 30; udp_timeout_stream = 180; }} "
                , defaultRedsocksLocalPort
                , hostIP
                , proxyHostPort
                , defaultConnectionMethod
                , username
                , password);
            }
            using (StreamWriter sw = File.CreateText(defaultRedsocksFileName))
            {
                sw.Write(config);
            }
            // 2. startRedSocks from adb
            var redsocksFileConfigPath = string.Concat(Directory.GetCurrentDirectory(), @"\", defaultRedsocksFileName);
            // push file redsocks.conf to the device
            ADBService.pushFiles(new Dictionary<string, string>() { { redsocksFileConfigPath, redsocksDir } }, deviceId);
            ADBService.startRedSocks(hostIP, redsocksDir, deviceId);
        }

        public static void stop(string deviceId)
        {
            ADBService.stopRedSocks(deviceId);
        }
    }


}
