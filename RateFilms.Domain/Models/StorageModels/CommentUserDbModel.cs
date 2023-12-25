using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("CommentLikes")]
    public class CommentUserDbModel
    {
        public Guid CommentId { get; set; }
        public CommentDbModel Comment { get; set; }
        public Guid UserId { get; set; }
        public UserDbModel User { get; set; }
    }
}
