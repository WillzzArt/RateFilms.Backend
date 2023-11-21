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

            builder.Entity<Locale>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id,
                    e.Key
                });
            });
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Serial> Serials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; } 
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Locale> Locales { get; set; }
    }
}
