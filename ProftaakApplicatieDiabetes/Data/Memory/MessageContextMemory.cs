using System;
using System.Collections.Generic;
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
            _messages.Add(new MessageModel(1, 1, 1, "Test Bericht Content 0", "TestTitle 0", new DateTime(2019, 11, 28, 13, 3, 0)));
            _messages.Add(new MessageModel(2, 1, 1, "Test Bericht Content 1", "TestTitle 1", new DateTime(2019, 11, 28, 13, 3, 0)));
            _messages.Add(new MessageModel(3, 2, 1, "Test Bericht Content 2", "TestTitle 2", new DateTime(2019, 11, 28, 13, 3, 0)));
            _messages.Add(new MessageModel(4, 2, 2, "Test Bericht Content 3", "TestTitle 3", new DateTime(2019, 11, 28, 13, 5, 0)));
            _messages.Add(new MessageModel(5, 2, 2, "Test Bericht Content 4", "TestTitle 4", new DateTime(2019, 11, 28, 14, 34, 0)));
            _messages.Add(new MessageModel(6, 3, 2, "Test Bericht Content 5", "TestTitle 5", new DateTime(2019, 12, 18, 7, 34, 0)));
            _messages.Add(new MessageModel(7, 3, 2, "Test Bericht Content 6", "TestTitle 6", new DateTime(2019, 12, 18, 7, 57, 0)));
            _messages.Add(new MessageModel(8, 4, 3, "Test Bericht Content 7", "TestTitle 7", new DateTime(2019, 12, 18, 7, 34, 0)));
            _messages.Add(new MessageModel(9, 4, 3, "Test Bericht Content 8", "TestTitle 8", new DateTime(2019, 12, 18, 7, 34, 0)));
            _messages.Add(new MessageModel(10, 5, 3, "Test Bericht Content 9", "TestTitle 9", new DateTime(2019, 12, 18, 7, 34, 0)));
            _messages.Add(new MessageModel(11, 5, 3, "Test Bericht Content 10", "TestTitle 10", new DateTime(2019, 12, 18, 7, 44, 0)));
        }


        public int GetConversationDoctor(int doctorId, int patientId)
        {
            if(doctorId == 2 && patientId == 1)
            {
                return 1;
            }
            if (doctorId == 2 && patientId == 3)
            {
                return 2;
            }
            if (doctorId == 4 && patientId == 5)
            {
                return 3;
            }
            return 0;
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
            switch (patientId)
            {
                case 1: return 1;
                case 3: return 2;
                case 5: return 3;
                default: return 0;
            }


        }

        public int GetDoctorIdFromPatientId(int patientId)
        {
            switch (patientId)
            {
                case 1: return 2;
                case 3: return 2;
                case 5: return 4;
                case 6: return 6;
                default: return 0;
            }
                
        }

        public void SendMessage(MessageModel message)
        {

        }

        public void StartChat(int senderId, int receiverId)
        {
           
        }
    }
}
