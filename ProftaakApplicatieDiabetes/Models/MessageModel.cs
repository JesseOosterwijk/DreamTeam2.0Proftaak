using System;
using Models.Interfaces;

namespace Models
{
    public class MessageModel : IMessage
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int CoupleId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateOfX { get; set; }

        public MessageModel(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public MessageModel(int senderId, int receiverId, string content, string title, DateTime dateOfX)
        {
            SenderId = senderId;
            CoupleId = receiverId;
            Content = content;
            Title = title;
            DateOfX = dateOfX;
        }
        public MessageModel()
        {

        }
    }
}
