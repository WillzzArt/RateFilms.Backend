using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(CommentDbModel commentDb, Guid userId, Guid movieId);
        Task<Comment> FindCommentById(Guid reviewId, bool isFilm);
        Task<IEnumerable<Comment>> GetCommentsInFilm(Guid filmId, int count, Guid? userId);
        Task<IEnumerable<Comment>> GetCommentsInSerial(Guid serialId, int count, Guid? userId);
        Task<bool> SetLikedComment(Guid commentId, Guid userId);
        Task SetNewReviewStatus(Comment review);
    }
}
