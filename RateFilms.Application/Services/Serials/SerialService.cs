using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.DTO.Serials;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services.Serials
{
    public class SerialService : ISerialService
    {
        private readonly ISerialRepositoty _serialRepositoty;

        public SerialService(ISerialRepositoty serialRepositoty)
        {
            _serialRepositoty = serialRepositoty;
        }
        public async Task CreateSerialAsync(Serial serial)
        {
            await _serialRepositoty.CreateAsync(SerialConvertor.SerialDomainConvertSerialDb(serial));
        }

        public async Task<IEnumerable<SerialResponse?>> GetSerialForAuthorizeUser(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SerialResponse?>> GetSerials()
        {
            throw new NotImplementedException();
        }

        public async Task SetFavoriteSerial(FavoriteMovie favoriteMovie, string userName)
        {
            throw new NotImplementedException();
        }
    }
}
