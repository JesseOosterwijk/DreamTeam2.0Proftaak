using Microsoft.AspNetCore.Mvc;
using Logic;
using Microsoft.AspNetCore.Authorization;

namespace ProftaakApplicatieDiabetes.Controllers
{
    [Authorize(Policy = "Admin")]
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