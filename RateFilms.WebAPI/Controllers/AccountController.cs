using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
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

        public AccountController(
            ILogger<AccountController> logger,
            IBaseRepository repository,
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Registration registration)
        {
            User user = new User();
            var password = HashPasswordHelper.HashPassword(registration.Password);
            user.Password = password;
            user.Email = registration.Email;
            user.UserName = registration.UserName;

            _repository.CreateAsync(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Login login)
        {
            User? user = await _userRepository.FindUser(login.UserLogin);
            if (user == null)
            {
                return BadRequest("user not found");
                
            }
            if (user.Password != HashPasswordHelper.HashPassword(login.Password))
            {
                return BadRequest("invalid password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
