using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface IMessageContext
    {
        bool SendMessage(MessageModel message);
        List<MessageModel> GetMessages(int senderId, int receiverId);
    }
}
