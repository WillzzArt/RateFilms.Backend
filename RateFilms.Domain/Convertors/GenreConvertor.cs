using RateFilms.Common.Models.KinopoiskMovie;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.Convertors
{
    public static class GenreConvertor
    {
        private static Dictionary<GenreKinopoisk, Genre> genreMap = new Dictionary<GenreKinopoisk, Genre>
        {
            { GenreKinopoisk.боевик, Genre.Action },
            { GenreKinopoisk.фэнтези, Genre.Fantasy },
            { GenreKinopoisk.ужасы, Genre.Horror },
            { GenreKinopoisk.приключения, Genre.Adventure },
            { GenreKinopoisk.мультфильм, Genre.Animated },
            { GenreKinopoisk.комедия, Genre.Comedy },
            { GenreKinopoisk.драма, Genre.Drama },
            { GenreKinopoisk.триллер, Genre.Thriller },
            { GenreKinopoisk.история, Genre.Historical },
            { GenreKinopoisk.мелодрама, Genre.Romance },
            { GenreKinopoisk.криминал, Genre.Crime },
            { GenreKinopoisk.аниме, Genre.Anime },
            { GenreKinopoisk.биография, Genre.Biography },
            { GenreKinopoisk.вестерн, Genre.Western },
            { GenreKinopoisk.военный, Genre.Military },
            { GenreKinopoisk.детектив, Genre.Detective },
            { GenreKinopoisk.детский, Genre.Children },
            { GenreKinopoisk.документальный, Genre.Documentary },
            { GenreKinopoisk.игра, Genre.Game },
            { GenreKinopoisk.концерт, Genre.Concert },
            { GenreKinopoisk.короткометражка, Genre.ShortFilm },
            { GenreKinopoisk.музыка, Genre.Music },
            { GenreKinopoisk.мюзикл, Genre.Musical },
            { GenreKinopoisk.новости, Genre.News },
            { GenreKinopoisk.семейный, Genre.Family },
            { GenreKinopoisk.спорт, Genre.Sports },
            { GenreKinopoisk.фантастика, Genre.Fantastic },
            { GenreKinopoisk.церемония, Genre.Ceremony }
        };

        public static Genre ConvertGenreKinopoiskToGenre(GenreKinopoisk genreKinopoisk)
        {
            if (genreMap.ContainsKey(genreKinopoisk))
            {
                return genreMap[genreKinopoisk];
            }
            return Genre.None;
        }
    }
}
