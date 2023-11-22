using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Models
{
    public class Movies
    {
        public IEnumerable<Film>? Films { get; set; }
        public IEnumerable<Serial>? Serials { get; set; }
    }
}
