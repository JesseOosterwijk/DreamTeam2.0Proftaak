using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    public class AccountContext : IAccountContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void AllowInfoSharing(int userId)
        {
            try
            {
                string query = "Update [User] set InfoSharing = 'True' where UserId = @UserId";

                using (SqlCommand com = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    com.Parameters.AddWithValue("@UserId", userId);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DisableInfoSharing(int userId)
        {
            try
            {
                string query = "Update [User] set InfoSharing = 'False' where UserId = @UserId";

                using (SqlCommand com = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    com.Parameters.AddWithValue("@UserId", userId);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool SharingIsEnabled(int userId)
        {
            string query = "SELECT UserId, InfoSharing " +
                        "FROM [User] " +
                        "WHERE [UserId] = @UserId";
            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                _conn.Open();
                command.Parameters.AddWithValue("@UserId", userId);

                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User
                    {
                        UserId = (int)rdr["UserId"],
                        InfoSharing = (bool)rdr["InfoSharing"]
                    };
                    if (user.InfoSharing == false)
                    {
                        rdr.Close();
                        _conn.Close();
                        return false;
                    }
                }
                rdr.Close();
                _conn.Close();
            }
            return true;
        }
    }
}
