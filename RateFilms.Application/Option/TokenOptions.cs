namespace RateFilms.Application.Option
{
    public class TokenOptions
    {
        public const string JwtSettings = "JwtSettings";

        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set;} = string.Empty;
        public string Secret { get; set;} = string.Empty;
        public string IssuerRefresh { get; set; } = string.Empty;
        public string AudienceRefresh { get; set; } = string.Empty;
        public string SecretRefresh { get; set; } = string.Empty;
    }
}
