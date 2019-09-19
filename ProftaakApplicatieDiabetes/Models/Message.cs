using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Message
    {
        public int SenderId { get; }
        public int ReceiverId { get; }
        public int MessageId { get; }
        public string Content { get; }
        public string Title { get; }
        public DateTime DateOfX { get; }

        public Message(int senderid, int receiverid, int messageid, string content, string title, DateTime dateofx)
        {
            SenderId = senderid;
            ReceiverId = receiverid;
            MessageId = messageid;
            Content = content;
            Title = title;
            DateOfX = dateofx;
        }
    }
}
