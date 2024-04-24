using RateFilms.Application.Services.Films;
using RateFilms.Application.Services.Serials;
using RateFilms.Domain.DTO.Movies;

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

        public async Task<Movie> GetAllFavoritesMovie(string username)
        {
            var films = await _filmService.GetAllFavoriteFilms(username);
            var serials = await _serialService.GetAllFavoriteSerials(username);

            return new Movie(films, serials);
        }

        public async Task<Movie> GetAllMovies()
        {
            var films = await _filmService.GetFilms();
            var serils = await _serialService.GetSerials();

            return new Movie(films, serils);
        }

        public async Task<Movie> GetAllMoviesForAuthorizeUser(string username)
        {
            var films = await _filmService.GetFilmForAuthorizeUser(username);
            var serils = await _serialService.GetSerialForAuthorizeUser(username);

            return new Movie(films, serils);
        }

        public async Task<Movie> GetMoviesWithUncheckedReview()
        {
            var films = await _filmService.GetFilmsWithUncheckedReview();
            var serils = await _serialService.GetSerialsWithUncheckedReview();

            return new Movie(films, serils);
        }
    }
}
