using WebSiteBanHang.Models;

namespace WebSiteBanHang.Interfaces
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetAllProductCategories();
        ProductCategory GetProductCategoryById(int productId, int categoryId);
        void AddProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(ProductCategory productCategory);
        void DeleteProductCategory(int productId, int categoryId);
    }
}
