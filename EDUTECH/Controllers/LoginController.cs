using EDUTECH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EDUTECH.Controllers
{
    public class LoginController : Controller
    {
        const string key = "UserId";
        private readonly EDUTECHDBContext _context;

        public LoginController(EDUTECHDBContext context)
        {
            this._context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            var userNew = await _context.Users.Where(m => m.Email == user.Email && m.Password == user.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                ModelState.AddModelError("Wrong", "Wrong username or password");
                return View();
            }
            else if (user.IsAdmin == true)
            {
                //HttpContext.Session.SetInt32(key, user.Id);
                return RedirectToAction("Index", "Home", new { area = "EmployeeDashbord" });
            }
            else if (user.IsSuperAgent == true)
            {
                //HttpContext.Session.SetInt32(key, user.Id);
                return RedirectToAction("Index", "Admin",
                        new { area = "Admin" });
            }
            else
            {
                return View();
            }
        }
    }
}
