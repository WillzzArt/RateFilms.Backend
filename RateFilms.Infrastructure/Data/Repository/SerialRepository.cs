using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
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
                .Select(s => new SerialDbModel
                {
                    Id = s.Id,
                    Description = s.Description,
                    ReleaseDate = s.ReleaseDate,
                    AgeRating = s.AgeRating,
                    Country = s.Country,
                    Favorites = s.Favorites.ToList(),
                    Name = s.Name,
                    Genre = s.Genre.ToList(),
                    PreviewImage = s.PreviewImage,
                    Seasons = s.Seasons.Select(season => new SeasonDbModel
                    {
                        Id = season.Id,
                        Description = season.Description,
                        Series = season.Series.ToList(),
                        CountMaxSeries = season.CountMaxSeries,
                        RealeseDate = season.RealeseDate,
                    }).ToList()
                })
                .ToListAsync();

            return SerialConvertor.SerialDbListConvertSerialDomainList(serials);
        }

        public async Task<Serial?> GetSerialWithFavoriteById(Guid serialId)
        {
            var serial = await _context.Serial
                .Select(s => new SerialDbModel
                {
                    Id = s.Id,
                    Description = s.Description,
                    ReleaseDate = s.ReleaseDate,
                    AgeRating = s.AgeRating,
                    Country = s.Country,
                    Favorites = s.Favorites.ToList(),
                    Name = s.Name,
                    Genre = s.Genre.ToList(),
                    PreviewImage = s.PreviewImage,
                    Seasons = s.Seasons.Select(season => new SeasonDbModel
                    {
                        Id = season.Id,
                        Description = season.Description,
                        Series = season.Series.ToList(),
                        CountMaxSeries = season.CountMaxSeries,
                        RealeseDate = season.RealeseDate,
                        Images = season.Images.ToList(),
                    }).ToList(),
                    People = s.People.Select(p => new PersonInMovieDbModel
                    {
                        PersonId = p.PersonId,
                        Person = p.Person,
                        Professions = p.Professions.ToList()
                    }).Where(p => p.Professions.Count() > 1).ToList()
                })
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
