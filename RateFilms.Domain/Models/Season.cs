using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Models
{
    public class Season : IEntity
    {
        public Guid Id { get; set; }
        public DateOnly RealeseDate { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image>? Images { get; set; }
        public float AvgRating { get; set; }
        public IEnumerable<Actor>? Actors { get; set; }

    }
}
