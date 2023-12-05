using BookMessenger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMessenger.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationContext db;
        public AdminController(ApplicationContext db) 
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
