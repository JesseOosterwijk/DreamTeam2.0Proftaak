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
<<<<<<< HEAD
=======
        //temporarily hard coded ID's because there are no sessions
        private int _senderId = 3;
        private int _receiverId = 2;    
        private AccountType _senderAccountType = AccountType.CareRecipient;

>>>>>>> 8afdb2f533208006418b20212a1504f63abcbe6a
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
