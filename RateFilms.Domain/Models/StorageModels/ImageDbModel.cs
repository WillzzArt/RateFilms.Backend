using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.StorageModels
{
    public class ImageDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid? FilmId { get; set; }
        public FilmDbModel Film { get; set; }
        public Guid? SeasonId { get; set; }
        public SeasonDbModel Season { get; set; }
    }
}
