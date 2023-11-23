using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.StorageModels
{
    public class ActorDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Guid? ImageId { get; set; }
        public ImageDbModel? Image { get; set; }
        public IEnumerable<FilmDbModel> Film { get; set; }
        public IEnumerable<SeasonDbModel> Season { get; set; }
    }
}