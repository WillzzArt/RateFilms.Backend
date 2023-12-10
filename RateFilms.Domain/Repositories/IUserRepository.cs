using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindUser(string userLogin);
        Task<User?> ChangePassword(string userLogin, string password);
    }
}
