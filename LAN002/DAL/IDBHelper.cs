using LAN002.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DAL
{
    public interface IDBHelper
    {
        List<PacketSimpInfo> GetList(int limit);

        string Name { get; }

        List<PacketSimpInfo> GetListSingleContent(int limit);

    }
}
