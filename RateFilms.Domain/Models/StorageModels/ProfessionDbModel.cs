using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("Profession")]
    public class ProfessionDbModel
    {
        public int Id { get; set; }
        public string Profession { get; set; }
        public IEnumerable<PersonInFilmDbModel> PersonInFilms { get; set; }
        public IEnumerable<PersonInSerialDbModel> PersonInSerials { get; set; } 
    }
}
