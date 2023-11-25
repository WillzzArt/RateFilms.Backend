namespace RateFilms.Domain.Models.DomainModels
{
    public class Serial
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        public int Duration { get; set; }
        public string? PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public int? SeriesCount { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
    }
}
