using Microsoft.Extensions.ML;
using RateFilms.Application.Services.Localization;
using RateFilms.Application.Services.Movies;
using RateFilms.Common.Models.MovieRatingModels;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Serials;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;
using System.Globalization;

namespace RateFilms.Application.Services.Serials
{
    public class SerialService : ISerialService
    {
        private readonly ISerialRepositoty _serialRepositoty;
        private readonly IUserRepository _userRepository;
        private readonly ICommentService _commentService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly PredictionEnginePool<MovieRating, MovieRatingPrediction> _predictionEnginePool;
        private readonly LocalizationService _localizationService;

        public SerialService(
            ISerialRepositoty serialRepositoty,
            IUserRepository userRepository,
            ICommentService commentService,
            IReviewRepository reviewRepository,
            IFavoriteRepository favoriteRepository,
            PredictionEnginePool<MovieRating, MovieRatingPrediction> predictionEnginePool,
            LocalizationService localizationService)
        {
            _serialRepositoty = serialRepositoty;
            _userRepository = userRepository;
            _commentService = commentService;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
            _predictionEnginePool = predictionEnginePool;
            _localizationService = localizationService;
            _localizationService.LoadTranslation();
        }

        public async Task<IEnumerable<SerialResponse?>> GetSerialForAuthorizeUser(string userName, CultureInfo culture)
        {
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            foreach (var serial in serials)
                LocalizeFieldsSerial(serial, culture);

            var favoriteSerialsForUser = serials
                .Select(f =>
                    new SerialResponse(
                        f,
                        f.Favorites?.FirstOrDefault(x => x.User.Id == user.Id))
                    ).ToList();

            return favoriteSerialsForUser;
        }

        public async Task<IEnumerable<SerialResponse>> GetSerials(CultureInfo culture)
        {
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();

            foreach (var serial in serials)
                LocalizeFieldsSerial(serial, culture);

            var res = serials.Select(s => new SerialResponse(s, null));

            return res;
        }

        public async Task<SerialExtendResponse?> GetSerialById(Guid id, CultureInfo culture)
        {
            var serial = await _serialRepositoty.GetSerialWithFavoriteById(id);
            var comment = await _commentService.GetCommentsInMovie(id, 5, null);

            var reviews = await _reviewRepository.GetReviewByStatus(id, null,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();

            if (serial != null)
            {
                LocalizeFieldsSerial(serial, culture);
                return new SerialExtendResponse(serial, null, comment, popularReview);
            }

            return null;
        }

        public async Task<SerialExtendResponse?> GetSerialForAuthorizeUserById(Guid id, string userName, CultureInfo culture)
        {
            var user = await _userRepository.FindUser(userName);
            if (user == null) throw new ArgumentException(nameof(userName));

            var serial = await _serialRepositoty.GetSerialWithFavoriteById(id);

            var comment = await _commentService.GetCommentsInMovie(id, 5, userName);

            var reviews = await _reviewRepository.GetReviewByStatus(id, user.Id,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();

            if (serial != null)
            {
                LocalizeFieldsSerial(serial, culture);
                return new SerialExtendResponse(serial, serial.Favorites?.FirstOrDefault(x => x.User.Id == user.Id), comment, popularReview);
            }

            return null;
        }

        public async Task<IEnumerable<SerialResponse>> GetAllFavoriteSerials(string userName, CultureInfo culture)
        {
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            foreach (var serial in serials)
                LocalizeFieldsSerial(serial, culture);

            var favoriteSerialsForUser = from s in serials
                                         where s.Favorites != null
                                         from fav in s.Favorites!
                                         where fav.User.Id == user.Id &&
                                         (fav.Status != StatusMovie.None || fav.Score != 0 || fav.IsFavorite != false)
                                         select new SerialResponse(s, fav);

            return favoriteSerialsForUser;
        }

        public async Task<IEnumerable<SerialResponse>> GetSerialsWithUncheckedReview(CultureInfo culture)
        {
            var serials = await _serialRepositoty.GetSerialsWithUncheckedReview();

            foreach (var serial in serials)
                LocalizeFieldsSerial(serial, culture);

            var res = serials.Select(s => new SerialResponse(s, null));

            return res;
        }

        public async Task<IEnumerable<SerialResponse>> GetRecommendedSerials(string username, CultureInfo culture)
        {
            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(username);

            var predictionHandler =
                async (PredictionEnginePool<MovieRating, MovieRatingPrediction> predictionEnginePool, MovieRating input) =>
                    await Task.FromResult(predictionEnginePool.Predict(modelName: "MovieRecommenderModel", input));

            var favorite = await _favoriteRepository.FindFavoriteMovies(user.Id);
            var serials = await _serialRepositoty.GetAllSerialsWithFavorite();
            var unWatchedSerials = serials.Where(f => !favorite.Any(fav => fav.MovieId == f.Id && (fav.Score != null || fav.Score != 0)));

            var resultSerials = new List<SerialResponse>();

            MovieRatingPrediction prediction = null;

            foreach (var serial in unWatchedSerials)
            {
                prediction = await predictionHandler(_predictionEnginePool, new MovieRating
                {
                    UserId = user.Id.ToString(),
                    MovieId = serial.Id.ToString(),
                    Genres = serial.Genre.Select(g => g.ToString()).ToArray()
                });

                if ((float)(100 / (1 + Math.Exp(-prediction.Score))) > 70)
                {
                    LocalizeFieldsSerial(serial, culture);
                    resultSerials.Add(new SerialResponse(serial, null));
                }
            }

            return resultSerials;
        }

        private void LocalizeFieldsSerial(Serial serial, CultureInfo culture)
        {
            _localizationService.SetLanguage(culture);

            serial.Name = _localizationService[serial.Name];
            serial.Description = _localizationService[serial.Description];
            serial.Country = serial.Country != null ? _localizationService[serial.Country] : null;

            if (serial.People.Any())
            {
                foreach (var people in serial.People)
                    people.Name = _localizationService[people.Name];
            }
        }
    }
}
