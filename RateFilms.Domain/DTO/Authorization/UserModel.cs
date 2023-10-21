using RateFilms.Domain.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.DTO.Authorization
{
    public class UserModel
    {
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
