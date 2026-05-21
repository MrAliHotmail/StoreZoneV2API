using StoreZoneV2API.Domain.Entities;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Application.Dtos.Products; // Add this using if your DTOs are in this namespace
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductsDto> AddProductAsync(CreateProductDto dto);
        Task<ProductsDto> UpdateProductAsync(UpdateProductDto dto);
        Task DeleteProductAsync(int id);
        Task<ProductsDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductsDto>> GetAllAsync();

    }
}
