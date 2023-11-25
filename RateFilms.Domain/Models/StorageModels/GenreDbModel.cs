using System.ComponentModel.DataAnnotations;

namespace RateFilms.Domain.Models.StorageModels
{
    public class GenreDbModel
    {
        [Key]
        public int Id { get; set; }
        public string Genre { get; set; }
        public IEnumerable<FilmDbModel>? Films { get; set; }
        public IEnumerable<SerialDbModel>? Serials { get; set; }
    }
}
