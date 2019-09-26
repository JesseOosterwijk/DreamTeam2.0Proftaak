using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using ProftaakApplicatieDiabetes.ViewModels;
using Logic.Interface;
using Models;


namespace ProftaakApplicatieDiabetes.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageLogic _messageLogic;
        public IActionResult ViewMessage()
        {
            return View();
        }

        public IActionResult SendMessage(MessageViewModel messageViewModel)
        {
            // Message message = new Message(messageViewModel.Title, messageViewModel.Content);
            MessageModel message = new MessageModel(messageViewModel.Title, messageViewModel.Content);
            _messageLogic.SendMessage(message);

            return ViewMessage();
        }
    }
}