using RateFilms.Domain.DTO;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<Film?>> GetFilms();
        Task CreateFilmsAsync(Film film);
        Task SetFavoriteFilm(FavoriteFilm favoriteFilm, string userName);
    }
}
