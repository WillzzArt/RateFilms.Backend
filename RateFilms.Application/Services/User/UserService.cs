using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RateFilms.Application.JWTApp;
using RateFilms.Application.Option;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;
        private readonly TokenOptions _tokenOption;

        public UserService(IBaseRepository baseRepository, IOptions<TokenOptions> tokenOption, IUserRepository userRepository)
        {
            _baseRepository = baseRepository;
            _userRepository = userRepository;
            _tokenOption = tokenOption.Value;
        }

        public async Task<LoginResponse?> Authenticate(LoginRequest model)
        {
            var user = await _userRepository.FindUser(model.UserLogin);

            if (user?.Password == HashPasswordHelper.HashPassword(model.Password))
            {
                var token = Token.CreateToken(_tokenOption, user);

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

            var token = Token.CreateToken(_tokenOption, UserConvertor.UserDbConvertUserDomain(user));

            return new LoginResponse(UserConvertor.UserDbConvertUserDomain(user), token);
        }

        public async Task<LoginResponse?> ChangePassword(LoginRequest model)
        {
            model.Password = HashPasswordHelper.HashPassword(model.Password);
            var user = await _userRepository.ChangePassword(model.UserLogin, model.Password);

            if (user != null)
            {

                var token = Token.CreateToken(_tokenOption, user);

                return new LoginResponse(user, token);
            }

            return null;
        }

        public async Task UpdateUser(UserExtendedResponse user, string username)
        {
            await _userRepository.UpdateUser(user, username);
        }
    }
}
