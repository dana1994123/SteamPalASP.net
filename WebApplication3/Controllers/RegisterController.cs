using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.Models.Auth;
using WebApplication3.Models.ViewModel;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly UserContext _userContext;

        public RegisterController(IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager, UserContext userContext)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _userContext = userContext;
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserLoginModel model)
        {

            IActionResult result = Unauthorized();

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email

            };
            var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
            if(userWithEmail == null)
            {
                var createUserResult = await _userManager.CreateAsync(user, model.Password);

                if (createUserResult.Succeeded)
                {
                    return Ok();

                }
                else
                {
                    return Ok(createUserResult.Errors.ToString());
                }
            }
            else
            {
                return Conflict();
            }

        }
    }
}
