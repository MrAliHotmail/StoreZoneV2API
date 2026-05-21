using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Infrastructure.Data;
using System.Linq.Expressions;

namespace StoreZoneV2API.Infrastructure.Repositories
{
    public class  GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        //============================================================
        public virtual async Task<T?> GetByIdAsync(int id, params string[] includeProperties)
        {
            var query = _dbSet.AsQueryable();
            query = ApplyIncludes(query, includeProperties);

            return await query.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

        }
        //============================================================
        public virtual async Task<IReadOnlyList<T?>> GetAllAsync(params string[] includeProperties)
        {
            var query = _dbSet.Where(e => e.IsActive).AsQueryable();
            query = ApplyIncludes(query, includeProperties);
            return await query.ToListAsync();
        }
        //============================================================
        public virtual async Task<IEnumerable<T?>> FindAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            var query = _dbSet.Where(e => e.IsActive).Where(predicate).AsQueryable();
            query = ApplyIncludes(query, includeProperties);
            return await query.ToListAsync();
        }
        //============================================================
        private static IQueryable<T> ApplyIncludes(IQueryable<T> query , params string[] includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (var includePropertey in includeProperties)
                {
                    query = query.Include(includePropertey);
                }
               
            }
            return query;
        }

        //============================================================
        public virtual async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        //============================================================
        public virtual async Task<T> UpdateAsync(T entity)
        {
           entity.UpdatedAt= DateTime.Now;  
            _context.Attach(entity);    
           _dbSet.Entry(entity).State = EntityState.Modified;
           return entity;
            
        }
        //============================================================
        public virtual async Task<T> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);

            }
            throw new KeyNotFoundException($"This Entity with id:{id} is not found !");
        }
        //============================================================
        public async Task SofteDeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsActive= false;
                entity.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(entity);
            }
            throw new KeyNotFoundException($"This Entity with id:{id} is not found !");
        }
        //============================================================
        public virtual async Task<bool> ExitsAsync(Expression<Func<T, bool>> predicate)
        {
            if(predicate == null)
            {
                return await _dbSet.AnyAsync(e => e.IsActive);
            }
            else
            {
                return await _dbSet.Where(e=>e.IsActive).AnyAsync(predicate);
            }
        }
        //============================================================
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null)
            {
                return await _dbSet.CountAsync(e => e.IsActive);
            }
            else
            {
                return await _dbSet.Where(e => e.IsActive).Where(predicate).CountAsync();
            }
        }
        //============================================================



        //public virtual async Task<IGenericRepository<PagedResult<T>>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null, params string[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}

    }

}
