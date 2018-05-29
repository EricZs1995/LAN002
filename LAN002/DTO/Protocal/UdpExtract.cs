using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("UDP")]
    public class UdpExtract
    {
        [PrimaryKey]
        [ForeignKey(typeof(IPExtract))]
        public int CaptureID { get; set; }
        [Required]
        public int SourcePort { get; set; }
        [Required]
        public int DestinationPort { get; set; }
        [Required]
        public  int Length { get;  set; }
        [Required]
        public int Checksum { get; set; }
        [Required]
        public int DataLength { get; set; }

    }
}
