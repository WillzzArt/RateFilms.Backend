﻿using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class ActorConvertor
    {
        public static Actor ActorDbConvertActorDomain(ActorDbModel actorDbModel)
        {
            if (actorDbModel == null) throw new ArgumentNullException(nameof(actorDbModel));

            var actor = new Actor
            {
                Id = actorDbModel.Id,
                Name = actorDbModel.Name,
                Age = actorDbModel.Age,
                Image = ImageDbConvertImageDomain(actorDbModel.Image)
            };

            return actor;
        }

        public static IEnumerable<Actor> ActorDbListConvertActorDomainList(IEnumerable<ActorDbModel> actorDbModels)
        {
            if (actorDbModels == null) throw new ArgumentNullException(nameof(actorDbModels));

            var actors = actorDbModels
                .Select(a => new Actor
                {
                    Id = a.Id,
                    Name = a.Name,
                    Age = a.Age,
                    Image = ImageDbConvertImageDomain(a.Image)
                });

            return actors;
        }

        public static Image ImageDbConvertImageDomain(ImageDbModel imageDbModel)
        {
            if (imageDbModel == null) throw new ArgumentNullException(nameof(imageDbModel));

            var image = new Image
            {
                Id = imageDbModel.Id,
                Url = imageDbModel.Url
            };

            return image;
        }

        public static IEnumerable<Image> ImageDbListConvertImageDomainList(IEnumerable<ImageDbModel> imageDbModels)
        {
            if (imageDbModels == null) throw new ArgumentNullException(nameof(imageDbModels));

            var images = imageDbModels
                .Select(img => new Image
                {
                    Id = img.Id,
                    Url = img.Url
                });

            return images;
        }
    }
}
