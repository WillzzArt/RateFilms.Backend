using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Models
{
    public class Image : IEntity
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
    }
}
