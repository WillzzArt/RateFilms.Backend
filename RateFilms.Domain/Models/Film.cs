using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Models
{
    public class Film: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Genre>? Genre { get; set; }
        public int? Duration { get; set; }
        public string? PreviewImage { get; set; }
        public float? avgRating { get; set; }
        public int ageRating { get; set; }
    }
}
