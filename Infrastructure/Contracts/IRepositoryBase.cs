using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entityToDelete);
        Task DeleteAsync(object id);
        Task<IEnumerable<TEntity>> FindByInclude(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> FindAll();

        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query,
            params object[] parameters);
        Task  AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entity);
        Task  UpdateAsync(TEntity entityToUpdate);
        Task SaveChangesAsync();



    }
}
