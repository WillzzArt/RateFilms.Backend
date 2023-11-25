using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("PersonInFilm")]
    public class PersonInFilmDbModel
    {
        public Guid PersonId { get; set; }
        public PersonDbModel Person { get; set; }
        public Guid FilmId { get; set; }
        public FilmDbModel Film { get; set; }
        public IEnumerable<ProfessionDbModel> Professions { get; set; }
    }
}
