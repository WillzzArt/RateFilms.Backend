using Newtonsoft.Json;
using RateFilms.Common.Helpers;
using RateFilms.Common.Models.KinopoiskMovie;
using RateFilms.Common.Models.Localization;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services.Movies
{
    public class MovieFromKinopoiskServise : IMovieFromKinopoiskServise
    {
        private string _movieUrl = "https://kinopoiskapiunofficial.tech/api/v2.2/films/";
        private string _personUrl = "https://kinopoiskapiunofficial.tech/api/v1/staff?filmId=";

        private static HttpClient _httpClient = new HttpClient();

        private readonly IMovieService _movieService;
        private readonly IMovieRepository _movieRepository;
        private readonly ILocalizationRepository _localizationRepository;

        public MovieFromKinopoiskServise(
            IMovieService movieService,
            IMovieRepository movieRepository,
            ILocalizationRepository localizationRepository)
        {
            _movieService = movieService;
            _movieRepository = movieRepository;
            _localizationRepository = localizationRepository;
        }

        public async Task CreateFilm(int filmId)
        {
            if (await _movieRepository.IsExistMovie(filmId)) throw new ArgumentException(nameof(filmId));

            var filmTask = SendRequest<MovieKinopoisk>(_movieUrl + filmId);
            var imageTask = SendRequest<ImageKinopoiskRoot>(_movieUrl + filmId + "/images?type=STILL&page=1");
            var peopleTask = SendRequest<List<PersonKinopoisk>>(_personUrl + filmId);

            await Task.WhenAll(filmTask, imageTask, peopleTask);

            var filmData = await filmTask;
            var imageData = await imageTask;
            var peopleData = await peopleTask;

            var movie = GenerateMovieModel<Film>(filmData, peopleData);

            if (movie is Film film)
            {
                film.Duration = filmData.FilmLength;
                film.Images = imageData.Items.Select(img => new Image
                {
                    isPreview = false,
                    Name = filmData.NameOriginal.Replace(" ", "") + "Img",
                    Url = img.ImageUrl,
                });

                await SaveResource(filmData, peopleData);

                await _movieService.CreateMovieAsync(film);
            }
        }

        public async Task CreateSerial(int serialId)
        {
            if (await _movieRepository.IsExistMovie(serialId)) throw new ArgumentException(nameof(serialId));

            var serialTask = SendRequest<MovieKinopoisk>(_movieUrl + serialId);
            var peopleTask = SendRequest<List<PersonKinopoisk>>(_personUrl + serialId);
            var seasonTask = SendRequest<SeasonRoot>(_movieUrl + serialId + "/seasons");

            await Task.WhenAll(serialTask, peopleTask, seasonTask);

            var serialData = await serialTask;
            var peopleData = await peopleTask;
            var seasonData = await seasonTask;

            var movie = GenerateMovieModel<Serial>(serialData, peopleData);

            if (movie is Serial serial)
            {
                serial.ReleaseDate = ConvertData(seasonData.Items.First().Episodes.First().ReleaseDate);

                serial.Seasons = seasonData.Items.Where(s => s.Episodes.Count() > 1).Select(s => new Season
                {
                    Description = serialData.NameOriginal.Replace(" ", "") + "Season" + s.Number + "DescriprionKey",

                    CountMaxSeries = s.Episodes.Count(),
                    RealeseDate = ConvertData(s.Episodes.First().ReleaseDate),

                    Series = s.Episodes.Select(ser => new Series
                    {
                        Duration = serialData.FilmLength,
                        RealeseDate = ConvertData(ser.ReleaseDate),
                        Name = string.IsNullOrWhiteSpace(ser.NameEn)
                            ? serialData.NameOriginal.Replace(" ", "") + "Season" + s.Number + "Seria" + ser.NameRu.Replace(" ", "") + "Key"
                            : serialData.NameOriginal.Replace(" ", "") + "Season" + s.Number + "Seria" + ser.NameEn.Replace(" ", "") + "Key",
                        PreviewImage = new Image
                        {
                            isPreview = true,
                            Url = "string",
                            Name = string.IsNullOrWhiteSpace(ser.NameEn)
                            ? ser.NameRu.Replace(" ", "") + "SeriaImg"
                            : ser.NameEn.Replace(" ", "") + "SeriaImg",
                        }
                    }),

                    Images = new List<Image>()
                    {
                        new Image()
                        {
                            isPreview = false,
                            Name = serialData.NameOriginal.Replace(" ", "") + "Season" + s.Number + "Img1",
                            Url = "string"
                        },
                        new Image()
                        {
                            isPreview = false,
                            Name = serialData.NameOriginal.Replace(" ", "") + "Season" + s.Number + "Img2",
                            Url = "string"
                        }
                    }
                });

                await SaveResource(serialData, peopleData, seasonData.Items.Where(s => s.Episodes.Count() > 1));

                await _movieService.CreateMovieAsync(serial);

                DateTimeOffset ConvertData(string Data) =>
                    DateTimeOffset.ParseExact(Data, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).UtcDateTime;

            }
        }

        private Movie GenerateMovieModel<T>(MovieKinopoisk movieData, IEnumerable<PersonKinopoisk> person) where T : Movie, new()
        {
            var name = movieData.NameOriginal.Replace(" ", "");
            var releaseDate = new DateTimeOffset(
                new DateTime(movieData.Year, new Random().Next(1, 13), new Random().Next(1, 30)));
            var countries = string.Join(", ", movieData.Countries.Select(c => c.Country).ToList());

            var people = person.GroupBy(p => p.StaffId)
                .Select(p => new Person
                {
                    KinopoiskId = p.Key,
                    Name = string.IsNullOrWhiteSpace(p.First().NameEn)
                        ? p.First().NameRu.Replace(" ", "") + p.Key + "Key"
                        : p.First().NameEn.Replace(" ", "") + p.Key + "Key",
                    Image = new Image
                    {
                        isPreview = false,
                        Name = string.IsNullOrWhiteSpace(p.First().NameEn)
                            ? p.First().NameRu.Replace(" ", "") + p.Key + "Img"
                            : p.First().NameEn.Replace(" ", "") + p.Key + "Img",
                        Url = p.First().PosterUrl,
                    },
                    Professions = p.Select(pr => pr.ProfessionKey.ToEnum(Profession.None))
                });

            Movie movie = new T
            {
                KinopoiskId = movieData.KinopoiskId,
                Name = name + "NameKey",
                Description = name + "DescriptionKey",
                AgeRating = int.Parse(movieData.RatingAgeLimits?.Replace("age", "") ?? "0"),
                ReleaseDate = releaseDate.UtcDateTime,
                Country = name + "CountryKey",
                type = MovieType.Film,
                PreviewImage = new Image
                {
                    isPreview = true,
                    Url = movieData.PosterUrlPreview,
                    Name = name + "Preview"
                },
                People = people,
                Genre = movieData.Genres.Select(g => GenreConvertor.ConvertGenreKinopoiskToGenre(g.Genre.ToEnum(GenreKinopoisk.None)))
            };

            return movie;
        }

        private async Task SaveResource(MovieKinopoisk movie, IEnumerable<PersonKinopoisk> people, IEnumerable<SeasonKinopoisk>? seasons = null)
        {
            var name = movie.NameOriginal.Replace(" ", "");
            var countries = string.Join(", ", movie.Countries.Select(c => c.Country).ToList());

            await CreateResourse(name + "NameKey", movie.NameRu, "ru-RU");
            await CreateResourse(name + "NameKey", movie.NameEn ?? movie.NameOriginal, "en-GB");
            await CreateResourse(name + "DescriptionKey", movie.DescriptionRu, "ru-RU");
            await CreateResourse(name + "DescriptionKey", movie.DescriptionEn, "en-GB");
            await CreateResourse(name + "CountryKey", countries, "ru-RU");
            await CreateResourse(name + "CountryKey", "string", "en-GB");

            foreach (var person in people.GroupBy(p => p.StaffId))
            {
                if (!await _movieRepository.IsExistPerson(person.Key))
                {
                    var key = string.IsNullOrWhiteSpace(person.First().NameEn)
                        ? person.First().NameRu.Replace(" ", "") + person.Key + "Key"
                        : person.First().NameEn.Replace(" ", "") + person.Key + "Key";

                    await CreateResourse(key, person.First().NameRu, "ru-RU");
                    await CreateResourse(key, person.First().NameEn, "en-GB");
                }
            }

            if (seasons != null)
            {
                foreach (var season in seasons)
                {
                    var keyDiscription = name + "Season" + season.Number + "DescriprionKey";

                    await CreateResourse(keyDiscription, season.Episodes.First().Synopsis, "ru-RU");
                    await CreateResourse(keyDiscription, "string", "en-GB");

                    foreach (var series in season.Episodes)
                    {
                        var key = string.IsNullOrWhiteSpace(series.NameEn)
                            ? name + "Season" + season.Number + "Seria" + series.NameRu.Replace(" ", "") + "Key"
                            : name + "Season" + season.Number + "Seria" + series.NameEn.Replace(" ", "") + "Key";

                        await CreateResourse(key, series.NameRu, "ru-RU");
                        await CreateResourse(key, series.NameEn, "en-GB");
                    }
                }
            }

            async Task CreateResourse(string key, string value, string culture)
            {
                if (value == null) value = "string";

                var resourceRu = new Resource
                {
                    Key = key,
                    Value = value
                };

                await _localizationRepository.CreateResource(resourceRu, culture);
            }

        }

        private static async Task<T> SendRequest<T>(string url)
        {

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("X-API-KEY", "eb61cfe3-b95d-42d6-9bc0-523a9e33581c");

            using HttpResponseMessage response = await _httpClient.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<T>(content);

            if (res == null) throw new ArgumentNullException();

            return res;
        }
    }
}
