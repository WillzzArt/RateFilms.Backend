using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(CommentDbModel commentDb, Guid userId, Guid movieId);
        Task<IEnumerable<Comment>> GetCommentsInFilm(Guid filmId, Guid? userId);
        Task<IEnumerable<Comment>> GetCommentsInSerial(Guid serialId, Guid? userId);
        Task<bool> SetLikedComment(Guid commentId, Guid userId);
    }
}
