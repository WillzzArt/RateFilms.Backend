using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO;
using RateFilms.Domain.Models.Authorization;
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

        //TODO
        public async Task CreateAsync(SerialDbModel serial)
        {
            if (serial == null)
            {
                throw new ArgumentNullException(nameof(serial));
            }

            var saveSerial = new SerialDbModel
            {
                Name = serial.Name,
                Description = serial.Description,
                AgeRating = serial.AgeRating,
                RealeseDate = serial.RealeseDate,
                PreviewImage = serial.PreviewImage
            };

            await _context.Serial.AddAsync(saveSerial);

            if (serial.People.Any())
            {
                var professions = _context.Profession.ToList();

                foreach (var person in serial.People)
                {
                    var savePersInSerial = new PersonInSerialDbModel
                    {
                        Serial = saveSerial,
                        PersonId = person.PersonId
                    };

                    if (person.PersonId != Guid.Empty)
                    {
                        await _context.PersonInSerials.AddAsync(savePersInSerial);
                    }
                    else if (person.Person.Image?.Id == Guid.Empty)
                    {
                        var savePerson = new PersonDbModel
                        {
                            Age = person.Person.Age,
                            Image = person.Person.Image,
                            Name = person.Person.Name
                        };

                        await _context.Person.AddAsync(savePerson);
                        savePersInSerial.Person = savePerson;
                        await _context.PersonInSerials.AddAsync(savePersInSerial);
                    }

                    savePersInSerial.Professions = professions
                            .Where(p => person.Professions.Any(prof => prof.Id == p.Id)).ToList();
                }
            }

            if (serial.Seasons.Any())
            {
                foreach (var season in serial.Seasons)
                {
                    var saveSeason = new SeasonDbModel
                    {
                        Description = season.Description,
                        Serial = saveSerial,
                        RealeseDate = season.RealeseDate
                    };

                    await _context.Season.AddAsync(saveSeason);

                    if (season.Series.Any())
                    {
                        foreach (var series in season.Series)
                        {
                            var saveSeries = new SeriesDbModel
                            {
                                Name = series.Name,
                                Duration = series.Duration,
                                RealeseDate = series.RealeseDate,
                                Season = saveSeason,
                                PreviewImage = series.PreviewImage
                            };

                            await _context.Series.AddAsync(saveSeries);
                        }
                    }

                    if (season.Images.Any())
                    {
                        foreach (var image in season.Images)
                        {
                            if (image.Id == Guid.Empty)
                            {
                                image.Season = saveSeason;
                                await _context.Image.AddAsync(image);
                            }
                        }
                    }
                }
            }

            var genries = new List<GenreDbModel>();

            foreach (var genre in serial.Genre)
            {
                genries.Add(_context.Genre.Find(genre.Id));
            }

            saveSerial.Genre = genries;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Serial>> GetAllSerials()
        {
            var serials = await _context.Serial
                .Include(s => s.Seasons)
                    .ThenInclude(s => s.Series)
                .Include(s => s.PreviewImage)
                .Include(s => s.Genre)
                .ToListAsync();

            return SerialConvertor.SerialDbListConvertSerialDomainList(serials);
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

        public async Task<Serial?> GetSerialById(Guid serialId)
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
                .FirstOrDefaultAsync(s => s.Id == serialId);

            if (serial != null)
            {
                return SerialConvertor.SerialDbConvertSerialDomain(serial);
            }

            return null;
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

        public async Task SetFavoriteSerial(FavoriteMovie favoriteSerial, User user)
        {
            var favoriteSerialDb = await _context.FavoriteSerials
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.SerialId == favoriteSerial.MovieId);

            if (favoriteSerialDb == null)
            {
                var saveFavorite = new FavoriteSerialDbModel
                {
                    SerialId = favoriteSerial.MovieId,
                    UserId = user.Id,
                    Status = favoriteSerial.StatusMovie,
                    IsFavorite = favoriteSerial.IsFavorite,
                    Score = favoriteSerial.Score
                };

                await _context.FavoriteSerials.AddAsync(saveFavorite);
            }
            else
            {
                favoriteSerialDb.Status = favoriteSerial.StatusMovie;
                favoriteSerialDb.IsFavorite = favoriteSerial.IsFavorite;
            }

            await _context.SaveChangesAsync();
        }
    }
}
