using WebSiteBanHang.Interfaces;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Repositories
{
    public class EFOrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public EFOrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.ToList();
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            return _context.OrderItems.Find(orderItemId);
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            _context.SaveChanges();
        }

        public void DeleteOrderItem(int orderItemId)
        {
            var orderItem = _context.OrderItems.Find(orderItemId);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                _context.SaveChanges();
            }
        }
    }
}