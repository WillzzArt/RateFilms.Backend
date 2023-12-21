using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.DTO.Serials;

namespace RateFilms.Domain.DTO.Movies
{
    public class Movie
    {
        public List<FilmResponse> Films { get; set; }
        public List<SerialResponse> Serials { get; set; }

        public Movie(IEnumerable<FilmResponse> films, IEnumerable<SerialResponse> serials)
        {
            Films = films.ToList();
            Serials = serials.ToList();
        }
    }
}
