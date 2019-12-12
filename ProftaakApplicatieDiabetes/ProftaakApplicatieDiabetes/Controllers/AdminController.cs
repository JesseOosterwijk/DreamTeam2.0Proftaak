using Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakApplicatieDiabetes.Models;
using ProftaakApplicatieDiabetes.ViewModels;
using System;

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
            UserViewModel uvm = new UserViewModel()
            {
                Users = _userLogic.GetAllUsers()
            };

            return View("UserOverview", uvm);
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

            return View("UserOverview", userViewModel);
        }

        public ActionResult ChangePassword(User user)
        {
            string changedPassword = _accountLogic.ChangePassword(user.UserId);

            UserViewModel userViewModel = new UserViewModel
            {
                Users = _userLogic.GetAllUsers()
            };

            return View("UserOverview", userViewModel);
        }


    }
}