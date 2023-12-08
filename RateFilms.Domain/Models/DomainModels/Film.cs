namespace RateFilms.Domain.Models.DomainModels
{
    public class Film
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? RealeseDate { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        public int Duration { get; set; }
        public string? Country { get; set; }
        public Image PreviewImage { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public int AgeRating { get; set; }
        public IEnumerable<Person> People { get; set; }
        public IEnumerable<Favorite>? Favorites { get; set; }


        public bool IsAnnouncement()
        {
            if (RealeseDate == null || RealeseDate > DateTimeOffset.UtcNow)
            {
                return true;
            }

            return false;
        }

        public double? GetAvgRating()
        {
            if (Favorites != null)
            {
                var avgRating = 0.0;
                if (Favorites.Any(fav => fav.Score != null))
                {
                    avgRating = Favorites
                        .Where(fav => fav.Score != null)
                        .Select(fav => (int)fav.Score!)
                        .Average();
                }

                if (avgRating > 0)
                {
                    return Math.Round((double)avgRating, 2);
                }

            }
            return null;
        }
    }
}