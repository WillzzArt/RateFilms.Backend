using RateFilms.Application.Services.Movies;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services.Films
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentService _commentService;

        public FilmService(
            IFilmRepository filmRepository,
            IUserRepository userRepository,
            ICommentService commentSerivice)
        {
            _filmRepository = filmRepository;
            _userRepository = userRepository;
            _commentService = commentSerivice;
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

            if (film != null)
            {
                return new FilmExtendResponse(film, film.Favorites?.FirstOrDefault(x => x.User.Id == user.Id), comment);
            }

            return null;
        }

        public async Task<FilmExtendResponse?> GetFilmById(Guid id)
        {
            var film = await _filmRepository.GetFilmWithFavoriteById(id);
            var comment = await _commentService.GetCommentsInFilm(id, 5, null);

            if (film != null)
            {
                return new FilmExtendResponse(film, null, comment);
            }

            return null;
        }

        public async Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName)
        {
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            await _filmRepository.SetFavoriteFilm(favoriteFilm, user);
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
    }
}
