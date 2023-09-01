using Infrastructure.Contracts;
using LeaveManagmentSystemAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BC170404491.FYP.Data
{
    public class BaseRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal ApplicationDbContext _context;
        internal DbSet<TEntity> _dbSet;

        public BaseRepository(
            ApplicationDbContext context
            )
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity) ;
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public async Task DeleteAsync(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public async Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            await DeleteAsync(entityToDelete);
        }

        public async Task<IEnumerable<TEntity>> FindByInclude(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, 
                IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            IQueryable<TEntity> query = _dbSet;
            return await query.ToListAsync();
        }



        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query, params object[] parameters)
        {
            return await _dbSet.FromSqlRaw(query, parameters).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

        }
        public async Task SaveChangesAsync( )
        {
              await _context.SaveChangesAsync();
        }

    }
}
