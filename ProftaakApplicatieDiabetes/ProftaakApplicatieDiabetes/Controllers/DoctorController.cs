using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Logic.Interface;
using Models;
using ProftaakApplicatieDiabetes.ViewModels;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class DoctorController : Controller
    {
        IDoctorLogic _doctorLogic = new DoctorLogic();
        //hardcoded for now
        private int _doctorId = 7;

        public IActionResult PatientDataOverviewView()
        {
            DoctorPatientSelectViewmodel viewModel = new DoctorPatientSelectViewmodel((List<User>)_doctorLogic.GetPatientsFromDoctorId(_doctorId));
            return View(viewModel);
        }
    }
}