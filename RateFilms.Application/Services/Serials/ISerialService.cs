using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.DTO.Serials;
using RateFilms.Domain.Models.DomainModels;
using System.Globalization;

namespace RateFilms.Application.Services.Serials
{
    public interface ISerialService
    {
        Task<IEnumerable<SerialResponse?>> GetSerials(CultureInfo culture);
        Task<IEnumerable<SerialResponse?>> GetSerialForAuthorizeUser(string userName, CultureInfo culture);
        Task<SerialExtendResponse?> GetSerialById(Guid id, CultureInfo culture);
        Task<SerialExtendResponse?> GetSerialForAuthorizeUserById(Guid id, string userName, CultureInfo culture);
        Task CreateSerialAsync(Serial film);
        Task SetFavoriteSerial(FavoriteMovie favoriteMovie, string userName);
        Task<IEnumerable<SerialResponse>> GetAllFavoriteSerials(string userName, CultureInfo culture);
        Task<IEnumerable<SerialResponse>> GetSerialsWithUncheckedReview(CultureInfo culture);
        Task<IEnumerable<SerialResponse>> GetRecommendedSerials(string username, CultureInfo culture);
    }
}
