using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class TokenDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTimeOffset DateOfEntry { get; set; }
    }
}
