using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Movies
{
    public class ReviewResponse
    {
        public Guid Id { get; }
        public string Text { get; }
        public long Date { get; }
        public int CountLike { get; }
        public UserMini User { get; }
        public bool IsLiked { get; }
        public string Status { get; }
        public AdminNoteResponse? Note { get; }
        public int? UserRating { get; }

        public ReviewResponse(Review review)
        {
            Id = review.Id;
            Text = review.Text;
            Date = review.Date.ToUnixTimeMilliseconds();
            CountLike = review.CountLike;
            User = new UserMini(review.User);
            IsLiked = review.IsLiked;
            Status = review.Status.ToString();
            Note = review.Note != null ? new AdminNoteResponse(review.Note) : null;
            UserRating = review.Score;
            
        }
    }
}
