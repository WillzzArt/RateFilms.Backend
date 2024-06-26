using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindUser(string userLogin);
        Task<User?> FindUserWithImage(string username);
        Task<User?> ChangePassword(string userLogin, string password);
        Task<bool> UpdateUser(UserExtendedResponse user, string username);
        Task<IEnumerable<UserMini>> FindUsersByUsername(string username);
        Task<bool> IsActualToken(string refreshToken);
        Task SwitchBann(Guid userId, bool isBanned);
    }
}
