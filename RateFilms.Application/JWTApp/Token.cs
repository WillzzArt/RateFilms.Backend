using Microsoft.IdentityModel.Tokens;
using RateFilms.Application.Option;
using RateFilms.Domain.Models.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RateFilms.Application.JWTApp
{
    internal static class Token
    {
        public static (string accessToken, string refreshToken) CreateToken(TokenOptions tokenOption, User user)
        {
            var accessClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(tokenOption.Secret));

            var accessCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var accessToken = new JwtSecurityToken(
                issuer: tokenOption.Issuer,
                audience: tokenOption.Audience,
                claims: accessClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: accessCreds);

            var jwtAccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);

            var refreshClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("isRefreshToken", "true")
            };

            var keyRefresh = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(tokenOption.SecretRefresh));

            var refreshCreds = new SigningCredentials(keyRefresh, SecurityAlgorithms.HmacSha512Signature);

            var refreshToket = new JwtSecurityToken(
                issuer: tokenOption.IssuerRefresh,
                audience: tokenOption.AudienceRefresh,
                claims: refreshClaims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: refreshCreds);

            var jwtRefreshToket = new JwtSecurityTokenHandler().WriteToken(refreshToket);

            return (jwtAccessToken, jwtRefreshToket);
        }
    }
}
