using RateFilms.Domain.Models;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Application.Services
{
    public class FilmService: IFilmService
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
            await _repository.CreateAsync(film);
        }

        public async Task<IEnumerable<Film?>> GetFilms()
        {
            
            return _filmRepository.GetAllFilms();
        }
    }
}
