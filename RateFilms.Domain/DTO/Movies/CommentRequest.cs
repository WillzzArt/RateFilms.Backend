using System.ComponentModel.DataAnnotations;

namespace RateFilms.Domain.DTO.Movies
{
    public class CommentRequest
    {
        public Guid? CommentId { get; set; }
        public Guid MovieId { get; set; }
        public string CommentText { get; set; }
    }
}
