using RateFilms.Application.Converters;
using RateFilms.Application.Models;
using RateFilms.Domain.Models;
using RateFilms.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Application.Services
{
    public class MovieService
    {
        private readonly IBaseRepository _repository;
        private readonly IFilmRepository _filmRepository;

        public MovieService(IBaseRepository baseRepository, IFilmRepository filmRepository) 
        {
            _repository = baseRepository;
            _filmRepository = filmRepository;
        }

        public async Task<Movies> GetAllMovies()
        {
            var serials = await _repository.GetAllAsync<Serial>();
            var films = _filmRepository.GetAllFilms();

            return MovieConverter.UnionFilmAndSerials(films, serials);
        }
    }
}
