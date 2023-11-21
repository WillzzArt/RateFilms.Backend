using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Infrastructure.StorageModels
{
    public class SerialDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; }
        public string PreviewImage { get; set; }
        public float AvgRating { get; set; }
        public int AgeRating { get; set; }
        public int SeriesCount { get; set; }
        public IEnumerable<SeasonDbModel> Seasons { get; set; }
    }
}
