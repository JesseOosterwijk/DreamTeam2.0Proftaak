using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakApplicatieDiabetes.Models;
using ProftaakApplicatieDiabetes.ViewModels;

namespace ProftaakApplicatieDiabetes.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IAccountLogic _accountLogic;

        public UserController(IUserLogic userLogic, IAccountLogic accountLogic)
        {
            _userLogic = userLogic;
            _accountLogic = accountLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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
                    new Claim(ClaimTypes.Sid, newCustomer.UserId.ToString()),
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
                    case Enums.AccountType.Admin:
                        return RedirectToAction("Index", "Admin");
                    case Enums.AccountType.CareRecipient:
                        return RedirectToAction("Index", "Home");
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
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
                                    _userLogic.CreateUser(new CareRecipient(userViewModel.UserBSN, userViewModel.FirstName, userViewModel.LastName, 
                                        userViewModel.Address, userViewModel.Residence, userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate), 
                                        (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), true, Enums.AccountType.CareRecipient, userViewModel.Weight, password));
                                    break;
                                case Enums.AccountType.Admin:
                                    _userLogic.CreateUser(new Administrator(userViewModel.UserBSN, userViewModel.FirstName, userViewModel.LastName, 
                                        userViewModel.Address, userViewModel.Residence, userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate), 
                                        (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), true, Enums.AccountType.Admin, userViewModel.Weight, password));
                                    break;
                                case Enums.AccountType.Doctor:
                                    _userLogic.CreateUser(new Doctor(userViewModel.UserBSN, userViewModel.FirstName, userViewModel.LastName, 
                                        userViewModel.Address, userViewModel.Residence, userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate), 
                                        (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), true, Enums.AccountType.Doctor, userViewModel.Weight, password));
                                    break;
                                default:
                                    _userLogic.CreateUser(new CareRecipient(userViewModel.UserBSN, userViewModel.FirstName, userViewModel.LastName,
                                       userViewModel.Address, userViewModel.Residence, userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate), 
                                       (Enums.Gender)Enum.Parse(typeof(Enums.Gender), userViewModel.UserGender), true, Enums.AccountType.CareRecipient, userViewModel.Weight, password));
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

        public IActionResult SettingsMenu()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            var accountType = (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);
            UserViewModel model = new UserViewModel(_userLogic.GetUserById(userId));

            model.Type = accountType.ToString();
            model.ShareInfo = _accountLogic.SharingIsEnabled(userId);
            model.DeleteAllow = _accountLogic.DeleteInfoIsEnabled(userId);
            return View(model);
        }

        [Authorize(Policy = "CareRecipient")]
        public IActionResult UpdateUserInfo()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            UserViewModel model = new UserViewModel
            {
                Weight = _userLogic.GetUserById(userId).Weight
            };
            return View(model);
        }

        [Authorize(Policy = "CareRecipient")]
        public IActionResult InfoSharing()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            if (_accountLogic.SharingIsEnabled(userId) == true)
            {
                ViewBag.Description = "Gegevens delen staat op dit moment aan";
                return View();
            }
            else
            {
                ViewBag.Description = "Gegevens delen staat op dit moment uit";
                return View();
            }
        }

        [Authorize(Policy = "CareRecipient")]
        public IActionResult AllowInfoShare(int patientId)
        {
            _accountLogic.AllowInfoSharing(patientId);

            return RedirectToAction("InfoSharing");
        }

        [Authorize(Policy = "CareRecipient")]
        public IActionResult DisableInfoShare(int patientId)
        {
            _accountLogic.DisableInfoSharing(patientId);

            return RedirectToAction("InfoSharing");
        }

        [Authorize(Policy = "CareRecipient")]
        public IActionResult DeleteAllow()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            if (_accountLogic.DeleteInfoIsEnabled(userId) == true)
            {
                ViewBag.Description = "Gegevens verwijderen wordt op dit moment toegestaan";
                return View();
            }
            else
            {
                ViewBag.Description = "Gegevens verwijderen wordt op dit moment niet toegestaan";
                return View();
            }
        }

        [Authorize(Policy = "CareRecipient")]
        public IActionResult EnableInfoDelete(int patientId)
        {
            _accountLogic.EnableInfoDelete(patientId);

            return RedirectToAction("DeleteAllow");
        }

        [Authorize(Policy = "CareRecipient")]
        public IActionResult DisableInfoDelete(int patientId)
        {
            _accountLogic.DisableInfoDelete(patientId);

            return RedirectToAction("DeleteAllow");
        }

        [HttpPost]
        [Authorize(Policy = "CareRecipient")]
        public IActionResult UpdateWeight(UserViewModel model)
        {
            var patientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            _accountLogic.UpdateWeight(model.Weight, patientId);

            return RedirectToAction("UpdateUserInfo");
        }

        public IActionResult Forbidden() => View();

    }
} 