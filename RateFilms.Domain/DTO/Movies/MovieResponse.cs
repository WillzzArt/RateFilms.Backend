using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.DTO.Serials;

namespace RateFilms.Domain.DTO.Movies
{
    public class MovieResponse
    {
        public List<FilmResponse> Films { get; set; }
        public List<SerialResponse> Serials { get; set; }

        public MovieResponse(IEnumerable<FilmResponse> films, IEnumerable<SerialResponse> serials)
        {
            Films = films.ToList();
            Serials = serials.ToList();
        }
    }
}
