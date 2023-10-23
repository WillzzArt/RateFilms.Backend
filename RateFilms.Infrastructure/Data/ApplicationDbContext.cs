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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
            });
        }

        DbSet<Film> Films { get; set; }
        DbSet<Serial> Serials { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Genre> Genres { get; set; } 
        DbSet<Season> Seasons { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Actor> Actors { get; set; }
    }
}
