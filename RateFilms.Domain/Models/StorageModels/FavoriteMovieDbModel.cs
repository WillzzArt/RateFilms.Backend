using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("FavoriteMovie")]
    public class FavoriteMovieDbModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDbModel User { get; set; }
        public Guid MovieId { get; set; }
        public MovieDbModel? Movie { get; set; }
        public StatusMovie Status { get; set; }
        public bool IsFavorite { get; set; }
        public int Score { get; set; }
        public IEnumerable<CommentDbModel>? Comments { get; set; }
    }
}
