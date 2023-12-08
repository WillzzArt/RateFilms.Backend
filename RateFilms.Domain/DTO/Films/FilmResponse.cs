using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Films
{
    public class FilmResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string? Description { get; }
        public long? RealeseDate { get; }
        public List<string> Genre { get; } = new List<string>();
        public Image PreviewImage { get; }
        public double? AvgRating { get; }
        public int AgeRating { get; }
        public bool IsAnnouncement { get; }
        public bool isFavorite { get;  } = false;
        public string? Status { get; }
        public string? Country { get; }
        public int? Score { get; }

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

            if (film.RealeseDate != null)
                RealeseDate = ((DateTimeOffset)film.RealeseDate).ToUnixTimeMilliseconds();

            PreviewImage = film.PreviewImage;
            AvgRating = film.GetAvgRating();
            AgeRating = film.AgeRating;
            isFavorite = favoriteFilm?.IsFavorite ?? false;
            Status = favoriteFilm?.Status.ToString() ?? StatusMovie.None.ToString();
            IsAnnouncement = film.IsAnnouncement();
            Country = film.Country;
        }
    }
}
