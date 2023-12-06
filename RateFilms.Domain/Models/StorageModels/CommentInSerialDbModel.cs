namespace RateFilms.Domain.Models.StorageModels
{
    public class CommentInSerialDbModel
    {
        public Guid FavoriteId { get; set; }
        public FavoriteSerialDbModel Favorite { get; set; }
        public Guid CommentId { get; set; }
        public CommentDbModel Comment { get; set; }
    }
}
