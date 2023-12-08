using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class FilmDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? ReleaseDate { get; set; }
        public int Duration { get; set; }
        public int AgeRating { get; set; }
        public string? Country { get; set; }
        public IEnumerable<GenreDbModel> Genre { get; set; }
        public IEnumerable<ImageDbModel> Images { get; set; }
        public IEnumerable<PersonInFilmDbModel> People { get; set; }
        public IEnumerable<FavoriteFilmDbModel>? Favorite { get; set; }

    }
}
