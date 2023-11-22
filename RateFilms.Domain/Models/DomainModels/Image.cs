using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Image : IEntity
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
    }
}
