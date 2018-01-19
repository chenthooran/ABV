using ABV.Domain.Entities;
using ABV.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABV.Web.Controllers
{

    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) 
                return new BadRequestObjectResult(GetErrors(result));

            return new OkObjectResult("Account created");
        }

        private List<string> GetErrors(IdentityResult result)
        {
            var modelErrors = new List<string>();
            foreach (var error in result.Errors)
            {
                modelErrors.Add(error.Description);
            }
            return modelErrors;
        }

    }
}