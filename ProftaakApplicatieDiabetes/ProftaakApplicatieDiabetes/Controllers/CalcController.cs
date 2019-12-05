using System;
using System.Linq;
using Logic;
using Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakApplicatieDiabetes.Models;

namespace ProftaakApplicatieDiabetes.Controllers
{
    [Authorize(Policy = "CareRecipient")]
    public class CalcController : Controller
    {
        private readonly ICalculationLogic calcLogic;
        private readonly IUserLogic userLogic;

        public CalcController(ICalculationLogic _calcLogic, IUserLogic _userLogic)
        {
            calcLogic = _calcLogic;
            userLogic = _userLogic;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculate()
        {
            var Id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            CalcViewModel model = new CalcViewModel
            {
                Weight = userLogic.GetUserById(Id).Weight
            };
            ViewBag.Result = TempData["Result"];

            return View(model);
        }

        public IActionResult Results(CalcViewModel model)
        {
            model.CalculatorResults = calcLogic.GetSpecificAdvice(23);
            return View(model);
        }

        [HttpPost]
        public IActionResult Calculate(CalcViewModel model)
        {
            model.Id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
            model.Weight = userLogic.GetUserById(model.Id).Weight;
            TempData["Result"] = Math.Round(calcLogic.CalculateMealtimeDose(new Calculation(model.Id, model.Weight, model.TotalCarbs, model.CurrentBloodsugar, model.TargetBloodSugar)));

            return RedirectToAction("Calculate");
        }
    }
}