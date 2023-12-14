﻿using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Convertors;
using RateFilms.Domain.DTO.Authorization;
using RateFilms.Domain.Models.Authorization;
using RateFilms.Domain.Models.StorageModels;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> ChangePassword(string userLogin, string password)
        {
            var userDb = await _context.User.FirstOrDefaultAsync(u => u.UserName == userLogin
                                                                || u.Email == userLogin);

            if (userDb != null)
            {
                userDb.Password = password;
                await _context.SaveChangesAsync();
                return UserConvertor.UserDbConvertUserDomain(userDb);
            }

            return null;
        }

        public async Task<User?> FindUser(string userLogin)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == userLogin
                                                                || u.Email == userLogin);

            if (user == null) return null;

            return UserConvertor.UserDbConvertUserDomain(user);
        }

        public async Task UpdateUser(UserExtendedResponse user, string username)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.Email)) throw new ArgumentException(nameof(user));

            var userData = await _context.User.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (userData == null) throw new ArgumentException(nameof(user));
            if (userData.UserName != username) throw new ArgumentException(nameof(user));

            userData.UserName = user.UserName;
            userData.Email = user.Email;
            userData.Age = user.Age ?? userData.Age;
            userData.Phone = user.Phone ?? userData.Phone;
            userData.Name = string.IsNullOrWhiteSpace(user.Name) ? userData.Name : user.Name;

            if (user.Image != null)
            {
                if (userData.ImageId != null)
                {
                    var img = await _context.Image.FirstAsync(x => x.Id ==  userData.ImageId);
                    userData.ImageId = null;
                    _context.Image.Remove(img);
                }
                
                userData.Image = PersonConvertor.ImageDomainConvertImageDb(user.Image);
                
            }

            await _context.SaveChangesAsync();
        }
    }
}
