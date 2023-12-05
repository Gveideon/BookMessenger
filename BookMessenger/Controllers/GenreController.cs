using Microsoft.AspNetCore.Mvc;

namespace BookMessenger.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
