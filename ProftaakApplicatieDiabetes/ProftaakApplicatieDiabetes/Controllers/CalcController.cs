using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProftaakApplicatieDiabetes.Models;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class CalcController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculate(CalcModel model)
        {
            return View(model);
        }
    }
}