using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Film : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Genre Genre { get; set; }
        public int Duration { get; set; }
        public string? PreviewImage { get; set; }
        public IEnumerable<Image>? Images { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public string? Author { get; set; }
        public IEnumerable<Actor>? Actors { get; set; }
    }
}