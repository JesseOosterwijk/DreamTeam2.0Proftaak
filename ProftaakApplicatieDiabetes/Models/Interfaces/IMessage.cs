using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interfaces
{
    public interface IMessage
    {
        int SenderId { get; }
        int ReceiverId { get; }
        int MessageId { get; }
        string Content { get; }
        string Title { get; }
        DateTime DateOfX { get; }
    }
}
