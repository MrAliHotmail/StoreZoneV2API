using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //sepecal methods for product repository not exist in generic repository
        Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync();
        Task<IReadOnlyList<Product>> GetProductByMinPriceAsync(decimal minPrice);
    }
}
