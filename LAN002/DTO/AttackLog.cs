using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO
{
    [Alias("AttackLog")]
    public class AttackLog
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        [Required]
        [ForeignKey(typeof(AdapterInfo))]
        public int AdapterID { get; set; }

        public DateTime AttackTime { get; set; }

        public string AttackInfo { get; set; }
    }
}
