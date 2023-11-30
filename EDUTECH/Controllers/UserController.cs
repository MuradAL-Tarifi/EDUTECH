using EDUTECH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mail;

namespace EDUTECH.Controllers
{
    public class UserController : Controller
    {
        const string key = "UserId";
        private readonly EDUTECHDBContext _context;

        public UserController(EDUTECHDBContext context)
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
            if (userNew == null)
            {
                ModelState.AddModelError("Wrong", "Wrong username or password");
                return View();
            }
            else if (userNew.IsSuperAgent == true)
            {
                HttpContext.Session.SetInt32(key, userNew.Id);
                return RedirectToAction("Index", "Agent");
            }
            else if (userNew.IsAdmin == true)
            {
                HttpContext.Session.SetInt32(key, userNew.Id);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(User user)
        {
            var userNew = await _context.Users.Where(m => m.Email == user.Email).FirstOrDefaultAsync();
            if (userNew!=null)
            {
                SendPasswrodLinkEmail(userNew.Email);
                return View("Login");
            }
            else
            {
                ModelState.AddModelError("Wrong", "Email Not Found");
                return View();
            }
        }
        [NonAction]
        public void SendPasswrodLinkEmail(string email)
        {
            var userInfo = _context.Users.Where(x => x.Email == email).FirstOrDefault();

            userInfo.Password = CreateRandomPassword(9);
            _context.SaveChanges();


            MailMessage mm = new MailMessage();
            mm.To.Add(email);
            mm.Subject = "password chanded successfully.";
            mm.Body = "We are excited to tell you that your password chanded successfully.\n" + "Your New Password : " + userInfo.Password;
            mm.From = new MailAddress("EDUTECHWebSite1@gmail.com");

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            
            smtp.Credentials = new System.Net.NetworkCredential("EDUTECHWebSite1@gmail.com", "nkemojgrnxqngjgk");
            smtp.Send(mm);

        }
        [NonAction]
        public string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.SetInt32(key, 0);
            return RedirectToAction("Login", "User");
        }
    }
}
