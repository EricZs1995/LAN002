
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    //[OrmLiteTable("pcapsimp")]
    [Alias("pcap_simp")]
    public class PacketSimpInfo
    {


        public PacketSimpInfo(int capnum, string timeVal, string sIp, string dIp, string tranType, int fraLen, string info)
        {
            this.No = capnum;
            this.Time = timeVal;
            this.Source = sIp;
            this.Destination = dIp;
            this.Protocol = tranType;
            this.Length = fraLen;
            this.Info = info;
        }

        //[AutoIncrement]                                 // Auto Insert Id assigned by RDBMS
        //public int Id { get; set; }

        public int No { get; set; }

        public string Time { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string Protocol { get; set; }

        public int Length { get; set; }

        public string Info { get; set; }
        
    }
}
