using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class UserDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Guid? ImageId { get; set; }
        public ImageDbModel? Image { get; set; }
        public IEnumerable<FavoriteFilmDbModel>? FavoriteFilms { get; set; }
        public IEnumerable<FavoriteSerialDbModel>? FavoriteSerials { get; set; }
        public IEnumerable<CommentDbModel> Comments { get; set; }
    }
}
