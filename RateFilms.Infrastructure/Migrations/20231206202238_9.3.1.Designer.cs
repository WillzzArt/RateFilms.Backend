﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RateFilms.Infrastructure.Data;

#nullable disable

namespace RateFilms.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231206202238_9.3.1")]
    partial class _931
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CommentDbModelUserDbModel", b =>
                {
                    b.Property<Guid>("CommentsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("CommentsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("CommentLikes", (string)null);
                });

            modelBuilder.Entity("FilmDbModelGenreDbModel", b =>
                {
                    b.Property<Guid>("FilmsId")
                        .HasColumnType("uuid");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.HasKey("FilmsId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("FilmGenries", (string)null);
                });

            modelBuilder.Entity("GenreDbModelSerialDbModel", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<Guid>("SerialsId")
                        .HasColumnType("uuid");

                    b.HasKey("GenreId", "SerialsId");

                    b.HasIndex("SerialsId");

                    b.ToTable("SerialGenries", (string)null);
                });

            modelBuilder.Entity("PersonInFilmDbModelProfessionDbModel", b =>
                {
                    b.Property<int>("ProfessionsId")
                        .HasColumnType("integer");

                    b.Property<Guid>("PersonInFilmsFilmId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonInFilmsPersonId")
                        .HasColumnType("uuid");

                    b.HasKey("ProfessionsId", "PersonInFilmsFilmId", "PersonInFilmsPersonId");

                    b.HasIndex("PersonInFilmsFilmId", "PersonInFilmsPersonId");

                    b.ToTable("PersonInFilmProfession", (string)null);
                });

            modelBuilder.Entity("PersonInSerialDbModelProfessionDbModel", b =>
                {
                    b.Property<int>("ProfessionsId")
                        .HasColumnType("integer");

                    b.Property<Guid>("PersonInSerialsSerialId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonInSerialsPersonId")
                        .HasColumnType("uuid");

                    b.HasKey("ProfessionsId", "PersonInSerialsSerialId", "PersonInSerialsPersonId");

                    b.HasIndex("PersonInSerialsSerialId", "PersonInSerialsPersonId");

                    b.ToTable("PersonInSerialProfession", (string)null);
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.CommentDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CountLike")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsEdit")
                        .HasColumnType("boolean");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.CommentInFilmDbModel", b =>
                {
                    b.Property<Guid>("FavoriteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.HasKey("FavoriteId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("CommentInFilm");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.CommentInSerialDbModel", b =>
                {
                    b.Property<Guid>("FavoriteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.HasKey("FavoriteId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("CommentInSerial");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FavoriteFilmDbModel", b =>
                {
                    b.Property<Guid>("FilmId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<bool>("isFavorite")
                        .HasColumnType("boolean");

                    b.HasKey("FilmId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteFilm");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FavoriteSerialDbModel", b =>
                {
                    b.Property<Guid?>("SerialId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("boolean");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("SerialId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteSerial");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FilmDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AgeRating")
                        .HasColumnType("integer");

                    b.Property<float?>("AvgRating")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.GenreDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Genre")
                        .IsUnique();

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.ImageDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FilmId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SeasonId")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isPreview")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.PersonDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.PersonInFilmDbModel", b =>
                {
                    b.Property<Guid>("FilmId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("FilmId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonInFilm");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.PersonInSerialDbModel", b =>
                {
                    b.Property<Guid>("SerialId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("SerialId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonInSerial");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.ProfessionDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Profession")
                        .IsUnique();

                    b.ToTable("Profession");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SeasonDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float?>("AvgRating")
                        .HasColumnType("real");

                    b.Property<int?>("CountMaxSeries")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("RealeseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("SerialId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SerialId");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SerialDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AgeRating")
                        .HasColumnType("integer");

                    b.Property<float?>("AvgRating")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PreviewImageId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("RealeseDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PreviewImageId");

                    b.ToTable("Serial");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SeriesDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float?>("AvgRating")
                        .HasColumnType("real");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PreviewImageId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("RealeseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SeasonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PreviewImageId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.UserDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("CommentDbModelUserDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.CommentDbModel", null)
                        .WithMany()
                        .HasForeignKey("CommentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.UserDbModel", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmDbModelGenreDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.FilmDbModel", null)
                        .WithMany()
                        .HasForeignKey("FilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.GenreDbModel", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreDbModelSerialDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.GenreDbModel", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.SerialDbModel", null)
                        .WithMany()
                        .HasForeignKey("SerialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonInFilmDbModelProfessionDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.ProfessionDbModel", null)
                        .WithMany()
                        .HasForeignKey("ProfessionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.PersonInFilmDbModel", null)
                        .WithMany()
                        .HasForeignKey("PersonInFilmsFilmId", "PersonInFilmsPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PersonInSerialDbModelProfessionDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.ProfessionDbModel", null)
                        .WithMany()
                        .HasForeignKey("ProfessionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.PersonInSerialDbModel", null)
                        .WithMany()
                        .HasForeignKey("PersonInSerialsSerialId", "PersonInSerialsPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.CommentInFilmDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.CommentDbModel", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.FavoriteFilmDbModel", "Favorite")
                        .WithMany("Comments")
                        .HasForeignKey("FavoriteId")
                        .HasPrincipalKey("FavoriteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Favorite");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.CommentInSerialDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.CommentDbModel", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.FavoriteSerialDbModel", "Favorite")
                        .WithMany("Comments")
                        .HasForeignKey("FavoriteId")
                        .HasPrincipalKey("FavoriteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Favorite");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FavoriteFilmDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.FilmDbModel", "Film")
                        .WithMany("Favorite")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.UserDbModel", "User")
                        .WithMany("FavoriteFilms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FavoriteSerialDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.SerialDbModel", "Serial")
                        .WithMany("Favorites")
                        .HasForeignKey("SerialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.UserDbModel", "User")
                        .WithMany("FavoriteSerials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Serial");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.ImageDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.FilmDbModel", "Film")
                        .WithMany("Images")
                        .HasForeignKey("FilmId");

                    b.HasOne("RateFilms.Domain.Models.StorageModels.SeasonDbModel", "Season")
                        .WithMany("Images")
                        .HasForeignKey("SeasonId");

                    b.Navigation("Film");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.PersonDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.ImageDbModel", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.PersonInFilmDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.FilmDbModel", "Film")
                        .WithMany("People")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.PersonDbModel", "Person")
                        .WithMany("Films")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.PersonInSerialDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.PersonDbModel", "Person")
                        .WithMany("Serials")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RateFilms.Domain.Models.StorageModels.SerialDbModel", "Serial")
                        .WithMany("People")
                        .HasForeignKey("SerialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Serial");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SeasonDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.SerialDbModel", "Serial")
                        .WithMany("Seasons")
                        .HasForeignKey("SerialId");

                    b.Navigation("Serial");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SerialDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.ImageDbModel", "PreviewImage")
                        .WithMany()
                        .HasForeignKey("PreviewImageId");

                    b.Navigation("PreviewImage");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SeriesDbModel", b =>
                {
                    b.HasOne("RateFilms.Domain.Models.StorageModels.ImageDbModel", "PreviewImage")
                        .WithMany()
                        .HasForeignKey("PreviewImageId");

                    b.HasOne("RateFilms.Domain.Models.StorageModels.SeasonDbModel", "Season")
                        .WithMany("Series")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PreviewImage");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FavoriteFilmDbModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FavoriteSerialDbModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.FilmDbModel", b =>
                {
                    b.Navigation("Favorite");

                    b.Navigation("Images");

                    b.Navigation("People");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.PersonDbModel", b =>
                {
                    b.Navigation("Films");

                    b.Navigation("Serials");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SeasonDbModel", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.SerialDbModel", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("People");

                    b.Navigation("Seasons");
                });

            modelBuilder.Entity("RateFilms.Domain.Models.StorageModels.UserDbModel", b =>
                {
                    b.Navigation("FavoriteFilms");

                    b.Navigation("FavoriteSerials");
                });
#pragma warning restore 612, 618
        }
    }
}
