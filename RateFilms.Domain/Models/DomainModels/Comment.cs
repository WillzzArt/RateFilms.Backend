using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsEdit { get; set; }
        public ReviewStatus Status { get; set; }
        public User User { get; set; }
        public int CountLike { get; set; }
        public bool IsLiked { get; set; }
    }
}
