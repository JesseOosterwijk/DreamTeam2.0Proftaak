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
            string query =
                "INSERT INTO Message (SenderBSN, ReceiverBSN, Content, Title, DateOf) VALUES (@senderID, @receiverID, @content, @title, @dateOf)";

            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                _conn.Open();
                command.Parameters.AddWithValue("@senderID", message.SenderId);
                command.Parameters.AddWithValue("@receiverID", message.ReceiverId);
                command.Parameters.AddWithValue("@content", message.Content);
                command.Parameters.AddWithValue("@title", message.Title);
                command.Parameters.AddWithValue("@dateOf", message.DateOfX);


                command.ExecuteNonQuery();
                _conn.Close();
            }


        }

        public List<MessageModel> GetMessages()
        {
            string query = "SELECT SenderBSN, ReceiverBSN, Content, Title, DateOf FROM Message";

            List<MessageModel> messages = new List<MessageModel>();
            SqlCommand command = new SqlCommand(query, _conn);

            //command.Parameters.AddWithValue("@senderID", senderId);
           // command.Parameters.AddWithValue("@receiverID", receiverId);
            _conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                messages.Add(new MessageModel(
                    reader.GetInt32(reader.GetOrdinal("SenderBSN")),
                    reader.GetInt32(reader.GetOrdinal("ReceiverBSN")),
                    reader.GetString(reader.GetOrdinal("Content")),
                    reader.GetString(reader.GetOrdinal("Title")),
                    reader.GetDateTime(reader.GetOrdinal("DateOf"))
                ));
            }
            reader.Close();
            _conn.Close();
            return messages;
        }
    }
}
