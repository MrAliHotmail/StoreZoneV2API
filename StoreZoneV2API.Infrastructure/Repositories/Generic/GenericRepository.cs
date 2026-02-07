using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Domain.Entities;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;

namespace StoreZoneV2API.Infrastructure.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
                
        }
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

        public virtual async Task<T?> GetByIdAsync(int id , params string[] includeProperties)
        {
            var query = _dbSet.AsQueryable();
            query = ApplyIncludes(query, includeProperties);

            return await query.FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

        }
        public virtual async Task<IEnumerable<T?>> GetAllAsync(params string[] includeProperties)
        {
            var query = _dbSet.Where(e=>e.IsActive).AsQueryable();
            query = ApplyIncludes(query  , includeProperties);
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<T?>> FindAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            var query = _dbSet.Where(e=>e.IsActive).Where(predicate).AsQueryable();
            query= ApplyIncludes(query , includeProperties);
            return await query.ToListAsync();
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }
        public virtual async Task<T> UpdateAsync(T entity)
        {
           entity.UpdatedAt= DateTime.Now;  
            _context.Attach(entity);    
           _dbSet.Entry(entity).State = EntityState.Modified;
           return entity;
            
        }
        public virtual async Task<T> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);

            }
            throw new KeyNotFoundException($"This Entity with id:{id} is not found !");
        }
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

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null)
            {
                return await _dbSet.CountAsync(e=>e.IsActive);
            }
            else
            {
                return await _dbSet.Where(predicate).CountAsync();

            }
        }
         
        
        //public virtual async Task<IGenericRepository<PagedResult<T>>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null, params string[] includeProperties)
        //{
        //    throw new NotImplementedException();
        //}
       
    }

}
