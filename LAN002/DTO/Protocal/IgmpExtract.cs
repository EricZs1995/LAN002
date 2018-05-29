using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DTO.Protocal
{
    [Alias("IGMP")]
    public class IgmpExtract
    {
        [PrimaryKey]
        [ForeignKey(typeof(IPExtract))]
        public int CapNum { get; set; }


    }
}
