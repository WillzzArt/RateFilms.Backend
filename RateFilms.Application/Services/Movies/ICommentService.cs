using RateFilms.Domain.DTO.Movies;

namespace RateFilms.Application.Services.Movies
{
    public interface ICommentService
    {
        Task CreateComment(CommentRequest commentRequest, string username, bool isFilm);
        Task PublishReview(Guid reviewId, string username, bool isFilm, bool isPublish = false);
        Task UpdateComment(CommentRequest commentRequest, string username);
        Task DeleteComment(CommentRequest commentRequest, string username);
        Task<IEnumerable<CommentResponse>> GetCommentsInFilm(Guid filmId, int count, string? username);
        Task<IEnumerable<CommentResponse>> GetCommentsInSerial(Guid filmId, int count, string? username);
        Task<bool> ChangeLikeOnComment(Guid commentId, string username);
    }
}
