using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Data.Interfaces;
using Data.Memory;
using Enums;
using Models;

namespace Data.Contexts
{
    public class DoctorContext : IDoctorContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        //public IEnumerable<User> GetPatientsFromDoctorId(int doctorId)
        //{
        //    List<User> users = new List<User>();
        //    try
        //    {
        //        string query = "SELECT User.UserId, User.BSN, User.AccountType, User.FirstName, User.LastName, User.Email, User.Address, User.Residence, User.Gender, User.Weight, User.DateOfBirth, User.Status FROM ProfessionaltoPatient, User WHERE ProfessionaltoPatient.DoctorId = @doctorId AND ProfessionaltoPatient.PatientId = User.UserId";


        //        SqlCommand command = new SqlCommand(query, _conn);

        //        command.Parameters.AddWithValue("@doctorId", doctorId);
        //        _conn.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            users.Add(new User(
        //                reader.GetInt32(reader.GetOrdinal("UserId")),
        //                reader.GetInt32(reader.GetOrdinal("BSN")),
        //                (AccountType)Enum.Parse(typeof(AccountType),reader.GetString(reader.GetOrdinal("AccountType"))),
        //                reader.GetString(reader.GetOrdinal("FirstName")),
        //                reader.GetString(reader.GetOrdinal("LastName")),
        //                reader.GetString(reader.GetOrdinal("Email")),
        //                reader.GetString(reader.GetOrdinal("Address")),
        //                reader.GetString(reader.GetOrdinal("Residence")),
        //                (Gender)Enum.Parse(typeof(Gender), reader.GetString(reader.GetOrdinal("Gender"))),
        //                reader.GetInt32(reader.GetOrdinal("Weight")),
        //                reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
        //                reader.GetBoolean(reader.GetOrdinal("Status"))
        //                ));
        //        }
                
        //        _conn.Close();
        //        return users;
        //    }
        //    catch (Exception e)
        //    {
        //        _conn.Close();
        //        throw e;
        //    }
        //}

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
                        User user = new User();

                        user.UserId = (int)reader["UserId"];
                        user.FirstName = reader["Firstname"].ToString();
                        user.LastName = reader["Lastname"].ToString();

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
