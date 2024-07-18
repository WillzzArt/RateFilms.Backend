using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;
using System.Globalization;

namespace RateFilms.Application.Services.Movies
{
    public interface IMovieService
    {
        Task<MovieResponse> GetAllMovies(CultureInfo culture);
        Task<MovieResponse> GetAllMoviesForAuthorizeUser(string username, CultureInfo culture);
        Task<MovieResponse> GetAllFavoritesMovie(string username, CultureInfo culture);
        Task<MovieResponse> GetMoviesWithUncheckedReview(CultureInfo culture);
        Task<MovieResponse> GetRecommendedMovie(string username, CultureInfo culture);
        Task SetFavoriteMovie(FavoriteMovie favoriteFilm, string userName);
        Task CreateMovieAsync(Film film);
        Task CreateMovieAsync(Serial serial);
    }
}
