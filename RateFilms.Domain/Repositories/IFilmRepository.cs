using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> GetAllFilms();
        Task<IEnumerable<Film>> GetAllFilmsWithFavorite();
        Task CreateAsync(FilmDbModel film);
        Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName);
    }
}
