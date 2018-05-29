using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    public class RealChartItem
    {


        public RealChartItem(int seconds, DateTime dateTime, int v, int fraLen)
        {
            Second = seconds;
            DateTime = dateTime;
            Count = v;
            SumLen = fraLen;
        }

        public int Second { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
        public int SumLen { get; set; }
    }
}
