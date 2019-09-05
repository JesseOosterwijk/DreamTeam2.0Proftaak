using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Models;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserLogic _userLogic;

        public AdminController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}