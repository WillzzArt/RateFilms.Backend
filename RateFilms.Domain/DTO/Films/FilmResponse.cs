using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Films
{
    public class FilmResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string> Genre { get; set; } = new List<string>();
        public int Duration { get; set; }
        public Image PreviewImage { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public List<Person>? People { get; set; } = new List<Person>();

        public FilmResponse(Film film)
        {
            Id = film.Id;
            Name = film.Name;
            Description = film.Description;
            if (film.Genre.Any())
            {
                Genre = film.Genre
                    .Select(x => x.ToString())
                    .ToList();
            }
            Duration = film.Duration;
            PreviewImage = film.PreviewImage;
            if (film.Images.Any())
            {
                Images = film.Images
                    .Select(x => x)
                    .ToList();
            }
            AvgRating = film.AvgRating;
            AgeRating = film.AgeRating;
            if (film.People != null && film.People.Any())
            {
                People = film.People
                    .Select(x => x)
                    .ToList();
            }
        }
    }
}
