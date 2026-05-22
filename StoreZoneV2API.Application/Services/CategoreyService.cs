using StoreZoneV2API.Domain.Interfaces.Data;

namespace StoreZoneV2API.Application.Services
{
    public class CategoreyService 
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoreyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Category?>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }
        public async Task AddCategoryAsync(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            await  _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category != null)
            {
                await  _unitOfWork.Categories.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

    }

}
