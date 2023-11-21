using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Infrastructure.StorageModels
{
    public class UserDbModel: IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
