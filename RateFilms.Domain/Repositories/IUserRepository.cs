using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindUser(string userLogin);
    }
}
