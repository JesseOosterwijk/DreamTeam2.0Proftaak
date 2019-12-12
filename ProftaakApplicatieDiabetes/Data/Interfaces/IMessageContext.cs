using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IMessageContext
    {
        void SendMessage(MessageModel message);
        List<MessageModel> GetConversationMessages(int coupleId);
        int GetDoctorIdFromPatientId(int patientId);
        int GetConversationPatient(int patientId);
        int GetConversationDoctor(int doctorId, int patientId);
        void StartChat(int senderId, int receiverId);
    }
}
