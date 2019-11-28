using System.Collections.Generic;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Logic.Interface;
using Models;
using ProftaakApplicatieDiabetes.ViewModels;
using ProftaakApplicatieDiabetes.Models;
using System.Linq;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorLogic _doctorLogic;
        private readonly IAccountLogic _accountLogic;

        public DoctorController(IDoctorLogic doctorLogic, IAccountLogic accountLogic)
        {
            _doctorLogic = doctorLogic;
            _accountLogic = accountLogic;
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