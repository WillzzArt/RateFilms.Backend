using RateFilms.Domain.DTO;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface IFilmRepository
    {
        IEnumerable<Film?> GetAllFilms();
        Task CreateAsync(FilmDbModel film);
        Task SetFavoriteFilm(FavoriteFilm favoriteFilm, string userName);
    }
}
