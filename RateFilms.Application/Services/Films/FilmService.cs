using Microsoft.Extensions.ML;
using RateFilms.Application.Services.Localization;
using RateFilms.Application.Services.Movies;
using RateFilms.Common.Models.Localization;
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

        public async Task<IEnumerable<FilmResponse?>> GetFilmForAuthorizeUser(string userName, CultureInfo culture)
        {
            var films = await _filmRepository.GetAllFilmsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            foreach (var film in films)
                LocalizeFieldsFilm(film, culture);

            var favoriteFilmsForUser = films
                .Select(f =>
                    new FilmResponse(
                        f,
                        f.Favorites?.FirstOrDefault(x => x.User.Id == user.Id))
                    ).ToList();

            return favoriteFilmsForUser;
        }

        public async Task<IEnumerable<FilmResponse?>> GetFilms(CultureInfo culture)
        {
            var films = await _filmRepository.GetAllFilmsWithFavorite();

            foreach (var film in films)
                LocalizeFieldsFilm(film, culture);

            var filmsResponse = films.Select(f => new FilmResponse(f, null)).ToList();

            return filmsResponse;
        }

        public async Task<FilmExtendResponse?> GetFilmForAuthorizeUserById(Guid id, string userName, CultureInfo culture)
        {
            var user = await _userRepository.FindUser(userName);
            if (user == null) throw new ArgumentException(nameof(userName));

            var film = await _filmRepository.GetFilmWithFavoriteById(id);

            var comment = await _commentService.GetCommentsInMovie(id, 5, userName);

            var reviews = await _reviewRepository.GetReviewByStatus(id, user.Id,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();


            if (film != null)
            {
                LocalizeFieldsFilm(film, culture);

                return new FilmExtendResponse(film, film.Favorites?.FirstOrDefault(x => x.User.Id == user.Id), comment, popularReview);
            }

            return null;
        }

        public async Task<FilmExtendResponse?> GetFilmById(Guid id, CultureInfo culture)
        {
            var film = await _filmRepository.GetFilmWithFavoriteById(id);

            var comment = await _commentService.GetCommentsInMovie(id, 5, null);

            var reviews = await _reviewRepository.GetReviewByStatus(id, null,
                x => x.Status == ReviewStatus.Published);

            var popularReview = reviews.OrderByDescending(r => r.CountLike).FirstOrDefault();

            if (film != null)
            {
                LocalizeFieldsFilm(film, culture);

                return new FilmExtendResponse(film, null, comment, popularReview);
            }

            return null;
        }

        public async Task<IEnumerable<FilmResponse>> GetAllFavoriteFilms(string userName, CultureInfo culture)
        {
            var films = await _filmRepository.GetAllFilmsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            foreach (var film in films)
                LocalizeFieldsFilm(film, culture);

            var favoriteFilmsForUser = from f in films
                                       where f.Favorites != null
                                       from fav in f.Favorites!
                                       where fav.User.Id == user.Id &&
                                       (fav.Status != StatusMovie.None || fav.Score != 0 || fav.IsFavorite != false) 
                                       select new FilmResponse(f, fav);

            return favoriteFilmsForUser;
        }

        public async Task<IEnumerable<FilmResponse>> GetFilmsWithUncheckedReview(CultureInfo culture)
        {
            var films = await _filmRepository.GetFilmsWithUncheckedReview();

            foreach (var film in films)
                LocalizeFieldsFilm(film, culture);

            var filmsRespons = films.Select(f => new FilmResponse(f, null)).ToList();

            return filmsRespons;
        }

        public async Task<IEnumerable<FilmResponse>> GetRecommendedFilms(string username, CultureInfo culture)
        {
            var user = await _userRepository.FindUser(username);

            if (user == null) throw new ArgumentException(username);

            var predictionHandler =
                async (PredictionEnginePool<MovieRating, MovieRatingPrediction> predictionEnginePool, MovieRating input) =>
                    await Task.FromResult(predictionEnginePool.Predict(modelName: "MovieRecommenderModel", input));

            var favorite = await _favoriteRepository.FindFavoriteMovies(user.Id);
            var films = await _filmRepository.GetAllFilmsWithFavorite();
            var unWatchedFilms = films.Where(f => !favorite.Any(fav => fav.MovieId == f.Id && (fav.Score != null || fav.Score != 0)));

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
                    LocalizeFieldsFilm(film, culture);
                    resultFilms.Add(new FilmResponse(film, null));
                }
            }

            return resultFilms;
        }

        private void LocalizeFieldsFilm(Film film, CultureInfo culture)
        {
            _localizationService.SetLanguage(culture);

            film.Name = _localizationService[film.Name];
            film.Description = _localizationService[film.Description];
            film.Country = film.Country != null ? _localizationService[film.Country] : null;

            if (film.People.Any())
            {
                foreach (var people in film.People)
                    people.Name = _localizationService[people.Name];
            }
        }
    }
}
