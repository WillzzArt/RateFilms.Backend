namespace RateFilms.Domain.Models.DomainModels
{
    public class Season
    {
        public Guid Id { get; set; }
        public DateTime? RealeseDate { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public float? AvgRating { get; set; }
        public IEnumerable<Series> Series { get; set; }
    }
}
