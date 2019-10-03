using Logic;
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
        

        public MessageController(IMessageLogic messageLogic)
        {
            _messageLogic = messageLogic;
        }

        public IActionResult ViewMessage(MessageViewModel messageViewModel)
        {
            return View(messageViewModel);
        }

        public IActionResult SendMessage(MessageViewModel messageViewModel)
        {
            MessageModel message = new MessageModel(messageViewModel.Title, messageViewModel.Content);
            _messageLogic.SendMessage(message);

            return ViewMessage(messageViewModel);
        }
    }
}