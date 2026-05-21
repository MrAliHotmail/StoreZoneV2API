using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Application.Dtos.Products;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Domain.Interfaces.Services;

namespace StoreZoneV2API.Application.Services
{
    public class ProductService : IProductService   
    {
        //====================================================================
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IOrderRepository orderRepository , 
                              IProductRepository productRepository , 
                              IUnitOfWork unitOfWork,
                              IMapper mapper)
        {          
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //====================================================================
        public async Task<ProductsDto> AddProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            var addedProduct = await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductsDto>(addedProduct);
        }
        //====================================================================
        public Task DeleteProductAsync(int id)
        {
            var product = _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _productRepository.DeleteAsync(id);
            return _unitOfWork.SaveChangesAsync();
        }
        //====================================================================
        public async Task<IEnumerable<ProductsDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductsDto>>(products);
        }
        //====================================================================
        public async Task<ProductsDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductsDto?>(product);
        }
        //====================================================================
        public async Task UpdatePriceAsync(int productId, decimal newPrice)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            bool hasActiveOrders = await _orderRepository.HasActiveOrderAsync(productId);
            if (hasActiveOrders is true)
            {
                throw new Exception("Cannot update price. There are active orders for this product.");
            }
            product.setPrice(newPrice, hasActiveOrders);
            await _unitOfWork.SaveChangesAsync();
        }
        //====================================================================
        public Task<Product> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
        //====================================================================
        public Task<ProductsDto> UpdateProductAsync(UpdateProductDto dto)
        {
            throw new NotImplementedException();
        }
        //====================================================================

    }
}
