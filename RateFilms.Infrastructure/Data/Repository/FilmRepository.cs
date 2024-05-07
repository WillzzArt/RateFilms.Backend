using Microsoft.EntityFrameworkCore;
using RateFilms.Common.Helpers;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Movies;
using RateFilms.Domain.Models.Authorization;
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
                ReleaseDate = film.ReleaseDate,
                Country = film.Country
            };

            await _context.Film.AddAsync(saveFilm);

            await SavePerson(film.People, saveFilm);
            await SaveGenre(film.Genre, saveFilm);
            await SavaImage(film.Images, saveFilm);

            await _context.SaveChangesAsync();
        }

        private async Task SavePerson(IEnumerable<PersonInFilmDbModel> people, FilmDbModel saveFilm)
        {
            var professions = _context.Profession.ToList();

            foreach (var person in people)
            {
                var savePersInFilm = new PersonInFilmDbModel
                {
                    Film = saveFilm,
                    PersonId = person.PersonId
                };

                if (person.PersonId == Guid.Empty)
                {
                    var savePerson = new PersonDbModel
                    {
                        Age = person.Person.Age,
                        Image = person.Person.Image,
                        Name = person.Person.Name
                    };

                    await _context.Person.AddAsync(savePerson);
                    savePersInFilm.Person = savePerson;
                }

                await _context.PersonInFilm.AddAsync(savePersInFilm);

                savePersInFilm.Professions = professions
                        .Where(p => person.Professions.Any(prof => prof.Id == p.Id)).ToList();
            }
        }

        private async Task SavaImage(IEnumerable<ImageDbModel> images, FilmDbModel saveFilm)
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

        private async Task SaveGenre(IEnumerable<GenreDbModel> genres, FilmDbModel saveFilm)
        {
            var genries = new List<GenreDbModel>();

            foreach (var genre in genres)
            {
                var genreData = await _context.Genre.FindAsync(genre.Id);
                genries.Add(genreData!);
            }

            saveFilm.Genre = genries;
        }

        public async Task<IEnumerable<Film>> GetAllFilmsWithFavorite()
        {
            var filmsDb = await _context.Film
                .Include(f => f.Images)
                .Include(f => f.Genre)
                .Include(f => f.Favorite)
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
                .Include(f => f.Favorite)
                .FirstOrDefaultAsync(f => f.Id == filmId);

            if (filmDb != null)
            {
                return FilmConvertor.FilmDbConvertFilmDomain(filmDb, filmDb.Favorite);
            }

            return null;
        }

        public async Task SetFavoriteFilm(FavoriteMovie favoriteFilm, User user)
        {
            var favoriteFilmDb = await _context.FavoriteFilms
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.FilmId == favoriteFilm.MovieId);

            if (favoriteFilmDb == null)
            {
                var saveFavorite = new FavoriteFilmDbModel
                {
                    FilmId = favoriteFilm.MovieId,
                    UserId = user.Id,
                    Status = favoriteFilm.StatusMovie.ToEnum(StatusMovie.None),
                    IsFavorite = favoriteFilm.IsFavorite ?? false,
                    Score = favoriteFilm.Score ?? 0
                };

                await _context.FavoriteFilms.AddAsync(saveFavorite);
            }
            else
            {
                favoriteFilmDb.Status = favoriteFilm.StatusMovie != null
                    ? favoriteFilm.StatusMovie.ToEnum(StatusMovie.None)
                    : favoriteFilmDb.Status;
                favoriteFilmDb.IsFavorite = favoriteFilm.IsFavorite ?? favoriteFilmDb.IsFavorite;
                favoriteFilmDb.Score = favoriteFilm.Score ?? favoriteFilmDb.Score;

                /*if (favoriteFilmDb.Status == StatusMovie.None &&
                    favoriteFilmDb.IsFavorite == false &&
                    favoriteFilmDb.Score == 0)
                {
                    _context.Remove(favoriteFilmDb);
                }*/
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Film>> GetFilmsWithUncheckedReview()
        {
            var films = new HashSet<Film>();


            var idList = await _context.Comment
            .Where(c => c.Status == ReviewStatus.Unpublished && c.CommentInFilm != null)
            .Select(c => c.CommentInFilm!.Favorite!.FilmId)
            .ToListAsync();

            foreach (var id in idList)
            {
                var film = await _context.Film
                    .Include(f => f.Images)
                    .Include(f => f.Genre)
                    .Include(f => f.Favorite)
                    .FirstOrDefaultAsync(c => c.Id == id);

                films.Add(FilmConvertor.FilmDbConvertFilmDomain(film!, film!.Favorite));
            }

            return films;
        }
    }
}
