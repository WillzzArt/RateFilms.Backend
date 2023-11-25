namespace RateFilms.Domain.Models.DomainModels
{
    public class Film
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        public int Duration { get; set; }
        public Image PreviewImage { get; set; }
        public IEnumerable<Image>? Images { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public IEnumerable<Person>? People { get; set; }
    }
}