using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.DomainModels;
using RateFilms.Domain.Models.StorageModels;

namespace RateFilms.Domain.Convertors
{
    public static class UserConvertor
    {
        public static User UserDbConvertUserDomain(UserDbModel userDbModel)
        {
            if (userDbModel == null) throw new ArgumentNullException(nameof(userDbModel));

            var user = new User()
            {
                Id = userDbModel.Id,
                Age = userDbModel.Age,
                Email = userDbModel.Email,
                Name = userDbModel.Name,
                Password = userDbModel.Password,
                Phone = userDbModel.Phone,
                Role = userDbModel.Role,
                Username = userDbModel.UserName,
                Image = PersonConvertor.ImageDbConvertImageDomain(userDbModel.Image ?? new ImageDbModel())
            };

            return user;
        }

        public static UserDbModel UserDomainConvertUserDb(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userDb = new UserDbModel
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.Username,
                Age = user.Age,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                Role = user.Role,
                Image = PersonConvertor.ImageDomainConvertImageDb(user.Image ?? new Image())
            };

            return userDb;
        }

        public static IEnumerable<User> UserDbListConvertUserDomainList(IEnumerable<UserDbModel> userDbModels)
        {
            if (userDbModels == null) throw new ArgumentNullException(nameof(userDbModels));

            var users = userDbModels
                .Select(UserDbConvertUserDomain);

            return users;
        }
    }
}
