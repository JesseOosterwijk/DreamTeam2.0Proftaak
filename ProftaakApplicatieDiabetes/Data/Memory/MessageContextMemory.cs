using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Memory
{
    public class MessageContextMemory : IMessageContext
    {
        List<MessageModel> _messages;


        public MessageContextMemory()
        {
            _messages = new List<MessageModel>();
            _messages.Add(new MessageModel(0, 1, "Test Bericht Content 0", "TestTitle 0", new DateTime(2019, 11, 28, 13, 3, 0)));
            _messages.Add(new MessageModel(0, 1, "Test Bericht Content 1", "TestTitle 0", new DateTime(2019, 11, 28, 13, 3, 0)));
            _messages.Add(new MessageModel(1, 2, "Test Bericht Content 2", "TestTitle 0", new DateTime(2019, 11, 28, 13, 3, 0)));
            _messages.Add(new MessageModel(1, 2, "Test Bericht Content 3", "TestTitle 1", new DateTime(2019, 11, 28, 13, 5, 0)));
            _messages.Add(new MessageModel(1, 2, "Test Bericht Content 4", "TestTitle 2", new DateTime(2019, 11, 28, 14, 34, 0)));
            _messages.Add(new MessageModel(3, 3, "Test Bericht Content 5", "TestTitle 4", new DateTime(2019, 12, 18, 9, 34, 0)));
        }


        public int GetConversationDoctor(int doctorId, int patientId)
        {
            throw new NotImplementedException();
        }

        public List<MessageModel> GetConversationMessages(int coupleId)
        {
            List<MessageModel> messages = new List<MessageModel>();
            foreach(MessageModel message in _messages)
            {
                if(message.CoupleId == coupleId)
                {
                    messages.Add(message);
                }
            }

            return messages;
        }

        public int GetConversationPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        public int GetDoctorIdFromPatientId(int patientId)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(MessageModel message)
        {
            
        }

        public void StartChat(int senderId, int receiverId)
        {
           
        }
    }
}
