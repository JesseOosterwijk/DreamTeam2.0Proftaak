﻿using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Models.Interfaces;

namespace Logic.Interface
{
    public interface IMessageLogic
    {
        void SendMessage(MessageModel message);
    }
}
