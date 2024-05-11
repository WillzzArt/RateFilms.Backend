using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Films;
using RateFilms.Application.Services.Serials;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.DTO.Serials;
using RateFilms.Domain.Models.DomainModels;
using System.Security.Claims;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmsController : Controller
    {
        private readonly ILogger<FilmsController> _logger;

        private readonly IFilmService _filmService;
        private readonly ISerialService _serialService;

        public FilmsController(
            ILogger<FilmsController> logger,
            IFilmService filmService,
            ISerialService serialService)
        {
            _logger = logger;
            _filmService = filmService;
            _serialService = serialService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var favoriteFilm = await _filmService.GetFilmForAuthorizeUser(User.Identity.Name!);
                return Ok(favoriteFilm);
            }

            var film = await _filmService.GetFilms();

            return Ok(film);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(Guid id)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var favoriteFilm = await _filmService.GetFilmForAuthorizeUserById(id, User.Identity.Name!);

                if (favoriteFilm == null) return NotFound();

                return Ok(favoriteFilm);
            }

            var film = await _filmService.GetFilmById(id);

            if (film == null) return NotFound();

            return Ok(film);
        }

        [Authorize]
        [HttpGet("Favorite")]
        public async Task<IActionResult> GetFavoriteFilm()
        {
            var favoriteFilm = await _filmService.GetAllFavoriteFilms(User.Identity!.Name!);
            return Ok(favoriteFilm);
        }

        [Authorize]
        [HttpGet("RecommendedFilms")]
        public async Task<IActionResult> GetRecommendedFilms()
        {
            var film = await _filmService.GetRecommendedFilms(User.Identity!.Name!);
            return Ok(film);
        }

        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddFilms(Film film)
        {
            await _filmService.CreateFilmsAsync(film);

            return Ok();
            //return Redirect("/Films");
        }

        [Authorize]
        [HttpPost("SetFavorite")]
        public async Task<IActionResult> SetFavorite(FavoriteMovie favorite)
        {
            ClaimsPrincipal claims = HttpContext.User;
            await _filmService.SetFavoriteFilm(favorite, claims.Identity!.Name!);

            return Ok();
        }

        /*[Authorize]
        [HttpPost("SetFavorites")]
        public async Task<IActionResult> SetFavorites(int min, int max)
        {
            ClaimsPrincipal claims = HttpContext.User;

            var films = await _filmService.GetFilms();
            var serial = await _serialService.GetSerials();
            var filmss = films.ToList();
            var serials = serial.ToList();
            var rnd = new Random();

            for (var i = 0; i < 6; i++)
            {
                var fav = new FavoriteMovie
                {
                    MovieId = filmss[rnd.Next(0, 18)].Id,
                    IsFavorite = false,
                    StatusMovie = StatusMovie.None.ToString(),
                    Score = rnd.Next(min, max)
                };

                await _filmService.SetFavoriteFilm(fav, claims.Identity!.Name!);
            }
            for (var i = 0; i < 6; i++)
            {
                var fav = new FavoriteMovie
                {
                    MovieId = serials[rnd.Next(0, 9)].Id,
                    IsFavorite = false,
                    StatusMovie = StatusMovie.None.ToString(),
                    Score = rnd.Next(min, max)
                };

                await _serialService.SetFavoriteSerial(fav, claims.Identity!.Name!);
            }
            

            return Ok();
        }*/

        [Authorize(Policy = "admin")]
        [HttpGet("GetFilmsWithUncheckedReview")]
        public async Task<IActionResult> GetFilmsWithUncheckedReview()
        {
            var film = await _filmService.GetFilmsWithUncheckedReview();

            return Ok(film);
        }
        
    }
}
