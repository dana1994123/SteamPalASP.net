using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.Models.Auth;
using WebApplication3.Models.ViewModel;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly UserContext _userContext;

        public LoginController(IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager, UserContext userContext)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _userContext = userContext;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginModel login)
        {
            IActionResult result = Unauthorized();
            var user = await AuthenticateUser(login);
            if(user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                result = Ok(new { token = tokenString });
            }
            return result;
        }

        private object GenerateJSONWebToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claimsIdentity = new[]
            {
                new Claim("Name" , user.Email),
                new Claim("TestClaim" , "Any thing you want")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
               _config["Jwt:Issuer"],
               claims : claimsIdentity,
               expires : DateTime.Now.AddMonths(4),
               signingCredentials: credentials
               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task <User> AuthenticateUser(UserLoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var userInfo = await _userManager.FindByEmailAsync(login.Email);
            }
            return null;
        }
    }
}
