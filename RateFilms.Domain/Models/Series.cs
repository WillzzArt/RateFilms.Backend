using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Models
{
    public class Series: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
    }
}
