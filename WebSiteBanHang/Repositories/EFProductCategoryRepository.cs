using WebSiteBanHang.Interfaces;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Repositories
{
    public class EFProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return _context.ProductCategories.ToList();
        }

        public ProductCategory GetProductCategoryById(int productId, int categoryId)
        {
            return _context.ProductCategories.Find(productId, categoryId);
        }

        public void AddProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            _context.SaveChanges();
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            _context.SaveChanges();
        }

        public void DeleteProductCategory(int productId, int categoryId)
        {
            var productCategory = _context.ProductCategories.Find(productId, categoryId);
            if (productCategory != null)
            {
                _context.ProductCategories.Remove(productCategory);
                _context.SaveChanges();
            }
        }
    }
}
