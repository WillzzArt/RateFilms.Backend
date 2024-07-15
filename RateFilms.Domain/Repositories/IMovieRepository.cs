using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface IMovieRepository
    {
        Task CreateAsync(MovieDbModel movie);
        Task SetFavoriteMovie(FavoriteMovie favoriteSerial, User user);
    }
}
