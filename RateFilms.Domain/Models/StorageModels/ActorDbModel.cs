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
        public Guid? FilmId { get; set; }
        public FilmDbModel? Film { get; set; }
        public Guid? SeasonId { get; set; }
        public SeasonDbModel? Season { get; set; }
    }
}
