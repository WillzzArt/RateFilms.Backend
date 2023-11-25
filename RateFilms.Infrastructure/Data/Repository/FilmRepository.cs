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

        //TODO
        public async Task CreateAsync(FilmDbModel film)
        {
            if (film == null)
            {
                throw new ArgumentNullException(nameof(film));
            }

            await _context.SaveChangesAsync();
        }

        public IEnumerable<Film?> GetAllFilms()
        {
            var filmsDb = _context.Film
                .Include(g => g.People)
                .Include(img => img.Images)
                .ToList();

            foreach (var film in filmsDb)
            {
                var actors = new List<PersonInFilmDbModel>();

                foreach (var actor in film.People)
                {
                    var person = _context.PersonInFilm
                        .Include(p => p.Professions)
                        .Where(a => a.PersonId == actor.PersonId)
                        .Single();

                    person.Person = _context.Person
                        .Include(p => p.Image)
                        .Where(p => p.Id == actor.PersonId)
                        .Single();

                    actors.Add(person);
                }

                film.People = actors;
            }

            var films = FilmConvertor.FilmDbListConvertFilmDomainList(filmsDb);

            return films;
        }
    }
}
