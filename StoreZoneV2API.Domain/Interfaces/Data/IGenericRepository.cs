using StoreZoneV2API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
   

    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int  id , params string[] includeProperties);
        Task<IEnumerable<T?>> GetAllAsync(params string[] includeProperties);
        Task<IEnumerable<T?>> FindAsync(Expression<Func<T , bool>> predicate ,params string[] includeProperties);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task SofteDeleteAsync(int id);

        Task<bool> ExitsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        //Task<PagedResult<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null, params string[] includeProperties);

        

    }
}
