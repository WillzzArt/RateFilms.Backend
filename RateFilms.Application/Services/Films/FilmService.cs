using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO;
using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services.Films
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
            var films = await _filmRepository.GetAllFilms();

            var filmsRespons = films.Select(f => new FilmResponse(f, null)).ToList();

            return filmsRespons;
        }

        public async Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName)
        {
            await _filmRepository.SetFavoriteFilm(favoriteFilm, userName);
        }
    }
}
