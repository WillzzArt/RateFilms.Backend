using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RateFilms.Application.Services;
using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
using RateFilms.WebAPI.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IBaseRepository _repository;

        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        private readonly IUserService _userService;

        public AccountController(
            ILogger<AccountController> logger,
            IBaseRepository repository,
            IUserRepository userRepository,
            IConfiguration configuration,
            IUserService userService)
        {
            _logger = logger;
            _repository = repository;
            _userRepository = userRepository;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(Registration model)
        {
            var response = await _userService.Register(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or email already exists" });
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
