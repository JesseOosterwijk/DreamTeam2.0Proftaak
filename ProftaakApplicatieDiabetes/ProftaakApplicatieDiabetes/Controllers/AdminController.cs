using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakApplicatieDiabetes.Models;
using System.Collections.Generic;

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

        [HttpGet]
        public ActionResult UserOverview()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (User user in _userLogic.GetAllUsers())
            {
                users.Add(new UserViewModel(user));
            }

            return View("UserOverview", users);
        }
    }
}