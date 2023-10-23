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

        private readonly IFilmService _filmService;
        
        public FilmsController(
            ILogger<FilmsController> logger,
            IFilmService filmService)
        {
            _logger = logger;
            _filmService = filmService;
        }

        [HttpGet("GetFilms")]
        public async Task<IEnumerable<Film>> IndexAsync()
        {
            var film = await _filmService.GetFilms();
            return film;
        }

        [Authorize(Policy = "admin")]
        [HttpPost("CreateFilm")]
        public async Task<IActionResult> AddFilms(Film film)
        {
            await _filmService.CreateFilmsAsync(film);

            return Redirect("/Films/GetFilms");
        }

        [Authorize(Policy = "admin")]
        [HttpGet("GetAdmin")]
        public IActionResult CreateFilm()
        {

            return Ok("I admin");
        }
    }
}
