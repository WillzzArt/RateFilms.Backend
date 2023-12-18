using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services;
using RateFilms.Domain.DTO.Authorization;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserData(UserExtendedResponse user)
        {
            var token = await _userService.UpdateUser(user, User.Identity!.Name!);
            if (token != null)
            {
                return Ok(token);
            }

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    var authorizeUser = await _userService.FindUserForProfile(User.Identity.Name!);
                    return Ok(authorizeUser);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                var user = await _userService.FindUserForProfile(username);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
