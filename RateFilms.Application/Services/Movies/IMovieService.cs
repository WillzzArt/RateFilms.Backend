using RateFilms.Domain.DTO.Movies;
using System.Globalization;

namespace RateFilms.Application.Services.Movies
{
    public interface IMovieService
    {
        Task<Movie> GetAllMovies(CultureInfo culture);
        Task<Movie> GetAllMoviesForAuthorizeUser(string username, CultureInfo culture);
        Task<Movie> GetAllFavoritesMovie(string username, CultureInfo culture);
        Task<Movie> GetMoviesWithUncheckedReview(CultureInfo culture);
        Task<Movie> GetRecommendedMovie(string username, CultureInfo culture);
    }
}
