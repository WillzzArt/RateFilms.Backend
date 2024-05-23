using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Movies;
using RateFilms.WebAPI.Helpers;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
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
    }
}
