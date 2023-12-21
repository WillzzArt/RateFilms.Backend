using RateFilms.Domain.Models.DomainModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("FavoriteFilm")]
    public class FavoriteFilmDbModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FavoriteId { get; set; }
        public Guid UserId { get; set; }
        public UserDbModel User { get; set; }
        public Guid FilmId { get; set; }
        public FilmDbModel? Film { get; set; }
        public StatusMovie Status { get; set; }
        public bool IsFavorite { get; set; }
        public int? Score { get; set; }
        public IEnumerable<CommentInFilmDbModel>? Comments { get; set; }
    }
}
