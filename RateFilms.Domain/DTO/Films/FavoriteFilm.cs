using RateFilms.Domain.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace RateFilms.Domain.DTO.Films
{
    public class FavoriteFilm
    {
        [Required]
        public Guid FilmId { get; set; }
        public StatusMovie StatusMovie { get; set; } = StatusMovie.None;
        public bool IsFavorite { get; set; } = false;
    }
}
