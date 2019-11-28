using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult ViewMessage()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            MessageViewModel messageViewModel = new MessageViewModel()
            {
                //Messages = _messageLogic.GetConversationMessages()
            };
            
            return View(messageViewModel);
        }

        [HttpPost]
        public IActionResult SendMessage(MessageViewModel messageViewModel)
        {
            MessageModel message = new MessageModel
            {
                Title = messageViewModel.Title,
                Content = messageViewModel.Content
            }; 

            int senderId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            _messageLogic.SendMessage(message);

            return RedirectToAction("ViewMessage");
        }

        public IActionResult StartChat(int receiverId)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role).Value == "Professional")
            {
                int doctorId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
                _messageLogic.StartChat(doctorId, receiverId);
            }
            else if (User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role).Value == "CareRecipient")
            {
                int patientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
                _messageLogic.StartChat(patientId, receiverId);
            }

            return View("ViewMessage");
        }
    }
}