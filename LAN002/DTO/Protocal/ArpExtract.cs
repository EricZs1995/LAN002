using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("ARP")]
    public class ArpExtract
    {
        [PrimaryKey]
        [ForeignKey(typeof(EthernetExtract))]
        public int CaptureID { get; set; }
        [Required]
        [StringLength(20)]
        public string HardwareAddressType { get; set; }
        [Required]
        [StringLength(20)]
        public string ProtocolAddressType { get; set; }
        [Required]
        public int HardwareAddressLength { get; set; }
        [Required]
        public int ProtocolAddressLength { get; set; }
        [Required]
        [StringLength(20)]
        public string Operation { get; set; }
        [Required]
        [StringLength(20)]
        public string SenderProtocolAddress { get; set; }
        [Required]
        [StringLength(20)]
        public string TargetProtocolAddress { get; set; }
        [Required]
        [StringLength(20)]
        public string SenderHardwareAddress { get; set; }
        [Required]
        [StringLength(20)]
        public string TargetHardwareAddress { get; set; }
    }
}
