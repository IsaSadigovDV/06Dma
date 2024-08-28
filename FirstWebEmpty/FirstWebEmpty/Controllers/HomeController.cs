using Microsoft.AspNetCore.Mvc;

namespace FirstWebEmpty.Controllers
{
    //controller
    public class HomeController:Controller
    {
        //action
        public string Hello()
        {
            return "Hello wordl";
        }
        public string HelloUser(string name)
        {
            return $"Hello {name}";
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
