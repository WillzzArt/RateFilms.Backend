using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FavoriteFilmDbModel?> FindFavoriteFilm(Guid filmId, Guid userId)
        {
            return await _context.FavoriteFilms
                .FirstOrDefaultAsync(fav => fav.FilmId == filmId && fav.UserId == userId);
        }

        public async Task<IEnumerable<FavoriteFilmDbModel>> FindFavoriteFilms(Guid userId)
        {
            return await _context.FavoriteFilms.Where(fav => fav.UserId == userId).ToListAsync();
        }

        public async Task<FavoriteSerialDbModel?> FindFavoriteSerial(Guid serialId, Guid userId)
        {
            return await _context.FavoriteSerials
                .FirstOrDefaultAsync(fav => fav.SerialId == serialId && fav.UserId == userId);
        }

        public async Task<IEnumerable<FavoriteSerialDbModel>> FindFavoriteSerials(Guid userId)
        {
            return await _context.FavoriteSerials.Where(fav => fav.UserId == userId).ToListAsync();
        }
    }
}
