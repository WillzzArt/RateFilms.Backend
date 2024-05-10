using Microsoft.AspNetCore.Mvc;
using RateFilms.Domain.Repositories;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favouriteRepository;

        public FavoriteController(IFavoriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
        }

        [HttpGet("InSerial")]
        public async Task<IActionResult> GetFavInSerial()
        {
            var fav = await _favouriteRepository.FindFavoriteInSerials();
            return Ok(fav);
        }

        [HttpGet("InFilm")]
        public async Task<IActionResult> GetFavInFilm()
        {
            var fav = await _favouriteRepository.FindFavoriteInFilms();
            return Ok(fav);
        }
    }
}
