using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface IMessageContext
    {
        void SendMessage(MessageModel message);
        List<MessageModel> GetMessages(int senderId, int receiverId);
        int GetDoctorIdFromPatientId(int patientId);
        void StartChat(int doctorId, int patientId);
    }
}
