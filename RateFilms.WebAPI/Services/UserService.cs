using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;
using RateFilms.WebAPI.JWT;

namespace RateFilms.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IBaseRepository baseRepository, IConfiguration configuration, IUserRepository userRepository)
        {
            _baseRepository = baseRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> Authenticate(LoginRequest model)
        {
            var user = await _userRepository.FindUser(model.UserLogin);

            if (user is not null && user.Password == HashPasswordHelper.HashPassword(model.Password))
            {
                var token = Token.CreateToken(_configuration, user);

                return new LoginResponse(user, token);
            }

            return null;
        }

        public async Task<LoginResponse?> Register(Registration model)
        {
            UserDbModel user = new UserDbModel();
            var password = HashPasswordHelper.HashPassword(model.Password);
            user.Password = password;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Role = Role.User;

            try
            {
                await _baseRepository.CreateAsync(user);
            }
            catch (DbUpdateException ex)
            {
                // Произошла ошибка сохранения
                if (ex.InnerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    Console.WriteLine("Повторяющееся значение ключа нарушает ограничение уникальности.");
                    Console.WriteLine(ex.InnerException.Message);
                }
                else
                {
                    // Ошибка сохранения по другой причине
                    Console.WriteLine("Произошла ошибка сохранения.");
                    Console.WriteLine(ex.InnerException.Message);
                }
                return null;
            }

            var token = Token.CreateToken(_configuration, UserConvertor.UserDbConvertUserDomain(user));

            return new LoginResponse(UserConvertor.UserDbConvertUserDomain(user), token);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var user = await _baseRepository.GetAllAsync<UserDbModel>();
            return UserConvertor.UserDbListConvertUserDomainList(user);
        }

        public async Task<User?> GetById(Guid id)
        {
            var user = await _baseRepository.FindByIdAsync<UserDbModel>(id);

            if (user == null) return null;

            return UserConvertor.UserDbConvertUserDomain(user);
        }
    }
}
