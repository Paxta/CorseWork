using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tutorial.SqlConn
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"LAPTOP-CMSS62U7\SQLEXPRESS";

            string database = "TP_COURSE";
            string username = "Parviz";
            string password = "aaaaa0";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
