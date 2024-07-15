namespace RateFilms.Domain.Models.DomainModels
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? ReleaseDate { get; set; }
        public Image PreviewImage { get; set; }
        public int AgeRating { get; set; }
        public string? Country { get; set; }
        public MovieType type { get; set; }
        public IEnumerable<Favorite>? Favorites { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
        public IEnumerable<Person> People { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return obj is Movie movie && Id == movie.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
