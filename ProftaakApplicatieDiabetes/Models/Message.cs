using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Message
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int MessageId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateOfX { get; set; }
    }
}
