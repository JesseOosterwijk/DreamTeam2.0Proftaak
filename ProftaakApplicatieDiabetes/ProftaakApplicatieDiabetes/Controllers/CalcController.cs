using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        public IActionResult Results(CalcViewModel model)
        {
            model.CalculatorResults = calcLogic.GetSpecificAdvice(23);
            return View(model);
        }

        [HttpPost]
        public IActionResult Calculate(CalcViewModel model)
        {
            model.userBSN = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
            ViewBag.Result = Math.Round(calcLogic.CalculateMealtimeDose(new Calculation(model.userBSN, model.Weight, model.TotalCarbs, model.CurrentBloodsugar, model.TargetBloodSugar)));

            return View();
        }
    }
}