using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.DTO.Authorization
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public TokenModel Token { get; set; }
        public string Role { get; set; }

        public LoginResponse(User user, (string accessToken, string refreshToken) token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = new TokenModel(token);
            Role = user.Role.ToString();
        }
    }
}
