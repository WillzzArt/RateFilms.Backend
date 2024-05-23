using RateFilms.Application.Services.Films;
using RateFilms.Application.Services.Serials;
using RateFilms.Domain.DTO.Movies;
using System.Globalization;

namespace RateFilms.Application.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IFilmService _filmService;
        private readonly ISerialService _serialService;

        public MovieService(IFilmService filmService, ISerialService serialService)
        {
            _filmService = filmService;
            _serialService = serialService;
        }

        public async Task<Movie> GetAllFavoritesMovie(string username, CultureInfo culture)
        {
            var films = await _filmService.GetAllFavoriteFilms(username, culture);
            var serials = await _serialService.GetAllFavoriteSerials(username, culture);

            return new Movie(films, serials);
        }

        public async Task<Movie> GetAllMovies(CultureInfo culture)
        {
            var films = await _filmService.GetFilms(culture);
            var serils = await _serialService.GetSerials(culture);

            return new Movie(films, serils);
        }

        public async Task<Movie> GetAllMoviesForAuthorizeUser(string username, CultureInfo culture)
        {
            var films = await _filmService.GetFilmForAuthorizeUser(username, culture);
            var serils = await _serialService.GetSerialForAuthorizeUser(username, culture);

            return new Movie(films, serils);
        }

        public async Task<Movie> GetMoviesWithUncheckedReview(CultureInfo culture)
        {
            var films = await _filmService.GetFilmsWithUncheckedReview(culture);
            var serils = await _serialService.GetSerialsWithUncheckedReview(culture);

            return new Movie(films, serils);
        }

        public async Task<Movie> GetRecommendedMovie(string username, CultureInfo culture)
        {
            var films = await _filmService.GetRecommendedFilms(username, culture);
            var serials = await _serialService.GetRecommendedSerials(username, culture);

            return new Movie(films, serials);
        }
    }
}
