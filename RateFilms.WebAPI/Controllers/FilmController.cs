using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Films;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.WebAPI.Helpers;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : Controller
    {
        private readonly ILogger<FilmController> _logger;

        private readonly IFilmService _filmService;

        public FilmController(
            ILogger<FilmController> logger,
            IFilmService filmService)
        {
            _logger = logger;
            _filmService = filmService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var favoriteFilm = await _filmService.GetFilmForAuthorizeUser(User.Identity.Name!, CultureHelper.GetCurrentCulture(Request));
                return Ok(favoriteFilm);
            }

            var film = await _filmService.GetFilms(CultureHelper.GetCurrentCulture(Request));

            return Ok(film);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(Guid id)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var favoriteFilm = await _filmService.GetFilmForAuthorizeUserById(id, User.Identity.Name!, CultureHelper.GetCurrentCulture(Request));

                if (favoriteFilm == null) return NotFound();

                return Ok(favoriteFilm);
            }

            var film = await _filmService.GetFilmById(id, CultureHelper.GetCurrentCulture(Request));

            if (film == null) return NotFound();

            return Ok(film);
        }

        [Authorize]
        [HttpGet("Favorite")]
        public async Task<IActionResult> GetFavoriteFilm()
        {
            var favoriteFilm = await _filmService.GetAllFavoriteFilms(User.Identity!.Name!, CultureHelper.GetCurrentCulture(Request));
            return Ok(favoriteFilm);
        }

        [Authorize]
        [HttpGet("RecommendedFilms")]
        public async Task<IActionResult> GetRecommendedFilms()
        {
            var film = await _filmService.GetRecommendedFilms(User.Identity!.Name!, CultureHelper.GetCurrentCulture(Request));
            return Ok(film);
        }

        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddFilms(Film film)
        {
            await _filmService.CreateFilmsAsync(film);

            return Ok();
        }

        [Authorize(Policy = "admin")]
        [HttpGet("GetFilmsWithUncheckedReview")]
        public async Task<IActionResult> GetFilmsWithUncheckedReview()
        {
            var film = await _filmService.GetFilmsWithUncheckedReview(CultureHelper.GetCurrentCulture(Request));

            return Ok(film);
        }



    }
}
