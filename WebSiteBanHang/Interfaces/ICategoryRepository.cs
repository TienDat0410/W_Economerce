using System.Collections.Generic;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Interfaces {
    public interface ICategoryRepository {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);

    }
}
