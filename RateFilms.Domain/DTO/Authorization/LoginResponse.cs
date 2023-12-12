﻿using RateFilms.Domain.Models.Authorization;

namespace RateFilms.Domain.DTO.Authorization
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public LoginResponse(User user, string token)
        {
            Id = user.Id;
            UserName = user.UserName;
            Token = token;
            Role = user.Role.ToString();
        }
    }
}
