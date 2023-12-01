namespace RateFilms.Domain.Models.DomainModels
{
    public class Serial
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? RealeseDate { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        public Image? PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
        public IEnumerable<Favorite>? Favorites { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}
