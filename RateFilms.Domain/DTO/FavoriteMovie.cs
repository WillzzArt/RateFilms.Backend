using RateFilms.Domain.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace RateFilms.Domain.DTO
{
    public class FavoriteMovie
    {
        [Required]
        public Guid MovieId { get; set; }
        public StatusMovie StatusMovie { get; set; } = StatusMovie.None;
        public bool IsFavorite { get; set; } = false;
        public int? Score { get; set; }
    }
}
