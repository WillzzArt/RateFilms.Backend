using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<Film?>> GetFilms();
        Task<IEnumerable<FilmAuthorizeResponse?>> GetFilmForAuthorizeUser(string userName);
        Task CreateFilmsAsync(Film film);
        Task SetFavoriteFilm(FavoriteFilm favoriteFilm, string userName);
    }
}
