using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.Interfaces.Data
{
    public interface ICategoryRespository : IGenericRepository<Category>
    {
        Task<Category?>  GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>?> GetAllCategoriesAsync();
        Task<Category> CreateCategoryAsync(Category category);

    }
}
