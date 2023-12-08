using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SerialResponse
    {
        private bool _isAnnouncement;
        private bool _isOngoing;
        private int _countSeriesLeft;
        private int? _countMaxSeries;

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public long? RealeseDate { get; }
        public List<string> Genre { get; } = new List<string>();
        public Image? PreviewImage { get; }
        public float? AvgRating { get; }
        public int AgeRating { get; }
        public bool IsFavorite { get;  } = false;
        public string? Status { get; }
        public bool IsAnnouncement { get => _isAnnouncement; }
        public bool IsOngoing { get => _isOngoing; }
        public int CountSeriesLeft { get => _countSeriesLeft; }
        public int? CountMaxSeries { get => _countMaxSeries; }
        public long? LastReleaseSeriesDate { get; }
        public string? Country { get; }


        public SerialResponse(Serial serial, Favorite? favoriteSerial)
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
            IsFavorite = favoriteSerial?.IsFavorite ?? false;
            Status = favoriteSerial?.Status.ToString() ?? StatusMovie.None.ToString();
            serial.CountSeries(out _isAnnouncement, out _isOngoing, out _countMaxSeries, out _countSeriesLeft);
            LastReleaseSeriesDate = serial.GetLastReleaseSeriesDate(IsAnnouncement);
            Country = serial.Country;
        }
    }
}
