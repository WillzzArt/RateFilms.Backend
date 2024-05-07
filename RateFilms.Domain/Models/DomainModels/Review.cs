using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.Models.DomainModels
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
        public User User { get; set; }
        public int CountLike { get; set; }
        public bool IsLiked { get; set; }
        public AdminNote? Note { get; set; }
        public ReviewStatus Status { get; set; }
        public int Score { get; set; }
    }
}
