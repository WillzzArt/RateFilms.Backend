using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;
using System;

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
                AvgRating = film.AvgRating
            };

            await _context.Film.AddAsync(saveFilm);

            if (film.People != null)
            {
                var professions = new List<ProfessionDbModel>();

                professions.AddRange(_context.Profession);

                foreach (var person in film.People)
                {
                    if (person.PersonId != Guid.Empty)
                    {
                        var savePersInFilm = new PersonInFilmDbModel
                        {
                            Film = saveFilm,
                            PersonId = person.PersonId
                        };
                        await _context.PersonInFilm.AddAsync(savePersInFilm);

                        savePersInFilm.Professions = professions.Where(p => person.Professions.Any(prof => prof.Id == p.Id)).ToList();
                    }
                    else
                    {
                        if (person.Person.Image != null && person.Person.Image.Id == Guid.Empty)
                        {
                            var savePerson = new PersonDbModel
                            {
                                Age = person.Person.Age,
                                Image = person.Person.Image,
                                Name = person.Person.Name
                            };

                            _context.Person.Add(savePerson);

                            var savePersInFilm = new PersonInFilmDbModel
                            {
                                Person = savePerson,
                                Film = saveFilm,
                            };
                            await _context.PersonInFilm.AddAsync(savePersInFilm);
                            /*p => p.Id == person.Professions.FirstOrDefault(prof => prof.Id == p.Id)?.Id*/
                            savePersInFilm.Professions = professions.Where(p => person.Professions.Any(prof => prof.Id == p.Id)).ToList();
                        }
                        
                    }
                }
            }

            if (film.Images != null)
            {
                foreach (var image in film.Images)
                {
                    if (image.Id == Guid.Empty)
                    {
                        image.Film = saveFilm;
                        _context.Image.Add(image);
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

        public IEnumerable<Film?> GetAllFilms()
        {
            var filmsDb = _context.Film
                .Include(p => p.People)
                .Include(img => img.Images)
                .Include(g => g.Genre)
                .ToList();

            foreach (var film in filmsDb)
            {
                var actors = new List<PersonInFilmDbModel>();

                foreach (var actor in film.People)
                {
                    var person = _context.PersonInFilm
                        .Include(p => p.Professions)
                        .Where(a => a.PersonId == actor.PersonId && a.FilmId == film.Id)
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
