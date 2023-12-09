using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.DTO.Serials;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services.Serials
{
    public class SerialService : ISerialService
    {
        private readonly ISerialRepositoty _serialRepositoty;
        private readonly IUserRepository _userRepository;

        public SerialService(
            ISerialRepositoty serialRepositoty,
            IUserRepository userRepository)
        {
            _serialRepositoty = serialRepositoty;
            _userRepository = userRepository;
        }
        public async Task CreateSerialAsync(Serial serial)
        {
            await _serialRepositoty.CreateAsync(SerialConvertor.SerialDomainConvertSerialDb(serial));
        }


        public async Task<IEnumerable<SerialResponse?>> GetSerialForAuthorizeUser(string userName)
        {
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            var favoriteSerialsForUser = serials
                .Select(f =>
                    new SerialResponse(
                        f,
                        f.Favorites?.FirstOrDefault(x => x.User.Id == user.Id))
                    ).ToList();

            return favoriteSerialsForUser;
        }

        public async Task<IEnumerable<SerialResponse?>> GetSerials()
        {
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();

            var res = serials.Select(s => new SerialResponse(s, null));

            return res;
        }

        public async Task<SerialExtendResponse?> GetSerialById(Guid id)
        {
            var serial = await _serialRepositoty.GetSerialWithFavoriteById(id);

            if (serial != null)
            {
                return new SerialExtendResponse(serial, null);
            }

            return null;
        }

        public async Task<SerialExtendResponse?> GetSerialForAuthorizeUserById(Guid id, string userName)
        {
            var serial = await _serialRepositoty.GetSerialWithFavoriteById(id);
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(nameof(userName));

            if (serial != null)
            {
                return new SerialExtendResponse(serial, serial.Favorites?.FirstOrDefault(x => x.User.Id == user.Id));
            }

            return null;
        }

        public async Task SetFavoriteSerial(FavoriteMovie favoriteMovie, string userName)
        {
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            await _serialRepositoty.SetFavoriteSerial(favoriteMovie, user);
        }

        public async Task<IEnumerable<SerialResponse>> GetAllFavoriteSerials(string userName)
        {
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            var favoriteSerialsForUser = from s in serials
                                       where s.Favorites != null
                                       from fav in s.Favorites!
                                       where fav.User.Id == user.Id
                                       select new SerialResponse(s, fav);

            return favoriteSerialsForUser;
        }
    }
}
