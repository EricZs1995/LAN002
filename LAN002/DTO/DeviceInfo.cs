using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    class DeviceInfo
    {

        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public string Description { get; set; }

        public string GatewayAddress { get; set; }

        public string Netmask { get; set; }

        public string MacAddress { get; set; }

        public string IpV4 { get; set; }
    }
}
