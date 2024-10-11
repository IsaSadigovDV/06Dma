using Microsoft.AspNetCore.Mvc;

namespace Api006.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
