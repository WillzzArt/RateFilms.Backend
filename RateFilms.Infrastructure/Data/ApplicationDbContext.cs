using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RateFilms.Application.Option;
using RateFilms.Common.Models.Localization;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ConnectionOptions _connectionOptions;

        public ApplicationDbContext(IOptions<ConnectionOptions> connectionOptions)
        {
            _connectionOptions = connectionOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionOptions.WebApiDatabase);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDbModel>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
            });


            builder.Entity<TokenDbModel>(entity =>
            {
                entity.HasIndex(e => e.Token).IsUnique();
            });

            builder.Entity<GenreDbModel>(entity =>
            {
                entity.HasIndex(e => e.Genre).IsUnique();
            });

            builder.Entity<ProfessionDbModel>(entity =>
            {
                entity.HasIndex(e => e.Profession).IsUnique();
            });

            builder.Entity<FavoriteMovieDbModel>()
                .HasKey(fm => new { fm.MovieId, fm.UserId });

            builder.Entity<PersonInMovieDbModel>()
                .HasKey(pm => new { pm.MovieId, pm.PersonId });

            builder.Entity<MovieDbModel>()
                .HasMany(m => m.Genre)
                .WithMany(g => g.Movies)
                .UsingEntity(j => j.ToTable("MovieGenres"));

            builder.Entity<CommentUserDbModel>()
                .HasKey(c => new { c.UserId, c.CommentId });

            builder.Entity<AdminNoteDbModel>()
                .HasKey(a => new { a.UserId, a.ReviewId });

            builder.Entity<PersonInMovieDbModel>()
                .HasMany(pm => pm.Professions)
                .WithMany(g => g.PersonInMovies)
                .UsingEntity(j => j.ToTable("PersonInMovieProfession"));

            builder.Entity<CommentDbModel>()
                .HasOne(c => c.AdminNote)
                .WithOne(c => c.Review);

            builder.Entity<CommentDbModel>()
                .HasOne(c => c.Favorite)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FavoriteId)
                .HasPrincipalKey(f => f.Id);
        }

        public DbSet<MovieDbModel> Movie { get; set; }
        public DbSet<FilmDbModel> Film { get; set; }
        public DbSet<SerialDbModel> Serial { get; set; }
        public DbSet<UserDbModel> User { get; set; }
        public DbSet<SeasonDbModel> Season { get; set; }
        public DbSet<ImageDbModel> Image { get; set; }
        public DbSet<PersonDbModel> Person { get; set; }
        public DbSet<FavoriteMovieDbModel> FavoriteMovie { get; set; }
        public DbSet<GenreDbModel> Genre { get; set; }
        public DbSet<ProfessionDbModel> Profession { get; set; }
        public DbSet<PersonInMovieDbModel> PersonInMovie { get; set; }
        public DbSet<SeriesDbModel> Series { get; set; }
        public DbSet<CommentDbModel> Comment { get; set; }
        public DbSet<CommentUserDbModel> UserComment { get; set; }
        public DbSet<AdminNoteDbModel> AdminNote { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<Culture> Culture { get; set; }
        public DbSet<TokenDbModel> Token { get; set; }
    }
}
