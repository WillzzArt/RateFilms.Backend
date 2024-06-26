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

        [AllowAnonymous]
        [HttpGet("FindUsers/{username}")]
        public async Task<IActionResult> SearchUsers(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return BadRequest();

            var users = await _userService.FindUsers(username);

            return Ok(users);
        }

        [Authorize(Policy = "admin")]
        [HttpPost("BanUser")]
        public async Task<IActionResult> BanUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return BadRequest();

            await _userService.SwitchBan(username, true);

            return Ok();
        }

        [Authorize(Policy = "admin")]
        [HttpPost("UnbanUser")]
        public async Task<IActionResult> UnbanUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return BadRequest();

            await _userService.SwitchBan(username, false);

            return Ok();
        }
    }
}
