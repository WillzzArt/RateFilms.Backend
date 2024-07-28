using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SerialResponse: MovieModel
    {
        private bool _isAnnouncement;
        private bool _isOngoing;
        private int _countSeriesLeft;
        private int? _countMaxSeries;

        public bool IsAnnouncement { get => _isAnnouncement; }
        public bool IsOngoing { get => _isOngoing; }
        public int CountSeriesLeft { get => _countSeriesLeft; }
        public int? CountSeriesMax { get => _countMaxSeries; }
        public long? LastSeriesReleaseDate { get; }
        

        public SerialResponse(Serial serial, Favorite? favoriteSerial)
            : base(serial, favoriteSerial)
        {
            serial.CountSeries(out _isAnnouncement, out _isOngoing, out _countMaxSeries, out _countSeriesLeft);
            LastSeriesReleaseDate = serial.GetLastReleaseSeriesDate(IsAnnouncement);
        }
    }
}
