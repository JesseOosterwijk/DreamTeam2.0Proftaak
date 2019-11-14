using Microsoft.AspNetCore.Mvc;
using Logic;

namespace ProftaakApplicatieDiabetes.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserLogic _userLogic;

        public AdminController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}