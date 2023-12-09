namespace RateFilms.Domain.Models.DomainModels
{
    public class Serial
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? RealeseDate { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        public Image? PreviewImage { get; set; }
        public int AgeRating { get; set; }
        public string? Country { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
        public IEnumerable<Favorite>? Favorites { get; set; }
        public IEnumerable<Person> People { get; set; }

        public void CountSeries(
            out bool IsAnnouncement,
            out bool IsOngoing,
            out int? CountMaxSeries,
            out int CountSeriesLeft)
        {
            IsAnnouncement = false;
            IsOngoing = false;
            CountSeriesLeft = 0;
            CountMaxSeries = null;

            var maxSeries = 0;
            var seriesLeft = 0;
            var flag = true;

            if (RealeseDate == null || RealeseDate > DateTimeOffset.UtcNow)
            {
                IsAnnouncement = true;
                return;
            }

            foreach (var season in Seasons)
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

        public long? GetLastReleaseSeriesDate(bool IsAnnouncement)
        {
            if (!IsAnnouncement)
            {
                var lastDate = new DateTimeOffset();
                foreach (var season in Seasons)
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
