using RateFilms.Domain.DTO;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface ISerialRepositoty
    {
        Task<IEnumerable<Serial>> GetAllSerials();
        Task<IEnumerable<Serial>> GetAllSerialsWithFavorite();
        Task CreateAsync(SerialDbModel serial);
        Task SetFavoriteSerial(FavoriteMovie favoriteSerial, string userName);
    }
}
