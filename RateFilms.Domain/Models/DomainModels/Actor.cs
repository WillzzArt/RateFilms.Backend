using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Actor : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Image? Image { get; set; }
    }
}
