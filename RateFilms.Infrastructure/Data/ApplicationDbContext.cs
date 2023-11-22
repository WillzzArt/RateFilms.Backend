using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RateFilms.Domain.StorageModels;

namespace RateFilms.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=rateFilms; Username=postgres; Password=root");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDbModel>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
            });
        }

        public DbSet<FilmDbModel> Films { get; set; }
        public DbSet<SerialDbModel> Serials { get; set; }
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<SeasonDbModel> Seasons { get; set; }
        public DbSet<ImageDbModel> Images { get; set; }
        public DbSet<ActorDbModel> Actors { get; set; }
    }
}
