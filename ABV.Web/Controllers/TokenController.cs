using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ABV.Domain.Entities;
using ABV.Web.ViewModels;

namespace ABV.Web.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public TokenController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // GET: api/token
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

                    if (!result.Succeeded)
                    {
                        return Unauthorized();
                    }

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, model.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, role[0])
                    };

                    var token = new JwtSecurityToken
                    (
                        issuer: _configuration["Token:Issuer"],
                        audience: _configuration["Token:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(14),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                                SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), isAdmin = role[0] == "Admins" ? true : false });
                }
            }

            return BadRequest();
        }
    }
}