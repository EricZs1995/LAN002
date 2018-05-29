using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("ICMP")]
    public class IcmpExtract
    {
        [PrimaryKey]
        [ForeignKey(typeof(IPExtract))]
        public int CaptureID { get; set; }
        [Required]
        [StringLength(30)]
        public string Type { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public int Checksum { get; set; }
        [Required]
        public int Sequence { get; set; }
    }
}
