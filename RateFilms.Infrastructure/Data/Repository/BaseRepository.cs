﻿using Microsoft.EntityFrameworkCore;
using RateFilms.Domain.Models.Interfaces;
using RateFilms.Domain.Repositories;

namespace RateFilms.Infrastructure.Data.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync<T>(Guid? id) where T : class, IEntity
        {
            if (id != null)
            {
                var oldEntity = await _context.Set<T>().FirstOrDefaultAsync(o => o.Id == id);
                if (oldEntity != null)
                {
                    _context.Set<T>().Remove(oldEntity);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<T?> FindByIdAsync<T>(Guid id) where T : class, IEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IEntity
        {
            var t = await _context.Set<T>().ToListAsync();
            return t;
        }

        public async Task<bool> UpdateAsync<T>(T entity, Guid? id) where T : class, IEntity
        {
            if (id != null)
            {
                var oldEntity = await _context.Set<T>().FirstOrDefaultAsync(o => o.Id == id);
                if (oldEntity != null)
                {
                    entity.Id = oldEntity.Id;
                    _context.Set<T>().Update(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

    }
}
