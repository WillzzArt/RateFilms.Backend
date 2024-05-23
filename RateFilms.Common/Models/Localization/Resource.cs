using System.ComponentModel.DataAnnotations;

namespace RateFilms.Common.Models.Localization
{
    public class Resource
    {
        [Key]
        public int Id { get; set; } 
        public string Key { get; set; }
        public string Value { get; set; }
        public Culture Culture { get; set; }
    }
}
