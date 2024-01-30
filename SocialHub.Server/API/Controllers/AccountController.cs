using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialHub.Server.API.DTOs;
using SocialHub.Server.API.Services;
using System.Security.Claims;

namespace SocialHub.Server.API.Controllers
{
    //Treating this a its own service, keeping it as seperate from the rest of the API as possible

    //[AllowAnonymous], when placed at the controller level, allows all methods in the controller to be accessed without authorization
    //If placed at the method level, only that method can be accessed without authorization

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(UserManager<AppUser> userManager, TokenService tokenService ) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly TokenService _tokenService = tokenService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                ModelState.AddModelError("username", "Username is already in use");
                return ValidationProblem();
            }
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.EmailAddress))
            {
                ModelState.AddModelError("emailAddress", "Email Address is already in use");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                //Email is from Microsoft.AspNetCore.Identity, EmailAddress is from RegisterDto                
                Email = registerDto.EmailAddress,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(result.Succeeded)
            {
                return CreateUserObject(user);
            }
            return BadRequest(result.Errors);
        }
        
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }

    }
}
