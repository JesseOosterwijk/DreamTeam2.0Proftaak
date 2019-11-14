using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IMessageContext
    {
        bool SendMessage(MessageModel message);
        List<MessageModel> GetMessages(int senderId, int receiverId);
    }
}
