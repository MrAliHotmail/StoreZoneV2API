using StoreZoneV2API.Domain.Entities;
using StoreZoneV2API.Domain.Interfaces.Data;

namespace StoreZoneV2API.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _productRepository.GetAllAsync();

        public async Task<Product?> GetByIdAsync(Guid id) => await _productRepository.GetByIdAsync(id);

        public async Task<Guid> CreateAsync(string name , decimal price)
        {
             var product = new  Product(name, price);
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateAsync(Guid id , string name , decimal price)
        {
            var product = await _productRepository.GetByIdAsync(id)  ??throw new KeyNotFoundException($"Cant found product {id}");
           product.setName(name);
           product.setPrice(price);
            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Cant found product {id}");
            _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }

    }
}
