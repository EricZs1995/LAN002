using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    [Alias("rt_data")]
    public class RealTimeInfo
    {
        public RealTimeInfo(int capnum, DateTime dateTime,double seconds, string ethType, int fraLen, string ipType)
        {
            CaptNum = capnum;
            DateTime = dateTime;
            Seconds = seconds;
            InterProType = ethType;
            FrameLen = fraLen;
            TranType = ipType;
        }

        [AutoIncrement]                                 // Auto Insert Id assigned by RDBMS
        public int Id { get; set; }

        public int CaptNum { get; set; }
        public DateTime DateTime { get; set; }
        public double Seconds { get; set; }
        public string InterProType { get; set; }
        public int FrameLen { get; set; }
        public string TranType { get; set; }
    }
}
