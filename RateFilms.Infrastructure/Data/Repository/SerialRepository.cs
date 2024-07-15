using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class SerialRepository : ISerialRepositoty
    {
        private ApplicationDbContext _context;

        public SerialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Serial>> GetAllSerialsWithFavorite()
        {
            var serials = await _context.Serial
                .Include(s => s.Seasons)
                    .ThenInclude(s => s.Series)
                .Include(s => s.PreviewImage)
                .Include(s => s.Genre)
                .Include(s => s.Favorites)
                .ToListAsync();

            return SerialConvertor.SerialDbListConvertSerialDomainList(serials);
        }

        public async Task<Serial?> GetSerialWithFavoriteById(Guid serialId)
        {
            var serial = await _context.Serial
                .Include(s => s.People)
                    .ThenInclude(p => p.Professions)
                .Include(s => s.People)
                    .ThenInclude(p => p.Person)
                        .ThenInclude(p => p.Image)
                .Include(s => s.Seasons)
                    .ThenInclude(s => s.Series)
                        .ThenInclude(s => s.PreviewImage)
                .Include(s => s.Seasons)
                    .ThenInclude(s => s.Images)
                .Include(s => s.PreviewImage)
                .Include(s => s.Genre)
                .Include(s => s.Favorites)
                .FirstOrDefaultAsync(s => s.Id == serialId);

            if (serial != null)
            {
                return SerialConvertor.SerialDbConvertSerialDomain(serial, serial.Favorites);
            }

            return null;
        }
        
        //TODO: объеденить с фильмами наверное
        public async Task<IEnumerable<Serial>> GetSerialsWithUncheckedReview()
        {
            var serials = new HashSet<Serial>();


            var idList = await _context.Comment
                .Where(c => c.Status == ReviewStatus.Unpublished && c.Favorite!.Movie!.Type == MovieType.Serial)
                .Select(c => c.Favorite!.MovieId)
                .ToListAsync();

            foreach (var id in idList)
            {
                var serial = await _context.Serial
                    .Include(s => s.Seasons)
                        .ThenInclude(s => s.Series)
                    .Include(s => s.PreviewImage)
                    .Include(s => s.Genre)
                    .Include(s => s.Favorites)
                    .FirstOrDefaultAsync(c => c.Id == id);

                serials.Add(SerialConvertor.SerialDbConvertSerialDomain(serial!, serial!.Favorites));
            }

            return serials;
        }
    }
}
