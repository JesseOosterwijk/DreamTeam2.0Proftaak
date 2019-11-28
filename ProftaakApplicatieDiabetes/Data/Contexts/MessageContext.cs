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

        }

        public List<MessageModel> GetConversationMessages(int coupleId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            return messages;
        }

        //TO BE REFACTORED
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

        public int GetConversationDoctor(int doctorId, int patientId)
        {
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

        public void StartChat(int senderId, int receiverId)
        {
            try
            {
                string query = "Insert into [UserMessage] (ReceiverId, SenderId) Values (@ReceiverId, @SenderId)";

                using (SqlCommand com = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    com.Parameters.AddWithValue("@SenderId", senderId);
                    com.Parameters.AddWithValue("@ReceiverId", receiverId);

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
