namespace RateFilms.Domain.DTO.Movies
{
    public class AdminNote
    {
        public Guid ReviewId { get; set; }
        public string? Note { get; set; }
        public bool IsFilm { get; set; }
        public bool IsPublish { get; set; }
    }
}
