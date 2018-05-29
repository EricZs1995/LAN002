using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("Frame")]
    public class FrameExtract
    {
        [PrimaryKey]
        public int CaptureID { get; set; }
        [Required]
        [ForeignKey(typeof(AdapterInfo))]
        public int AdapterID { get; set; }
        [Required]
        public DateTime CaptureTime { get; set; }
        [Required]
        public int FrameLength { get; set; }
        [Required]
        [StringLength(20)]
        public string LinkLayerType { get; set; }
    }
}
