using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using ProftaakApplicatieDiabetes.Controllers;
using WebSocketManager;

namespace ProftaakApplicatieDiabetes
{
    public class ChatHandler : WebSocketHandler
    {
        //private Random rnd = new Random();
        private readonly ChatManager _chatManager;
        public ChatHandler(WebSocketConnectionManager webSocketConnectionManager, ChatManager chatManager) : base(webSocketConnectionManager)
        {
            _chatManager = chatManager;
        }

        public async Task SendMessage(string socketId, string message, string title)
        {
            MessageModel messageModel = new MessageModel();
            messageModel.SocketId = socketId;
            messageModel.Content = message;
            messageModel.Title = title;
            messageModel.DateOfX = DateTime.Now;
            _chatManager.Messages.Add(messageModel);
            await InvokeClientMethodToAllAsync("pingMessage", socketId, message, title);
        }
    }
}
