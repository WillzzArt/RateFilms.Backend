using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Genre : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Film>? Films { get; set; }
    }
}
