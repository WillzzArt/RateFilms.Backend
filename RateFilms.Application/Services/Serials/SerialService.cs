using Microsoft.ML;
using RateFilms.Application.Services.Movies;
using RateFilms.Common.MovieRatingModels;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Films;
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
        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public SerialService(
            ISerialRepositoty serialRepositoty,
            IUserRepository userRepository,
            ICommentService commentService,
            IReviewRepository reviewRepository,
            IFavoriteRepository favoriteRepository)
        {
            _serialRepositoty = serialRepositoty;
            _userRepository = userRepository;
            _commentService = commentService;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
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

            var reviews = await _reviewRepository.GetReviewByStatus(id, null, false,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();

            if (serial != null)
            {
                return new SerialExtendResponse(serial, null, comment, popularReview);
            }

            return null;
        }

        public async Task<SerialExtendResponse?> GetSerialForAuthorizeUserById(Guid id, string userName)
        {
            var user = await _userRepository.FindUser(userName);
            if (user == null) throw new ArgumentException(nameof(userName));

            var serial = await _serialRepositoty.GetSerialWithFavoriteById(id);

            var comment = await _commentService.GetCommentsInSerial(id, 5, userName);

            var reviews = await _reviewRepository.GetReviewByStatus(id, user.Id, false,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();

            if (serial != null)
            {
                return new SerialExtendResponse(serial, serial.Favorites?.FirstOrDefault(x => x.User.Id == user.Id), comment, popularReview);
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

        public async Task<IEnumerable<SerialResponse>> GetSerialsWithUncheckedReview()
        {
            var serials = await _serialRepositoty.GetSerialsWithUncheckedReview();

            var res = serials.Select(s => new SerialResponse(s, null));

            return res;
        }

        public async Task<IEnumerable<SerialResponse>> GetRecommendedSerials(string username)
        {
            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(username);

            var mLContext = new MLContext();
            DataViewSchema modelSchema;
            var trainedModel = mLContext.Model.Load(Path.Combine(Environment.CurrentDirectory, "Data", "MovieRecommenderModel.zip"), out modelSchema);

            var predictionEngine = mLContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(trainedModel);

            var favorite = await _favoriteRepository.FindFavoriteSerials(user.Id);
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();
            var unWatchedSerials = serials.Where(f => !favorite.Any(fav => fav.SerialId == f.Id && (fav.Score != null || fav.Score != 0)));

            var resultSerials = new List<SerialResponse>();

            MovieRatingPrediction prediction = null;

            foreach (var serial in unWatchedSerials)
            {
                prediction = predictionEngine.Predict(new MovieRating
                {
                    UserId = user.Id.ToString(),
                    MovieId = serial.Id.ToString(),
                    Genres = serial.Genre.Select(g => g.ToString()).ToArray()
                });

                if ((float)(100 / (1 + Math.Exp(-prediction.Score))) > 65)
                {
                    resultSerials.Add(new SerialResponse(serial, null));
                }
            }

            return resultSerials;
        }
    }
}
