using Data.Interfaces;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();
    }
}
