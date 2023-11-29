using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindUser(string userLogin);
        Task<User?> ChangePassword(string userLogin, string password);
    }
}
