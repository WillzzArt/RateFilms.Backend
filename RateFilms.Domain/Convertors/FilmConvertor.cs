using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class FilmConvertor
    {
        public static Film FilmDbConvertFilmDomain(FilmDbModel filmDbModel)
        {
            if (filmDbModel == null) throw new ArgumentNullException(nameof(filmDbModel));

            var film = new Film
            {
                Id = filmDbModel.Id,
                Name = filmDbModel.Name,
                Description = filmDbModel.Description,
                People = PersonConvertor.PersonInFilmDbListConvertPersonDomainList(filmDbModel.People ?? new List<PersonInFilmDbModel>()),
                AgeRating = filmDbModel.AgeRating,
                AvgRating = filmDbModel.AvgRating,
                Duration = filmDbModel.Duration,
                Genre = filmDbModel.Genre.Select(g => g.Genre.ToEnum(Genre.None)),
                Images = PersonConvertor.ImageDbListConvertImageDomainList(filmDbModel.Images)
            };

            return film;
        }

        public static IEnumerable<Film> FilmDbListConvertFilmDomainList(IEnumerable<FilmDbModel> filmDbModels)
        {
            if (filmDbModels == null) throw new ArgumentNullException(nameof(filmDbModels));

            var films = filmDbModels
                .Select(f => new Film
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    People = PersonConvertor.PersonInFilmDbListConvertPersonDomainList(f.People ?? new List<PersonInFilmDbModel>()),
                    AgeRating = f.AgeRating,
                    AvgRating = f.AvgRating,
                    Duration = f.Duration,
                    Genre = f.Genre.Select(g => g.Genre.ToEnum(Genre.None)),
                    Images = PersonConvertor.ImageDbListConvertImageDomainList(f.Images)
                }).ToList();

            return films;
        }

        public static FilmDbModel FilmDomainConvertFilmDb(Film film)
        {
            if (film == null) throw new ArgumentNullException(nameof(film));

            film.Images.ToList().Add(film.PreviewImage);

            var filmDb = new FilmDbModel
            {
                Id = film.Id,
                Name = film.Name,
                Description = film.Description,
                People = PersonConvertor.PersonDomainListConvertPersonInFilmDbList(film.People ?? new List<Person>(), film.Id),
                AgeRating = film.AgeRating,
                AvgRating = film.AvgRating,
                Duration = film.Duration,
                Genre = film.Genre
                .Select(g => new GenreDbModel
                {
                    Id = (int)g,
                    Genre = g.ToString()
                }),
                Images = PersonConvertor.ImageDomainListConvertImageDbList(film.Images ?? new List<Image>())
            };

            return filmDb;
        }
    }
}
