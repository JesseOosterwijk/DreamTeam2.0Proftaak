﻿using System.Linq;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using ProftaakApplicatieDiabetes.ViewModels;
using Logic.Interface;
using Microsoft.AspNetCore.SignalR;
using Models;
using System.Security.Claims;


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
            MessageViewModel messageViewModel = new MessageViewModel();
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
            messageViewModel.Messages = _messageLogic.GetMessages(_messageLogic.GetSenderId(), _messageLogic.GetReceiverId(_messageLogic.GetAccountType(), _messageLogic.GetSenderId()));
            
            return View(messageViewModel);
        }

        [HttpPost]
        public IActionResult ViewMessage(MessageViewModel messageViewModel)
        {
            MessageModel message = new MessageModel(messageViewModel.Title, messageViewModel.Content);
            ModelState.Clear();
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
            ViewBag.WasMessageSendSuccessfully = _messageLogic.SendMessage(message, _messageLogic.GetSenderId(), _messageLogic.GetReceiverId(_messageLogic.GetAccountType(), _messageLogic.GetSenderId()));
            return ViewMessage();
        }
    }
}