using Logic;
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
        private readonly IAccountLogic _accountLogic;

        public AdminController(IUserLogic userLogic, IAccountLogic accountLogic)
        {
            _userLogic = userLogic;
            _accountLogic = accountLogic;
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

        public ActionResult ChangePassword()
        {
            return View("ChangePasswordView");
        }

        public ActionResult DisableUser(User user)
        {
            bool status = true;

            if (user.Status)
            {
                status = false;
            }

            UserViewModel userViewModel = new UserViewModel
            {
                Users = _userLogic.GetAllUsers()
            };

            _accountLogic.UpdateStatus(user.UserId, status);
         
            return View("UserOverview");
        }
    }
}