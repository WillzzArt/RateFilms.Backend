using System.ComponentModel.DataAnnotations;

namespace RateFilms.Domain.Models.StorageModels
{
    public class GenreDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Genre { get; set; }
        public IEnumerable<MovieDbModel> Movies { get; set; }
    }
}
