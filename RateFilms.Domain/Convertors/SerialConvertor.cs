using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class SerialConvertor
    {
        public static Serial SerialDbConvertSerialDomain(SerialDbModel serialDbModel)
        {
            if (serialDbModel == null) throw new ArgumentNullException(nameof(serialDbModel));

            var serial = new Serial
            {
                Id = serialDbModel.Id,
                Name = serialDbModel.Name,
                Description = serialDbModel.Description,
                PreviewImage = PersonConvertor.ImageDbConvertImageDomain(serialDbModel.PreviewImage ?? new ImageDbModel()),
                AgeRating = serialDbModel.AgeRating,
                AvgRating = serialDbModel.AvgRating,
                Genre = serialDbModel.Genre.Select(g => g.Genre.ToEnum(Genre.None)),
                Seasons = SeasonDbListConvertSeasonDomain(serialDbModel.Seasons),
                RealeseDate = serialDbModel.RealeseDate,
                People = PersonConvertor.PersonInMovieDbListConvertPersonDomainList(serialDbModel.People)
            };

            return serial;
        }

        public static IEnumerable<Season> SeasonDbListConvertSeasonDomain(IEnumerable<SeasonDbModel> seasonDbModels)
        {
            if (seasonDbModels == null) throw new ArgumentNullException(nameof(seasonDbModels));

            var seasons = seasonDbModels
                .Select(s => new Season
                {
                    Id = s.Id,
                    AvgRating = s.AvgRating,
                    Description = s.Description,
                    Images = PersonConvertor.ImageDbListConvertImageDomainList(s.Images),
                    RealeseDate = s.RealeseDate,
                    Series = SeriesDbListConvertSeriesDomain(s.Series)
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
                    AvgRating = s.AvgRating,
                    PreviewImage = PersonConvertor.ImageDbConvertImageDomain(s.PreviewImage ?? new ImageDbModel())
                });

            return series;
        }
    }
}
