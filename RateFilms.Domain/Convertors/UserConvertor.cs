using RateFilms.Domain.Models.Authorization;
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
                UserName = userDbModel.UserName
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
                UserName = user.UserName,
                Age = user.Age,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                Role = user.Role
            };

            return userDb;
        }

        public static IEnumerable<User> UserDbListConvertUserDomainList(IEnumerable<UserDbModel> userDbModels)
        {
            if (userDbModels == null) throw new ArgumentNullException(nameof(userDbModels));

            var users = userDbModels
                .Select(u => new User
                {
                    Id = u.Id,
                    Age = u.Age,
                    Email = u.Email,
                    Name = u.Name,
                    Password = u.Password,
                    Phone = u.Phone,
                    Role = u.Role,
                    UserName = u.UserName
                });

            return users;
        }
    }
}
