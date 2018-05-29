using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    [Alias("BlackList")]
    public class BlackList
    {

        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string IP { get; set; }
        [Required]
        public int SynCount { get; set; }
        [Required]
        public DateTime LastUpdateTime { get; set; }

    }
}
