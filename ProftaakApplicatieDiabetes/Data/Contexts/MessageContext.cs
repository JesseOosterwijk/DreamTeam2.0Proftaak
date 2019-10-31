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

        public bool SendMessage(MessageModel message)
        {
            try
            {
                string query =
                    "INSERT INTO Message (SenderId, ReceiverId, Content, Title, DateOf) VALUES (@senderID, @receiverID, @content, @title, @dateOf)";

                using (SqlCommand command = new SqlCommand(query, _conn))
                {
                    _conn.Open();
                    command.Parameters.AddWithValue("@senderID", message.SenderId);
                    command.Parameters.AddWithValue("@receiverID", message.ReceiverId);
                    command.Parameters.AddWithValue("@content", message.Content);
                    command.Parameters.AddWithValue("@title", message.Title);
                    command.Parameters.AddWithValue("@dateOf", message.DateOfX);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                _conn.Close();
                return false;
            }
            finally
            {
                _conn.Close();
            }

            return true;

        }

        public List<MessageModel> GetMessages(int senderId, int receiverId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            try
            {
                string query = "SELECT SenderId, ReceiverId, Content, Title, DateOf FROM Message WHERE SenderId = @senderID AND ReceiverId = @receiverID";

                
                SqlCommand command = new SqlCommand(query, _conn);

                command.Parameters.AddWithValue("@senderID", senderId);
                command.Parameters.AddWithValue("@receiverID", receiverId);
                _conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    messages.Add(new MessageModel(
                        reader.GetInt32(reader.GetOrdinal("SenderId")),
                        reader.GetInt32(reader.GetOrdinal("ReceiverId")),
                        reader.GetString(reader.GetOrdinal("Content")),
                        reader.GetString(reader.GetOrdinal("Title")),
                        reader.GetDateTime(reader.GetOrdinal("DateOf"))
                    ));
                }
                reader.Close();
                return messages;
            }
            catch (Exception)
            {                
                _conn.Close();
                return messages;
            }
            finally
            {
                _conn.Close();
            }          
        }
    }
}
