using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using ProftaakApplicatieDiabetes.Models;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class CalcController : Controller
    {
        private readonly ICalculationLogic calcLogic;

        public CalcController(ICalculationLogic _calcLogic)
        {
            calcLogic = _calcLogic;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Calculate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Calculate(CalcModel model)
        {
            calcLogic.CalculateMealtimeDose(model.Weight, model.TotalCarbs, model.CurrentBloodsugar, model.TargetBloodSugar);
            return RedirectToAction("Calculate");
        }
    }
}