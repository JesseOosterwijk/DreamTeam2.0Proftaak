using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class MessageContext : IMessageContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void SendMessage(MessageModel message)
        {

            //try
            //{
            //    string query =
            //        "INSERT INTO Message (SenderId, ReceiverId, Content, Title, DateOf) VALUES (@senderID, @receiverID, @content, @title, @dateOf)";

            //    using (SqlCommand command = new SqlCommand(query, _conn))
            //    {
            //        _conn.Open();
            //        command.Parameters.AddWithValue("@senderID", message.SenderId);
            //        command.Parameters.AddWithValue("@receiverID", message.ReceiverId);
            //        command.Parameters.AddWithValue("@content", message.Content);
            //        command.Parameters.AddWithValue("@title", message.Title);
            //        command.Parameters.AddWithValue("@dateOf", message.DateOfX);

            //        command.ExecuteNonQuery();
            //    }
            //}
            //catch (Exception)
            //{
            //    _conn.Close();
            //    return false;
            //}
            //finally
            //{
            //    _conn.Close();
            //}

            //return true;

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
            //try
            //{
            //    string query = "SELECT SenderId, ReceiverId, Content, Title, DateOf FROM Message WHERE SenderId = @senderID AND ReceiverId = @receiverID";

            //    using (SqlCommand command = new SqlCommand(query, _conn))
            //    {
            //        _conn.Open();

            //        command.Parameters.AddWithValue("@senderID", senderId);
            //        command.Parameters.AddWithValue("@receiverID", receiverId);

            //        SqlDataReader reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            MessageModel message = new MessageModel
            //            {
            //                SenderId = (int)reader["SenderID"],
            //                ReceiverId = (int)reader["ReceiverId"],
            //                Content = (string)reader["Content"],
            //                Title = (string)reader["Title"],
            //                DateOfX = (DateTime)reader["DateOf"]
            //            };

            //            messages.Add(message);
            //        }
            //        reader.Close();
            //        return messages;
            //    }
            //}
            //catch (Exception)
            //{                
            //    _conn.Close();
            //    return messages;
            //}
            //finally
            //{
            //    _conn.Close();
            //}          
        }

        public void StartChat(int doctorId, int patientId)
        {
            try
            {
                string query = "Insert into [UserMessage] (ReceiverId, SenderId) Values (@ReceiverId, @SenderId)";

                using (SqlCommand com = new SqlCommand(query, _conn))
                {
                    _conn.Open();

                    com.Parameters.AddWithValue("@DoctorId", doctorId);
                    com.Parameters.AddWithValue("@PatientId", patientId);

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
    }
}
