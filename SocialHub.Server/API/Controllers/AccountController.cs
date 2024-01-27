using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialHub.Server.API.DTOs;
using SocialHub.Server.API.Services;

namespace SocialHub.Server.API.Controllers
{
    //Treating this a its own service, keeping it as seperate from the rest of the API as possible

    //AllowAnonymous attribute allows the user to access the login page without being authenticated
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(UserManager<AppUser> userManager, TokenService tokenService ) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly TokenService _tokenService = tokenService;

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
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = _tokenService.CreateToken(user),
                    Username = user.UserName
                };
            }

            return Unauthorized();
        }

        //[HttpPost("register")]

    }
}
