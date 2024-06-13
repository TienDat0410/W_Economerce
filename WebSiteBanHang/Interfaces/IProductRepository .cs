using WebSiteBanHang.Models;

namespace WebSiteBanHang.Interfaces {
    public interface IProductRepository {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
    }

}
