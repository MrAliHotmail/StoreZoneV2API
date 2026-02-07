using StoreZoneV2API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        void UpdateAsync(Product product);
        void DeleteAsync(Product product);
        Task SaveChangesAsync();
    }
}
