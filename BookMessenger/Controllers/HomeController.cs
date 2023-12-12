using BookMessenger.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BookMessenger.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;

            // create admin 
            var u = db.Users.FirstOrDefault(u => u.Login == "ADMIN" && u.Password == "ADMIN");
            if(u is null)
            {
                u = new User { Login = "ADMIN", Password = "ADMIN", Role = TypeRole.Admin };
                db.Users.Add(u);
                db.SaveChanges();
            }
        }
      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Reg(User user)
        {
            var u = db.Users.Find(user.Id);
            if (u == null && user != null && user.Login != null && user.Password != null)
            {
                user.Role = TypeRole.User;
                var profile = new UserProfile
                {
                    User = user,
                };
                user.UserProfile = profile;
                db.Users.Add(user);
                db.UserProfiles.Add(profile);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            if (user == null) return RedirectToAction("Index");
            var u = db.Users.FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password).Result;
            if (u == null) return RedirectToAction("Index");
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, u.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, u.Role.ToString()),
                new Claim(ClaimTypes.Surname, u.Id.ToString()),
             };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Logout()
        {
            await Request.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}