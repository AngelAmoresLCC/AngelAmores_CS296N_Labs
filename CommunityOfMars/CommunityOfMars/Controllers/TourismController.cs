using Microsoft.AspNetCore.Mvc;

namespace CommunityOfMars.Controllers
{
    public class TourismController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
