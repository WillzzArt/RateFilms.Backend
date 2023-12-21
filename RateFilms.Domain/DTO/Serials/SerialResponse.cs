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
        public long? ReleaseDate { get; }
        public List<string> Genre { get; } = new List<string>();
        public Image PreviewImage { get; }
        public double? AvgRating { get; }
        public int AgeRating { get; }
        public bool IsFavorite { get;  } = false;
        public string Status { get; }
        public bool IsAnnouncement { get => _isAnnouncement; }
        public bool IsOngoing { get => _isOngoing; }
        public int CountSeriesLeft { get => _countSeriesLeft; }
        public int? CountSeriesMax { get => _countMaxSeries; }
        public long? LastSeriesReleaseDate { get; }
        public string? Country { get; }
        public int? UserRating { get; }

        public SerialResponse(Serial serial, Favorite? favoriteSerial)
        {
            Id = serial.Id;
            Name = serial.Name;
            Description = serial.Description;
            if (serial.RealeseDate != null) ReleaseDate = ((DateTimeOffset)serial.RealeseDate).ToUnixTimeMilliseconds();
            if (serial.Genre.Any())
            {
                Genre = serial.Genre
                    .Select(x => x.ToString())
                    .ToList();
            }

            PreviewImage = serial.PreviewImage;
            AvgRating = Favorite.GetAvgRating(serial.Favorites);
            AgeRating = serial.AgeRating;
            IsFavorite = favoriteSerial?.IsFavorite ?? false;
            Status = favoriteSerial?.Status.ToString() ?? StatusMovie.None.ToString();
            serial.CountSeries(out _isAnnouncement, out _isOngoing, out _countMaxSeries, out _countSeriesLeft);
            LastSeriesReleaseDate = serial.GetLastReleaseSeriesDate(IsAnnouncement);
            Country = serial.Country;
            UserRating = favoriteSerial?.Score;
        }
    }
}
