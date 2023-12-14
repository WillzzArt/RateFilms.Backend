using RateFilms.Domain.DTO.Authorization;

namespace RateFilms.Application.Services
{
    public interface IUserService
    {
        Task<LoginResponse?> Authenticate(LoginRequest model);
        Task<LoginResponse?> Register(Registration model);
        Task<LoginResponse?> ChangePassword(LoginRequest model);
        Task UpdateUser(UserExtendedResponse user, string username);
    }
}
