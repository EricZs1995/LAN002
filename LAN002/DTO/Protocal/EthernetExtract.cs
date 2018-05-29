using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("Ethernet")]
    public class EthernetExtract
    {
        [PrimaryKey]
        [ForeignKey(typeof(FrameExtract))]
        public int CaptureID { get; set; }
        [Required]
        [StringLength(20)]
        public string SourceHwAddress { get; set; }
        [Required]
        [StringLength(20)]
        public string DestinationHwAddress { get; set; }
        [Required]
        [StringLength(8)]
        public string Type { get; set; }

    }
}
