﻿using Logic;
using Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakApplicatieDiabetes.Models;
using System.Collections.Generic;

namespace ProftaakApplicatieDiabetes.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserLogic _userLogic;

        public AdminController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserOverview()
        {
            UserViewModel uvm = new UserViewModel()
            {
                Users = _userLogic.GetAllUsers()
            };

            return View("UserOverview", uvm);
        }
    }
}