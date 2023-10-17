using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Models.Authorization;
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

        public async Task<User?> FindUser(string userLogin)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.UserName == userLogin
                                                                || u.Email == userLogin);
        }
    }
}
