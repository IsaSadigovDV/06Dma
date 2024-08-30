using Microsoft.AspNetCore.Mvc;

namespace MvcFirstCrud.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
