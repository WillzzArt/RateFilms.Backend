using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class FilmConvertor
    {
        public static Film FilmDbConvertFilmDomain(
            FilmDbModel filmDbModel, 
            IEnumerable<FavoriteFilmDbModel>? favoriteFilm = null)
        {
            if (filmDbModel == null) throw new ArgumentNullException(nameof(filmDbModel));

            var previewImage = new ImageDbModel();
            var images = new List<ImageDbModel>();

            if (filmDbModel.Images != null)
            {
                previewImage = filmDbModel.Images.FirstOrDefault(img => img.isPreview == true);
                images = filmDbModel.Images.Where(img => img.isPreview == false).ToList();
            }
             

            var film = new Film
            {
                Id = filmDbModel.Id,
                Name = filmDbModel.Name,
                Description = filmDbModel.Description,
                People = PersonConvertor.PersonInMovieDbListConvertPersonDomainList(filmDbModel.People ?? new List<PersonInFilmDbModel>()),
                AgeRating = filmDbModel.AgeRating,
                Duration = filmDbModel.Duration,
                Country = filmDbModel.Country,
                RealeseDate = filmDbModel.ReleaseDate,
                PreviewImage = PersonConvertor.ImageDbConvertImageDomain(previewImage)!,
                Genre = filmDbModel.Genre.Select(g => g.Genre.ToEnum(Genre.None)),
                Images = PersonConvertor.ImageDbListConvertImageDomainList(images)
            };

            if (favoriteFilm != null)
            {
                film.Favorites = favoriteFilm.Select(fFilms => new Favorite
                {
                    Id = fFilms.FavoriteId,
                    User = UserConvertor.UserDbConvertUserDomain(fFilms.User ?? new UserDbModel()),
                    IsFavorite = fFilms.IsFavorite,
                    Score = fFilms.Score,
                    //Comments =
                    Status = Enum.IsDefined(typeof(StatusMovie), fFilms.Status)
                        ? fFilms.Status
                        : StatusMovie.None

                });
            }

            return film;
        }

        public static IEnumerable<Film> FilmDbListConvertFilmDomainList(IEnumerable<FilmDbModel> filmDbModels)
        {
            if (filmDbModels == null) throw new ArgumentNullException(nameof(filmDbModels));

            var films = filmDbModels
                .Select(fDB => FilmDbConvertFilmDomain(fDB, fDB.Favorite)).ToList();

            return films;
        }

        public static FilmDbModel FilmDomainConvertFilmDb(Film film)
        {
            if (film == null) throw new ArgumentNullException(nameof(film));
            
            if (film.Images == null)
            {
                film.Images = new List<Image>();
            }

            var filmDb = new FilmDbModel
            {
                Id = film.Id,
                Name = film.Name,
                Description = film.Description,
                People = PersonConvertor.PersonDomainListConvertPersonInFilmDbList(film.People ?? new List<Person>(), film.Id),
                AgeRating = film.AgeRating,
                Duration = film.Duration,
                ReleaseDate = film.RealeseDate,
                Country = film.Country,
                Genre = film.Genre
                .Select(g => new GenreDbModel
                {
                    Id = (int)g,
                    Genre = g.ToString()
                }),
                Images = PersonConvertor.ImageDomainListConvertImageDbList(film.Images.Append(film.PreviewImage))
            };

            return filmDb;
        }
    }
}
