using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Authorization
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public Image? Image { get; set; }
        public string Token { get; set; }

        public LoginResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            UserName = user.UserName;
            Email = user.Email;
            Age = user.Age;
            Phone = user.Phone;
            Image = user.Image;
            Token = token;
        }
    }
}
