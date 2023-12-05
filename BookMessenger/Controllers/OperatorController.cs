using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMessenger.Controllers
{
    [Authorize]
    public class OperatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
