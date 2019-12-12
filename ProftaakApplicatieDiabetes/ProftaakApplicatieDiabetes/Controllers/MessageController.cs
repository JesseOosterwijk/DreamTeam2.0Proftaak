using System.Collections.Generic;
using System.Linq;
using Enums;
using Microsoft.AspNetCore.Mvc;
using ProftaakApplicatieDiabetes.ViewModels;
using Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Models;


namespace ProftaakApplicatieDiabetes.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageLogic _messageLogic;
        
        public MessageController(IMessageLogic messageLogic)
        {
            _messageLogic = messageLogic;
        }

        
        public IActionResult ViewMessage(int id)
        {
            MessageViewModel messageViewModel = new MessageViewModel();
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
            if (User.IsInRole("Doctor"))
            {
                messageViewModel.Messages =
                    new List<MessageModel>(_messageLogic.ViewMessagesDoctor(
                        AccountType.Doctor,
                        userId,
                        id
                    ));
                messageViewModel.OtherUserId = id;
                messageViewModel.CoupleId = _messageLogic.GetConversationDoctor(userId, id);
            }
            if (User.IsInRole("CareRecipient"))
            {
                List<MessageModel> messages = _messageLogic.ViewMessagesPatient(
                    AccountType.CareRecipient,
                    userId);
                messageViewModel.Messages.AddRange(messages);
                messageViewModel.CoupleId = _messageLogic.GetConversationPatient(userId);
            }
            return View(messageViewModel);
        }

        [HttpPost]
        public IActionResult SendMessage(MessageViewModel messageViewModel)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            MessageModel message = new MessageModel
            {
                SenderId =  userId,
                Title = messageViewModel.Title,
                Content = messageViewModel.Content,
                CoupleId = messageViewModel.CoupleId
            };

            _messageLogic.SendMessage(message);

            if (User.IsInRole("Doctor"))
            {
                return RedirectToAction("ViewMessage", new {id = messageViewModel.OtherUserId});
            }

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