namespace RateFilms.Domain.DTO.Authorization
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }

        public TokenModel((string accessToken, string refreshToken) token)
        {
            AccessToken = token.accessToken;
            RefreshToken = token.refreshToken;
            ExpiresIn = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeMilliseconds();
        }
    }
}
