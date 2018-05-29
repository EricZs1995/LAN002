using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("IP")]
    public class IPExtract
    {
        [PrimaryKey]
        [ForeignKey(typeof(EthernetExtract))]
        public int CaptureID { get; set; }

        [Required]
        public int Version { get; set; }
        [Required]
        public int HeaderLength { get; set; }
        [Required]
        [StringLength(20)]
        public string TypeOfService { get; set; }
        [Required]
        public int TotalLength { get; set; }
        [Required]
        public int FragmentOffset { get; set; }
        [Required]
        public int TimeToLive { get; set; }
        [Required]
        [StringLength(10)]
        public string Protocol { get; set; }
        [Required]
        public int Checksum { get; set; }
        [Required]
        [StringLength(20)]
        public string SourceAddress { get; set; }
        [Required]
        [StringLength(20)]
        public string DestinationAddress { get; set; }

    }
}
