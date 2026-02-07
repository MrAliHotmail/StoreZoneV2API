using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Domain.Entities;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Infrastructure.Data;

namespace StoreZoneV2API.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }   

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void UpdateAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public void DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
