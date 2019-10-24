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
        private int _senderId = 3;
        private int _receiverId = 239567262;

        private readonly IMessageContext _context;

        public MessageLogic(IMessageContext context)
        {
            _context = context;
        }

        public bool SendMessage(MessageModel message)
        {
            if (message.Title != null && message.Content != null)
            {
                message.DateOfX = GetCurrentDateTime();
                message.SenderId = GetSenderId();
                message.ReceiverId = GetReceiverId();
                return _context.SendMessage(message);
            }

            return false;
        }

        public List<MessageModel> GetMessages()
        {
            List<MessageModel> messages = new List<MessageModel>();
            messages.AddRange(_context.GetMessages(GetSenderId(), GetReceiverId()));
            messages.AddRange(_context.GetMessages(GetReceiverId(), GetSenderId()));
            messages = messages.OrderByDescending(m => m.DateOfX).ToList();
            return messages;
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
