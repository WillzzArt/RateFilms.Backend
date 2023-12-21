using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SeasonResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public long? ReleaseDate { get; set; }
        public List<Image>? Images { get; set; }
        public float? AvgRating { get; set; }
        public List<SeriesResponse> Series { get; set; } = new List<SeriesResponse>();

        public SeasonResponse(Season season)
        {
            Id = season.Id;
            Description = season.Description;
            if (season.RealeseDate != null) ReleaseDate = ((DateTimeOffset)season.RealeseDate).ToUnixTimeMilliseconds();
            if (season.Images.Any()) Images = season.Images.ToList();
            //AvgRating = season.AvgRating;
            Series = season.Series.Select(s => new SeriesResponse(s)).ToList();
        }
    }
}
