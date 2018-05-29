using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAN002.Service;
using System.IO;

namespace LAN002.DTO
{
    public static class Globals
    {
        private static ICaptureDevice device;
        private static DeviceInfo deviceInfo;
        private static DevicesService devicesService;
        private static int index;
        private static DateTime startTime;
        public static int Index { get => index; set => index = value; }
        public static DateTime StartTime { get => startTime; set => startTime = value; }
        internal static ICaptureDevice Device { get => device; set => device = value; }
        internal static DeviceInfo DeviceInfo { get => deviceInfo; set => deviceInfo = value; }
        internal static DevicesService DevicesService { get => devicesService; set => devicesService = value; }

        public static long ipToLong(String strIP)
        {
            int j = 0;
            int i = 0;
            long[] ip = new long[4];
            int position1 = strIP.IndexOf(".");
            int position2 = strIP.IndexOf(".", position1 + 1);
            int position3 = strIP.IndexOf(".", position2 + 1);
            ip[0] = long.Parse(strIP.Substring(0, position1));
            ip[1] = long.Parse(strIP.Substring(position1 + 1, position2 - position1 - 1));
            ip[2] = long.Parse(strIP.Substring(position2 + 1, position3 - position2 - 1));
            ip[3] = long.Parse(strIP.Substring(position3 + 1));
            return (ip[0] << 24) + (ip[1] << 16) + (ip[2] << 8) + ip[3];
        }

        public static string sqlToString(string sqlFileName)
        {
            string path = @"E:\source\repos\LAN002\LAN002\DAL\Sql\";
            //if (!path.EndsWith(@"\"))
            //{
            //    path += @"\";
            //}
            path += sqlFileName;
            //Console.WriteLine("path:{0}", path);
            string script = string.Empty;
            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                script = file.OpenText().ReadToEnd();
                //Console.WriteLine("info:{0}", script);
            }
            return script;
        }
    }
}
