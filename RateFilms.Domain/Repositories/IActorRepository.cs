using RateFilms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Repositories
{
    public interface IActorRepository
    {
        IEnumerable<Film?> FindActorByFilmId();
    }
}
