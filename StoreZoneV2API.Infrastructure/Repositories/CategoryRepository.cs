using StoreZoneV2API.Domain.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;
using StoreZoneV2API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace StoreZoneV2API.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRespository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        { 
              await _context.Categories.AddAsync(category);
            return category;
            
        }

        public async Task<IEnumerable<Category>?> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();

        }

        public async Task<Category?> GetCategoryByIdAsync(int id) 
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
            return category;
        }

        
    }
}
