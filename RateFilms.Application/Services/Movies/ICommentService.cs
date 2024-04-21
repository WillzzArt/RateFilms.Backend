using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services.Movies
{
    public interface ICommentService
    {
        Task CreateComment(CommentRequest commentRequest, string username, bool isFilm);
        Task ChangeReviewStatus(Guid reviewId, string username, bool isFilm);
        Task PublishReview(AdminNote adminNote, string username);
        Task UpdateComment(CommentRequest commentRequest, string username);
        Task DeleteComment(CommentRequest commentRequest, string username);
        Task<IEnumerable<CommentResponse>> GetCommentsInFilm(Guid filmId, int count, string? username);
        Task<IEnumerable<CommentResponse>> GetCommentsInSerial(Guid serialId, int count, string? username);
        Task<IEnumerable<CommentResponse>> GetReviewsInMovie(Guid movieId, int count, string status, bool isFilm, string? username = null);
        Task<bool> ChangeLikeOnComment(Guid commentId, string username);
    }
}
