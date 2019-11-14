﻿using System.Collections.Generic;
using Enums;
using Models;

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
