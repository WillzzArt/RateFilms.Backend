using RateFilms.Common.Helpers;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class PersonConvertor
    {
        public static Person ActorDbConvertActorDomain(PersonDbModel actorDbModel)
        {
            if (actorDbModel == null) throw new ArgumentNullException(nameof(actorDbModel));

            var actor = new Person
            {
                Id = actorDbModel.Id,
                Name = actorDbModel.Name,
                Age = actorDbModel.Age,
                Image = ImageDbConvertImageDomain(actorDbModel.Image ?? new ImageDbModel())
            };

            return actor;
        }

        public static IEnumerable<Person> PersonInMovieDbListConvertPersonDomainList(IEnumerable<PersonInFilmDbModel> personDbModels)
        {
            if (personDbModels == null) throw new ArgumentNullException(nameof(personDbModels));

            var person = personDbModels
                .Select(a => new Person
                {
                    Id = a.PersonId,
                    Name = a.Person.Name,
                    Age = a.Person.Age,
                    Image = ImageDbConvertImageDomain(a.Person.Image),
                    Professions = a.Professions.Select(p => p.Profession.ToEnum(Profession.None))
                }).ToList();

            return person;
        }
        public static IEnumerable<Person> PersonInMovieDbListConvertPersonDomainList(IEnumerable<PersonInSerialDbModel> personDbModels)
        {
            if (personDbModels == null) throw new ArgumentNullException(nameof(personDbModels));

            var person = personDbModels
                .Select(a => new Person
                {
                    Id = a.PersonId,
                    Name = a.Person.Name,
                    Age = a.Person.Age,
                    Image = ImageDbConvertImageDomain(a.Person.Image),
                    Professions = a.Professions.Select(p => p.Profession.ToEnum(Profession.None))
                }).ToList();

            return person;
        }

        public static IEnumerable<PersonInFilmDbModel> PersonDomainListConvertPersonInFilmDbList(IEnumerable<Person> people, Guid filmId)
        {
            if (people == null) throw new ArgumentNullException(nameof(people));

            var peopleDb = people
                .Select(a => new PersonInFilmDbModel
                {
                    PersonId = a.Id,
                    Person = new PersonDbModel
                    {
                        Id = a.Id,
                        Age = a.Age,
                        Name = a.Name,
                        ImageId = a.Image?.Id,
                        Image = ImageDomainConvertImageDb(a.Image)
                    },
                    FilmId = filmId,
                    Professions = a.Professions.Select(p => new ProfessionDbModel
                    {
                        Id = (int)p,
                        Profession = p.ToString()
                    })
                }).ToList();

            return peopleDb;
        }

        public static IEnumerable<PersonInSerialDbModel> PersonDomainListConvertPersonInSerialDbList(IEnumerable<Person> people, Guid serialId)
        {
            if (people == null) throw new ArgumentNullException(nameof(people));

            var peopleDb = people
                .Select(a => new PersonInSerialDbModel
                {
                    PersonId = a.Id,
                    Person = new PersonDbModel
                    {
                        Id = a.Id,
                        Age = a.Age,
                        Name = a.Name,
                        ImageId = a.Image?.Id,
                        Image = ImageDomainConvertImageDb(a.Image)
                    },
                    SerialId = serialId,
                    Professions = a.Professions.Select(p => new ProfessionDbModel
                    {
                        Id = (int)p,
                        Profession = p.ToString()
                    })
                }).ToList();

            return peopleDb;
        }

        public static Image? ImageDbConvertImageDomain(ImageDbModel? imageDbModel)
        {
            //if (imageDbModel == null) throw new ArgumentNullException(nameof(imageDbModel));
            if (imageDbModel == null) return null;

            var image = new Image
            {
                Id = imageDbModel.Id,
                Url = imageDbModel.Url,
                isPreview = imageDbModel.isPreview
            };

            return image;
        }

        public static ImageDbModel? ImageDomainConvertImageDb(Image? image)
        {
            if (image == null) return null;

            var imageDb = new ImageDbModel
            {
                Id = image.Id,
                Url = image.Url,
                isPreview = image.isPreview
            };

            return imageDb;
        }

        public static List<Image> ImageDbListConvertImageDomainList(IEnumerable<ImageDbModel> imageDbModels)
        {
            if (imageDbModels == null) throw new ArgumentNullException(nameof(imageDbModels));

            var images = imageDbModels
                .Select(img => new Image
                {
                    Id = img.Id,
                    Url = img.Url,
                    isPreview = img.isPreview
                }).ToList();

            return images;
        }

        public static List<ImageDbModel> ImageDomainListConvertImageDbList(IEnumerable<Image> image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));

            var images = image
                .Select(img => new ImageDbModel
                {
                    Id = img.Id,
                    Url = img.Url,
                    isPreview = img.isPreview
                }).ToList();

            return images;
        }
    }
}
