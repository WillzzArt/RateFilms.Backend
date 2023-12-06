using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Serials
{
    public class SerialResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? RealeseDate { get; set; }
        public List<string> Genre { get; set; } = new List<string>();
        public Image? PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public bool IsFavorite { get; set; } = false;
        public string? Status { get; set; }
        public bool IsAnnouncement { get; set; } = false;
        public bool IsOngoing { get; set; } = false;
        public int CountSeriesLeft { get; set; }
        public int? CountMaxSeries { get; set; }
        public long? LastReleaseSeriesDate { get; set; }

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
            if (serial.RealeseDate != null && serial.RealeseDate > DateTimeOffset.UtcNow)
            {
                IsAnnouncement = true;
            }
            else
            {
                if (serial.Seasons.Any())
                {
                    CountSeries(serial.Seasons);
                    LastReleaseSeriesDate = SetLastReleaseSeriesDate(serial.Seasons);
                }
            }
        }

        private void CountSeries(IEnumerable<Season> seasons)
        {
            var maxSeries = 0;
            var seriesLeft = 0;
            var flag = true;
            foreach (var season in seasons)
            {
                if (flag)
                {
                    if (season.CountMaxSeries != null)
                    {
                        maxSeries += (int)season.CountMaxSeries;
                    }
                    else
                    {
                        flag = false;
                    }
                }

                foreach (var series in season.Series)
                {
                    if (series.RealeseDate < DateTimeOffset.UtcNow)
                    {
                        seriesLeft++;
                    }
                }
            }

            if (flag)
            {
                if (seriesLeft == 0)
                {
                    IsAnnouncement = true;
                }
                else if (seriesLeft < maxSeries)
                {
                    IsOngoing = true;
                }
                CountMaxSeries = maxSeries;
                CountSeriesLeft = seriesLeft;
            }
            else
            {
                if (seriesLeft == 0)
                {
                    IsAnnouncement = true;
                }
                else 
                {
                    IsOngoing = true;
                }
                CountSeriesLeft = seriesLeft;
            }
        }

        private long? SetLastReleaseSeriesDate(IEnumerable<Season> seasons)
        {
            if (!IsAnnouncement)
            {
                var lastDate = new DateTimeOffset();
                foreach (var season in seasons)
                {
                    foreach (var series in season.Series)
                    {
                        if (series.RealeseDate != null &&
                            series.RealeseDate > lastDate &&
                            series.RealeseDate < DateTimeOffset.UtcNow &&
                            series.RealeseDate >= DateTimeOffset.UtcNow.AddMonths(-1))
                        {
                            lastDate = (DateTimeOffset)series.RealeseDate;
                        }
                    }
                }

                if (lastDate != new DateTimeOffset())
                {
                    return lastDate.ToUnixTimeMilliseconds();
                }
            }

            return null;
        }
    }

}
