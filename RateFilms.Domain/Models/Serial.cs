using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Models
{
    public class Serial: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; }
        public string PreviewImage { get; set; }
        public float AvgRating { get; set; }
        public int AgeRating { get; set; }
        public int SeriesCount { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
    }
}
