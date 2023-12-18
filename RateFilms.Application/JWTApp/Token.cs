using Microsoft.IdentityModel.Tokens;
using RateFilms.Application.Option;
using RateFilms.Domain.Models.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RateFilms.Application.JWTApp
{
    internal static class Token
    {
        public static string CreateToken(TokenOptions tokenOption, User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(tokenOption.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: tokenOption.Issuer,
                audience: tokenOption.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
