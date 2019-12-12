using System;

namespace Models.Interfaces
{
    public interface IMessage
    {
        int SenderId { get; set; }
        int CoupleId { get; set; }
        int MessageId { get; set; }
        string Content { get; set; }
        string Title { get; set; }
        DateTime DateOfX { get; set; }
    }
}
