using Microsoft.AspNetCore.Mvc;

namespace CommunityOfMars.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Links()
        {
            return View();
        }
    }
}
