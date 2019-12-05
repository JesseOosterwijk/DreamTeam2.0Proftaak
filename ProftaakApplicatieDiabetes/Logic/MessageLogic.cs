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

        public void SendMessage(MessageModel message)
        {
            if (message.Title != null && message.Content != null)
            {
                message.DateOfX = GetCurrentDateTime();
                _messageContext.SendMessage(message);
            }
        }

        public List<MessageModel> ViewMessagesPatient(AccountType type, int patientId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            if (type == AccountType.CareRecipient)
            {
                int doctorId = GetDoctorIdFromPatientId(patientId);
                if (doctorId != 0)
                {
                    int coupleId = GetConversationPatient(patientId);
                    if (coupleId == 0)
                    {
                        StartChat(doctorId, patientId);
                        coupleId = GetConversationPatient(patientId);
                    }
                    messages = GetConversationMessages(coupleId);
                }
            }
            return messages;
        }

        public List<MessageModel> ViewMessagesDoctor(AccountType type, int doctorId, int patientId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            if (type == AccountType.Doctor)
            {
                int coupleId = GetConversationDoctor(doctorId, patientId);
                if (coupleId == 0)
                {
                    StartChat(doctorId, patientId);
                    coupleId = GetConversationDoctor(doctorId, patientId);
                }
                messages = GetConversationMessages(coupleId);
            }
            return messages;
        }

        private List<MessageModel> GetConversationMessages(int coupleId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            messages.AddRange(_messageContext.GetConversationMessages(coupleId));
            messages = messages.OrderByDescending(m => m.DateOfX).ToList();

            return messages;
        }

        private DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        private int GetDoctorIdFromPatientId(int patientId)
        {
            return _messageContext.GetDoctorIdFromPatientId(patientId);
        }

        public int GetConversationPatient(int patientId)
        {
            int coupleId = _messageContext.GetConversationPatient(patientId);

            return coupleId;
        }

        public int GetConversationDoctor(int doctorId, int patientId)
        {
            int coupleId = _messageContext.GetConversationDoctor(doctorId, patientId);

            return coupleId;
        }

        public void StartChat(int doctorId, int patientId)
        {
            _messageContext.StartChat(doctorId, patientId);
        }
    }
}
