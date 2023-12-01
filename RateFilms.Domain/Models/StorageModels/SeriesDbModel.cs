using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class SeriesDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime? RealeseDate { get; set; }
        public Guid? PreviewImageId { get; set; }
        public ImageDbModel? PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public Guid SeasonId { get; set; }
        public SeasonDbModel Season { get; set; }
    }
}
