using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    [Alias("stat")]
    public class StatisticsInfo
    {
        public StatisticsInfo(int fraLen, double seconds, string linkType, string sMac, string dMac, int linkHeadLen, string ethType, string sIp, string dIp, int ipTotalLen, int ipHeadLen, string ipType, int sPort, int dPort, int tranTotalLen, int tranHeadLen, int dataLen, bool compMac,bool compIP)
        {
            FrameLen = fraLen;
            Seconds = seconds;
            LinkType = linkType;
            SourceMac = sMac;
            DestinationMac = dMac;
            LinkHeadLen = linkHeadLen;
            InterProType = ethType;
            SourceIp = sIp;
            DestinationIp = dIp;
            IpTotalLen = ipTotalLen;
            IpHeadLen = ipHeadLen;
            TranType = ipType;
            SourcePort = sPort;
            DestinationPort = dPort;
            TranTotalLen = tranTotalLen;
            TranHeadLen = tranHeadLen;
            DataLen = dataLen;
            CompMac = CompMac;
            CompIp = compIP;
        }

        [AutoIncrement]                                 // Auto Insert Id assigned by RDBMS
        public int Id { get; set; }
        public int FrameLen { get; set; }
        public double Seconds { get; set; }
        public string LinkType { get; set; }
        public string SourceMac { get; set; }
        public string DestinationMac { get; set; }
        public int LinkHeadLen { get; set; }
        public string InterProType { get; set; }
        public string SourceIp { get; set; }
        public string DestinationIp { get; set; }
        public int IpTotalLen { get; set; }
        public int IpHeadLen { get; set; }
        public string TranType { get; set; }
        public int SourcePort { get; set; }
        public int DestinationPort { get; set; }
        public int TranTotalLen { get; set; }
        public int TranHeadLen { get; set; }
        public int DataLen { get; set; }
        public bool CompMac { get; set; }
        public bool CompIp { get; set; }

    }
}
