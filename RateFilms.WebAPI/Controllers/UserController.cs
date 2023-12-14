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
            await _userService.UpdateUser(user, User.Identity!.Name!);

            return Ok();
        }
    }
}
