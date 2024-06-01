using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Movies
{
    public class AdminNoteResponse
    {
        public UserMini user { get; }
        public long Date { get; }
        public string Note { get; }

        public AdminNoteResponse(AdminNote note)
        {
            user = new UserMini(note.user);
            Date = note.Date.ToUnixTimeMilliseconds();
            Note = note.Note;
        }
    }
}
