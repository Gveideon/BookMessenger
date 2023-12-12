using BookMessenger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMessenger.Controllers
{
    public class UserController : Controller
    {
        ApplicationContext db;
        public UserController(ApplicationContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllUsers()
        {
            return View(db.Users.ToList());
        }
        public IActionResult EditUser(int? id)
        {
            if(id is not null)
            {
                var u = db.Users.FirstOrDefault(u => u.Id == id);
                return View(u);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            db.Users.Update(user);
            db.SaveChanges();
            if (User.FindFirst(ClaimTypes.Role)?.Value == TypeRole.Admin.ToString())
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index");
        }
        public IActionResult CreateUser()
        {
          return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var profile = new UserProfile
            {
                User = user,
            };
            user.UserProfile = profile;
            db.Users.Add(user);
            db.UserProfiles.Add(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id != null)
            {
                var u = db.Users.FirstOrDefault(u => u.Id == id);
                if (u != null)
                {
                    db.Users.Remove(u);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public IActionResult UserDetails(int? id)
        {
            var u = db.Users.FirstOrDefault(u => u.Id == id);
            if (u is not null)
            {
                return View(u);
            }
            return NotFound();
        }
    }
}
