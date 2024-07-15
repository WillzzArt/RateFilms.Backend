namespace RateFilms.Domain.Models.DomainModels
{
    public class Film: Movie
    {
        public int Duration { get; set; }
        public IEnumerable<Image> Images { get; set; }


        public bool IsAnnouncement()
        {
            if (ReleaseDate == null || ReleaseDate > DateTimeOffset.UtcNow)
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