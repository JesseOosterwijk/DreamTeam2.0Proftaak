using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Message()
        {
            return View();
        }
    }
}