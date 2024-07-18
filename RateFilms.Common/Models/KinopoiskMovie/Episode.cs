using Newtonsoft.Json;

namespace RateFilms.Common.Models.KinopoiskMovie
{
    public class Episode
    {
        [JsonProperty("episodeNumber")]
        public int EpisodeNumber { get; set; }
        [JsonProperty("nameRu")]
        public string NameRu { get; set; }

        [JsonProperty("nameEn")]
        public string NameEn { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }
    }
}
