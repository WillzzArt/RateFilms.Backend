using System.ComponentModel.DataAnnotations;

namespace RateFilms.Common.Models.Localization
{
    public class Culture
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
    }
}
