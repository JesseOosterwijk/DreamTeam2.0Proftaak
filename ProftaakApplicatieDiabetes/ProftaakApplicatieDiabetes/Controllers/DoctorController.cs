using System.Collections.Generic;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Logic.Interface;
using Models;
using ProftaakApplicatieDiabetes.ViewModels;
using ProftaakApplicatieDiabetes.Models;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorLogic _doctorLogic;

        public DoctorController(IDoctorLogic doctorLogic)
        {
            _doctorLogic = doctorLogic;
        }
        //hardcoded for now
        //private int _doctorId = 7;

        //public IActionResult PatientDataOverviewView()
        //{
        //    DoctorPatientSelectViewmodel viewModel = new DoctorPatientSelectViewmodel((List<User>)_doctorLogic.GetPatientsFromDoctorId(_doctorId));
        //    return View(viewModel);
        //}

        public IActionResult GetAllLinkedPatients()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            UserViewModel model = new UserViewModel()
            {
                Users = _doctorLogic.GetAllLinkedPatients(userId)
            };

            return View(model);
        }
    }
}