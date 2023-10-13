using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Models
{
    public class Genre: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Film>? Films { get; set; }
    }
}
