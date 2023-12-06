﻿using RateFilms.Domain.DTO.People;
using RateFilms.Domain.Models.DomainModels;

namespace RateFilms.Domain.DTO.Films
{
    public class FilmResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long? RealeseDate { get; set; }
        public List<string> Genre { get; set; } = new List<string>();
        public Image PreviewImage { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public bool isFavorite { get; set; } = false;
        public string? Status { get; set; }

        public FilmResponse(Film film, Favorite? favoriteFilm)
        {
            Id = film.Id;
            Name = film.Name;
            Description = film.Description;
            if (film.Genre.Any())
            {
                Genre = film.Genre
                    .Select(x => x.ToString())
                    .ToList();
            }

            if (film.RealeseDate != null)
                RealeseDate = ((DateTimeOffset)film.RealeseDate).ToUnixTimeMilliseconds();

            PreviewImage = film.PreviewImage;
            AvgRating = film.AvgRating;
            AgeRating = film.AgeRating;
            isFavorite = favoriteFilm?.IsFavorite ?? false;
            Status = favoriteFilm?.Status.ToString() ?? StatusMovie.None.ToString();
        }
    }
}
