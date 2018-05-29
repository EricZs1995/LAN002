
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Data;
using LAN002.DTO;
using System;
using LAN002.DTO.Protocal;

namespace LAN002.DAL
{
    public class ServiceStackDBHelper// : IDBHelper
    {
        public string Name => "ServiceStack";

        private static ServiceStackDBHelper instance = new ServiceStackDBHelper();

        OrmLiteConnectionFactory dbFactory = DB.CreateSqlServerConnectionFactory();
        OrmLiteConnectionFactory dbFactory1 = DB.CreateSqlServerConnectionFactory1();
        IDbConnection db;
        public ServiceStackDBHelper()
        {
        }

        //获取唯一可用的对象
        public static ServiceStackDBHelper getInstance()
        {
            return instance;
        }
        #region 创建表
        //建表
        public void ConductBefore()
        {
            using (db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<PacketSimpInfo>();

            }
            using (db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<PacketCompInfo>();

            }
            using (db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<PacketInfo>();

            }
            using (db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<RealTimeInfo>();
            }
            using (db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<StatisticsInfo>();
            }


            using (db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<AdapterInfo>();

            }
            //using (db = dbFactory1.Open())
            //{
            //    db.CreateTableIfNotExists<FrameExtract>();

            //}
            //using (db = dbFactory1.Open())
            //{
            //    db.CreateTableIfNotExists<EthernetExtract>();

            //}
            //using (db = dbFactory1.Open())
            //{
            //    db.CreateTableIfNotExists<IPExtract>();
            //}
            //using (db = dbFactory1.Open())
            //{
            //    db.CreateTableIfNotExists<ArpExtract>();
            //}
            //using (db = dbFactory1.Open())
            //{
            //    db.CreateTableIfNotExists<TcpExtract>();

            //}
            //using (db = dbFactory1.Open())
            //{
            //    db.CreateTableIfNotExists<UdpExtract>();

            //}
            //using (db = dbFactory1.Open())
            //{
            //    db.CreateTableIfNotExists<IcmpExtract>();
            //}
            //using (db = dbFactory.Open())
            //{
            //    db.CreateTableIfNotExists<AttackLog>();

            //}
            //using (db = dbFactory.Open())
            //{
            //    db.CreateTableIfNotExists<BlackList>();

            //}
            //using (db = dbFactory.Open())
            //{
            //    db.CreateTableIfNotExists<CaptureLog>();
            //}
        }

        public void InsertAllTable(PacketSimpInfo packetSimpInfo
            , PacketCompInfo packetCompInfo
            , PacketInfo packetInfo
            , RealTimeInfo realTimeInfo)
        {
            using (db = dbFactory.Open())
            {
                db.Insert<PacketSimpInfo>(packetSimpInfo);
                db.Insert<PacketCompInfo>(packetCompInfo);
                db.Insert<PacketInfo>(packetInfo);
                db.Insert<RealTimeInfo>(realTimeInfo);
            }
        }
        #endregion

