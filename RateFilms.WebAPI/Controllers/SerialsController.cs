using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Serials;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SerialsController : Controller
    {
        private readonly ISerialService _serialService;

        public SerialsController(ISerialService serialService)
        {
            _serialService = serialService;
        }

        [Authorize(Policy = "admin")]
        [HttpPost("CreateSerial")]
        public async Task<IActionResult> AddSerials(Serial serial)
        {
            await _serialService.CreateSerialAsync(serial);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSerial()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var favoriteSerial = await _serialService.GetSerialForAuthorizeUser(User.Identity.Name!);

                if (favoriteSerial == null) return NotFound();

                return Ok(favoriteSerial);
            }

            var serial = await _serialService.GetSerials();

            if (serial == null) return NotFound();

            return Ok(serial);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSerialById(Guid id)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var favoriteSerial = await _serialService.GetSerialForAuthorizeUserById(id, User.Identity.Name!);
                return Ok(favoriteSerial);
            }

            var serial = await _serialService.GetSerialById(id);

            return Ok(serial);
        }

        [Authorize]
        [HttpGet("Favorite")]
        public async Task<IActionResult> GetRecommendedSerials()
        {
            var favoriteSerial = await _serialService.GetAllFavoriteSerials(User.Identity!.Name!);
            return Ok(favoriteSerial);
        }

        [Authorize]
        [HttpGet("RecommendedSerials")]
        public async Task<IActionResult> GetFavoriteSerials()
        {
            var serials = await _serialService.GetRecommendedSerials(User.Identity!.Name!);
            return Ok(serials);
        }


        [Authorize]
        [HttpPost("SetFavorite")]
        public async Task<IActionResult> SetFavoriteSerial(FavoriteMovie favorite)
        {
            await _serialService.SetFavoriteSerial(favorite, User.Identity!.Name!);

            return Ok();
        }

        [Authorize(Policy = "admin")]
        [HttpGet("GetSerialsWithUncheckedReview")]
        public async Task<IActionResult> GetSerialsWithUncheckedReview()
        {
            var serials = await _serialService.GetSerialsWithUncheckedReview();

            return Ok(serials);
        }
    }
}
