﻿using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class SerialDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly RealeseDate { get; set; }
        public float? AvgRating { get; set; }
        public int AgeRating { get; set; }
        public Guid? ImageId { get; set; }
        public ImageDbModel? PreviewImage { get; set; }
        public IEnumerable<SeasonDbModel> Seasons { get; set; }
        public IEnumerable<PersonInSerialDbModel> People { get; set; }
        public IEnumerable<GenreDbModel> Genre { get; set; }
        public IEnumerable<FavoriteSerialDbModel> Favorites { get; set; }
    }
}
