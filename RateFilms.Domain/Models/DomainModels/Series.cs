namespace RateFilms.Domain.Models.DomainModels
{
    public class Series
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public Image PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public DateOnly RealeseDate { get; set; }
    }
}
