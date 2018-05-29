using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    public class PacketStatByLength
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int TotalNum { get; set; }
        public int Num { get; set; }
        public int SumLen { get; set; }
        public int MaxVal { get; set; }
        public int MinVal { get; set; }
        public double Duration { get; set; }
        public double Rate { get; set; }
        public double PercentNum { get; set; }
        public double Average { get; set; }

    }
}
