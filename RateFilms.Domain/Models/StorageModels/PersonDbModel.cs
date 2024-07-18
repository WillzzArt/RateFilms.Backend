using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class PersonDbModel : IEntity
    {
        public Guid Id { get; set; }
        public int? KinopoiskId {  get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Guid? ImageId { get; set; }
        public ImageDbModel? Image { get; set; }
        public IEnumerable<PersonInMovieDbModel> Movies { get; set; }
    }
}