using Microsoft.AspNetCore.Mvc;

namespace BookStore.areas.admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
