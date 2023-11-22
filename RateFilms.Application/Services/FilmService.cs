using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;
using RateFilms.Domain.StorageModels;

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

        public async Task CreateFilmsAsync(FilmDbModel film)
        {
            await _repository.CreateAsync(film);
        }

        public async Task<IEnumerable<Film?>> GetFilms()
        {
            return _filmRepository.GetAllFilms();
        }
    }
}
