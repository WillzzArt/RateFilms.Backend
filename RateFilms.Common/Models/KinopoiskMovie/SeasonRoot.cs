using Newtonsoft.Json;

namespace RateFilms.Common.Models.KinopoiskMovie
{
    public class SeasonRoot
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public IEnumerable<SeasonKinopoisk> Items { get; set;}
    }

    public class SeasonKinopoisk
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("episodes")]
        public IEnumerable<Episode> Episodes { get; set; }
    }
}
