﻿using RateFilms.Domain.DTO;
using RateFilms.Domain.DTO.Films;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services.Films
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmResponse?>> GetFilms();
        Task<IEnumerable<FilmResponse?>> GetFilmForAuthorizeUser(string userName);
        Task<FilmExtendResponse?> GetFilmById(Guid id);
        Task<FilmExtendResponse?> GetFilmForAuthorizeUser(Guid id, string userName);
        Task CreateFilmsAsync(Film film);
        Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName);
    }
}
