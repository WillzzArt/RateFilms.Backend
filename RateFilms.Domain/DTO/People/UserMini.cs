﻿using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.People
{
    public class UserMini
    {
        public Guid Id { get; }
        public string Username {  get; }
        public Image? Image { get; }

        public UserMini(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Image = user.Image;
        }
    }
}
