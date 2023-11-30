using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services
{
    public interface ISerialService
    {
        Task<IEnumerable<FilmResponse?>> GetFilms();
        Task<IEnumerable<FilmResponse?>> GetFilmForAuthorizeUser(string userName);
        Task CreateFilmsAsync(Film film);
        Task SetFavoriteFilm(FavoriteFilm favoriteFilm, string userName);
    }
}
