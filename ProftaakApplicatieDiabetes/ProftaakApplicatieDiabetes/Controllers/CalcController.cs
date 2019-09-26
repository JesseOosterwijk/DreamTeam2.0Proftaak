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
            CalcModel model = new CalcModel();
            model.Result = 0;
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(CalcModel model)
        {
            int hardcodedBSN = 226044440;
            model.Result = calcLogic.CalculateMealtimeDose(model.Weight, model.TotalCarbs, model.CurrentBloodsugar, model.TargetBloodSugar, hardcodedBSN);
            return Redirect(Url.Action("Calculate/" + model.Result));
            //return RedirectToAction("Calculate");
        }
    }
}