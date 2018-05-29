using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    [Alias("CaptureLog")]
    public class CaptureLog
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string AdapterName { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public long Packets { get; set; }
        [Required]
        public long Bytes { get; set; }
        

    }
}
