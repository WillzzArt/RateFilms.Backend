using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Movies
{
    public class MovieModel
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public long? ReleaseDate { get; }
        public List<string> Genre { get; } = new List<string>();
        public Image PreviewImage { get; }
        public double? AvgRating { get; }
        public int AgeRating { get; }
        public bool isFavorite { get; } = false;
        public string Status { get; }
        public string? Country { get; }
        public int? UserRating { get; }

        public MovieModel(Movie movie, Favorite? favoriteMovie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Description = movie.Description;
            if (movie.Genre.Any())
            {
                Genre = movie.Genre
                    .Select(x => x.ToString())
                    .ToList();
            }

            if (movie.ReleaseDate != null)
                ReleaseDate = ((DateTimeOffset)movie.ReleaseDate).ToUnixTimeMilliseconds();

            PreviewImage = movie.PreviewImage;
            AvgRating = Favorite.GetAvgRating(movie.Favorites);
            AgeRating = movie.AgeRating;
            isFavorite = favoriteMovie?.IsFavorite ?? false;
            Status = favoriteMovie?.Status.ToString() ?? StatusMovie.None.ToString();
            Country = movie.Country;
            UserRating = favoriteMovie?.Score;
        }
    }
}
