using Microsoft.EntityFrameworkCore.ChangeTracking;
using RateFilms.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateFilms.Domain.Repositories
{
    public interface IBaseRepository
    {
        Task SaveChangesAsync();

        Task<EntityEntry<T>> CreateAsync<T>(T entity) where T : class, IEntity;

        Task<EntityEntry<T>?> UpdateAsync<T>(T entity, Guid? id) where T : class, IEntity;

        Task<bool> DeleteAsync<T>(Guid? id) where T : class, IEntity;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IEntity;

        Task<T?> FindByIdAsync<T>(Guid? id) where T : class, IEntity;
    }
}
