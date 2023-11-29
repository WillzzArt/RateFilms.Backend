using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> ChangePassword(string userLogin, string password)
        {
            var userDb = await _context.User.FirstOrDefaultAsync(u => u.UserName == userLogin
                                                                || u.Email == userLogin);

            if (userDb != null)
            {
                userDb.Password = password;
                await _context.SaveChangesAsync();
                return UserConvertor.UserDbConvertUserDomain(userDb);
            }

            return null;
        }

        public async Task<User?> FindUser(string userLogin)
        {
            var user = await _context.Set<UserDbModel>().FirstOrDefaultAsync(u => u.UserName == userLogin
                                                                || u.Email == userLogin);

            if (user == null) return null;

            return UserConvertor.UserDbConvertUserDomain(user);
        }
    }
}
