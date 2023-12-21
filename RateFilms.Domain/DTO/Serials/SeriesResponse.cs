using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SeriesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public long? ReleaseDate { get; set; }
        public Image PreviewImage { get; set; }
        public float? AvgRating { get; set; }

        public SeriesResponse(Series series)
        {
            Id = series.Id;
            Name = series.Name;
            Duration = series.Duration;
            if (series.RealeseDate != null) ReleaseDate = ((DateTimeOffset)series.RealeseDate).ToUnixTimeMilliseconds();
            PreviewImage = series.PreviewImage;
            //AvgRating = series.AvgRating;
        }
    }
}
