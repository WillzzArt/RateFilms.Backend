using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Serials;
using RateFilms.Domain.DTO;
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
                return Ok(favoriteSerial);
            }

            var serial = await _serialService.GetSerials();

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
        [HttpPost("SetFavorite")]
        public async Task<IActionResult> SetFavoriteSerial(FavoriteMovie favorite)
        {
            await _serialService.SetFavoriteSerial(favorite, User.Identity!.Name!);

            return Ok();
        }
    }
}
