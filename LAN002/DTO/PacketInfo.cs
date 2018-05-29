using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    [Alias("pcap")]
    public class PacketInfo
    {
        public PacketInfo(int capnum, int fraLen, DateTime dateTime,double seconds, string timeVal, string epochTime, string linkType, string sMac, string dMac, string ethType, string sIp, string dIp, int tTL, int totalLen, int ipHeadLen, int ipChecksum, int version, string ipType, int sPort, int dPort, long seqNum, long ackNum, int tranHeadLen, int windowSize, int tranChecksum, int dataLen, string info, string flag)
        {
            CapNum = capnum;
            FrameLen = fraLen;
            DateTime = dateTime;
            Seconds = seconds;
            TimeVal = timeVal;
            EpochTime = epochTime;
            LinkType = linkType;
            SourceMac = sMac;
            DestinationMac = dMac;
            InterProType = ethType;
            SourceIp = sIp;
            DestinationIp = dIp;
            TTL = TTL;
            TotalLen = totalLen;
            IpHeadLen = ipHeadLen;
            IpChecksum = ipChecksum;
            IpVersion = version;
            TranType = ipType;
            SourcePort = sPort;
            DestinationPort = dPort;
            SeqNum = seqNum;
            AckNum = ackNum;
            TranHeadLen = tranHeadLen;
            WindowSize = windowSize;
            TranChecksum = tranChecksum;
            DataLen = dataLen;
            Info = info;
            Flag = flag;
        }

        [AutoIncrement]                                 // Auto Insert Id assigned by RDBMS
        public int Id { get; set; }
        public int CapNum { get; set; }
        public int FrameLen { get; set; }
        public DateTime DateTime { get; set; }
        public double Seconds { get; set; }
        public string TimeVal { get; set; }
        public string EpochTime { get; set; }
        public string LinkType { get; set; }
        public string SourceMac { get; set; }
        public string DestinationMac { get; set; }
        public string InterProType { get; set; }
        public string SourceIp { get; set; }
        public string DestinationIp { get; set; }
        public int TTL { get; set; }
        public int TotalLen { get; set; }
        public int IpHeadLen { get; set; }
        public int IpChecksum { get; set; }
        public int IpVersion { get; set; }
        public string TranType { get; set; }
        public int SourcePort { get; set; }
        public int DestinationPort { get; set; }
        public long SeqNum { get; set; }
        public long AckNum { get; set; }
        public int TranHeadLen { get; set; }
        public int WindowSize { get; set; }
        public int TranChecksum { get; set; }
        public int DataLen { get; set; }
        public string Info { get; set; }
        public string Flag { get; set; }
    }
}
