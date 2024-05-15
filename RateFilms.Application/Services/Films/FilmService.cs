using Microsoft.Extensions.ML;
using RateFilms.Application.Services.Localization;
using RateFilms.Application.Services.Movies;
using RateFilms.Common.Models.MovieRatingModels;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;
using System.Globalization;

namespace RateFilms.Application.Services.Films
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentService _commentService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly PredictionEnginePool<MovieRating, MovieRatingPrediction> _predictionEnginePool;
        private readonly LocalizationService _localizationService;

        public FilmService(
            IFilmRepository filmRepository,
            IUserRepository userRepository,
            ICommentService commentSerivice,
            IReviewRepository reviewRepository,
            IFavoriteRepository favoriteRepository,
            PredictionEnginePool<MovieRating, MovieRatingPrediction> predictionEnginePool,
            LocalizationService localizationService)
        {
            _filmRepository = filmRepository;
            _userRepository = userRepository;
            _commentService = commentSerivice;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
            _predictionEnginePool = predictionEnginePool;
            _localizationService = localizationService;
            _localizationService.LoadTranslation();
        }

        public async Task CreateFilmsAsync(Film film)
        {
            await _filmRepository.CreateAsync(FilmConvertor.FilmDomainConvertFilmDb(film));
        }


        public async Task<IEnumerable<FilmResponse?>> GetFilmForAuthorizeUser(string userName)
        {
            var films = await _filmRepository.GetAllFilmsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            var favoriteFilmsForUser = films
                .Select(f =>
                    new FilmResponse(
                        f,
                        f.Favorites?.FirstOrDefault(x => x.User.Id == user.Id))
                    ).ToList();

            return favoriteFilmsForUser;
        }

        public async Task<IEnumerable<FilmResponse?>> GetFilms()
        {
            var films = await _filmRepository.GetAllFilmsWithFavorite();

            var filmsRespons = films.Select(f => new FilmResponse(f, null)).ToList();

            return filmsRespons;
        }

        public async Task<FilmExtendResponse?> GetFilmForAuthorizeUserById(Guid id, string userName)
        {
            var user = await _userRepository.FindUser(userName);
            if (user == null) throw new ArgumentException(nameof(userName));

            var film = await _filmRepository.GetFilmWithFavoriteById(id);

            var comment = await _commentService.GetCommentsInFilm(id, 5, userName);

            var reviews = await _reviewRepository.GetReviewByStatus(id, user.Id, true,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();


            if (film != null)
            {
                return new FilmExtendResponse(film, film.Favorites?.FirstOrDefault(x => x.User.Id == user.Id), comment, popularReview);
            }

            return null;
        }

        public async Task<FilmExtendResponse?> GetFilmById(Guid id, CultureInfo culture)
        {
            var film = await _filmRepository.GetFilmWithFavoriteById(id);

            var comment = await _commentService.GetCommentsInFilm(id, 5, null);

            var reviews = await _reviewRepository.GetReviewByStatus(id, null, true,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();

            if (film != null)
            {
                _localizationService.SetLanguage(culture);
                film.Name = _localizationService[film.Name];
                film.Description = _localizationService[film.Description];
                film.Country = film.Country != null ? _localizationService[film.Country] : null;

                foreach (var people in film.People)
                    people.Name = _localizationService[people.Name];

                return new FilmExtendResponse(film, null, comment, popularReview);
            }

            return null;
        }

        public async Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName)
        {
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            await _filmRepository.SetFavoriteFilm(favoriteFilm, user);

            /*if (favoriteFilm.Score != null && favoriteFilm.Score != 0)
            {
                MLContext mlContext = new MLContext();

                var modelHandler = async (PredictionEnginePool<MovieRating, MovieRatingPrediction> predictionEnginePool, string modelName) =>
                    await Task.FromResult(predictionEnginePool.GetModel(modelName));

                var dataPrepPipeline = await modelHandler(_predictionEnginePool, "data_preparation_pipeline");
                var trainedModel = await modelHandler(_predictionEnginePool, "MovieRecommenderModel");
                
                var predictor = (trainedModel as TransformerChain<ITransformer>)!.LastTransformer as FieldAwareFactorizationMachinePredictionTransformer;
                var originalModelParameters = predictor!.Model;

                var film = await _filmRepository.GetFilmWithFavoriteById(favoriteFilm.MovieId);

                var inputData = new List<MovieRating>() { new MovieRating
                {
                    UserId = user.Id.ToString(),
                    MovieId = favoriteFilm.MovieId.ToString(),
                    Genres = film.Genre.Select(g => g.ToString()).ToArray(),
                    Label = favoriteFilm.Score! > 3.5 ? true : false
                }};

                var retrainingDataView = mlContext.Data.LoadFromEnumerable(inputData);
                var newData = dataPrepPipeline.Transform(retrainingDataView);
                var transformedNewData = dataPrepPipeline.Transform(newData);

                var retrainedModel =
                    mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new string[] { "Features" })
                        .Fit(transformedNewData, null, originalModelParameters);

                //var modelDataView = retrainedModel.Transform(newData);

                var modelPath = Path.Combine(Environment.CurrentDirectory, "../RateFilms.WebAPI/Data", "MovieRecommenderModel.zip");

                mlContext.Model.Save(retrainedModel, transformedNewData.Schema, modelPath);

            }*/
        }

        public async Task<IEnumerable<FilmResponse>> GetAllFavoriteFilms(string userName)
        {
            var films = await _filmRepository.GetAllFilmsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            var favoriteFilmsForUser = from f in films
                                       where f.Favorites != null
                                       from fav in f.Favorites!
                                       where fav.User.Id == user.Id
                                       select new FilmResponse(f, fav);

            return favoriteFilmsForUser;
        }

        public async Task<IEnumerable<FilmResponse>> GetFilmsWithUncheckedReview()
        {
            var films = await _filmRepository.GetFilmsWithUncheckedReview();

            var filmsRespons = films.Select(f => new FilmResponse(f, null)).ToList();

            return filmsRespons;
        }

        public async Task<IEnumerable<FilmResponse>> GetRecommendedFilms(string username)
        {
            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(username);

            var predictionHandler =
                async (PredictionEnginePool<MovieRating, MovieRatingPrediction> predictionEnginePool, MovieRating input) =>
                    await Task.FromResult(predictionEnginePool.Predict(modelName: "MovieRecommenderModel", input));

            var favorite = await _favoriteRepository.FindFavoriteFilms(user.Id);
            var films = await _filmRepository.GetAllFilmsWithFavorite();
            var unWatchedFilms = films.Where(f => !favorite.Any(fav => fav.FilmId == f.Id && (fav.Score != null || fav.Score != 0)));

            var resultFilms = new List<FilmResponse>();

            MovieRatingPrediction prediction = null;

            foreach (var film in unWatchedFilms)
            {
                prediction = await predictionHandler(_predictionEnginePool, new MovieRating
                {
                    UserId = user.Id.ToString(),
                    MovieId = film.Id.ToString(),
                    Genres = film.Genre.Select(g => g.ToString()).ToArray()
                });

                if ((float)(100 / (1 + Math.Exp(-prediction.Score))) > 70)
                {
                    resultFilms.Add(new FilmResponse(film, null));
                }
            }

            return resultFilms;
        }
    }
}
