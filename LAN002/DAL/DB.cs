
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAN002.DAL
{
    class DB
    { 
  //      <connectionStrings>
  //  <add name = "test" connectionString="Data Source=.;Initial Catalog=test;User ID=sa;Password=123456;Pooling=true;Max Pool Size = 5120;Min Pool Size=0;" providerName="System.Data.SqlClient"/>
  //  <!--<add name = "test" connectionString="Data Source=192.168.2.254;Initial Catalog=test;User ID=sa;Password=123456;Pooling=true;Max Pool Size = 5120;Min Pool Size=0;" providerName="System.Data.SqlClient"/>-->
  //</connectionStrings>
    	
        public static string MysqlConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConn"].ConnectionString;
        public static string SServerConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["SsConn"].ConnectionString;



        //public static IDbConnection CreateMysqlConnection()
        //{
        //    return new MySqlConnection(MysqlConnStr);
        //}
		public static string SServerConnStr1 = System.Configuration.ConfigurationManager.ConnectionStrings["SsConn1"].ConnectionString;
        public static OrmLiteConnectionFactory CreateSqlServerConnectionFactory()
        {
            return new OrmLiteConnectionFactory( SServerConnStr,SqlServerDialect.Provider);
        }
        public static OrmLiteConnectionFactory CreateSqlServerConnectionFactory1()
        {
            return new OrmLiteConnectionFactory(SServerConnStr1, SqlServerDialect.Provider);
        }
    }
}
