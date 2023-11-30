using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IUserRepository _userRepository;

        public FilmService(
            IFilmRepository filmRepository,
            IUserRepository userRepository)
        {
            _filmRepository = filmRepository;
            _userRepository = userRepository;
        }

        public async Task CreateFilmsAsync(Film film)
        {
            await _filmRepository.CreateAsync(FilmConvertor.FilmDomainConvertFilmDb(film));
        }

        public async Task<IEnumerable<FilmAuthorizeResponse?>> GetFilmForAuthorizeUser(string userName)
        {
            var films = await _filmRepository.GetAllFilmsWithFavorite();
            var user = await _userRepository.FindUser(userName);

            if (user == null) throw new ArgumentException(userName);

            var favoriteFilmsForUser = films
                .Select(f =>
                    new FilmAuthorizeResponse(
                        f,
                        f.Favorites?.FirstOrDefault(x => x.User.Id == user.Id))
                    ).ToList();

            /*var favoriteFilmsForUser = (from film in films
                                   from favorite in film.Favorites
                                   where favorite.User.Id == user.Id
                                   select new FilmAuthorizeResponse(film, favorite)).ToList();*/

            return favoriteFilmsForUser;
        }

        public async Task<IEnumerable<Film?>> GetFilms()
        {
            return await _filmRepository.GetAllFilms();
        }

        public async Task SetFavoriteFilm(FavoriteFilm favoriteFilm, string userName)
        {
            await _filmRepository.SetFavoriteFilm(favoriteFilm, userName);
        }
    }
}
