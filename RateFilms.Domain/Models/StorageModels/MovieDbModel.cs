using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class MovieDbModel: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? ReleaseDate { get; set; }
        //public Image PreviewImage { get; set; }
        public int AgeRating { get; set; }
        public string? Country { get; set; }
        public MovieType Type { get; set; }
        public IEnumerable<FavoriteMovieDbModel>? Favorites { get; set; }
        public IEnumerable<GenreDbModel> Genre { get; set; }
        public IEnumerable<PersonInMovieDbModel> People { get; set; }
    }
}
