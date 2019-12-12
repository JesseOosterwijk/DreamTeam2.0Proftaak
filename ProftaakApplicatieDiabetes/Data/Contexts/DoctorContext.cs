using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Data.Interfaces;
using Data.Memory;
using Models;

namespace Data.Contexts
{
    public class DoctorContext : IDoctorContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public IEnumerable<User> GetAllLinkedPatients(int userId)
        {
            var model = new List<User>();
            try
            {
                string query =
                    "select * from [ProfessionaltoPatient] pp " +
                    "inner join[User] u on u.UserId = pp.PatientId " +
                    "where pp.DoctorId = @userId";

                using (SqlCommand com = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    com.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserId = (int)reader["UserId"],
                            FirstName = reader["Firstname"].ToString(),
                            LastName = reader["Lastname"].ToString()
                        };

                        model.Add(user);
                    }
                    reader.Close();
                }
                return model;
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

        public IEnumerable<Calculation> GetPatientData(int patientId)
        {
            var model = new List<Calculation>();

            string query = "select * from [Measurement] m " +
                "inner join[User] u on u.UserId = m.UserId " +
                "Where m.UserId = @userId " +
                "And u.InfoSharing = 'True' ";

            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                _conn.Open();
                command.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = patientId;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string encryptingString = reader.GetString(8);

                    Calculation calc = new Calculation
                    {
                        UserId = (int)reader["UserId"],
                        UserFirstName = (string)reader["FirstName"],
                        UserLastName = (string)reader["LastName"],
                        TotalCarbs = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(2), encryptingString)),
                        Weight = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(3), encryptingString)),
                        CurrentBloodsugar = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(4), encryptingString)),
                        TargetBloodSugar = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(5), encryptingString)),
                        InsulinAdvice = Convert.ToInt32(Encrypting.Decrypt(reader.GetString(6), encryptingString)),
                        Date = (DateTime)reader["Date"]
                    };

                    model.Add(calc);
                }
                reader.Close();
            }
            return model;
        }
    }
}
