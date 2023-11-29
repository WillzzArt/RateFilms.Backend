using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Application.Services
{
    public interface IUserService
    {
        Task<LoginResponse?> Authenticate(LoginRequest model);
        Task<LoginResponse?> Register(Registration model);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<LoginResponse?> ChangePassword(LoginRequest model);
    }
}
