using Microsoft.AspNetCore.Mvc;

namespace BookMessenger.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
