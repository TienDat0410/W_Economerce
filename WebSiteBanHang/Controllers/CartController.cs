using Microsoft.AspNetCore.Mvc;
using WebSiteBanHang.Extensions;
using WebSiteBanHang.Models;
using WebSiteBanHang.Repositories;
using System.Linq;
using System.Threading.Tasks;
using WebSiteBanHang.Interfaces;

public class CartController : Controller
{
    private readonly IProductRepository _productRepository;

    public CartController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<Order>("Cart") ?? new Order { OrderItems = new List<OrderItem>() };

        // Đảm bảo rằng tất cả các Product của OrderItem được thiết lập
        foreach (var item in cart.OrderItems)
        {
            if (item.Product == null)
            {
                item.Product = await _productRepository.GetProductByIdAsync(item.ProductId);
            }
        }

        return View(cart);
    }

    [HttpPost]
    public IActionResult UpdateCart(int productId, int quantity)
    {
        var cart = HttpContext.Session.GetObjectFromJson<Order>("Cart");
        if (cart != null)
        {
            var cartItem = cart.OrderItems.FirstOrDefault(item => item.ProductId == productId);
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    cart.OrderItems.Remove(cartItem);
                }

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int productId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<Order>("Cart");
        if (cart != null)
        {
            var cartItem = cart.OrderItems.FirstOrDefault(item => item.ProductId == productId);
            if (cartItem != null)
            {
                cart.OrderItems.Remove(cartItem);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
        }
        return RedirectToAction("Index");
    }
}