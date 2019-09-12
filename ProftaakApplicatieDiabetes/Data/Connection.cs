using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Data
{
    class Connection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection();
        }
    }
}
