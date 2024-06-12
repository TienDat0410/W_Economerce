using WebSiteBanHang.Models;

namespace WebSiteBanHang.Interfaces
{
    public interface IOrderItemRepository
    {
        IEnumerable<OrderItem> GetAllOrderItems();
        OrderItem GetOrderItemById(int orderItemId);
        void AddOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(int orderItemId);
    }
}
