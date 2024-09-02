using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
