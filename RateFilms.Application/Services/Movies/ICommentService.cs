using RateFilms.Domain.DTO.Movies;

namespace RateFilms.Application.Services.Movies
{
    public interface ICommentService
    {
        Task CreateComment(CommentRequest commentRequest, string username, bool isFilm);
        Task ChangeReviewStatus(Guid reviewId, string username, bool isFilm);
        Task PublishReview(AdminNoteRequest adminNote, string username);
        Task UpdateComment(CommentRequest commentRequest, string username);
        Task DeleteComment(CommentRequest commentRequest, string username);
        Task<IEnumerable<CommentResponse>> GetCommentsInFilm(Guid filmId, int countComm, string? username);
        Task<IEnumerable<CommentResponse>> GetCommentsInSerial(Guid serialId, int countComm, string? username);
        Task<IEnumerable<ReviewResponse>> GetUncheckedReviewsInMovie(Guid movieId, int count, bool isFilm, string? username);
        Task<bool> ChangeLikeOnComment(Guid commentId, string username);
    }
}
