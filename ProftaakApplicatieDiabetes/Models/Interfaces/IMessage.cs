using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interfaces
{
    public interface IMessage
    {
        int SenderId { get; set; }
        int ReceiverId { get; set; }
        int MessageId { get; set; }
        string Content { get; set; }
        string Title { get; set; }
        DateTime DateOfX { get; set; }
    }
}
