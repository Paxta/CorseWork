using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ЛАБОРАТОРНЫЕ_РАБОТЫ__ЯП_
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "192.168.43.54";
            int port = 3306;
            string database = "testing";
            string username = "sending";
            string password = "maxon";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
    class GlobVars
    {
        public static string Id;
        public static string Name;
    }
}
