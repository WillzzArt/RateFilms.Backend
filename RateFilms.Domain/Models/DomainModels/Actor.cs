namespace RateFilms.Domain.Models.DomainModels
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Image? Image { get; set; }
    }
}
