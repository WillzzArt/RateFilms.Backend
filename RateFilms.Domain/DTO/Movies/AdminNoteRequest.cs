namespace RateFilms.Domain.DTO.Movies
{
    public class AdminNoteRequest
    {
        public Guid ReviewId { get; set; }
        public string? Note { get; set; }
        public bool IsFilm { get; set; }
        public bool IsPublish { get; set; }
    }
}
