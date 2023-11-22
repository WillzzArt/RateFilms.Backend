using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;
using RateFilms.Domain.StorageModels;

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
            var filmsDb = _context.Films
                .Include(g => g.Actors)
                .Include(img => img.Images)
                .ToList();

            foreach (var film  in filmsDb)
            {
                var actors = new List<ActorDbModel>();

                foreach (var actor in film.Actors)
                {
                    actors.Add(_context.Actors.Include(g => g.Image).Where(a => a.Id == actor.Id).Single());
                }

                film.Actors = actors;
            }

            var films = FilmConvertor.FilmDbListConvertFilmDomainList(filmsDb);

            /*foreach (var film in films)
            {
                var actors = new List<Actor>();

                foreach (var genre in film.Genre)
                    genre.Films = null;

                foreach (var actor in film.Actors)
                {
                    actors.Add(_context.Actors.Include(g => g.Image).Where(a => a.Id == actor.Id).Single());
                }
                film.Actors = actors;
            }*/

            return films;
        }
    }
}
