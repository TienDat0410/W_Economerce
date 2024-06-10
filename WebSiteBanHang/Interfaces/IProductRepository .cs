using WebSiteBanHang.Models;

namespace WebSiteBanHang.Interfaces {
    public interface IProductRepository {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }

}
