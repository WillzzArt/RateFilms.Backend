using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class FilmRepository : IFilmRepository
    {
        private ApplicationDbContext _context;

        public FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //TODO
        public async Task CreateAsync(FilmDbModel film)
        {
            if (film == null)
            {
                throw new ArgumentNullException(nameof(film));
            }

            var saveFilm = new FilmDbModel
            {
                Name = film.Name,
                Description = film.Description,
                Duration = film.Duration,
                AgeRating = film.AgeRating,
                AvgRating = film.AvgRating,
                ReleaseDate = film.ReleaseDate
            };

            await _context.Film.AddAsync(saveFilm);

            if (film.People.Any())
            {
                var professions = _context.Profession.ToList();

                foreach (var person in film.People)
                {
                    var savePersInFilm = new PersonInFilmDbModel
                    {
                        Film = saveFilm,
                        PersonId = person.PersonId
                    };

                    if (person.PersonId != Guid.Empty)
                    {
                        await _context.PersonInFilm.AddAsync(savePersInFilm);
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
                        savePersInFilm.Person = savePerson;
                        await _context.PersonInFilm.AddAsync(savePersInFilm);
                    }

                    savePersInFilm.Professions = professions
                            .Where(p => person.Professions.Any(prof => prof.Id == p.Id)).ToList();
                }
            }

            if (film.Images.Any())
            {
                foreach (var image in film.Images)
                {
                    if (image.Id == Guid.Empty)
                    {
                        image.Film = saveFilm;
                        await _context.Image.AddAsync(image);
                    }
                }
            }
            var genries = new List<GenreDbModel>();

            foreach (var genre in film.Genre)
            {
                genries.Add(_context.Genre.Find(genre.Id));
            }

            saveFilm.Genre = genries;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Film>> GetAllFilms()
        {
            var filmsDb = await _context.Film
                .Include(f => f.People)
                    .ThenInclude(p => p.Professions)
                .Include(f => f.People)
                    .ThenInclude(p => p.Person)
                        .ThenInclude(p => p.Image)
                .Include(p => p.Images)
                .Include(p => p.Genre)
                .ToListAsync();

            return FilmConvertor.FilmDbListConvertFilmDomainList(filmsDb);
        }

        public async Task<IEnumerable<Film>> GetAllFilmsWithFavorite()
        {
            var filmsDb = await _context.Film
                .Include(f => f.People)
                    .ThenInclude(p => p.Professions)
                .Include(f => f.People)
                    .ThenInclude(p => p.Person)
                        .ThenInclude(p => p.Image)
                .Include(p => p.Images)
                .Include(p => p.Genre)
                .Include(f => f.Favorite)
                .ToListAsync();

            return FilmConvertor.FilmDbListConvertFilmDomainList(filmsDb);
        }

        public async Task SetFavoriteFilm(FavoriteMovie favoriteFilm, string userName)
        {
            var user = _context.User.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                throw new ArgumentException(nameof(userName));
            }

            var favoriteFilmDb = await _context.FavoriteFilms
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.FilmId == favoriteFilm.MovieId);

            if (favoriteFilmDb == null)
            {
                var saveFavorite = new FavoriteFilmDbModel
                {
                    FilmId = favoriteFilm.MovieId,
                    UserId = user.Id,
                    Status = favoriteFilm.StatusMovie,
                    isFavorite = favoriteFilm.IsFavorite
                };

                await _context.FavoriteFilms.AddAsync(saveFavorite);
            }
            else
            {
                favoriteFilmDb.Status = favoriteFilm.StatusMovie;
                favoriteFilmDb.isFavorite = favoriteFilm.IsFavorite;
            }

            await _context.SaveChangesAsync();
        }
    }
}
