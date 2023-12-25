using RateFilms.Domain.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace RateFilms.Domain.DTO.Movies
{
    public class FavoriteMovie
    {
        [Required]
        public Guid MovieId { get; set; }
        public string? StatusMovie { get; set; }
        public bool? IsFavorite { get; set; }
        public int? Score { get; set; }
    }
}
