using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class FilmConvertor
    {
        public static Film FilmDbConvertFilmDomain(FilmDbModel filmDbModel)
        {
            if (filmDbModel == null) throw new ArgumentNullException(nameof(filmDbModel));

            var film = new Film
            {
                Id = filmDbModel.Id,
                Name = filmDbModel.Name,
                Description = filmDbModel.Description,
                Actors = ActorConvertor.ActorDbListConvertActorDomainList(filmDbModel.Actors),
                AgeRating = filmDbModel.AgeRating,
                AvgRating = filmDbModel.AvgRating,
                Duration = filmDbModel.Duration,
                Author = filmDbModel.Autor,
                Genre = filmDbModel.Genre,
                Images = ActorConvertor.ImageDbListConvertImageDomainList(filmDbModel.Images),
                PreviewImage = filmDbModel.PreviewImage,

            };

            return film;
        }
    }
}
