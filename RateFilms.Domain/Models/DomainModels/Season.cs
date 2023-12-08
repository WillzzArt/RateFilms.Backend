namespace RateFilms.Domain.Models.DomainModels
{
    public class Season
    {
        public Guid Id { get; set; }
        public DateTimeOffset? RealeseDate { get; set; }
        public string? Description { get; set; }
        public int? CountMaxSeries { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Series> Series { get; set; }
    }
}
