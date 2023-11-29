using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IBaseRepository _repository;
        private readonly IFilmRepository _filmRepository;

        public FilmService(IBaseRepository baseRepository, IFilmRepository filmRepository)
        {
            _repository = baseRepository;
            _filmRepository = filmRepository;
        }

        public async Task CreateFilmsAsync(Film film)
        {
            await _filmRepository.CreateAsync(FilmConvertor.FilmDomainConvertFilmDb(film));
        }

        public async Task<IEnumerable<Film?>> GetFilms()
        {
            return _filmRepository.GetAllFilms();
        }

        public async Task SetFavoriteFilm(FavoriteFilm favoriteFilm, string userName)
        {
            await _filmRepository.SetFavoriteFilm(favoriteFilm, userName);
        }
    }
}
