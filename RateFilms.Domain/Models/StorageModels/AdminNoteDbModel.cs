using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    /// <summary>
    /// Примечание к рецензии от администатора
    /// </summary>
    [Table("NoteToReview")]
    public class AdminNoteDbModel
    {
        public Guid UserId { get; set; }
        public UserDbModel User { get; set; }
        public Guid ReviewId { get; set; }
        public CommentDbModel Review { get; set; }
        public string Note { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
