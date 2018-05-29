using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    public class PointStatTranResult
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public int Packets { get; set; }
        public int Bytes { get; set; }
        public int TxPackets { get; set; }
        public int TxBytes { get; set; }
        public int RxPackets { get; set; }
        public int RxBytes { get; set; }

        public override string ToString()
        {
            return string.Format("Address: {0}, Port: {1}, Packets: {2}, Bytes: {3}, TxPackets: {4}, TxBytes: {5}, RxPackets: {6}, RxBytes: {7}", Address, Port, Packets, Bytes, TxPackets, TxBytes, RxPackets, RxBytes);
        }
    }
}
