using Newtonsoft.Json;

namespace RateFilms.Common.Models.KinopoiskMovie
{
    public class GenreModel
    {
        [JsonProperty("genre")]
        public string Genre { get; set; }
    }
}
