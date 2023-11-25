using RateFilms.Domain.Models.Interfaces;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Models.StorageModels
{
    public class FilmDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public IEnumerable<GenreDbModel> Genre { get; set; }
        public IEnumerable<ImageDbModel> Images { get; set; }
        public IEnumerable<PersonInFilmDbModel>? People { get; set; }

    }
}
