using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    public class ProtocalStatByLayer
    {
        public int LinkNum { get; set; }
        public int FrameSum { get; set; }
        public int LinkHeadSum { get; set; }
        public int IpV4Num { get; set; }
        public int IpV4HSum { get; set; }
        public int IpV4TSum { get; set; }
        public int IpV4TCPNum { get; set; }
        public int IpV4TCPHSum { get; set; }
        public int IpV4TCPTSum { get; set; }
        public int IpV4UDPNum { get; set; }
        public int IpV4UDPHSum { get; set; }
        public int IpV4UDPTSum { get; set; }
        public int IpV6Num { get; set; }
        public int IpV6HSum { get; set; }
        public int IpV6TSum { get; set; }
        public int IpV6TCPNum { get; set; }
        public int IpV6TCPHSum { get; set; }
        public int IpV6TCPTSum { get; set; }
        public int IpV6UDPNum { get; set; }
        public int IpV6UDPHSum { get; set; }
        public int IpV6UDPTSum { get; set; }
        public int ArpNum { get; set; }
        public int ArpHSum { get; set; }
        public int ArpTSum { get; set; }
        public double Duration { get; set; }


    }
}
