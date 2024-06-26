using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RateFilms.Application.Services.Movies;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;

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
            await _commentSerivice.CreateComment(comment, User.Identity!.Name!, true);
            return Ok();
        }

        [Authorize]
        [HttpPost("CommentInSerial")]
        public async Task<IActionResult> CreateCommentInSerial(CommentRequest comment)
        {
            await _commentSerivice.CreateComment(comment, User.Identity!.Name!, false);
            return Ok();
        }

        [HttpGet("CommentInFilm")]
        public async Task<IActionResult> GetCommentInFilm(Guid filmId, int countComm)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var comments = await _commentSerivice.GetCommentsInFilm(filmId, countComm, User.Identity.Name);
                return Ok(comments);
            }
            else
            {
                var comments = await _commentSerivice.GetCommentsInFilm(filmId, countComm, null);
                return Ok(comments);
            }
            
        }

        [HttpGet("CommentInSerial")]
        public async Task<IActionResult> GetCommentInSerial(Guid serialId, int countComm)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var comments = await _commentSerivice.GetCommentsInSerial(serialId, countComm, User.Identity.Name);
                return Ok(comments);
            }
            else
            {
                var comments = await _commentSerivice.GetCommentsInSerial(serialId, countComm, null);
                return Ok(comments);
            }
        }

        [Authorize]
        [HttpPut("{commentId}")]
        public async Task<IActionResult> LikeComment(Guid commentId)
        {
            var isUpdate = await _commentSerivice.ChangeLikeOnComment(commentId, User.Identity!.Name!);
            if (isUpdate)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPut("ChangeReviewStatus")]
        public async Task<IActionResult> ChangeReviewStatusInFilm(Guid reviewId)
        {
            await _commentSerivice.ChangeReviewStatus(reviewId, User.Identity!.Name!);

            return Ok();
        }

        [Authorize(Policy = "admin")]
        [HttpPut("PublishReview")]
        public async Task<IActionResult> PublishReviewInFilm(AdminNoteRequest adminNote)
        {
            await _commentSerivice.PublishReview(adminNote, User.Identity!.Name!);

            return Ok();
        }

        [Authorize(Policy = "admin")]
        [HttpGet("UserReviews")]
        public async Task<IActionResult> GetUserReviews(Guid movieId, bool isFilm)
        {
            var reviews = await _commentSerivice.GetUncheckedReviewsInMovie(movieId, isFilm, null);

            return Ok(reviews);
        }

        [Authorize]
        [HttpGet("MyReviews")]
        public async Task<IActionResult> GetMyReviews(Guid movieId, bool isFilm)
        {
            var reviews = await _commentSerivice.GetUncheckedReviewsInMovie(movieId, isFilm, User.Identity!.Name!);

            return Ok(reviews);
        }

        [HttpGet("ReviewsInMovie")]
        public async Task<IActionResult> GetReviewsInMovie(Guid movieId, bool isFilm)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var comments = await _commentSerivice.GetReviewsInMovie(movieId, isFilm, User.Identity.Name);
                comments = comments.ToList();

                return Ok(comments);
            }
            else
            {
                var comments = await _commentSerivice.GetReviewsInMovie(movieId, isFilm, null);
                comments = comments.ToList();

                return Ok(comments);
            }

        }

        [Authorize]
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> UpdateReview(Guid reviewId, string text)
        {
            var isUpdate = await _commentSerivice.UpdateReview(reviewId, text);
            if (isUpdate)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = "admin")]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            await _commentSerivice.DeleteComment(commentId);
            return Ok();
        }
    }
}
