﻿using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Models.Authorization
{
    public class User: IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
