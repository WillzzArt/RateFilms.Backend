using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.Models.Authorization
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Image? Image { get; set; }
    }
}
