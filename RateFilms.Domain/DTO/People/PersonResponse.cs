using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.People
{
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Image? Image { get; set; }
        public List<string> Professions { get; set; } = new List<string>();

        public PersonResponse(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Age = person.Age;
            Image = person.Image;
            Professions = person.Professions.Select(p => p.ToString()).ToList();
        }
    }
}
