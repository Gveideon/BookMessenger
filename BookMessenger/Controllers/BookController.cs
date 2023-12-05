using BookMessenger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMessenger.Controllers
{
    public class BookController : Controller
    {
        ApplicationContext db;
        public BookController(ApplicationContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var books = db.Books.ToList();
            return View();
        }
    }
}
