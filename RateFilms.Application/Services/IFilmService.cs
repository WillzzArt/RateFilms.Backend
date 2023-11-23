using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.StorageModels;

namespace RateFilms.Application.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<Film?>> GetFilms();
        Task CreateFilmsAsync(Film film);
    }
}
