using System;
using System.Collections.Generic;
using System.Text;
using Logic.Interface;
using Models;
using Models.Interfaces;
using Data;
using Data.Contexts;
using Data.Interfaces;


namespace Logic
{
    public class MessageLogic : IMessageLogic
    {
        //temporarily hard coded ID's because there are no sessions
        private int _senderId = 226044440;
        private int _receiverId = 239567262;

        private readonly IMessageContext _context;

        public MessageLogic(IMessageContext context)
        {
            _context = context;
        }

        public void SendMessage(MessageModel message)
        {
            message.DateOfX = GetCurrentDateTime();
            message.SenderId = GetSenderId();
            message.ReceiverId = GetReceiverId();
            _context.SendMessage(message);
        }

        public List<MessageModel> GetMessages()
        {
            return _context.GetMessages();
        }

        private DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        private int GetSenderId()
        {
            return _senderId;
        }

        private int GetReceiverId()
        {
            return _receiverId;
        }
    }
}
