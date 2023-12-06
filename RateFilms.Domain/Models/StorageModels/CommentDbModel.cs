using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class CommentDbModel: IEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsEdit { get; set; }
        public int CountLike { get; set; }
        public IEnumerable<UserDbModel> Users { get; set; }
    }
}
