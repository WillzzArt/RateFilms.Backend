using System.ComponentModel.DataAnnotations.Schema;

namespace RateFilms.Domain.Models.StorageModels
{
    [Table("PersonInSerial")]
    public class PersonInSerialDbModel
    {
        public Guid PersonId { get; set; }
        public PersonDbModel Person { get; set; }
        public Guid SerialId { get; set; }
        public SerialDbModel Serial { get; set; }
        public IEnumerable<ProfessionDbModel> Profession { get; set; }
    }
}
