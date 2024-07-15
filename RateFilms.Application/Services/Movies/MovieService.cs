using RateFilms.Application.Services.Films;
using RateFilms.Application.Services.Serials;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Repositories;
using System.Globalization;

namespace RateFilms.Application.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IFilmService _filmService;
        private readonly ISerialService _serialService;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public MovieService(
            IFilmService filmService, 
            ISerialService serialService, 
            IMovieRepository movieRepository, 
            IUserRepository userRepository)
        {
            _filmService = filmService;
            _serialService = serialService;
            _movieRepository = movieRepository;
            this._userRepository = userRepository;
        }

        public async Task<MovieResponse> GetAllFavoritesMovie(string username, CultureInfo culture)
        {
            var films = await _filmService.GetAllFavoriteFilms(username, culture);
            var serials = await _serialService.GetAllFavoriteSerials(username, culture);

            return new MovieResponse(films, serials);
        }

        public async Task<MovieResponse> GetAllMovies(CultureInfo culture)
        {
            var films = await _filmService.GetFilms(culture);
            var serils = await _serialService.GetSerials(culture);

            return new MovieResponse(films, serils);
        }

        public async Task<MovieResponse> GetAllMoviesForAuthorizeUser(string username, CultureInfo culture)
        {
            var films = await _filmService.GetFilmForAuthorizeUser(username, culture);
            var serils = await _serialService.GetSerialForAuthorizeUser(username, culture);

            return new MovieResponse(films, serils);
        }

        public async Task<MovieResponse> GetMoviesWithUncheckedReview(CultureInfo culture)
        {
            var films = await _filmService.GetFilmsWithUncheckedReview(culture);
            var serils = await _serialService.GetSerialsWithUncheckedReview(culture);

            return new MovieResponse(films, serils);
        }

        public async Task<MovieResponse> GetRecommendedMovie(string username, CultureInfo culture)
        {
            var films = await _filmService.GetRecommendedFilms(username, culture);
            var serials = await _serialService.GetRecommendedSerials(username, culture);

            return new MovieResponse(films, serials);
        }

        public async Task SetFavoriteMovie(FavoriteMovie favoriteFilm, string userName)
        {
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            await _movieRepository.SetFavoriteMovie(favoriteFilm, user);

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
    }
}
