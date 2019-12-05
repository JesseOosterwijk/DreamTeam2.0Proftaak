using System.Collections.Generic;
using Enums;
using Models;

namespace Logic.Interface
{
    public interface IMessageLogic
    {
        void SendMessage(MessageModel message);
        List<MessageModel> ViewMessagesPatient(AccountType type, int patientId);
        List<MessageModel> ViewMessagesDoctor(AccountType type, int doctorId, int patientId);
        int GetConversationPatient(int patientId);
        int GetConversationDoctor(int doctorId, int patientId);
        void StartChat(int doctorId, int patientId);
    }
}
