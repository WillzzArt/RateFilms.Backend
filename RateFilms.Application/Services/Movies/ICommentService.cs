using RateFilms.Domain.DTO.Movies;

namespace RateFilms.Application.Services.Movies
{
    public interface ICommentService
    {
        Task CreateComment(CommentRequest commentRequest, string username, bool isFilm);
        Task ChangeReviewStatus(Guid reviewId, string username);
        Task PublishReview(AdminNoteRequest adminNote, string username);
        Task<bool> UpdateReview(Guid reviewId, string text);
        Task<IEnumerable<CommentResponse>> GetCommentsInMovie(Guid filmId, int countComm, string? username);
        Task<IEnumerable<ReviewResponse>> GetUncheckedReviewsInMovie(Guid movieId, string? username);
        Task<IEnumerable<ReviewResponse>> GetReviewsInMovie(Guid movieId, string? username);
        Task<bool> ChangeLikeOnComment(Guid commentId, string username);
        Task DeleteComment(Guid commentId);
    }
}
