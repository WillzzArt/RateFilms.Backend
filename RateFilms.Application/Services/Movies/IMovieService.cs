using RateFilms.Domain.DTO.Movies;

namespace RateFilms.Application.Services.Movies
{
    public interface IMovieService
    {
        Task<Movie> GetAllMovies();
        Task<Movie> GetAllMoviesForAuthorizeUser(string username);
        Task<Movie> GetAllFavoritesMovie(string username);
        Task<Movie> GetMoviesWithUncheckedReview();
    }
}
