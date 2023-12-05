using Microsoft.AspNetCore.Mvc;

namespace BookMessenger.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
