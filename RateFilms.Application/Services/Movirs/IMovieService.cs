using RateFilms.Domain.DTO.Movies;

namespace RateFilms.Application.Services.Movirs
{
    public interface IMovieService
    {
        Task<Movie> GetAllMovies();
        Task<Movie> GetAllMoviesForAuthorizeUser(string username);
    }
}
