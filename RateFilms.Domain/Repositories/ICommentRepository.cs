using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(CommentDbModel commentDb, Guid userId, Guid movieId);
        Task<IEnumerable<Comment>> GetCommentsInMovie(Guid movieId, Guid? userId);
        Task<bool> SetLikedComment(Guid commentId, Guid userId);
        Task DeleteComment(Guid commentId);
    }
}
