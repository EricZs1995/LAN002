using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("TCP")]
    public class TcpExtract
    {
        [PrimaryKey]
        [ForeignKey(typeof(IPExtract))]
        public int CaptureID { get; set; }
        [Required]
        public  int SourcePort { get; set; }
        [Required]
        public  int DestinationPort { get; set; }
        [Required]
        public long SequenceNumber { get; set; }
        [Required]
        public long AcknowledgmentNumber { get; set; }
        [Required]
        public int HeaderLength { get; set; }
        [Required]
        public  bool Fin { get; set; }
        [Required]
        public  bool Syn { get; set; }
        [Required]
        public  bool Rst { get; set; }
        [Required]
        public  bool Psh { get; set; }
        [Required]
        public  bool Ack { get; set; }
        [Required]
        public  bool Urg { get; set; }
        [Required]
        public int WindowSize { get; set; }
        [Required]
        public int Checksum { get; set; }
        [Required]
        public int DataLength { get; set; }
    }
}
