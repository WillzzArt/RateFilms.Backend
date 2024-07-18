using Microsoft.EntityFrameworkCore;
using RateFilms.Common.Models.Localization;
using RateFilms.Domain.Repositories;
using System.Globalization;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private ApplicationDbContext _context;

        public LocalizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetResource(CultureInfo culture) => 
             _context.Resource
                .Include(r => r.Culture)
                .Where(r => r.Culture.Name == culture.Name)
                .ToDictionary(l => l.Key, l => l.Value);

        public async Task CreateResource(Resource resource, string culture)
        {
            var cultureDb = await _context.Culture.FirstOrDefaultAsync(c => c.Name == culture);

            if (cultureDb == null) throw new ArgumentException(culture);

            resource.Culture = cultureDb;

            _context.Resource.Add(resource);

            await _context.SaveChangesAsync();
        }
    }
}
