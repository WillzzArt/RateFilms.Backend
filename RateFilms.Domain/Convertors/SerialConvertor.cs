﻿using RateFilms.Common.Helpers;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class SerialConvertor
    {
        public static Serial SerialDbConvertSerialDomain(
            SerialDbModel serialDbModel,
            IEnumerable<FavoriteSerialDbModel>? favorites = null)
        {
            if (serialDbModel == null) throw new ArgumentNullException(nameof(serialDbModel));

            var serial = new Serial
            {
                Id = serialDbModel.Id,
                Name = serialDbModel.Name,
                Description = serialDbModel.Description,
                PreviewImage = PersonConvertor.ImageDbConvertImageDomain(serialDbModel.PreviewImage ?? new ImageDbModel()),
                AgeRating = serialDbModel.AgeRating,
                Country = serialDbModel.Country,
                Genre = serialDbModel.Genre.Select(g => g.Genre.ToEnum(Genre.None)),
                Seasons = SeasonDbListConvertSeasonDomain(serialDbModel.Seasons ?? new List<SeasonDbModel>()),
                RealeseDate = serialDbModel.RealeseDate,
                People = PersonConvertor.PersonInMovieDbListConvertPersonDomainList(serialDbModel.People ?? new List<PersonInSerialDbModel>())
            };

            if (favorites != null)
            {
                serial.Favorites = favorites.Select(fSerial => new Favorite
                {
                    Id = fSerial.FavoriteId,
                    User = UserConvertor.UserDbConvertUserDomain(fSerial.User ?? new UserDbModel()),
                    IsFavorite = fSerial.IsFavorite,
                    Score = fSerial.Score,
                    Status = Enum.IsDefined(typeof(StatusMovie), fSerial.Status) 
                        ? fSerial.Status 
                        : StatusMovie.None
                });
            }

            return serial;
        }

        public static IEnumerable<Season> SeasonDbListConvertSeasonDomain(IEnumerable<SeasonDbModel> seasonDbModels)
        {
            if (seasonDbModels == null) throw new ArgumentNullException(nameof(seasonDbModels));

            var seasons = seasonDbModels
                .Select(s => new Season
                {
                    Id = s.Id,
                    Description = s.Description,
                    CountMaxSeries = s.CountMaxSeries,
                    Images = PersonConvertor.ImageDbListConvertImageDomainList(s.Images ?? new List<ImageDbModel>()),
                    RealeseDate = s.RealeseDate,
                    Series = SeriesDbListConvertSeriesDomain(s.Series ?? new List<SeriesDbModel>())
                });

            return seasons;
        }

        public static IEnumerable<Series> SeriesDbListConvertSeriesDomain(IEnumerable<SeriesDbModel> seriesDbModels)
        {
            if (seriesDbModels == null) throw new ArgumentNullException(nameof(seriesDbModels));

            var series = seriesDbModels
                .Select(s => new Series
                {
                    Id = s.Id,
                    Name = s.Name,
                    Duration = s.Duration,
                    RealeseDate = s.RealeseDate,
                    PreviewImage = PersonConvertor.ImageDbConvertImageDomain(s.PreviewImage)!
                });

            return series;
        }

        public static SerialDbModel SerialDomainConvertSerialDb(Serial serial)
        {
            if (serial == null) throw new ArgumentNullException(nameof(serial));

            var serialDb = new SerialDbModel
            {
                Id = serial.Id,
                Name = serial.Name,
                Description = serial.Description,
                AgeRating = serial.AgeRating,
                Country = serial.Country,
                Genre = serial.Genre
                .Select(g => new GenreDbModel
                {
                    Id = (int)g,
                    Genre = g.ToString()
                }),
                RealeseDate = serial.RealeseDate,
                People = PersonConvertor.PersonDomainListConvertPersonInSerialDbList(serial.People, serial.Id),
                PreviewImageId = serial.PreviewImage?.Id,
                PreviewImage = PersonConvertor.ImageDomainConvertImageDb(serial.PreviewImage),
                Seasons = SeasonDomainListConvertSeasonDbList(serial.Seasons)
            };

            return serialDb;
        }

        private static IEnumerable<SeasonDbModel> SeasonDomainListConvertSeasonDbList(IEnumerable<Season> seasons)
        {
            if (seasons == null) throw new ArgumentNullException(nameof(seasons));

            var seasonsDb = seasons
                .Select(s => new SeasonDbModel
                {
                    Id = s.Id,
                    Description = s.Description,
                    RealeseDate = s.RealeseDate,
                    CountMaxSeries = s.CountMaxSeries,
                    Images = PersonConvertor.ImageDomainListConvertImageDbList(s.Images),
                    Series = SeriesDomainListConvertSeriesDbList(s.Series)
                });

            return seasonsDb;
        }

        private static IEnumerable<SeriesDbModel> SeriesDomainListConvertSeriesDbList(IEnumerable<Series> series)
        {
            if (series == null) throw new ArgumentNullException(nameof(series));

            var seriesDb = series
                .Select(s => new SeriesDbModel
                {
                    Id = s.Id,
                    Duration = s.Duration,
                    Name = s.Name,
                    PreviewImage = PersonConvertor.ImageDomainConvertImageDb(s.PreviewImage),
                    PreviewImageId = s.PreviewImage.Id,
                    RealeseDate = s.RealeseDate
                });

            return seriesDb;
        }

        public static IEnumerable<Serial> SerialDbListConvertSerialDomainList(IEnumerable<SerialDbModel> serials)
        {
            if (serials == null) throw new ArgumentNullException(nameof(serials));

            var serialList = serials
                .Select(s => SerialDbConvertSerialDomain(s, s.Favorites))
                .ToList();

            return serialList;
        }
    }
}
