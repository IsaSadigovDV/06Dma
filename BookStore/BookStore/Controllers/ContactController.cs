using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
