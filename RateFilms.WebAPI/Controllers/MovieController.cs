using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Movies;
using RateFilms.Domain.DTO.Movies;
using RateFilms.WebAPI.Helpers;
using System.Security.Claims;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieFromKinopoiskServise _movieFromKinopoiskService;

        public MovieController(IMovieService movieService, IMovieFromKinopoiskServise movieFromKinopoiskService)
        {
            _movieService = movieService;
            _movieFromKinopoiskService = movieFromKinopoiskService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var movieFavorite = await _movieService.GetAllMoviesForAuthorizeUser(User.Identity.Name!, CultureHelper.GetCurrentCulture(Request));

                return Ok(movieFavorite);
            }

            var movie = await _movieService.GetAllMovies(CultureHelper.GetCurrentCulture(Request));

            return Ok(movie);
        }

        [Authorize]
        [HttpGet("Favorite")]
        public async Task<IActionResult> GetFavoriteMovies()
        {
            var favoriteMovie = await _movieService.GetAllFavoritesMovie(User.Identity!.Name!, CultureHelper.GetCurrentCulture(Request));
            return Ok(favoriteMovie);
        }

        [Authorize]
        [HttpGet("Recommended")]
        public async Task<IActionResult> GetRecomendedMovies()
        {
            var movie = await _movieService.GetRecommendedMovie(User.Identity!.Name!, CultureHelper.GetCurrentCulture(Request));
            return Ok(movie);
        }

        [Authorize(Policy = "admin")]
        [HttpGet("GetMoviesWithUncheckedReview")]
        public async Task<IActionResult> GetMoviesWithUncheckedReview()
        {
            var movies = await _movieService.GetMoviesWithUncheckedReview(CultureHelper.GetCurrentCulture(Request));

            return Ok(movies);
        }

        [Authorize]
        [HttpPost("SetFavorite")]
        public async Task<IActionResult> SetFavorite(FavoriteMovie favorite)
        {
            ClaimsPrincipal claims = HttpContext.User;
            await _movieService.SetFavoriteMovie(favorite, claims.Identity!.Name!);

            return Ok();
        }

        [Authorize(Policy = "admin")]
        [HttpPost("CreateFilmFromKinopoisk")]
        public async Task<IActionResult> CreateFilmFromKinopoisk(int movieId)
        {
            await _movieFromKinopoiskService.CreateFilm(movieId);
            return Ok();
        }

        [Authorize(Policy = "admin")]
        [HttpPost("CreateSerialFromKinopoisk")]
        public async Task<IActionResult> CreateSerialFromKinopoisk(int movieId)
        {
            await _movieFromKinopoiskService.CreateSerial(movieId);
            return Ok();
        }

    }
}
