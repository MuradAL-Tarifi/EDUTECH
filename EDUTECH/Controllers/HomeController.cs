using EDUTECH.Dto;
using EDUTECH.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDUTECH.Controllers
{
    public class HomeController : Controller
    {
        private readonly EDUTECHDBContext context;

        public HomeController(EDUTECHDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
		public IActionResult Index()
        {
            HomeViewModel homeViewModel=new HomeViewModel()
            {
                Universities = context.Universities.Take(6).ToList(),
            };
            return View(homeViewModel);
        }
        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }
		[HttpPost]
		public IActionResult ContactUs(Message message)
		{
            if (ModelState.IsValid)
            {
                if (message != null)
                {
                    context.Messages.Add(message);
                    context.SaveChanges();
					return View();

				}
			}
			return View(message);
		}
	}
}
