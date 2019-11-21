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

                _conn.Close();
                return doctorId;
            }
            catch (Exception e)
            {
                _conn.Close();
                throw e;
            }
        }

        public List<MessageModel> GetMessages(int senderId, int receiverId)
        {

            List<MessageModel> messages = new List<MessageModel>();
            return messages;
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
            catch (Exception)
            {
                _conn.Close();
            }
            finally
            {
                _conn.Close();
            }
        }

        private void GetCouple()
        {

        }
    }
}
