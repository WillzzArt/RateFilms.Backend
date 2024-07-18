using Newtonsoft.Json;

namespace RateFilms.Common.Models.KinopoiskMovie
{
    public class MovieKinopoisk
    {
        [JsonProperty("kinopoiskId")]
        public int KinopoiskId { get; set; }
        [JsonProperty("nameRu")]
        public string NameRu { get; set; }

        [JsonProperty("nameEn")]
        public string? NameEn { get; set;}

        [JsonProperty("nameOriginal")]
        public string NameOriginal { get; set;}

        [JsonProperty("posterUrlPreview")]
        public string PosterUrlPreview { get; set;}

        [JsonProperty("year")]
        public int Year { get; set;}

        [JsonProperty("filmLength")]
        public int FilmLength { get; set; }

        [JsonProperty("description")]
        public string DescriptionRu { get; set; }
        public string DescriptionEn { get; set; } = "string";

        [JsonProperty("ratingAgeLimits")]
        public string RatingAgeLimits { get; set; }

        [JsonProperty("countries")]
        public IEnumerable<CountryModel> Countries { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<GenreModel> Genres { get; set; }
    }
}
