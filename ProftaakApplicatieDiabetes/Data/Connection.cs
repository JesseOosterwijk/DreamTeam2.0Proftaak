using System.Data.SqlClient;

namespace Data
{
    class Connection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=mssql.fhict.local;Initial Catalog=dbi398785_diabetesdb;User ID=dbi398785_diabetesdb;Password=DreamTeam;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
