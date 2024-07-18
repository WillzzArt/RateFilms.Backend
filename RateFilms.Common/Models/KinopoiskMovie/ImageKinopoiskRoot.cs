using Newtonsoft.Json;

namespace RateFilms.Common.Models.KinopoiskMovie
{
    public class ImageKinopoiskRoot
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("items")]
        public IEnumerable<ImageKinopoisk> Items { get; set; }
    }

    public class ImageKinopoisk
    {
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("previewUrl")]
        public string PreviewUrl { get; set; }

    }
}
