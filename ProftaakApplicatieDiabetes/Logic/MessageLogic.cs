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
using System.Security.Claims;
using Enums;


namespace Logic
{
    public class MessageLogic : IMessageLogic
    {
        //temporarily hard coded ID's because there are no sessions
        private int _senderId = 3;
        private int _receiverId = 2;    
        private AccountType _senderAccountType = AccountType.CareRecipient;

        private readonly IMessageContext _messageContext;
        private readonly IPatientToDoctorContext _patientToDoctorContext = new PatientToDoctorContextSQL();

        public MessageLogic(IMessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        public bool SendMessage(MessageModel message, int senderId, int receiverId)
        {
            if (message.Title != null && message.Content != null)
            {
                message.DateOfX = GetCurrentDateTime();
                message.SenderId = senderId;
                message.ReceiverId = receiverId;
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

        public int GetSenderId()
        {
            return _senderId;
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
            return _senderAccountType;
        }
    }
}
