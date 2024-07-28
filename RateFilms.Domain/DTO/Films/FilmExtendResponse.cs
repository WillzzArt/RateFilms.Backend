using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Films
{
    public class FilmExtendResponse: FilmResponse
    {
        public int Duration { get; }
        public List<Image>? Images { get; }
        public int CountFavorite { get; }
        public List<PersonResponse> People { get; } = new List<PersonResponse>();
        public IEnumerable<CommentResponse>? Comments { get; }
        public ReviewResponse? Review { get; }
        public Dictionary<int, int>? Ratings { get; }
        public Dictionary<string, int> StatusOfPeople { get; }

        public FilmExtendResponse(
            Film film, 
            Favorite? favoriteFilm, 
            IEnumerable<CommentResponse> comments, 
            Review? review)
            : base(film, favoriteFilm)
        {
            Duration = film.Duration;

            if (film.Images.Any())
            {
                Images = film.Images
                    .Select(x => x)
                    .ToList();
            }

            CountFavorite = Favorite.GetCountFavorite(film.Favorites);

            if (film.People != null && film.People.Any())
            {
                People = film.People
                    .Select(x => new PersonResponse(x))
                    .ToList();
            }

            Ratings = Favorite.GetRatings(film.Favorites);
            StatusOfPeople = Favorite.GetStatusOfPeople(film.Favorites);
            Comments = comments;
            Review = review != null ? new ReviewResponse(review) : null;
        }
    }
}
