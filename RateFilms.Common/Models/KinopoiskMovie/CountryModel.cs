using Newtonsoft.Json;

namespace RateFilms.Common.Models.KinopoiskMovie
{
    public class CountryModel
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
