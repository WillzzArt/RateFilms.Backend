using RateFilms.Application.Models;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Converters
{
    internal static class MovieConverter
    {
        public static Movies UnionFilmAndSerials(IEnumerable<Film> films, IEnumerable<Serial> serials)
        {
            return new Movies
            {
                Films = films,
                Serials = serials
            };
        }
    }
}
