using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;
using System.Globalization;

namespace RateFilms.Application.Services.Films
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmResponse?>> GetFilms(CultureInfo culture);
        Task<IEnumerable<FilmResponse?>> GetFilmForAuthorizeUser(string userName, CultureInfo culture);
        Task<FilmExtendResponse?> GetFilmById(Guid id, CultureInfo culture);
        Task<FilmExtendResponse?> GetFilmForAuthorizeUserById(Guid id, string userName, CultureInfo culture);
        Task<IEnumerable<FilmResponse>> GetAllFavoriteFilms(string userName, CultureInfo culture);
        Task CreateFilmsAsync(Film film);
        Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName);
        Task<IEnumerable<FilmResponse>> GetFilmsWithUncheckedReview(CultureInfo culture);
        Task<IEnumerable<FilmResponse>> GetRecommendedFilms(string username, CultureInfo culture);
    }
}
