using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    public class ConvStatTranResult
    {
        public string AddressA { get; set; }
        public int PortA { get; set; }
        public string AddressB { get; set; }
        public int PortB { get; set; }
        public int Packets { get; set; }
        public double pps { get; set; }
        public int Bytes { get; set; }
        public double bps { get; set; }
        public int PacketAtB { get; set; }
        public double AtBpps { get; set; }
        public int BytesAtB { get; set; }
        public double AtBbbs { get; set; }
        public int PacketBtA { get; set; }
        public double BtApps { get; set; }
        public int BytesBtA { get; set; }
        public double BtAbbs { get; set; }
        public double RelStart { get; set; }
        public double Duration { get; set; }
    }
}
