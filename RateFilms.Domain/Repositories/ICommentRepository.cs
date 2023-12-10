using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(CommentDbModel commentDb, Guid userId, Guid movieId);
        Task<IEnumerable<Comment>> GetCommentsInFilm(Guid FilmId, int count);
        Task<IEnumerable<Comment>> GetCommentsInSerial(Guid SerialId, int count);
    }
}
