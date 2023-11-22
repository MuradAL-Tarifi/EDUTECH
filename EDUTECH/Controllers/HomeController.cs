using Microsoft.AspNetCore.Mvc;

namespace EDUTECH.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
