﻿using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.DTO.People;

namespace RateFilms.Application.Services
{
    public interface IUserService
    {
        Task<LoginResponse?> Authenticate(LoginRequest model);
        Task<LoginResponse?> Register(Registration model);
        Task<LoginResponse?> ChangePassword(LoginRequest model);
        Task<string?> UpdateUser(UserExtendedResponse user, string username);
        Task<UserExtendedResponse?> FindUserForProfile(string username);
        Task<IEnumerable<UserMini>> FIndUsers(string username);
    }
}
