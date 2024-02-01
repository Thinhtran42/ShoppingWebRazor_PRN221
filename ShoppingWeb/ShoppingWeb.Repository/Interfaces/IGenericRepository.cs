using System;
using ShoppingWeb.Repository.Commons;

namespace ShoppingWeb.Repository.Interfaces
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task AddAsync(TEntity entity);
        Task Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        Task AddRangeAsync(List<TEntity> entities);
        void UpdateRange(List<TEntity> entities);
        void DeleteRange(List<TEntity> entities);
        Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10);
    }
}

