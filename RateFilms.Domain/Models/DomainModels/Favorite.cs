using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Favorite
    {
        public User User { get; set; }
        public bool IsFavorite { get; set; } = false;
        public StatusMovie? Status { get; set; } = StatusMovie.None;
    }
}
