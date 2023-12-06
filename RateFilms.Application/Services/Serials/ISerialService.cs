﻿using RateFilms.Domain.DTO;
using RateFilms.Domain.DTO.Serials;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Application.Services.Serials
{
    public interface ISerialService
    {
        Task<IEnumerable<SerialResponse?>> GetSerials();
        Task<IEnumerable<SerialResponse?>> GetSerialForAuthorizeUser(string userName);
        Task CreateSerialAsync(Serial film);
        Task SetFavoriteSerial(FavoriteMovie favoriteMovie, string userName);
    }
}
