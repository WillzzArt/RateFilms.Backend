using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.Helpers;
using RateFilms.Domain.Models.Authorization;
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
            User user = new User();
            var password = HashPasswordHelper.HashPassword(model.Password);
            user.Password = password;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Role = Role.User;

            await _baseRepository.CreateAsync(user);

            var token = Token.CreateToken(_configuration, user);

            return new LoginResponse(user, token);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _baseRepository.GetAllAsync<User>();
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _baseRepository.FindByIdAsync<User>(id);
        }
    }
}
