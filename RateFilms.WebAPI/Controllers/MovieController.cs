using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Movirs;

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
                var movieFavorite = await _movieService.GetAllMoviesForAuthorizeUser(User.Identity.Name!);

                return Ok(movieFavorite);
            }

            var movie = await _movieService.GetAllMovies();

            return Ok(movie);
        }
    }
}
