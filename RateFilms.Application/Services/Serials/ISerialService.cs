using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.DTO.Serials;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services.Serials
{
    public interface ISerialService
    {
        Task<IEnumerable<SerialResponse?>> GetSerials();
        Task<IEnumerable<SerialResponse?>> GetSerialForAuthorizeUser(string userName);
        Task<SerialExtendResponse?> GetSerialById(Guid id);
        Task<SerialExtendResponse?> GetSerialForAuthorizeUserById(Guid id, string userName);
        Task CreateSerialAsync(Serial film);
        Task SetFavoriteSerial(FavoriteMovie favoriteMovie, string userName);
        Task<IEnumerable<SerialResponse>> GetAllFavoriteSerials(string userName);
        Task<IEnumerable<SerialResponse>> GetSerialsWithUncheckedReview();
    }
}
