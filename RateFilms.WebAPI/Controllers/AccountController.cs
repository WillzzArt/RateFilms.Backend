using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services;
using RateFilms.Domain.DTO.Authorization;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IUserService _userService;

        public AccountController(
            ILogger<AccountController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword(LoginRequest model)
        {
            var result = await _userService.ChangePassword(model);

            if (result == null)
            {
                return BadRequest(new { message = "Username not exists" });
            }

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(Registration model)
        {
            var response = await _userService.Register(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or email already exists" });
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
