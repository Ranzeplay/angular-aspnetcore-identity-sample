using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.EmailAddress,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Get database user in order to get userId
                user = await _userManager.FindByEmailAsync(model.EmailAddress);

                return new JsonResult(new
                {
                    user.Email,
                    user.UserName,
                    user.Id
                });
            }

            return new JsonResult(result.Errors.ToArray());
        }
    }
}
