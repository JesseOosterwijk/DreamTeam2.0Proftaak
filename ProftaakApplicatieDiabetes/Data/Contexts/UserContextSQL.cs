using System.Data.SqlClient;
using Data.Interfaces;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {
        private readonly SqlConnection _conn = new SqlConnection(@"Data Source=mssql.fhict.local;Initial Catalog=dbi398785_diabetesdb;User ID=dbi398785_diabetesdb;Password=DreamTeam;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
