using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Films
{
    public class FilmExtendResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string? Description { get; }
        public long? RealeseDate { get; }
        public List<string> Genre { get; } = new List<string>();
        public int Duration { get; }
        public Image PreviewImage { get; }
        public List<Image> Images { get; } = new List<Image>();
        public float? AvgRating { get; }
        public int AgeRating { get; }
        public List<PersonResponse>? People { get; } = new List<PersonResponse>();
        public bool isFavorite { get; } = false;
        public string? Status { get; }
        public string? Country { get; }

        public FilmExtendResponse(Film film, Favorite? favoriteFilm)
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
            Country = film.Country;
        }
    }
}
