using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    public class PointStatMacORIpResult
    {
        public string Address { get; set; }
        public int Packets { get; set; }
        public int Bytes { get; set; }
        public int TxPackets { get; set; }
        public int TxBytes { get; set; }
        public int RxPackets { get; set; }
        public int RxBytes { get; set; }

        public override string ToString()
        {
            return string.Format("Address: {0}, Packets: {1}, Bytes: {2}, TxPackets: {3}, TxBytes: {4}, RxPackets: {5}, RxBytes: {6}", Address, Packets, Bytes, TxPackets, TxBytes, RxPackets, RxBytes);
        }

    }
}
