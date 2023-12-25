using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Authorization
{
    public class UserExtendedResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string Username { get; set; }
        public Image? Image { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public Dictionary<string, int> StatisticStatus { get; set; }

        public UserExtendedResponse()
        {
            StatisticStatus = new Dictionary<string, int>();
        }

        public UserExtendedResponse(User user, IEnumerable<Favorite> favorites)
        {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            Image = user.Image;
            Email = user.Email;
            Age = user.Age;
            Phone = user.Phone;
            StatisticStatus = Favorite.GetStatusOfPeople(favorites);
        }
    }
}
