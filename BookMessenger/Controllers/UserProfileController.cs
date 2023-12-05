using Microsoft.AspNetCore.Mvc;

namespace BookMessenger.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
