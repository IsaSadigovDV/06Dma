using Api006.Service.Dtos;
using Api006.Service.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api006.App.Apps.client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthsController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            IdentityUser user = new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };
            var res = await _userManager.CreateAsync(user, dto.Password);

            if (!res.Succeeded)
            {
                return StatusCode(400, new { items = res.Errors });
            }

            await _userManager.AddToRoleAsync(user, "Admin");
            return StatusCode(201, new { description = "User created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return StatusCode(404, new { desc = "User is not found" });

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return StatusCode(400, new { desc = "Username or password is not correct" });

            var roles = await _userManager.GetRolesAsync(user);

            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, dto.UserName),
                new Claim(ClaimTypes.NameIdentifier, dto.Password)
            };

            foreach (var role in roles)
            {
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secret_key = "Yalniz ve yalninz Inci dersde idi :)";
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));

            var jwtToken = new JwtSecurityToken
                (
                    issuer: "https://localhost:7085",
                    audience: "https://localhost:7085",
                    claims: authclaims,
                    expires:DateTime.UtcNow.AddHours(3),
                    signingCredentials: new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(token);


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
