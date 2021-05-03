using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Models;
using Server.Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUpAsync(SignUpViewModel model)
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

            return BadRequest(JsonSerializer.Serialize(result.Errors.ToArray()));
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignInAsync(SignInViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);

            if (user != null)
            {
                var isPasswordMatch = await _userManager.CheckPasswordAsync(user, model.Password);

                if (isPasswordMatch)
                {
                    // Generate JWT Token
                    var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey)), SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                    {
                       new Claim(ClaimTypes.NameIdentifier, user.Id),
                       new Claim(ClaimTypes.Email, user.Email),
                       new Claim(ClaimTypes.Name, user.UserName)
                    };

                    var tokenOptions = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddHours(_jwtSettings.ExpireHours),
                        signingCredentials: credentials);

                    var result = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return new JsonResult(new JwtViewModel
                    {
                        Content = result,
                        ExpireHours = _jwtSettings.ExpireHours,
                        IsSucceeded = true
                    });
                }
            }

            return Unauthorized("Errors occured while attempting logging in");
        }
    }
}
