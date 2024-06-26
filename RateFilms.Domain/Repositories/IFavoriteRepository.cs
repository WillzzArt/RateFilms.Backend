﻿using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface IFavoriteRepository
    {
        Task<FavoriteFilmDbModel?> FindFavoriteFilm(Guid filmId, Guid userId);
        Task<FavoriteSerialDbModel?> FindFavoriteSerial(Guid serialId, Guid userId);
        Task<IEnumerable<FavoriteFilmDbModel>> FindFavoriteFilms(Guid userId);
        Task<IEnumerable<FavoriteSerialDbModel>> FindFavoriteSerials(Guid userId);
        Task<IEnumerable<FavoriteSerialDbModel>> FindFavoriteInSerials();
        Task<IEnumerable<FavoriteFilmDbModel>> FindFavoriteInFilms();
    }
}
