using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    [Alias("Adapter")]
    public class AdapterInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string FriendlyName { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(20)]
        public string GatewayAddress { get; set; }

        [StringLength(20)]
        public string Netmask { get; set; }
        [Required]
        [StringLength(20)]
        public string MacAddress { get; set; }

        [StringLength(20)]
        public string IpV4 { get; set; }
    }
}
