using Microsoft.EntityFrameworkCore;
using RateFilms.Common.Helpers;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(MovieDbModel movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            switch (movie)
            {
                case FilmDbModel film:
                    {
                        var saveFilm = new FilmDbModel
                        {
                            KinopoiskId = film.KinopoiskId,
                            Name = film.Name,
                            Description = film.Description,
                            Duration = film.Duration,
                            AgeRating = film.AgeRating,
                            ReleaseDate = film.ReleaseDate,
                            Country = film.Country,
                            Type = MovieType.Film
                        };

                        await _context.Film.AddAsync(saveFilm);

                        await SavePerson(film.People, saveFilm);
                        await SaveGenre(film.Genre, saveFilm);
                        await SaveImage(film.Images, saveFilm);

                        await _context.SaveChangesAsync();
                        break;
                    }
                case SerialDbModel serial:
                    {
                        var saveSerial = new SerialDbModel
                        {
                            KinopoiskId = serial.KinopoiskId,
                            Name = serial.Name,
                            Description = serial.Description,
                            AgeRating = serial.AgeRating,
                            ReleaseDate = serial.ReleaseDate,
                            PreviewImage = serial.PreviewImage,
                            Country = serial.Country,
                            Type = MovieType.Serial
                        };

                        await _context.Serial.AddAsync(saveSerial);

                        await SavePerson(serial.People, saveSerial);
                        await SaveSeason(serial.Seasons, saveSerial);
                        await SaveGenre(serial.Genre, saveSerial);

                        await _context.SaveChangesAsync();
                        break;
                    }
            }
        }

        public async Task SetFavoriteMovie(FavoriteMovie favoriteMovie, User user)
        {
            var favoriteMovieDb = await _context.FavoriteMovie
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.MovieId == favoriteMovie.MovieId);

            if (favoriteMovieDb == null)
            {
                var saveFavorite = new FavoriteMovieDbModel
                {
                    Id = Guid.NewGuid(),
                    MovieId = favoriteMovie.MovieId,
                    UserId = user.Id,
                    Status = favoriteMovie.StatusMovie.ToEnum(StatusMovie.None),
                    IsFavorite = favoriteMovie.IsFavorite ?? false,
                    Score = favoriteMovie.Score ?? 0
                };

                await _context.FavoriteMovie.AddAsync(saveFavorite);
            }
            else
            {
                favoriteMovieDb.Status = favoriteMovie.StatusMovie != null
                    ? favoriteMovie.StatusMovie.ToEnum(StatusMovie.None)
                    : favoriteMovieDb.Status;
                favoriteMovieDb.IsFavorite = favoriteMovie.IsFavorite ?? favoriteMovieDb.IsFavorite;
                favoriteMovieDb.Score = favoriteMovie.Score ?? favoriteMovieDb.Score;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistMovie(int kinopoiskId)
        {
            return await _context.Movie.AnyAsync(x => x.KinopoiskId == kinopoiskId);
        }

        public async Task<bool> IsExistPerson(int kinopoiskId)
        {
            return await _context.Person.AnyAsync(x => x.KinopoiskId == kinopoiskId);
        }

        private async Task SavePerson(IEnumerable<PersonInMovieDbModel> people, MovieDbModel saveFilm)
        {
            var professions = _context.Profession.ToList();

            foreach (var person in people)
            {
                var personDb = await _context.Person.FirstOrDefaultAsync(p => p.KinopoiskId == person.Person.KinopoiskId);

                var savePersInMovie = new PersonInMovieDbModel
                {
                    Movie = saveFilm,
                    PersonId = person.PersonId
                };

                if (personDb == null)
                {
                    if (person.PersonId == Guid.Empty)
                    {
                        var savePerson = new PersonDbModel
                        {
                            KinopoiskId = person.Person.KinopoiskId,
                            Age = person.Person.Age,
                            Image = person.Person.Image,
                            Name = person.Person.Name
                        };

                        await _context.Person.AddAsync(savePerson);

                        savePersInMovie.PersonId = savePerson.Id;
                        savePersInMovie.Person = savePerson;
                    }
                }
                else
                {
                    savePersInMovie.PersonId = personDb.Id;
                }

                await _context.PersonInMovie.AddAsync(savePersInMovie);

                savePersInMovie.Professions = professions
                       .Where(p => person.Professions.Any(prof => prof.Id == p.Id)).ToList();
            }
        }

        private async Task SaveImage(IEnumerable<ImageDbModel> images, FilmDbModel saveFilm)
        {
            foreach (var image in images)
            {
                if (image.Id == Guid.Empty)
                {
                    image.Film = saveFilm;
                    await _context.Image.AddAsync(image);
                }
            }
        }

        private async Task SaveGenre(IEnumerable<GenreDbModel> genres, MovieDbModel saveFilm)
        {
            var genries = new List<GenreDbModel>();

            foreach (var genre in genres)
            {
                var genreData = await _context.Genre.FindAsync(genre.Id);
                genries.Add(genreData!);
            }

            saveFilm.Genre = genries;
        }

        private async Task SaveSeason(IEnumerable<SeasonDbModel> seasons, SerialDbModel saveSerial)
        {
            foreach (var season in seasons)
            {
                var saveSeason = new SeasonDbModel
                {
                    Description = season.Description,
                    Serial = saveSerial,
                    RealeseDate = season.RealeseDate
                };

                await _context.Season.AddAsync(saveSeason);

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
}
