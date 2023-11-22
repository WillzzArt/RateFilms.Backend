using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.StorageModels
{
    public class FilmDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public float AgeRating { get; set; }
        public string? Autor { get; set; }
        public Genre Genre { get; set; }
        public IEnumerable<ImageDbModel> Images { get; set; }
        public IEnumerable<ActorDbModel>? Actors { get; set; }

    }
}
