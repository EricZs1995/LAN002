using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{

    [Alias("pcap_comp")]
    public class PacketCompInfo
    {

        public PacketCompInfo(int no, int frameNum, int framelentgh, int capturelength, string epochTime, string sourceMac, string destMac, string typ, string source, string dest, int sourcePort, int destPort, string tim, int headerLength, int timeToLive, string protocol, string flags)
        {
            No = no;
            FrameNum = frameNum;
            Framelentgh = framelentgh;
            Capturelength = capturelength;
            EpochTime = epochTime;
            SourceMac = sourceMac;
            DestMac = destMac;
            Source = source;
            Type = typ;
            Dest = dest;
            SourcePort = sourcePort;
            DestPort = destPort;
            TimeVal = tim;
            HeaderLength = headerLength;
            TimeToLive = timeToLive;
            Protocol = protocol;
            Flags = flags;
        }

        [AutoIncrement]                                 // Auto Insert Id assigned by RDBMS
        public int Id { get; set; }
        //No, frameNum, framelentgh, capturelength, sourceMac, destMac, Typ, source, dest, sourcePort, destPort, Tim, HeaderLength, TimeToLive, protocol, Flags
        public int No { get; set; } 
        public int FrameNum { get; set; }
        public int Framelentgh { get; set; }
        public string EpochTime { get; set; }
        public int Capturelength { get; set; }
        public string SourceMac { get; set; }
        public string DestMac { get; set; }
        public string Type { get; set; }//Ip层类型
        public string Source { get; set; }
        public string Dest { get; set; }
        public int SourcePort { get; set; }
        public int DestPort { get; set; }
        public string TimeVal { get; set; }
        public int HeaderLength { get; set; }
        public int TimeToLive { get; set; }
        public string Protocol { get; set; }
        public string Flags { get; set; }
    }
}
