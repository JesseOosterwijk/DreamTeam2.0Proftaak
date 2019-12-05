using Data.Interfaces;
using Models;
using System;
using System.Data.SqlClient;
using System.Data;

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

        public void UpdateWeight(int weight, int id)
        {
            try
            {
                string query = "UPDATE [User] SET [Weight] = @Weight WHERE [UserId] = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    cmd.Parameters.AddWithValue("@Weight", weight);
                    cmd.Parameters.AddWithValue("@UserId", id);

                    cmd.ExecuteNonQuery();
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
        
        public void UpdateStatus(int id, bool status)
        {
            try
            {
                string query = "UPDATE [User] SET Status = @Status WHERE UserId = @UserId";

                _conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = status;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("User not edited");
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
