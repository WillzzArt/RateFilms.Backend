namespace RateFilms.Domain.Models.DomainModels
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Image? Image { get; set; }
        public IEnumerable<Profession> Professions { get; set; }
    }
}
