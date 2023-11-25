using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class SeasonDbModel : IEntity
    {
        public Guid Id { get; set; }
        public DateOnly RealeseDate { get; set; }
        public string? Description { get; set; }
        public float? AvgRating { get; set; }
        public Guid? SerialId { get; set; }
        public SerialDbModel? Serial { get; set; }
        public IEnumerable<ImageDbModel> Images { get; set; }
        public IEnumerable<SeriesDbModel> Series { get; set; }
    }
}
