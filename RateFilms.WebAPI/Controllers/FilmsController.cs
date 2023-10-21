using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("GetFilms")]
        public async Task<IEnumerable<Film>> IndexAsync()
        {
            var user = await _repository.GetAllAsync<User>();

            
            return await _repository.GetAllAsync<Film>();
        }

        [Authorize(Policy = "admin")]
        [HttpGet("GetAdmin")]
        public IActionResult CreateFilm()
        {

            return Ok("I admin");
        }
    }
}
