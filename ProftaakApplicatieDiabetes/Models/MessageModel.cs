using System;
using System.Collections.Generic;
using System.Text;
using Models.Interfaces;

namespace Models
{
    public class MessageModel : IMessage
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int MessageId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateOfX { get; set; }

        public MessageModel(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }


    }
}
