using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.StorageModels;

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
                Actors = ActorConvertor.ActorDbListConvertActorDomainList(filmDbModel.Actors),
                AgeRating = filmDbModel.AgeRating,
                AvgRating = filmDbModel.AvgRating,
                Duration = filmDbModel.Duration,
                Author = filmDbModel.Autor,
                Genre = filmDbModel.Genre,
                Images = ActorConvertor.ImageDbListConvertImageDomainList(filmDbModel.Images),
                PreviewImage = filmDbModel.PreviewImage,

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
                    Actors = ActorConvertor.ActorDbListConvertActorDomainList(f.Actors ?? new List<ActorDbModel>()),
                    AgeRating = f.AgeRating,
                    AvgRating = f.AvgRating,
                    Duration = f.Duration,
                    Author = f.Autor,
                    Genre = f.Genre,
                    Images = ActorConvertor.ImageDbListConvertImageDomainList(f.Images),
                    PreviewImage = f.PreviewImage,
                }).ToList();

            return films;
        }

        public static FilmDbModel FilmDomainConvertFilmDb(Film film)
        {
            if (film == null) throw new ArgumentNullException(nameof(film));

            var filmDb = new FilmDbModel
            {
                Id = film.Id,
                Name = film.Name,
                Description = film.Description,
                Actors = ActorConvertor.ActorDomainListConvertActorDomainList(film.Actors ?? new List<Actor>()),
                AgeRating = film.AgeRating,
                AvgRating = film.AvgRating,
                Duration = film.Duration,
                Autor = film.Author,
                Genre = film.Genre,
                Images = ActorConvertor.ImageDomainListConvertImageDbList(film.Images ?? new List<Image>()),
                PreviewImage = film.PreviewImage,
            };

            return filmDb;
        }
    }
}
