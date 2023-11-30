using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SerialResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string> Genre { get; set; }
        public int Duration { get; set; }
        public string? PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public int? SeriesCount { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
        public IEnumerable<Favorite> Favorites { get; set; }
    }
}
