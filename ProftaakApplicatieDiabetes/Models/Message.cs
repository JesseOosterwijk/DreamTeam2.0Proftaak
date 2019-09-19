using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    class Message
    {
        public int SenderId { get; }
        public int ReceiverId { get; }
        public int MessageId { get; }
        public string Content { get; }
        public string Title { get; }
        public DateTime DateOfX { get; }
    }
}
