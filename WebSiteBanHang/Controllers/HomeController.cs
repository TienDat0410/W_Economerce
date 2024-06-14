using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSiteBanHang.Interfaces;
using WebSiteBanHang.Models;
using WebSiteBanHang.Repositories;
using WebSiteBanHang.Extensions;

namespace WebSiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            var viewModel = new HomeViewModel
            {
                Products = products,
                Categories = categories
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            if (quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            if (quantity > product.StockQuantity)
            {
                return BadRequest("Not enough stock available.");
            }

            var cart = HttpContext.Session.GetObjectFromJson<Order>("Cart") ?? new Order { OrderItems = new List<OrderItem>() };

            var cartItem = cart.OrderItems.FirstOrDefault(item => item.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price,
                    Product = product // Đảm bảo thuộc tính Product được gán giá trị
                };
                cart.OrderItems.Add(cartItem);
            }
            else
            {
                if (cartItem.Quantity + quantity > product.StockQuantity)
                {
                    return BadRequest("Not enough stock available.");
                }
                cartItem.Quantity += quantity;
            }

            product.StockQuantity -= quantity;
            await _productRepository.UpdateProductAsync(product);

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Order>("Cart") ?? new Order { OrderItems = new List<OrderItem>() };
            return View(cart);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}