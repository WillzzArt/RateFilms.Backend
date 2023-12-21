using RateFilms.Domain.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("FavoriteSerial")]
    public class FavoriteSerialDbModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FavoriteId { get; set; }
        public Guid UserId { get; set; }
        public UserDbModel User { get; set; }
        public Guid SerialId { get; set; }
        public SerialDbModel? Serial { get; set; }
        public StatusMovie Status { get; set; }
        public bool IsFavorite { get; set; }
        public int? Score { get; set; }
        public IEnumerable<CommentInSerialDbModel>? Comments { get; set; }
    }
}
