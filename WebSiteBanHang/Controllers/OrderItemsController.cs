using Microsoft.AspNetCore.Mvc;
using WebSiteBanHang.Interfaces;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemsController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public IActionResult Index()
        {
            var orderItems = _orderItemRepository.GetAllOrderItems();
            return View(orderItems);
        }

        public IActionResult Details(int id)
        {
            var orderItem = _orderItemRepository.GetOrderItemById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return View(orderItem);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _orderItemRepository.AddOrderItem(orderItem);
                return RedirectToAction(nameof(Index));
            }
            return View(orderItem);
        }

        public IActionResult Edit(int id)
        {
            var orderItem = _orderItemRepository.GetOrderItemById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return View(orderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _orderItemRepository.UpdateOrderItem(orderItem);
                return RedirectToAction(nameof(Index));
            }
            return View(orderItem);
        }

        public IActionResult Delete(int id)
        {
            var orderItem = _orderItemRepository.GetOrderItemById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return View(orderItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderItemRepository.DeleteOrderItem(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
