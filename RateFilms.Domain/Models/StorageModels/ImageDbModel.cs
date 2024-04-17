using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class ImageDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid? FilmId { get; set; }
        public FilmDbModel? Film { get; set; }
        public Guid? SeasonId { get; set; }
        public SeasonDbModel? Season { get; set; }
        public bool isPreview { get; set; }
        public string? Name {  get; set; }
        public byte[] Img { get; set; }
    }
}
