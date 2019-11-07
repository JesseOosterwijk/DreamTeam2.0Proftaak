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
        private readonly IPatientToDoctorContext _patientToDoctorContext;

        public MessageLogic(IMessageContext messageContext, IPatientToDoctorContext patientToDoctorContext)
        {
            _messageContext = messageContext;
            _patientToDoctorContext = patientToDoctorContext;
        }

        public bool SendMessage(MessageModel message, int senderId, int receiverId)
        {
            if (message.Title != null && message.Content != null)
            {
                message.DateOfX = GetCurrentDateTime();
                return _messageContext.SendMessage(message);
            }

            return false;
        }

        public List<MessageModel> GetMessages(int senderId, int receiverId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            messages.AddRange(_messageContext.GetMessages(senderId, receiverId));
            messages.AddRange(_messageContext.GetMessages(receiverId, senderId));
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
                receiverId = _patientToDoctorContext.GetDoctorIdFromPatientId(senderId);
            }
            return receiverId;
        }

        public AccountType GetAccountType()
        {
            return AccountType.CareRecipient;
        }
    }
}
