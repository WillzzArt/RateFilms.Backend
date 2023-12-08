using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SerialExtendResponse
    {
        private bool _isAnnouncement;
        private bool _isOngoing;
        private int _countSeriesLeft;
        private int? _countMaxSeries;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? RealeseDate { get; set; }
        public List<string> Genre { get; set; } = new List<string>();
        public Image? PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public List<SeasonResponse> Seasons { get; set; }
        public List<PersonResponse> People { get; set; }
        public bool isFavorite { get; set; } = false;
        public string? Status { get; set; }
        public bool IsAnnouncement { get => _isAnnouncement; }
        public bool IsOngoing { get => _isOngoing; }
        public int CountSeriesLeft { get => _countSeriesLeft; }
        public int? CountMaxSeries { get => _countMaxSeries; }
        public long? LastReleaseSeriesDate { get; }
        public string? Country { get; }

        public SerialExtendResponse(Serial serial, Favorite? favoriteSerial)
        {
            Id = serial.Id;
            Name = serial.Name;
            Description = serial.Description;
            if (serial.RealeseDate != null) RealeseDate = ((DateTimeOffset)serial.RealeseDate).ToUnixTimeMilliseconds();
            if (serial.Genre.Any())
            {
                Genre = serial.Genre
                    .Select(x => x.ToString())
                    .ToList();
            }

            PreviewImage = serial.PreviewImage;
            AvgRating = serial.AvgRating;
            AgeRating = serial.AgeRating;
            Seasons = serial.Seasons.Select(s => new SeasonResponse(s)).ToList();
            People = serial.People.Select(p => new PersonResponse(p)).ToList();
            isFavorite = favoriteSerial?.IsFavorite ?? false;
            Status = favoriteSerial?.Status.ToString() ?? StatusMovie.None.ToString();
            serial.CountSeries(out _isAnnouncement, out _isOngoing, out _countMaxSeries, out _countSeriesLeft);
            LastReleaseSeriesDate = serial.GetLastReleaseSeriesDate(IsAnnouncement);
            Country = serial.Country;
        }
    }
}
