using StoreZoneV2API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
    public interface ICategoreyRespository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>?> GetAllCategoriesAsync();
        Task<Category> CreateAsync(Category category);

    }
}
