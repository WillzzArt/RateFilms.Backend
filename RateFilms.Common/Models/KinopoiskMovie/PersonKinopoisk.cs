using Newtonsoft.Json;

namespace RateFilms.Common.Models.KinopoiskMovie
{
    public class PersonRoot
    {
        public IEnumerable<PersonKinopoisk> Items { get; set; }
    }
    public class PersonKinopoisk
    {
        [JsonProperty("staffId")]
        public int StaffId { get; set; }

        [JsonProperty("nameRu")]
        public string NameRu { get; set; }

        [JsonProperty("nameEn")]
        public string NameEn { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }     

        [JsonProperty("posterUrl")]
        public string PosterUrl { get; set; }

        [JsonProperty("professionText")]
        public string ProfessionText { get; set; }

        [JsonProperty("professionKey")]
        public string ProfessionKey { get; set; }
    }
}
