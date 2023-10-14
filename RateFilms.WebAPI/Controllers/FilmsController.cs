using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services;
using RateFilms.Domain.Models;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
using System.Collections;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmsController : Controller
    {
        private readonly ILogger<FilmsController> _logger;

        private readonly IBaseRepository _repository;
        
        public FilmsController(
            ILogger<FilmsController> logger, 
            IBaseRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("GetFilms")]
        public IActionResult Index()
        {
            User user = new User();
            user.UserName = "aaa";
            user.Email = "email@emai.ru";
            user.Password = "ada9a9a9a";
            user.Role = Role.User;

            _repository.CreateAsync(user);
            return Ok(user);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable> GetAll()
        {
            return await _repository.GetAllAsync<User>();
        }
    }
}
