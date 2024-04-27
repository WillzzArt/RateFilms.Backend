using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services.Films
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmResponse?>> GetFilms();
        Task<IEnumerable<FilmResponse?>> GetFilmForAuthorizeUser(string userName);
        Task<FilmExtendResponse?> GetFilmById(Guid id);
        Task<FilmExtendResponse?> GetFilmForAuthorizeUserById(Guid id, string userName);
        Task<IEnumerable<FilmResponse>> GetAllFavoriteFilms(string userName);
        Task CreateFilmsAsync(Film film);
        Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName);
        Task<IEnumerable<FilmResponse>> GetFilmsWithUncheckedReview();
    }
}
