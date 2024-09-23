using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signinmanager;

        public AuthController(RoleManager<IdentityRole> rolemanager, UserManager<AppUser> usermanager, SignInManager<AppUser> signinmanager)
        {
            _rolemanager = rolemanager;
            _usermanager = usermanager;
            _signinmanager = signinmanager;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) { return View(model); }

            AppUser appUser = new AppUser()
            { 
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
            };
           var res =   await _usermanager.CreateAsync(appUser,model.Password);

            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                    return View(model);
            }

            await _usermanager.AddToRoleAsync(appUser, "User");

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var user = await _usermanager.FindByNameAsync(model.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "Username or password is not correct");
                return View(model);
            }

           var res =  await _signinmanager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (!res.Succeeded)
            {
                if (res.IsLockedOut) 
                {
                    ModelState.AddModelError("", "Your account is blocked for 5 mins");
                    return View(model);
                }
                if (res.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Your account is blocked");
                    return View(model);
                }
                ModelState.AddModelError("", "Username or password is not correct");
                return View(model);
            }
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signinmanager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Info()
        {
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);

            if (user == null) 
            {
                return NotFound();
            } 
            return View(user);
        }


        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole()
        //    {
        //        Name = "Admin"
        //    };
        //    IdentityRole role1 = new IdentityRole()
        //    {
        //        Name = "User"
        //    };
        //    IdentityRole role2 = new IdentityRole()
        //    {
        //        Name = "SuperAdmin"
        //    };

        //    await _rolemanager.CreateAsync(role);
        //    await _rolemanager.CreateAsync(role1);
        //    await _rolemanager.CreateAsync(role2);

        //    return Json("OK");
        //}
    }
}
