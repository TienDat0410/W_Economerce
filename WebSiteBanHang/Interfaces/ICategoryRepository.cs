using System.Collections.Generic;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Interfaces {
    public interface ICategoryRepository {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);

    }
}
