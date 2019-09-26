using System.Data.SqlClient;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class UserContextSQL : IUserContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void SendMessage(MessageModel message)
        {
            string query = "INSERT INTO Message (MessageId, SenderBSN, ReceiverBSN, Content, Title, DateOf) VALUES (@messageId, @senderID, @receiverID, @content, @title, @dateOf)";

            using (SqlCommand command = new SqlCommand(query, _conn))
            {
                command.Parameters.AddWithValue("@messageID", message.MessageId);
                command.Parameters.AddWithValue("@senderID", message.SenderId);
                command.Parameters.AddWithValue("@receiverID", message.ReceiverId);
                command.Parameters.AddWithValue("@content", message.Content);
                command.Parameters.AddWithValue("@title", message.Title);
                command.Parameters.AddWithValue("@dateOf", message.DateOfX);

                _conn.Open();
                command.ExecuteNonQuery();
                _conn.Close();
            }
        }

    }

}
