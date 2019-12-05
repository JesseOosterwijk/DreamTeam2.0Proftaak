using System;
using Models.Interfaces;

namespace Models
{
    public class MessageModel : IMessage
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int CoupleId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateOfX { get; set; }

        public MessageModel(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public MessageModel(int messageId, int senderId, int coupleId, string content, string title, DateTime dateOfX)
        {
            MessageId = messageId;
            SenderId = senderId;
            CoupleId = coupleId;
            Content = content;
            Title = title;
            DateOfX = dateOfX;
        }
        public MessageModel()
        {

        }
    }
}
