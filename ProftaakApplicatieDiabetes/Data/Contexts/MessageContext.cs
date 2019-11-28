using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class MessageContext : IMessageContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void SendMessage(MessageModel message)
        {
            try
            {
                string query = "INSERT INTO Message (Content, Title, DateOf, CoupleId, SenderId) VALUES (@Content, @Title, @DateOf, @CoupleId, @SenderId) ";
                using (SqlCommand com = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    com.Parameters.AddWithValue("@Content", message.Content);
                    com.Parameters.AddWithValue("@Title", message.Title);
                    com.Parameters.AddWithValue("@DateOf", message.DateOfX);
                    com.Parameters.AddWithValue("@CoupleId", message.CoupleId);
                    com.Parameters.AddWithValue("@SenderId", message.SenderId);

                    com.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<MessageModel> GetConversationMessages(int coupleId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            try
            {
                string query = "SELECT MessageId, Content, Title, DateOfX, SenderId FROM Message WHERE CoupleId = @CoupleId";

                SqlCommand command = new SqlCommand(query, _conn);

                command.Parameters.AddWithValue("@CoupleId", coupleId);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    messages.Add(new MessageModel(
                        reader.GetInt32(reader.GetOrdinal("MessageId")),
                        reader.GetInt32(reader.GetOrdinal("SenderId")),
                        coupleId,
                        reader.GetString(reader.GetOrdinal("Content")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetDateTime(reader.GetOrdinal("DateOf"))
                    ));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _conn.Close();
            }
            return messages;
        }

        public int GetDoctorIdFromPatientId(int patientId)
        {
            int doctorId = 0;
            try
            {
                string query = "SELECT DoctorId FROM ProfessionaltoPatient WHERE PatientId = @patientId";

                SqlCommand command = new SqlCommand(query, _conn);

                command.Parameters.AddWithValue("@patientId", patientId);
                _conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    doctorId = reader.GetInt32(reader.GetOrdinal("DoctorId"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _conn.Close();
            }
            return doctorId;
        }

        public int GetConversationPatient(int patientId)
        {
            int coupleId = 0;
            try
            {
                string query = "SELECT CoupleId FROM UserMessage WHERE PatientId = @patientId";

                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.AddWithValue("@patientId", patientId);

                _conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    coupleId = reader.GetInt32(reader.GetOrdinal("CoupleId"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _conn.Close();
            }
            return coupleId;
        }

        public int GetConversationDoctor(int doctorId, int patientId)
        {
            int coupleId = 0;
            try
            {
                string query = "SELECT CoupleId FROM UserMessage WHERE PatientId = @patientId AND DoctorId = @doctorId";

                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.AddWithValue("@patientId", patientId);
                command.Parameters.AddWithValue("@doctorId", doctorId);

                _conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    coupleId = reader.GetInt32(reader.GetOrdinal("CoupleId"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _conn.Close();
            }
            return coupleId;
        }

        public void StartChat(int doctorId, int patientId)
        {
            try
            {
                string query = "INSERT INTO [UserMessage] (DoctorId, PatientId) VALUES (@DoctorId, @PatientId)";

                using (SqlCommand com = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    com.Parameters.AddWithValue("@DoctorId", doctorId);
                    com.Parameters.AddWithValue("@PatientId", patientId);

                    com.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
