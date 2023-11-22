using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Repositories;
using RateFilms.Domain.StorageModels;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
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
