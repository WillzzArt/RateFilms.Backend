using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("PersonInMovie")]
    public class PersonInMovieDbModel
    {
        public Guid PersonId { get; set; }
        public PersonDbModel Person { get; set; }
        public Guid MovieId { get; set; }
        public MovieDbModel Movie { get; set; }
        public IEnumerable<ProfessionDbModel> Professions { get; set; }
    }
}
