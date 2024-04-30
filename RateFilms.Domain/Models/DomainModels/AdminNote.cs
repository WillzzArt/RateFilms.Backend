using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.Models.DomainModels
{
    public class AdminNote
    {
        public User user { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Note { get; set; }
    }
}
