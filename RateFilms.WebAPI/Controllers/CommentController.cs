using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Movies;
using RateFilms.Domain.DTO.Movies;

namespace RateFilms.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentSerivice;

        public CommentController(ICommentService commentSerivice)
        {
            _commentSerivice = commentSerivice;
        }

        [Authorize]
        [HttpPost("CommentInFilm")]
        public async Task<IActionResult> CreateCommentInFilm(CommentRequest comment)
        {
            await _commentSerivice.CreateCommentInFilm(comment, User.Identity!.Name!);
            return Ok();
        }

        [Authorize]
        [HttpPost("CommentInSerial")]
        public async Task<IActionResult> CreateCommentInSerial(CommentRequest comment)
        {
            await _commentSerivice.CreateCommentInSerial(comment, User.Identity!.Name!);
            return Ok();
        }

        [HttpGet("CommentInFilm")]
        public async Task<IActionResult> GetCommentInFilm(Guid filmId, int count)
        {
            var comment = await _commentSerivice.GetCommentsInFilm(filmId, count);
            return Ok(comment);
        }

        [HttpGet("CommentInSerial")]
        public async Task<IActionResult> GetCommentInSerial(Guid serialId, int count)
        {
            var comment = await _commentSerivice.GetCommentsInSerial(serialId, count);
            return Ok(comment);
        }
    }
}
