using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
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

        public async Task<IEnumerable<Film>> GetAllFilmsWithFavorite()
        {
            var filmsDb = await _context.Film
                .Include(f => f.Images)
                .Include(f => f.Genre)
                .Include(f => f.Favorites)
                .ToListAsync();

            return FilmConvertor.FilmDbListConvertFilmDomainList(filmsDb);
        }

        public async Task<Film?> GetFilmWithFavoriteById(Guid filmId)
        {
            var filmDb = await _context.Film.Select(f => new FilmDbModel
            {
                Id = f.Id,
                Description = f.Description,
                ReleaseDate = f.ReleaseDate,
                AgeRating = f.AgeRating,
                Country = f.Country,
                Favorites = f.Favorites.ToList(),
                Name = f.Name,
                Duration = f.Duration,
                Images = f.Images.ToList(),
                Genre = f.Genre.ToList(),
                People = f.People.Select(p => new PersonInMovieDbModel
                {
                    PersonId = p.PersonId,
                    Person = p.Person,
                    Professions = p.Professions.ToList()
                }).Where(p => p.Professions.Count() > 1).ToList()

            }).FirstOrDefaultAsync(f => f.Id == filmId);

            if (filmDb != null)
            {
                return FilmConvertor.FilmDbConvertFilmDomain(filmDb, filmDb.Favorites);
            }

            return null;
        }

        //TODO: объеденить с сериалами наверно
        public async Task<IEnumerable<Film>> GetFilmsWithUncheckedReview()
        {
            var films = new HashSet<Film>();

            var idList = await _context.Comment
            .Where(c => c.Status == ReviewStatus.Unpublished && c.Favorite!.Movie!.Type == MovieType.Film)
            .Select(c => c.Favorite!.MovieId)
            .ToListAsync();

            foreach (var id in idList)
            {
                var film = await _context.Film
                    .Include(f => f.Images)
                    .Include(f => f.Genre)
                    .Include(f => f.Favorites)
                    .FirstOrDefaultAsync(c => c.Id == id);

                films.Add(FilmConvertor.FilmDbConvertFilmDomain(film!, film!.Favorites));
            }

            return films;
        }
    }
}
