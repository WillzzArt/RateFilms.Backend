using RateFilms.Application.Services.Movies;
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
        private readonly ICommentService _commentService;
        public SerialService(
            ISerialRepositoty serialRepositoty,
            IUserRepository userRepository,
            ICommentService commentService)
        {
            _serialRepositoty = serialRepositoty;
            _userRepository = userRepository;
            _commentService = commentService;
        }
        public async Task CreateSerialAsync(Serial serial)
        {
            if (serial.Seasons.Any(s => s.RealeseDate < serial.RealeseDate))
            {
                throw new ArgumentOutOfRangeException(nameof(serial.Seasons));
            }

            if (serial.Seasons.Any(s => s.Series.Any(sSeries => sSeries.RealeseDate < s.RealeseDate)))
            {
                throw new ArgumentOutOfRangeException("series");
            }

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
            var comment = await _commentService.GetCommentsInSerial(id, 5, null);

            if (serial != null)
            {
                return new SerialExtendResponse(serial, null, comment);
            }

            return null;
        }

        public async Task<SerialExtendResponse?> GetSerialForAuthorizeUserById(Guid id, string userName)
        {
            var user = await _userRepository.FindUser(userName);
            if (user == null) throw new ArgumentException(nameof(userName));

            var serial = await _serialRepositoty.GetSerialWithFavoriteById(id);

            var comment = await _commentService.GetCommentsInSerial(id, 5, userName);

            if (serial != null)
            {
                return new SerialExtendResponse(serial, serial.Favorites?.FirstOrDefault(x => x.User.Id == user.Id), comment);
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
