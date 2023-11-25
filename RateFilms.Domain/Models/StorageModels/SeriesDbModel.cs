﻿using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Models.StorageModels
{
    public class SeriesDbModel : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public Guid? ImageId { get; set; }
        public ImageDbModel? Image { get; set; }
        public float? AvgRating { get; set; }
        public Guid SeasonId { get; set; }
        public SeasonDbModel Season { get; set; }
    }
}
