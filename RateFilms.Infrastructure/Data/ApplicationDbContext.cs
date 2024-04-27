using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RateFilms.Domain.Models.StorageModels;

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

            builder.Entity<GenreDbModel>(entity =>
            {
                entity.HasIndex(e => e.Genre).IsUnique();
            });

            builder.Entity<ProfessionDbModel>(entity =>
            {
                entity.HasIndex(e => e.Profession).IsUnique();
            });

            builder.Entity<FavoriteFilmDbModel>()
                .HasKey(ff => new { ff.FilmId, ff.UserId });

            builder.Entity<FavoriteSerialDbModel>()
                .HasKey(fs => new { fs.SerialId, fs.UserId });

            builder.Entity<PersonInFilmDbModel>()
                .HasKey(pf => new { pf.FilmId, pf.PersonId });

            builder.Entity<PersonInSerialDbModel>()
                .HasKey(ps => new { ps.SerialId, ps.PersonId });

            builder.Entity<FilmDbModel>()
                .HasMany(f => f.Genre)
                .WithMany(g => g.Films)
                .UsingEntity(j => j.ToTable("FilmGenries"));

            builder.Entity<SerialDbModel>()
                .HasMany(f => f.Genre)
                .WithMany(g => g.Serials)
                .UsingEntity(j => j.ToTable("SerialGenries"));

            builder.Entity<CommentUserDbModel>()
                .HasKey(c => new { c.UserId, c.CommentId });

            builder.Entity<AdminNoteDbModel>()
                .HasKey(a => new { a.UserId, a.ReviewId });

            builder.Entity<PersonInFilmDbModel>()
                .HasMany(f => f.Professions)
                .WithMany(g => g.PersonInFilms)
                .UsingEntity(j => j.ToTable("PersonInFilmProfession"));

            builder.Entity<PersonInSerialDbModel>()
                .HasMany(f => f.Professions)
                .WithMany(g => g.PersonInSerials)
                .UsingEntity(j => j.ToTable("PersonInSerialProfession"));

            builder.Entity<CommentInFilmDbModel>()
                .HasKey(comm => new { comm.FavoriteId, comm.CommentId });

            builder.Entity<CommentInFilmDbModel>()
                .HasOne(c => c.Favorite)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FavoriteId)
                .HasPrincipalKey(f => f.FavoriteId);

            builder.Entity<CommentInSerialDbModel>()
                .HasKey(comm => new { comm.FavoriteId, comm.CommentId });

            builder.Entity<CommentInSerialDbModel>()
                .HasOne(c => c.Favorite)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FavoriteId)
                .HasPrincipalKey(f => f.FavoriteId);

            builder.Entity<CommentDbModel>()
                .HasOne(c => c.CommentInFilm)
                .WithOne(c => c.Comment);

            builder.Entity<CommentDbModel>()
                .HasOne(c => c.CommentInSerial)
                .WithOne(c => c.Comment);

            builder.Entity<CommentDbModel>()
                .HasOne(c => c.AdminNote)
                .WithOne(c => c.Review);
        }

        public DbSet<FilmDbModel> Film { get; set; }
        public DbSet<SerialDbModel> Serial { get; set; }
        public DbSet<UserDbModel> User { get; set; }
        public DbSet<SeasonDbModel> Season { get; set; }
        public DbSet<ImageDbModel> Image { get; set; }
        public DbSet<PersonDbModel> Person { get; set; }
        public DbSet<FavoriteFilmDbModel> FavoriteFilms { get; set; }
        public DbSet<FavoriteSerialDbModel> FavoriteSerials { get; set; }
        public DbSet<GenreDbModel> Genre { get; set; }
        public DbSet<ProfessionDbModel> Profession { get; set; }
        public DbSet<PersonInFilmDbModel> PersonInFilm { get; set; }
        public DbSet<PersonInSerialDbModel> PersonInSerials { get; set; }
        public DbSet<SeriesDbModel> Series { get; set; }
        public DbSet<CommentInFilmDbModel> CommentInFilm { get; set; }
        public DbSet<CommentInSerialDbModel> CommentInSerial { get; set; }
        public DbSet<CommentDbModel> Comment { get; set; }
        public DbSet<CommentUserDbModel> UserComment { get; set; }
        public DbSet<AdminNoteDbModel> AdminNote { get; set; }
    }
}
