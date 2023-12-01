using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Films
{
    public class FilmResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long? RealeseDate { get; set; }
        public List<string> Genre { get; set; } = new List<string>();
        public int Duration { get; set; }
        public Image PreviewImage { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public List<PersonResponse>? People { get; set; } = new List<PersonResponse>();
        public bool isFavorite { get; set; } = false;
        public string? Status { get; set; } = StatusMovie.None.ToString();

        public FilmResponse(Film film, Favorite? favoriteFilm)
        {
            Id = film.Id;
            Name = film.Name;
            Description = film.Description;
            if (film.Genre.Any())
            {
                Genre = film.Genre
                    .Select(x => x.ToString())
                    .ToList();
            }
            Duration = film.Duration;
            if (film.RealeseDate != null)
                RealeseDate = ((DateTimeOffset)film.RealeseDate).ToUnixTimeMilliseconds();

            PreviewImage = film.PreviewImage;
            if (film.Images.Any())
            {
                Images = film.Images
                    .Select(x => x)
                    .ToList();
            }
            AvgRating = film.AvgRating;
            AgeRating = film.AgeRating;
            if (film.People != null && film.People.Any())
            {
                People = film.People
                    .Select(x => new PersonResponse(x))
                    .ToList();
            }
            isFavorite = favoriteFilm?.IsFavorite ?? false;
            Status = favoriteFilm?.Status.ToString() ?? StatusMovie.None.ToString();
        }
    }
}
