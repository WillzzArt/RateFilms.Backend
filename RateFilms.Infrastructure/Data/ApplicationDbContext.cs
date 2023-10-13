using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using RateFilms.Domain.Models;
using RateFilms.Domain.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        protected readonly IConfiguration configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseNpgsql("Host=localhost; Database=rateFilms; Username=postgres; Password=root");
        }

        DbSet<Film> Films { get; set; }
        DbSet<Serial> Serials { get; set; }
        DbSet<Series> Series { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Genre> Genres { get; set; } 
    }
}
