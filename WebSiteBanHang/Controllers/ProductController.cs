using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSiteBanHang.Interfaces;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,ProductName,Description,Price,StockQuantity")] Product product, IFormFile image)
        {
            // Xóa các thuộc tính không cần thiết khỏi ModelState
            ModelState.Remove("Category");
            ModelState.Remove("OrderItems");
            ModelState.Remove("ImageUrl");
            if (image != null && image.Length > 0)
            {
                var imageUrl = await _productRepository.SaveProductImageAsync(image);
                product.ImageUrl = imageUrl;
            }

            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                await _productRepository.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            // Log the errors in ModelState
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,ProductName,Description,Price,StockQuantity,ImageUrl,CreatedAt,UpdatedAt")] Product product, IFormFile image)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            ModelState.Remove("Category");
            ModelState.Remove("OrderItems");
            ModelState.Remove("ImageUrl");

            // Lấy thông tin sản phẩm hiện tại từ cơ sở dữ liệu
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Giữ nguyên ImageUrl hiện tại nếu không có hình ảnh mới
            if (image == null || image.Length == 0)
            {
                product.ImageUrl = existingProduct.ImageUrl;
                ModelState.Remove("image");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật các thuộc tính từ sản phẩm chỉnh sửa
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.StockQuantity = product.StockQuantity;
                    existingProduct.UpdatedAt = DateTime.Now;

                    // Cập nhật hình ảnh nếu có
                    if (image != null && image.Length > 0)
                    {
                        var imageUrl = await _productRepository.SaveProductImageAsync(image);
                        existingProduct.ImageUrl = imageUrl;
                    }

                    await _productRepository.UpdateProductAsync(existingProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
            // POST: Products/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _productRepository.DeleteProductAsync(id);
                return RedirectToAction(nameof(Index));
            }

            private async Task<bool> ProductExists(int id)
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                return product != null;
            }
    }
}