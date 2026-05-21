using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Infrastructure.Data;

namespace StoreZoneV2API.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context) : base(context) 
        {
        }

       

        public async Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
        {
            return await _context.Products.AsNoTracking().Include(p => p.Category).ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductByMinPriceAsync(decimal minPrice)
        {
            return await _context.Products.AsNoTracking().Where(p=>p.Price >= minPrice).ToListAsync();
        }

       
    }
}
