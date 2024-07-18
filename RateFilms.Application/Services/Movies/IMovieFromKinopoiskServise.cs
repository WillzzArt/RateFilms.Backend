using RateFilms.Common.Models.KinopoiskMovie;

namespace RateFilms.Application.Services.Movies
{
    public interface IMovieFromKinopoiskServise
    {
        Task CreateFilm(int filmId);
        Task CreateSerial(int serialId);
    }
}
