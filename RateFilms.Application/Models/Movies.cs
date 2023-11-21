using RateFilms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Application.Models
{
    public class Movies
    {
        public IEnumerable<Film>? Films { get; set; }
        public IEnumerable<Serial>? Serials { get; set; }
    }
}
