namespace RateFilms.Domain.Models.DomainModels
{
    public class Film
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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

        public override bool Equals(object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return obj is Film film && Id == film.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}