using Microsoft.AspNetCore.Mvc;
using Logic.Interface;
using ProftaakApplicatieDiabetes.ViewModels;
using ProftaakApplicatieDiabetes.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProftaakApplicatieDiabetes.Controllers
{
    [Authorize(Policy = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorLogic _doctorLogic;
        private readonly IAccountLogic _accountLogic;
        private readonly IUserLogic _userLogic;

        public DoctorController(IDoctorLogic doctorLogic, IAccountLogic accountLogic, IUserLogic userLogic)
        {
            _doctorLogic = doctorLogic;
            _accountLogic = accountLogic;
            _userLogic = userLogic;
        }

        public IActionResult GetAllLinkedPatients()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            UserViewModel model = new UserViewModel()
            {
                Users = _doctorLogic.GetAllLinkedPatients(userId)
            };

            return View(model);
        }

        public IActionResult GetPatientData(int patientId)
        {
            DoctorPatientSelectViewmodel model = new DoctorPatientSelectViewmodel()
            {
                FirstName = _userLogic.GetUserById(patientId).FirstName,
                LastName = _userLogic.GetUserById(patientId).LastName,
                Calculations = _doctorLogic.GetPatientData(patientId)
            };
            if (_accountLogic.SharingIsEnabled(patientId))
            {
                ViewBag.Message = "";
            }
            else
            {
                ViewBag.Message = "Deze patiënt wil zijn gegevens op dit moment niet delen";
            }

            return View(model);
        }
    }
}