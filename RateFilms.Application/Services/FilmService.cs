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

        public FilmService(IBaseRepository baseRepository)
        {
            _repository = baseRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {

            return await _repository.GetAllAsync<User>();
        }
    }
}
