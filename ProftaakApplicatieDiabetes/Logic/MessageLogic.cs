using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interface;
using Models;
using Data.Interfaces;
using Enums;


namespace Logic
{
    public class MessageLogic : IMessageLogic
    {
        private readonly IMessageContext _messageContext;

        public MessageLogic(IMessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        public void SendMessage(MessageModel message, int senderId, int receiverId)
        {
            if (message.Title != null && message.Content != null)
            {
                message.DateOfX = GetCurrentDateTime();
                _messageContext.SendMessage(message);
            }
        }

        public List<MessageModel> GetMessages(int senderId, int receiverId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            messages.AddRange(_messageContext.GetMessages(senderId, receiverId));
            messages = messages.OrderByDescending(m => m.DateOfX).ToList();

            return messages;
        }

        private DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public int GetReceiverId(AccountType type, int senderId)
        {
            int receiverId = 0;
            if (type == AccountType.CareRecipient)
            {
                receiverId = _messageContext.GetDoctorIdFromPatientId(senderId);
            }
            return receiverId;
        }

        public AccountType GetAccountType()
        {
            return AccountType.CareRecipient;
        }

        public void StartChat(int doctorId, int patientId)
        {
            _messageContext.StartChat(doctorId, patientId);
        }
    }
}
