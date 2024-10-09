using Api006.Service.Dtos;
using Api006.Service.Dtos.Auth;
using Api006.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;


namespace Api006.App.Apps.client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var res = await _authService.Register(dto);
            return StatusCode(res.StatusCode);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var res = await _authService.Login(dto);
            return StatusCode(res.StatusCode, res.Data);
        }

        //[HttpPost("createrole")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole admin = new IdentityRole()
        //    {
        //        Name = "Admin",
        //    };

        //    IdentityRole superadmin = new IdentityRole()
        //    {
        //        Name = "SuperAdmin",
        //    };

        //    IdentityRole user = new IdentityRole()
        //    {
        //        Name = "User",
        //    };

        //    await _roleManager.CreateAsync(admin);
        //    await _roleManager.CreateAsync(superadmin);
        //    await _roleManager.CreateAsync(user);
        //    return Ok("roles created successfully");
        //}
    }
}
