using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Enums;
using Models;
using Models.Interfaces;

namespace Logic.Interface
{
    public interface IMessageLogic
    {
        bool SendMessage(MessageModel message, int senderId, int receiverId);
        List<MessageModel> GetMessages(int senderId, int receiverId);
        int GetReceiverId(AccountType type, int senderId);
        AccountType GetAccountType();
    }
}
