using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface IFavoriteRepository
    {
        Task<FavoriteFilmDbModel?> FindFavoriteFilm(Guid filmId, Guid userId);
        Task<FavoriteSerialDbModel?> FindFavoriteSerial(Guid serialId, Guid userId);
    }
}
