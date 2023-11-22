using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.StorageModels;

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
                Duration = serialDbModel.Duration,
                PreviewImage = serialDbModel.PreviewImage,
                AgeRating = serialDbModel.AgeRating,
                AvgRating = serialDbModel.AvgRating,
                SeriesCount = serialDbModel.SeriesCount,
                Genre = serialDbModel.Genre,
                Seasons = SeasonDbListConvertSeasonDomain(serialDbModel.Seasons),
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
                    Actors = ActorConvertor.ActorDbListConvertActorDomainList(s.Actors),
                    AvgRating = s.AvgRating,
                    Description = s.Description,
                    Images = ActorConvertor.ImageDbListConvertImageDomainList(s.Images),
                    RealeseDate = s.RealeseDate 
                });

            return seasons;
        }
    }
}
