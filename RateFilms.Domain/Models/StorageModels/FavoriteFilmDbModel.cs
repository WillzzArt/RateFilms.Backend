using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.StorageModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("FavoriteFilm")]
    public class FavoriteFilmDbModel
    {
        public Guid? UserId { get; set; }
        public UserDbModel? User { get; set; }
        public Guid? FilmId { get; set; }
        public FilmDbModel? Film { get; set; }
        public StatusMovie? Status { get; set; }
        public bool isFavorite { get; set; }
    }
}
