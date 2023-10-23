using RateFilms.Domain.Models.Interfaces;

namespace RateFilms.Domain.Repositories
{
    public interface IBaseRepository
    {
        Task CreateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;

        Task<bool> UpdateAsync<TEntity>(TEntity entity, Guid? id) where TEntity : class, IEntity;

        Task<bool> DeleteAsync<TEntity>(Guid? id) where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IEntity;

        Task<TEntity?> FindByIdAsync<TEntity>(Guid id) where TEntity : class, IEntity;
    }
}
