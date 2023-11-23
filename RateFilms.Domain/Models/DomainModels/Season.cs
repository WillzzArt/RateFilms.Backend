namespace RateFilms.Domain.Models.DomainModels
{
    public class Season
    {
        public Guid Id { get; set; }
        public DateOnly RealeseDate { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Image>? Images { get; set; }
        public float? AvgRating { get; set; }
        public IEnumerable<Actor>? Actors { get; set; }

    }
}
