using System;
using System.Collections.Generic;
using System.Linq;
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
        private int _senderId = 1;
        private int _receiverId = 2;

        private readonly IMessageContext _context;

        public MessageLogic(IMessageContext context)
        {
            _context = context;
        }

        public bool SendMessage(MessageModel message, int senderId, int receiverId)
        {
            if (message.Title != null && message.Content != null)
            {
                message.DateOfX = GetCurrentDateTime();
                message.SenderId = senderId;
                message.ReceiverId = receiverId;
                return _context.SendMessage(message);
            }

            return false;
        }

        public List<MessageModel> GetMessages(int senderId, int receiverId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            messages.AddRange(_context.GetMessages(senderId, receiverId));
            messages.AddRange(_context.GetMessages(receiverId, senderId));
            messages = messages.OrderByDescending(m => m.DateOfX).ToList();
            return messages;
        }

        private DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public int GetSenderId()
        {
            return _senderId;
        }

        public int GetReceiverId()
        {
            return _receiverId;
        }
    }
}
