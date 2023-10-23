﻿using RateFilms.Domain.Models;
using RateFilms.Domain.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Application.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetFilms();
        Task CreateFilmsAsync(Film film);
    }
}
