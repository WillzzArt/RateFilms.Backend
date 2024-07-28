using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SerialExtendResponse: SerialResponse
    {
        public int CountFavorite { get; }
        public List<SeasonResponse> Seasons { get; set; }
        public List<PersonResponse> People { get; set; }
        public IEnumerable<CommentResponse>? Comments { get; }
        public ReviewResponse? Review { get; }
        public Dictionary<int, int>? Ratings { get; }
        public Dictionary<string, int> StatusOfPeople { get; }

        public SerialExtendResponse(
            Serial serial, 
            Favorite? favoriteSerial, 
            IEnumerable<CommentResponse> comments, 
            Review? review)
            : base(serial, favoriteSerial)
        {
            CountFavorite = Favorite.GetCountFavorite(serial.Favorites);
            Seasons = serial.Seasons.Select(s => new SeasonResponse(s)).ToList();
            People = serial.People.Select(p => new PersonResponse(p)).ToList();
            Ratings = Favorite.GetRatings(serial.Favorites);
            StatusOfPeople = Favorite.GetStatusOfPeople(serial.Favorites);
            Comments = comments;
            Review = review != null ? new ReviewResponse(review) : null;
        }
    }
}