        public List<CaptureLog> GetCaptureLogs()
        {
            List<CaptureLog> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<CaptureLog>();
                }
                catch (Exception)
                {

                }
                return results;
            }
        }

        public List<BlackList> GetBlackList()
        {
            List<BlackList> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<BlackList>();
                }
                catch (Exception)
                {

                }
                return results;
            }
        }

        public List<AttackLog> GetAttackLog()
        {
            List<AttackLog> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<AttackLog>();
                }
                catch (Exception)
                {

                }
                return results;
            }
        }

        #region 插入数据
        //插入
        public bool InsertSimpPcap(PacketSimpInfo packetSimpInfo)
        {
            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<PacketSimpInfo>(packetSimpInfo);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }
                return false;
            }
        }
        public bool InsertCompPcap(PacketCompInfo packetCompInfo)
        {

            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<PacketCompInfo>(packetCompInfo);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }
                return false;
            }

        }
        public bool InsertPcap(PacketInfo packetInfo)
        {

            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<PacketInfo>(packetInfo);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }
                return false;
            }

        }
        public bool InsertRT(RealTimeInfo realTimeInfo)
        {

            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<RealTimeInfo>(realTimeInfo);
                    //Console.WriteLine("insert: {0}   {1}  flag:{2}", realTimeInfo.DateTime.ToString(), DateTime.Now.ToString(), flag);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }

                return false;
            }

        }

        public bool InsertStatistics(StatisticsInfo statisticsInfo)
        {
            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<StatisticsInfo>(statisticsInfo);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }
                return false;
            }
        }

        public bool InsertCaptureLog(CaptureLog captureLog)
        {
            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<CaptureLog>(captureLog);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }
                return false;
            }
        }

        public bool InsertAttackLog(AttackLog attackLog)
        {
            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<AttackLog>(attackLog);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }
                return false;
            }
        }
        public bool InsertBlackList(BlackList blackList)
        {
            using (db = dbFactory.Open())
            {
                try
                {
                    var flag = db.Insert<BlackList>(blackList);
                    if (flag >= 0) return true;
                }
                catch (Exception)
                {

                }
                return false;
            }
        }
        #endregion


        #region 协议分级统计
        public void GetTransProtocalPercent()
        {
            var q = db.From<PacketSimpInfo>()
                    .GroupBy(x => x.Protocol)
                    .Select(x => new
                    {
                        Name = x.Protocol,
                        Count = Sql.Count("*"),
                        MaxLen = Sql.Max(x.Length),
                        MinLen = Sql.Min(x.Length),
                        SumLen = Sql.Sum(x.Length)
                    });
            using (db = dbFactory.Open())
            {

                var results = db.Select<Dictionary<string, object>>(q);
            }
        }


        //协议分级统计
        public ProtocalStatByLayer ProtocalStatisticsByLayers()
        {
            string sql = Globals.sqlToString("ProtocalByLayer.sql");
            ProtocalStatByLayer results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Single<ProtocalStatByLayer>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }
        #endregion


        #region 分组长度统计

        public PacketStatByLength PacketStatisticsByLength(int start,int end)
        {
            string sql = Globals.sqlToString("PacketByLength.sql");
            PacketStatByLength results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Single<PacketStatByLength>(sql, new { start = start,end=end });
                }
                catch (Exception)
                {

                }
            }
            if (results != null)
            {
                results.Start = start;
                results.End = end;
            }
            return results;
        }
        #endregion


        #region 对话统计
        public List<ConvStatMacORIpResult> ConversationStatisticsByIpv4()
        {
            string sql = Globals.sqlToString("ConvByIpv4.sql");
            //Console.WriteLine("sql:{0}", sql);
            List<ConvStatMacORIpResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<ConvStatMacORIpResult>(sql);
                }
                catch (Exception)
                {

                }
                foreach (var v in results)
                {
                    Console.WriteLine("{0}", v.ToString());
                }
            }
            return results;
        }

        public List<ConvStatMacORIpResult> ConversationStatisticsByIpv6()
        {
            string sql = Globals.sqlToString("ConvByIpv6.sql");
            List<ConvStatMacORIpResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<ConvStatMacORIpResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public List<ConvStatMacORIpResult> ConversationStatisticsByMac()
        {
            string sql = Globals.sqlToString("ConvByMac.sql");
            List<ConvStatMacORIpResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<ConvStatMacORIpResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public List<ConvStatTranResult> ConversationStatisticsByIpTcp()
        {
            string sql = Globals.sqlToString("ConvByIpTcp.sql");
            List<ConvStatTranResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<ConvStatTranResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public List<ConvStatTranResult> ConversationStatisticsByIpUdp()
        {
            string sql = Globals.sqlToString("ConvByIpUdp.sql");
            List<ConvStatTranResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<ConvStatTranResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }
        #endregion


        #region 端点统计
        public List<PointStatMacORIpResult> EndPointStatisticsByIpv4()
        {
            string sql = Globals.sqlToString("EPointByIpv4.sql");
            List<PointStatMacORIpResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<PointStatMacORIpResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public List<PointStatMacORIpResult> EndPointStatisticsByIpv6()
        {
            string sql = Globals.sqlToString("EPointByIpv6.sql");
            List<PointStatMacORIpResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<PointStatMacORIpResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public List<PointStatMacORIpResult> EndPointStatisticsByMac()
        {
            string sql = Globals.sqlToString("EPointByMac.sql");
            List<PointStatMacORIpResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<PointStatMacORIpResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public List<PointStatTranResult> EndPointStatisticsByIpTcp()
        {
            string sql = Globals.sqlToString("EPointByIpTcp.sql");
            List<PointStatTranResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<PointStatTranResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public List<PointStatTranResult> EndPointStatisticsByIpUdp()
        {
            string sql = Globals.sqlToString("EPointByIpUdp.sql");
            List<PointStatTranResult> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<PointStatTranResult>(sql);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }
        #endregion


        #region 无用
        public void GetInterProtocalPercent()
        {
            using (db = dbFactory.Open())
            {
                var q = db.From<PacketCompInfo>()
                      .GroupBy(x => x.Protocol)
                      .Select(x => new
                      {
                          Name = x.Type,
                          Count = Sql.Count("*"),
                          MaxLen = Sql.Max(x.Framelentgh),
                          MinLen = Sql.Min(x.Framelentgh),
                          SumLen = Sql.Sum(x.Framelentgh)
                      });
                var results = db.Select<Dictionary<string, object>>(q);
            }
        }

        public void GetAllProtocalPercent()
        {
            using (db = dbFactory.Open())
            {

            }
        }

        #endregion
        

        #region 实时统计
        //准备
        public List<Dictionary<string, object>> GetBeforePacketStatistics(DateTime dateTime)
        {
            var q = db.From<RealTimeInfo>()
                    .Where(x => x.DateTime < dateTime)
                    .And(x => x.DateTime >= Globals.StartTime)
                    .GroupBy(x => (x.DateTime))
                    .Select(x => new
                    {
                        Time = x.DateTime,
                        Count = Sql.Count("*"),
                        SumLen = Sql.Sum(x.FrameLen)
                    });
            List<Dictionary<string, object>> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Select<Dictionary<string, object>>(q);
                }
                catch (Exception)
                {

                }
                return results;
            }
        }

        //单个数据
        public Dictionary<string, object> GetDataByDate(DateTime dateTime)
        {
            var q = db.From<RealTimeInfo>()
                    .Where(x => x.DateTime == dateTime)
                    .Select(x => new
                    {
                        Count = Sql.Count("*"),
                        SumLen = Sql.Sum(x.FrameLen)
                    });
            Dictionary<string, object> results = null;
            using (db = dbFactory.Open())
            {
                try
                {
                    results = db.Single<Dictionary<string, object>>(q);
                }
                catch (Exception)
                {

                }
                return results;
            }

        }
        #endregion
        

        #region 删表
        public void DropAllTables()
        {
            using (db = dbFactory.Open())
            {
                //db.DropTables(typeof(PacketSimpInfo), typeof(PacketCompInfo), typeof(PacketInfo), typeof(RealTimeInfo));
                db.DropTable<PacketSimpInfo>();
            }
            using (db = dbFactory.Open())
            {
                db.DropTable<PacketCompInfo>();
            }
            using (db = dbFactory.Open())
            {
                db.DropTable<PacketInfo>();
            }
            using (db = dbFactory.Open())
            {
                db.DropTable<RealTimeInfo>();
            }
            using (db = dbFactory.Open())
            {
                db.DropTable<StatisticsInfo>();
            }
        }
        #endregion


    }
}
