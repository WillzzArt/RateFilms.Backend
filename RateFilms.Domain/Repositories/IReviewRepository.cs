using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> FindReviewById(Guid reviewId, bool isFilm);
        Task<IEnumerable<Review>> GetReviewByStatus(Guid moviewId, Guid? userId, bool isFilm, Func<Review, bool> predicate);
        Task SetNewReviewStatus(Review review);
        Task CreateNoteToReview(Guid userId, Guid reviewId, string note);
        Task DeleteNoteToReview(Guid userId, Guid reviewId);
        Task<bool> UpdateReview(Guid reviewId, string text);
    }
}
