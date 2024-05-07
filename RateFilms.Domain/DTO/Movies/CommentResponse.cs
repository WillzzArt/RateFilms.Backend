using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Movies
{
    public class CommentResponse
    {
        public Guid Id { get; }
        public string Text { get; }
        public long Date { get; }
        public int CountLike { get;  }
        public UserMini User {  get; }
        public bool IsLiked { get; }

        public CommentResponse(Comment comment)
        {
            Id = comment.Id;
            Text = comment.Text;
            Date = comment.Date.ToUnixTimeMilliseconds();
            CountLike = comment.CountLike;
            User = new UserMini(comment.User);
            IsLiked = comment.IsLiked;
        }

    }
}
