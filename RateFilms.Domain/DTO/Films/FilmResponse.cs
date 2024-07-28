using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Films
{
    public class FilmResponse: MovieModel
    {
        public bool IsAnnouncement { get; }

        public FilmResponse(Film film, Favorite? favoriteFilm)
            : base(film, favoriteFilm)
        {
            IsAnnouncement = film.IsAnnouncement();
        }
    }
}
