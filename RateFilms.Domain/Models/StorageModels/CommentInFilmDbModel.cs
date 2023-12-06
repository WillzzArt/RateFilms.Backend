namespace RateFilms.Domain.Models.StorageModels
{
    public class CommentInFilmDbModel
    {
        public Guid FavoriteId { get; set; }
        public FavoriteFilmDbModel Favorite { get; set; }
        public Guid CommentId { get; set; }
        public CommentDbModel Comment { get; set; }

    }
}
