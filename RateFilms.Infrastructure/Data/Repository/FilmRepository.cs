using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.DomainModels;
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
            var filmDb = await _context.Film
                .Include(f => f.People)
                    .ThenInclude(p => p.Professions)
                .Include(f => f.People)
                    .ThenInclude(p => p.Person)
                        .ThenInclude(p => p.Image)
                .Include(p => p.Images)
                .Include(p => p.Genre)
                .Include(f => f.Favorites)
                .FirstOrDefaultAsync(f => f.Id == filmId);

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
