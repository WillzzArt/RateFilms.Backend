﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Films;
using RateFilms.Domain.DTO;
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

        public FilmsController(
            ILogger<FilmsController> logger,
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
                var favoriteFilm = await _filmService.GetFilmForAuthorizeUser(User.Identity.Name!);
                return Ok(favoriteFilm);
            }

            var film = await _filmService.GetFilms();

            return Ok(film);
        }

        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddFilms(Film film)
        {
            await _filmService.CreateFilmsAsync(film);

            return Redirect("/Films/GetFilms");
        }

        [Authorize]
        [HttpPost("SetFavorite")]
        public async Task<IActionResult> SetFavorite(FavoriteMovie favorite)
        {
            ClaimsPrincipal claims = HttpContext.User;
            await _filmService.SetFavoriteFilm(favorite, claims.Identity.Name);

            return Ok();
        }
    }
}
