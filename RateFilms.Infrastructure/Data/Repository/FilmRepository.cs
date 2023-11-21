using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Models;
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
        public IEnumerable<Film?> GetAllFilms()
        {
            var films = _context.Films
                .Include(g => g.Actors)
                .Include(g => g.Genre)
                .Include(img => img.Images)
                .ToList();

            

            foreach (var film in films)
            {
                var actors = new List<Actor>();

                foreach (var genre in film.Genre)
                    genre.Films = null;

                foreach(var actor in film.Actors)
                {
                    actors.Add(_context.Actors.Include(g => g.Image).Where(a => a.Id == actor.Id).Single());
                }
                film.Actors = actors;
            }

            return films;
        }
    }
}
