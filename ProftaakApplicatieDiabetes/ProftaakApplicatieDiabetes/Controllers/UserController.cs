using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic;
using Logic.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakApplicatieDiabetes.Models;
using ProftaakApplicatieDiabetes.ViewModels;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel userViewModel)
        {
            try
            {
                User newCustomer = _userLogic.CheckValidityUser(userViewModel.EmailAddress, userViewModel.Password);

                if (!newCustomer.Status)
                {
                    ViewBag.Message = "Dit account is geblokkeerd. Neem contact op met de administrator.";
                    return View();
                }

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, newCustomer.BSN.ToString()),
                    new Claim(ClaimTypes.Name, newCustomer.FirstName + " " + newCustomer.LastName),
                    new Claim(ClaimTypes.Gender, newCustomer.UserGender.ToString()),
                    new Claim(ClaimTypes.Email, newCustomer.EmailAddress),
                    new Claim(ClaimTypes.StreetAddress, newCustomer.Address),
                    new Claim(ClaimTypes.Role, newCustomer.UserAccountType.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                switch (newCustomer.UserAccountType)
                {
                    case Enums.AccountType.Administrator:
                        return RedirectToAction("Index", "Admin");
                    case Enums.AccountType.CareRecipient:
                        return RedirectToAction("ViewMessage", "Message");
                    case Enums.AccountType.Doctor:
                        return RedirectToAction("Index", "Home");
                    default:
                        return RedirectToAction("Overview", "CareRecipient");
                }
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "De gegevens zijn niet ingevuld";
                return View();
            }
            catch (IndexOutOfRangeException)
            {
                ViewBag.Message = "De gegevens komen niet overeen";
                return View();
            }
            catch (ArgumentException)
            {
                ViewBag.Message = "Wachtwoord verkeerd ingevuld";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserViewModel userViewModel, string password, string passwordValidation)
        {
            try
            {
                if (password == passwordValidation)
                {
                    if (_userLogic.CheckIfUserAlreadyExists(userViewModel.EmailAddress))
                    {
                        if (_userLogic.CheckIfEmailIsValid(userViewModel.EmailAddress))
                        {
                            switch (userViewModel.UserAccountType)
                            {
                                case Enums.AccountType.CareRecipient:
                                    _userLogic.CreateUser(new CareRecipient(userViewModel.UserBSN, Enums.AccountType.CareRecipient, userViewModel.FirstName, 
                                        userViewModel.LastName, userViewModel.EmailAddress, password, userViewModel.Address, userViewModel.Residence, 
                                        (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), userViewModel.Weight, 
                                        Convert.ToDateTime(userViewModel.BirthDate), true));
                                    break;
                                case Enums.AccountType.Administrator:
                                    _userLogic.CreateUser(new Administrator(userViewModel.UserBSN, Enums.AccountType.Administrator, userViewModel.FirstName,
                                        userViewModel.LastName, userViewModel.EmailAddress, password, userViewModel.Address, userViewModel.Residence,
                                        (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), userViewModel.Weight,
                                        Convert.ToDateTime(userViewModel.BirthDate), true));
                                    break;
                                case Enums.AccountType.Doctor:
                                    _userLogic.CreateUser(new Doctor(userViewModel.UserBSN, Enums.AccountType.Doctor, userViewModel.FirstName,
                                        userViewModel.LastName, userViewModel.EmailAddress, password, userViewModel.Address, userViewModel.Residence,
                                        (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), userViewModel.Weight,
                                        Convert.ToDateTime(userViewModel.BirthDate), true));
                                    break;
                                default:
                                    _userLogic.CreateUser(new CareRecipient(userViewModel.UserBSN, Enums.AccountType.CareRecipient, userViewModel.FirstName,
                                        userViewModel.LastName, userViewModel.EmailAddress, password, userViewModel.Address, userViewModel.Residence,
                                        (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), userViewModel.Weight,
                                        Convert.ToDateTime(userViewModel.BirthDate), true));
                                    break;
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Foutieve email ingevoerd";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Er bestaat al een account met deze e-mail";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "De wachtwoorden komen niet overheen";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Message = "De gebruiker is niet aangemaakt";
                return View();
            }
            return RedirectToAction("Login");
        }
    }
}